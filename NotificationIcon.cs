/*
 * NotificationIcon.cs
 * Created by Jon Sandström
 */

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace ZenStates
{
    public sealed class NotificationIcon
    {

        private static string MessageBoxTitle = Application.CompanyName + " " + Application.ProductName;

        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);

        public enum PerfBias { Auto = 0, None, Cinebench_R11p5, Cinebench_R15, Geekbench_3 };
        public enum PerfEnh { None = 0, Default, Level1, Level2, Level3_OC, Level4_OC };

        private NotifyIcon notifyIcon;
        private ContextMenu notificationMenu;

        private SettingsForm sf = null;

        private static System.Timers.Timer tempTimer;

        private static byte serviceTimeout;

        public static string mbName;
        public static string mbVendor;
        public static string cpuName;
        public static string smuVersion;
        public static int smuVersionInt;
        public static UInt64 cpuType;
        public static string biosVersion;

        public static DataInterface di;

        // from service 
        public static UInt64 ServiceVersion;

        public static bool isAvailable;
        public static bool supportedCpu;
        public static bool SettingsReset;
        public static bool TrayIconAtStart;
        public static bool ApplyAtStart;
        public static bool P80Temp;
        public static bool ShutdownUnclean;
        public static bool SettingsSaved;

        public static UInt64[] Pstate;
        public static UInt64[] BoostFreq;
        public static int Pstates = 3;
        public static UInt64 PstateOc;

        public static bool ZenC6Core;
        public static bool ZenC6Package;
        public static bool ZenCorePerfBoost;
        public static bool ZenOc;
        public static int ZenPPT;
        public static int ZenTDC;
        public static int ZenEDC;
        public static int ZenScalar;

        public static PerfBias perfBias;

        public static int Temp;

        // defaults/limits
        public const int maxRatio = 50;
        public const int minRatio = 12;
        public const int maxTurboVoltage = 1400;
        public const int minTurboVoltage = 800;
        public const int maxOffsetVoltage = 200;
        public const int minOffsetVoltage = -200;
        public const int maxTotalVoltage = 1400;

        private Icon[] iconsW;

        #region Initialize icon and menu
        public NotificationIcon()
        {

			Pstate = new UInt64[Pstates];
            BoostFreq = new UInt64[3];
            PstateOc = new UInt64();
            // Generate icons

            GenTempIcons();
            //Application.Exit();

            iconsW = new Icon[100];

            for (int i = 0; i < 100; i++)
            {
                try
                {
                    iconsW[i] = new Icon(@"icons\\32W" + i.ToString() + ".ico", 32, 32);
                }
                catch
                {
                    // Attempt to generate icons
                    GenTempIcons();
                    //throw new System.ApplicationException("Error loading icons, corrupt file structure?");
                }
            }

            // Init tray icon
            notifyIcon = new NotifyIcon
            {
                Visible = false,
                Icon = GenIcon()
            };
            notificationMenu = new ContextMenu(InitializeMenu());

            notifyIcon.DoubleClick += IconDoubleClick;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotificationIcon));
            notifyIcon.ContextMenu = notificationMenu;

            // Load service data interface
            di = new DataInterface(false);

            smuVersion = getSmuVersionString(di.MemRead(DataInterface.REG_SMU_VERSION));
            cpuType = di.MemRead(DataInterface.REG_CPU_TYPE);
            smuVersionInt = checkSmuVersion(di.MemRead(DataInterface.REG_SMU_VERSION));

            // Load initial temp value + icon
            tempTimerHandler(null, null);

            // Check if data from service is valid (offset 0x00 bit 0 isAvailable=1)
            if (!isAvailable)
            {
                throw new System.ApplicationException("Problems communicating with the service");
            }

            // Service version
            if (ServiceVersion != DataInterface.ServiceVersion) throw new System.ApplicationException("Application and service version doesn't match, please uninstall the previous version and relaunch the application.");

            // CPU check
            if (!supportedCpu) throw new System.ApplicationException("Unsupported CPU.");

            // Settings check
            if (SettingsReset)
            {
                MessageBox.Show("Settings have been reset.", MessageBoxTitle);
                sf = new SettingsForm();
                sf.Show();
            }
            else if (ShutdownUnclean)
            {
                MessageBox.Show("Last shutdown was not clean, settings have not been applied.", MessageBoxTitle);
                sf = new SettingsForm();
                sf.Show();
            }
            else if (ApplyAtStart)
            {
                // Apply previous settings
                Execute(DataInterface.NOTIFY_APPLY, false);
            }

            // Init temperature timer
            tempTimer = new System.Timers.Timer
            {
                Interval = 1000
            };
            tempTimer.Elapsed += new ElapsedEventHandler(tempTimerHandler);
            tempTimer.Start();
            serviceTimeout = 0;
            tempTimerHandler(null, null);

            notifyIcon.Visible = true;

            if ((smuVersionInt <= 2583 && cpuType <= 4)
                 || (smuVersionInt <= 4316 && cpuType > 4 && cpuType <= 6))
            {
                MessageBox.Show("Newer SMU version required. The application will most probably not work correctly. Please use version older than 0.8.0.");
            }

            MinimizeFootprint();

            string[] args = Environment.GetCommandLineArgs();
            //for(int i = 0; i<args.Length; i++) MessageBox.Show(i.ToString()+": "+args[i]);

            // Display main window
            if (args.Length < 2 || args[1] != "-min")
            {
                if (sf == null)
                {
                    sf = new SettingsForm();
                    sf.Show();
                }
            }

        }

        private MenuItem[] InitializeMenu()
        {
            MenuItem[] menu = new MenuItem[] {
                // Show product name + version in menu
                //new MenuItem(Application.ProductName+" "+Application.ProductVersion.Substring(0,Application.ProductVersion.LastIndexOf("."))),
                new MenuItem(Application.ProductName+" "+Application.ProductVersion),
                new MenuItem("Exit", menuExitClick)
            };
            menu[0].Enabled = false;
            return menu;
        }
        #endregion

        #region Main - Program entry point
        /// <summary>Program entry point.</summary>
        /// <param name="args">Command Line Arguments</param>
        [STAThread]
        public static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Please use a unique name for the mutex to prevent conflicts with other programs
            using (Mutex mtx = new Mutex(true, Application.ProductName + " " + Application.ProductVersion, out bool isFirstInstance))
            {
                if (isFirstInstance)
                {

                    // Fix path
                    Directory.SetCurrentDirectory(Application.StartupPath);

                    // Set motherboard vendor and CPU info
                    initVendorInfo();

                    // Check service running/installed
                    ServiceController svc = new ServiceController("ZenStatesSrv");

                    try
                    {
                        ServiceControllerStatus svc_status = svc.Status;
                    }
                    catch (InvalidOperationException ex)
                    {
                        //MessageBox.Show("ZenStatesSrv not installed, attempting to install ...", MessageBoxTitle);
                        ProcessStartInfo info = new ProcessStartInfo("sc.exe", "create ZenStatesSrv binPath= \"" + Path.Combine(Application.StartupPath, "ZenStatesSrv.exe") + "\" DisplayName= \"ZenStates\" start= auto")
                        {
                            UseShellExecute = true,
                            Verb = "runas"
                        };
                        try
                        {
                            Process process = Process.Start(info);
                            process.WaitForExit(10000);
                            ServiceControllerStatus svc_status = svc.Status;
                        }
                        catch (Exception ex2)
                        {
                            MessageBox.Show("Could not install ZenStatesSrv", MessageBoxTitle);
                            return;
                        }

                    }

                    if (svc.Status != ServiceControllerStatus.Running)
                    {
                        // Service not running, try to start it
                        //MessageBox.Show("ZenStatesSrv is not running", MessageBoxTitle);
                        ProcessStartInfo info = new ProcessStartInfo("sc.exe", "start ZenStatesSrv")
                        {
                            UseShellExecute = true,
                            Verb = "runas"
                        };
                        try
                        {
                            Process process = Process.Start(info);
                            process.WaitForExit(10000);
                            svc.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(5));
                            if (svc.Status != ServiceControllerStatus.Running) throw new Exception("ZenStatesSrv couldn't start.");
                        }
                        catch (Exception ex2)
                        {
                            MessageBox.Show("Could not start ZenStatesSrv, check the Event Log for further details", MessageBoxTitle);
                            return;
                        }
                    }

                    NotificationIcon notificationIcon;

                    try
                    {
                        notificationIcon = new NotificationIcon();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, MessageBoxTitle);
                        return;
                    }

                    notificationIcon.notifyIcon.Visible = true;

                    Application.Run();

                    notificationIcon.notifyIcon.Dispose();

                }
                else
                {
                    // The application is already running
                    // TODO: Display message box or change focus to existing application instance
                }
            } // releases the Mutex

        }
        #endregion

        #region Event Handlers

        private void menuExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void IconDoubleClick(object sender, EventArgs e)
        {

            if (sf == null || sf.IsDisposed)
            {
                sf = new SettingsForm();
            }

            sf.Show();
            sf.BringToFront();
            sf.WindowState = FormWindowState.Normal;
        }

        private static UInt64 waitCmd;
        private static bool waitCmdUpdateGui;

        public static void Execute(UInt64 cmd, bool updateGui)
        {
            di.MemWrite(DataInterface.REG_NOTIFY_STATUS, cmd);
            waitCmd = cmd;
            waitCmdUpdateGui = updateGui;
        }

        private static string getSmuVersionString(UInt64 version)
        {
            string[] versionString = new string[3];
            versionString[0] = ((version & 0x00FF0000) >> 16).ToString("D2");
            versionString[1] = ((version & 0x0000FF00) >> 8).ToString("D2");
            versionString[2] = (version & 0x000000FF).ToString("D2");

            return String.Join(".", versionString);
        }

        static int checkSmuVersion(UInt64 version)
        {
            // UInt64 version = di.MemRead(DataInterface.REG_SMU_VERSION);
            int smuMajor = (int)((version & 0x00FF0000) >> 16);
            int smuMinor = (int)((version & 0x0000FF00) >> 8);
            int smu = smuMajor * 100 + smuMinor;

            return smu;
        }

        void tempTimerHandler(object sender, ElapsedEventArgs e)
        {
            try
            {
                // timeout handling
                if (di.MemRead(DataInterface.REG_PING_PONG) == 0)
                {
                    // timeout cleared
                    serviceTimeout = 0;
                    di.MemWrite(DataInterface.REG_PING_PONG, 0x01);
                }
                else
                {
                    serviceTimeout++;
                    if (serviceTimeout > 3)
                    {
                        // 6000ms timeout occured
                        tempTimer.Stop();
                        MessageBox.Show("Lost contact with ZenStatesSrv", Application.ProductName);
                        Environment.Exit(0);
                    }
                }

                bool updateGui = false;

                // Manage client commands
                if (waitCmd != DataInterface.NOTIFY_CLEAR)
                {
                    // Check for execution
                    if (di.MemRead(DataInterface.REG_NOTIFY_STATUS) == DataInterface.NOTIFY_DONE)
                    {

                        // Clear command
                        di.MemWrite(DataInterface.REG_NOTIFY_STATUS, DataInterface.NOTIFY_CLEAR);

                        // Update GUI?
                        if (waitCmdUpdateGui) updateGui = true;

                        // Clear wait cmd
                        waitCmd = DataInterface.NOTIFY_CLEAR;

                    }
                }

                ServiceVersion = di.MemRead(DataInterface.REG_SERVER_VERSION);

                if ((di.MemRead(DataInterface.REG_SERVER_FLAGS) & DataInterface.FLAG_IS_AVAILABLE) == 0) isAvailable = false;
                else isAvailable = true;

                if ((di.MemRead(DataInterface.REG_SERVER_FLAGS) & DataInterface.FLAG_SUPPORTED_CPU) == 0) supportedCpu = false;
                else supportedCpu = true;

                if ((di.MemRead(DataInterface.REG_SERVER_FLAGS) & DataInterface.FLAG_SETTINGS_RESET) == 0) SettingsReset = false;
                else SettingsReset = true;

                if ((di.MemRead(DataInterface.REG_SERVER_FLAGS) & DataInterface.FLAG_SETTINGS_SAVED) == 0) SettingsSaved = false;
                else SettingsSaved = true;

                if ((di.MemRead(DataInterface.REG_SERVER_FLAGS) & DataInterface.FLAG_SHUTDOWN_UNCLEAN) == 0) ShutdownUnclean = false;
                else ShutdownUnclean = true;

                if ((di.MemRead(DataInterface.REG_SERVER_FLAGS) & DataInterface.FLAG_P80_TEMP_EN) == 0) P80Temp = false;
                else P80Temp = true;

                if ((di.MemRead(DataInterface.REG_SERVER_FLAGS) & DataInterface.FLAG_TRAY_ICON_AT_START) == 0) TrayIconAtStart = false;
                else TrayIconAtStart = true;

                if ((di.MemRead(DataInterface.REG_SERVER_FLAGS) & DataInterface.FLAG_APPLY_AT_START) == 0) ApplyAtStart = false;
                else ApplyAtStart = true;

                for (int i = 0; i < Pstates; i++) Pstate[i] = di.MemRead(DataInterface.REG_P0 + i);
                for (int i = 0; i < 2; i++) BoostFreq[i] = di.MemRead(DataInterface.REG_BOOST_FREQ_0 + i);
                BoostFreq[2] = Pstate[2];

                PstateOc = di.MemRead(DataInterface.REG_PSTATE_OC);

                if ((di.MemRead(DataInterface.REG_SERVER_FLAGS) & DataInterface.FLAG_C6CORE) == 0) ZenC6Core = false;
                else ZenC6Core = true;

                if ((di.MemRead(DataInterface.REG_SERVER_FLAGS) & DataInterface.FLAG_C6PACKAGE) == 0) ZenC6Package = false;
                else ZenC6Package = true;

                if ((di.MemRead(DataInterface.REG_SERVER_FLAGS) & DataInterface.FLAG_CPB) == 0) ZenCorePerfBoost = false;
                else ZenCorePerfBoost = true;

                if ((di.MemRead(DataInterface.REG_SERVER_FLAGS) & DataInterface.FLAG_OC) == 0) ZenOc = false;
                else ZenOc = true;

                ZenPPT = (int)di.MemRead(DataInterface.REG_PPT);
                ZenTDC = (int)di.MemRead(DataInterface.REG_TDC);
                ZenEDC = (int)di.MemRead(DataInterface.REG_EDC);
                ZenScalar = (int)di.MemRead(DataInterface.REG_SCALAR);

                perfBias = (PerfBias)di.MemRead(DataInterface.REG_PERF_BIAS);

                // Temperature display
                Temp = (int)di.MemRead(DataInterface.REG_TEMP);

                if (Temp > 99) Temp = 99;
                else if (Temp < 0) Temp = 0;

                notifyIcon.Icon = iconsW[Temp];

                if (sf != null)
                {
                    if (updateGui)
                    {
                        sf.ResetValues();
                    }
                    sf.SetSavedButton(!SettingsSaved);
                }

                // Handle startup
                /*if (loadWithOs && loadWithOs_state != 1)
                {
                    setServiceStartup("ZenTCsrv", 2);
                    setSystemStartup(true);
                    loadWithOs_state = 1;
                }
                else if (!loadWithOs && loadWithOs_state != 2)
                {
                    setServiceStartup("ZenTCsrv", 3);
                    setSystemStartup(false);
                    loadWithOs_state = 2;
                }*/

            }
            catch
            {
                // Suppress exception
            }
        }

        #endregion

        Icon GenIcon()
        {
            Graphics canvas;
            Bitmap iconBitmap = new Bitmap(32, 32, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            canvas = Graphics.FromImage(iconBitmap);

            //canvas.DrawIcon(iconImage, 0, 0);

            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Center
            };

            canvas.DrawString(
                    "ZS",
                    new Font("Tahoma", 16, FontStyle.Regular),
                    new SolidBrush(Color.White),
                    //new RectangleF(0, 3, 16, 13),
                    new RectangleF(0, 3, 32, 32),
                    format
                );

            return Icon.FromHandle(iconBitmap.GetHicon());
        }

        void GenTempIcons()
        {

            if (!Directory.Exists(@"icons")) Directory.CreateDirectory("icons");

            Graphics canvas;

            Font font = new Font("Tahoma", 18, FontStyle.Bold);
            Brush brush = new SolidBrush(Color.White);
            StringFormat format = new StringFormat();

            for (int i = 0; i < 100; i++)
            {
                Bitmap iconBitmap = new Bitmap(32, 32, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                canvas = Graphics.FromImage(iconBitmap);

                if (i > 9)
                {
                    format.Alignment = StringAlignment.Near;
                    canvas.DrawString(i.ToString(), font, brush, new RectangleF(-3, 0, 64, 32), format);
                }
                else
                {
                    format.Alignment = StringAlignment.Center;
                    canvas.DrawString(i.ToString(), font, brush, new RectangleF(0, 0, 32, 32), format);
                }
                using (FileStream stream = File.OpenWrite(@"icons\32W" + i.ToString() + ".ico"))
                {
                    Icon.FromHandle(iconBitmap.GetHicon()).Save(stream);
                }
            }
        }

        static void initVendorInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            foreach (ManagementObject obj in searcher.Get())
            {
                mbVendor = (string)obj["Manufacturer"];
                mbName = (string)obj["Product"];
            }
            if (searcher != null) searcher.Dispose();

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            foreach (ManagementObject obj in searcher.Get())
            {
                cpuName = (string)obj["Name"];
                cpuName = cpuName.Replace("(R)", "");
                cpuName = cpuName.Replace("(TM)", "");
            }
            if (searcher != null) searcher.Dispose();

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
            foreach (ManagementObject obj in searcher.Get())
            {
                biosVersion = (string)obj["SMBIOSBIOSVersion"];
                biosVersion = biosVersion.Trim();
            }
            if (searcher != null) searcher.Dispose();
        }

        static void MinimizeFootprint() { EmptyWorkingSet(Process.GetCurrentProcess().Handle); }
    }
}
