using ZenStates;
using Microsoft.Win32;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;
using System.Timers;

namespace ZenStatesSrv
{
    public class ZenStatesSrv : ServiceBase
    {
        public const string MyServiceName = "ZenStatesSrv";

        private const string fileNameGUI = "ZenStates.exe";
        private const string softwareName = "ZenStates";

        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);

        private static CPUHandler cpuh;
        private static DataInterface di;

        private static System.Timers.Timer t1;

        private static UInt64 server_flags;

        private static bool P80Temp = false;

        public ZenStatesSrv()
        {
            this.ServiceName = MyServiceName;
            ((ISupportInitialize)this.EventLog).BeginInit();
            if (!EventLog.SourceExists(this.ServiceName))
            {
                EventLog.CreateEventSource(this.ServiceName, "Application");
            }
            ((ISupportInitialize)this.EventLog).EndInit();

            this.EventLog.Source = this.ServiceName;
            this.EventLog.Log = "Application";
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Add S3 resume fix
            this.CanHandlePowerEvent = true;

            // Shutdown fix
            this.CanStop = true;
            this.CanShutdown = true;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            // TODO: Add cleanup code here (if required)
            base.Dispose(disposing);
        }

        /// <summary>
        /// Start this service.
        /// </summary>
        protected override void OnStart(string[] args)
        {
            // Run init in separate thread
            Thread initThread = new Thread(Init);
            initThread.Start();

        }

        /// <summary>
        /// Stop this service.
        /// </summary>
        protected override void OnStop()
        {
            Deinit();
        }

        protected override void OnShutdown()
        {
            Deinit();
        }

        private void Deinit()
        {
            // Normal shutdown handling
            cpuh.SettingsStore.LastState = 0x00;
            cpuh.SettingsStore.Save();

            cpuh.Unload();
        }

        private void Init()
        {
            // Init handlers

            try
            {
                cpuh = new CPUHandler();
                if (cpuh.cpuType == CPUHandler.CPUType.Unsupported) EventLog.WriteEntry("Unsupported CPU " + cpuh.GetCpuInfo().ToString("X8"));
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("CPUH Error: " + ex.Message);
                Environment.Exit(1);
            }

            try
            {
                di = new DataInterface(true);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(ex.Message);
                Environment.Exit(2);
            }

            SetServerFlags();

            // Initial values 
            di.MemWrite(DataInterface.REG_NOTIFY_STATUS, 0x00);
            di.MemWrite(DataInterface.REG_SERVER_VERSION, DataInterface.ServiceVersion);
            di.MemWrite(DataInterface.REG_PING_PONG, 0x01);
            di.MemWrite(DataInterface.REG_SMU_VERSION, cpuh.getSmuVersion());

            for (int i = 0; i < CPUHandler.NumPstates; i++) di.MemWrite(DataInterface.REG_P0 + i, cpuh.Pstate[i]);
            for (int i = 0; i < 2; i++) di.MemWrite(DataInterface.REG_BOOST_FREQ_0 + i, cpuh.Pstate[0]);

            di.MemWrite(DataInterface.REG_PSTATE_OC, cpuh.PstateOc);
            di.MemWrite(DataInterface.REG_CPU_TYPE, (UInt64)cpuh.cpuType);

            di.MemWrite(DataInterface.REG_PPT, (UInt64)cpuh.ZenPPT);
            di.MemWrite(DataInterface.REG_TDC, (UInt64)cpuh.ZenTDC);
            di.MemWrite(DataInterface.REG_EDC, (UInt64)cpuh.ZenEDC);
            di.MemWrite(DataInterface.REG_SCALAR, (UInt64)cpuh.ZenScalar);

            di.MemWrite(DataInterface.REG_PERF_BIAS, (UInt64)cpuh.PerformanceBias);

            // Timer start
            t1 = new System.Timers.Timer();
            t1.Elapsed += new ElapsedEventHandler(t1Handler);
            t1.Interval = 1000;
            t1.Enabled = true;

            // Optimize
            MinimizeFootprint();
        }

        private static void applySettings()
        {
            SetStartupService(true);
            SetStartupGUI(cpuh.TrayIconAtStart);

            // P80Temp = cpuh.P80Temp;

            if (cpuh.ZenOc)
            {
                if (cpuh.SetOcMode(true)) cpuh.setOverclockFrequencyAllCores(cpuh.PstateOc);
                // cpuh.setCmdTemp(Convert.ToUInt32(cpuh.ZenScalar), Convert.ToUInt32(cpuh.ZenEDC));
            }
            else
            {
                cpuh.SetOcMode(false);
                // Write new P-states
                for (int i = 0; i < CPUHandler.NumPstates; i++)
                {
                    cpuh.WritePstate(i, cpuh.Pstate[i]);
                }

                if (cpuh.cpuType >= CPUHandler.CPUType.Matisse)
                {
                    cpuh.setBoostFrequencySingleCore(cpuh.BoostFreq[0]);
                    cpuh.setBoostFrequencyAllCores(cpuh.BoostFreq[1]);
                }
            }

            cpuh.SetC6Core(cpuh.ZenC6Core);
            cpuh.SetC6Package(cpuh.ZenC6Package);
            cpuh.SetCpb(cpuh.ZenCorePerfBoost);

            cpuh.SetPPT(cpuh.ZenPPT);
            cpuh.SetTDC(cpuh.ZenTDC);
            cpuh.SetEDC(cpuh.ZenEDC);
            cpuh.SetScalar(cpuh.ZenScalar);

            cpuh.SetPerfBias(cpuh.PerformanceBias);
        }

        private static void t1Handler(object source, ElapsedEventArgs e)
        {

            // clear timeout bit
            di.MemWrite(DataInterface.REG_PING_PONG, 0x00);

            // Check notify status
            UInt64 NOTIFY_STATUS = di.MemRead(DataInterface.REG_NOTIFY_STATUS);

            switch (NOTIFY_STATUS)
            {
                case DataInterface.NOTIFY_CLIENT_FLAGS:

                    // store new data
                    UInt64 CLIENT_FLAGS = di.MemRead(DataInterface.REG_CLIENT_FLAGS);

                    // flags
                    if ((CLIENT_FLAGS & DataInterface.FLAG_APPLY_AT_START) == 0) cpuh.ApplyAtStart = false;
                    else cpuh.ApplyAtStart = true;

                    if ((CLIENT_FLAGS & DataInterface.FLAG_TRAY_ICON_AT_START) == 0) cpuh.TrayIconAtStart = false;
                    else cpuh.TrayIconAtStart = true;

                    if ((CLIENT_FLAGS & DataInterface.FLAG_P80_TEMP_EN) == 0) cpuh.P80Temp = false;
                    else cpuh.P80Temp = true;

                    P80Temp = cpuh.P80Temp;

                    if ((CLIENT_FLAGS & DataInterface.FLAG_C6CORE) == 0) cpuh.ZenC6Core = false;
                    else cpuh.ZenC6Core = true;

                    if ((CLIENT_FLAGS & DataInterface.FLAG_C6PACKAGE) == 0) cpuh.ZenC6Package = false;
                    else cpuh.ZenC6Package = true;

                    if ((CLIENT_FLAGS & DataInterface.FLAG_CPB) == 0) cpuh.ZenCorePerfBoost = false;
                    else cpuh.ZenCorePerfBoost = true;

                    if ((CLIENT_FLAGS & DataInterface.FLAG_OC) == 0) cpuh.ZenOc = false;
                    else cpuh.ZenOc = true;

                    // XFR2
                    cpuh.ZenPPT = (int)di.MemRead(DataInterface.REG_PPT);
                    cpuh.ZenTDC = (int)di.MemRead(DataInterface.REG_TDC);
                    cpuh.ZenEDC = (int)di.MemRead(DataInterface.REG_EDC);
                    cpuh.ZenScalar = (int)di.MemRead(DataInterface.REG_SCALAR);

                    // PerfBias
                    cpuh.PerformanceBias = (CPUHandler.PerfBias)di.MemRead(DataInterface.REG_PERF_BIAS);
                    cpuh.PstateOc = di.MemRead(DataInterface.REG_PSTATE_OC);

                    for (int i = 0; i < CPUHandler.NumPstates; i++)
                    {
                        cpuh.Pstate[i] = di.MemRead(DataInterface.REG_P0 + i);
                    }

                    for (int i = 0; i < CPUHandler.NumPstates - 1; i++)
                    {
                        cpuh.BoostFreq[i] = di.MemRead(DataInterface.REG_BOOST_FREQ_0 + i);
                    }
                    /*
                        // Apply settings
                        SetStartupService(true);
                        SetStartupGUI(cpuh.TrayIconAtStart);

                        // Write new P-states
                        if (cpuh.ZenOc)
                        {
                            if (cpuh.SetOcMode(true)) cpuh.setOverclockFrequencyAllCores(di.MemRead(DataInterface.REG_PSTATE_OC));
                            //cpuh.setCmdTemp(Convert.ToUInt32(cpuh.ZenScalar), Convert.ToUInt32(cpuh.ZenEDC));
                        }
                        else
                        {
                            cpuh.SetOcMode(false);
                            for (int i = 0; i < CPUHandler.NumPstates; i++)
                            {
                                cpuh.WritePstate(i, di.MemRead(DataInterface.REG_P0 + i));
                            }

                            if (cpuh.cpuType >= CPUHandler.CPUType.Matisse)
                            {
                                cpuh.setBoostFrequencySingleCore(di.MemRead(DataInterface.REG_BOOST_FREQ_0));
                                cpuh.setBoostFrequencyAllCores(di.MemRead(DataInterface.REG_BOOST_FREQ_1));
                            }
                        }

                        cpuh.SetC6Core(cpuh.ZenC6Core);
                        cpuh.SetC6Package(cpuh.ZenC6Package);
                        cpuh.SetCpb(cpuh.ZenCorePerfBoost);

                        if (cpuh.cpuType < CPUHandler.CPUType.Matisse)
                        {
                            cpuh.SetPPT(cpuh.ZenPPT);
                            cpuh.SetTDC(cpuh.ZenTDC);
                            cpuh.SetEDC(cpuh.ZenEDC);
                        }

                        cpuh.SetScalar(cpuh.ZenScalar);
                        cpuh.SetPerfBias(cpuh.PerformanceBias);
                    */

                    applySettings();
                    cpuh.SettingsSaved = false;

                    // Notify apply
                    di.MemWrite(DataInterface.REG_NOTIFY_STATUS, DataInterface.NOTIFY_DONE);

                    break;

                case DataInterface.NOTIFY_APPLY:
                    P80Temp = cpuh.P80Temp;

                    applySettings();

                    // Notify done
                    di.MemWrite(DataInterface.REG_NOTIFY_STATUS, DataInterface.NOTIFY_DONE);

                    break;

                case DataInterface.NOTIFY_SAVE:
                    cpuh.SaveSettings();

                    di.MemWrite(DataInterface.REG_NOTIFY_STATUS, DataInterface.NOTIFY_DONE);

                    break;

                case DataInterface.NOTIFY_RESTORE:
                    cpuh.Restore();

                    // Store new P-states
                    for (int i = 0; i < CPUHandler.NumPstates; i++)
                    {
                        di.MemWrite(DataInterface.REG_P0 + i, cpuh.Pstate[i]);
                    }

                    for (int i = 0; i < 2; i++)
                    {
                        di.MemWrite(DataInterface.REG_BOOST_FREQ_0 + i, cpuh.BoostFreq[i]);
                    }

                    di.MemWrite(DataInterface.REG_PSTATE_OC, cpuh.PstateOc);

                    di.MemWrite(DataInterface.REG_PPT, (UInt64)cpuh.ZenPPT);
                    di.MemWrite(DataInterface.REG_TDC, (UInt64)cpuh.ZenTDC);
                    di.MemWrite(DataInterface.REG_EDC, (UInt64)cpuh.ZenEDC);
                    di.MemWrite(DataInterface.REG_SCALAR, (UInt64)cpuh.ZenScalar);

                    // Perf bias
                    di.MemWrite(DataInterface.REG_PERF_BIAS, (UInt64)cpuh.PerformanceBias);

                    // Notify done
                    di.MemWrite(DataInterface.REG_NOTIFY_STATUS, DataInterface.NOTIFY_DONE);

                    cpuh.SettingsSaved = false;

                    break;
                default:
                    break;
            }

            // Update temperature
            double Temp = 0;
            bool res = cpuh.GetTemp(ref Temp);
            di.MemWrite(DataInterface.REG_TEMP, (UInt64)Math.Round(Temp));

            // Q-Code Temp
            if (cpuh.P80Temp && P80Temp)
            {
                cpuh.WritePort80Temp(Temp);
            }

            // Update server flags
            SetServerFlags();
        }

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            switch (powerStatus)
            {
                case PowerBroadcastStatus.ResumeAutomatic:
                case PowerBroadcastStatus.ResumeSuspend:

                    if (!cpuh.ShutdownUnclean)
                    {
                        cpuh.SetOcMode(cpuh.ZenOc);

                        if (cpuh.ZenOc)
                        {
                            cpuh.setOverclockFrequencyAllCores(cpuh.PstateOc);
                        }
                        else
                        {
                            // Write P-states
                            for (int i = 0; i < CPUHandler.NumPstates; i++)
                            {
                                cpuh.WritePstate(i, cpuh.Pstate[i]);
                            }
                        }

                        // Write C-states
                        cpuh.SetC6Core(cpuh.ZenC6Core);
                        cpuh.SetC6Package(cpuh.ZenC6Package);
                        cpuh.SetCpb(cpuh.ZenCorePerfBoost);

                        // Perf bias
                        cpuh.SetPerfBias(cpuh.PerformanceBias);

                        cpuh.SetPPT(cpuh.ZenPPT);
                        cpuh.SetTDC(cpuh.ZenTDC);
                        cpuh.SetEDC(cpuh.ZenEDC);
                        cpuh.SetScalar(cpuh.ZenScalar);
                    }

                    break;
                default:
                    break;
            }

            return false;
        }

        static void MinimizeFootprint()
        {
            EmptyWorkingSet(Process.GetCurrentProcess().Handle);
        }

        static void SetStartupGUI(bool enable)
        {
            RegistryKey rkApp = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (rkApp == null)
            {
                //64 bit OS?
                rkApp = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
                rkApp = rkApp.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            }

            if (rkApp != null)
            {

                if (rkApp.GetValue(softwareName) == null)
                {
                    // The value doesn't exist, the application is not set to run at startup
                    if (enable)
                    {
                        rkApp.SetValue(softwareName, "\"" + AppDomain.CurrentDomain.BaseDirectory + fileNameGUI + "\" -min");
                    }
                }
                else if (!enable)
                {
                    // The value exists, delete it
                    rkApp.DeleteValue(softwareName, false);
                }
            }
        }

        static void SetServerFlags()
        {

            server_flags = DataInterface.FLAG_IS_AVAILABLE;
            if (cpuh.cpuType != CPUHandler.CPUType.Unsupported) server_flags |= DataInterface.FLAG_SUPPORTED_CPU;
            if (cpuh.TrayIconAtStart) server_flags |= DataInterface.FLAG_TRAY_ICON_AT_START;
            if (cpuh.ApplyAtStart) server_flags |= DataInterface.FLAG_APPLY_AT_START;
            if (cpuh.SettingsStore.SettingsReset) server_flags |= DataInterface.FLAG_SETTINGS_RESET;
            if (cpuh.SettingsSaved) server_flags |= DataInterface.FLAG_SETTINGS_SAVED;
            if (cpuh.ShutdownUnclean) server_flags |= DataInterface.FLAG_SHUTDOWN_UNCLEAN;
            if (cpuh.P80Temp) server_flags |= DataInterface.FLAG_P80_TEMP_EN;

            if (cpuh.ZenC6Core) server_flags |= DataInterface.FLAG_C6CORE;
            if (cpuh.ZenC6Package) server_flags |= DataInterface.FLAG_C6PACKAGE;
            if (cpuh.ZenCorePerfBoost) server_flags |= DataInterface.FLAG_CPB;
            if (cpuh.ZenOc) server_flags |= DataInterface.FLAG_OC;

            di.MemWrite(DataInterface.REG_SERVER_FLAGS, server_flags);
        }

        static void SetStartupService(bool enable)
        {
            ServiceController svc = new ServiceController(MyServiceName);
            if (enable) ServiceHelper.ChangeStartMode(svc, ServiceStartMode.Automatic);
            else ServiceHelper.ChangeStartMode(svc, ServiceStartMode.Manual);
        }

        static bool CheckUpdate()
        {
            using (WebClient client = new WebClient())
            {

                byte[] response =
                client.UploadValues("http://elmorlabs.com/AsusZsUpdate", new NameValueCollection()
                {
                       { "version", Environment.Version.ToString() }

                });

                string result = System.Text.Encoding.UTF8.GetString(response);

                if (result == "Yes") return true;
                else return false;
            }
        }
    }

    public static class ServiceHelper
    {
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern Boolean ChangeServiceConfig(
            IntPtr hService,
            UInt32 nServiceType,
            UInt32 nStartType,
            UInt32 nErrorControl,
            String lpBinaryPathName,
            String lpLoadOrderGroup,
            IntPtr lpdwTagId,
            [In] char[] lpDependencies,
            String lpServiceStartName,
            String lpPassword,
            String lpDisplayName);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern IntPtr OpenService(
            IntPtr hSCManager, string lpServiceName, uint dwDesiredAccess);

        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr OpenSCManager(
            string machineName, string databaseName, uint dwAccess);

        [DllImport("advapi32.dll", EntryPoint = "CloseServiceHandle")]
        public static extern int CloseServiceHandle(IntPtr hSCObject);

        private const uint SERVICE_NO_CHANGE = 0xFFFFFFFF;
        private const uint SERVICE_QUERY_CONFIG = 0x00000001;
        private const uint SERVICE_CHANGE_CONFIG = 0x00000002;
        private const uint SC_MANAGER_ALL_ACCESS = 0x000F003F;

        public static void ChangeStartMode(ServiceController svc, ServiceStartMode mode)
        {
            var scManagerHandle = OpenSCManager(null, null, SC_MANAGER_ALL_ACCESS);
            if (scManagerHandle == IntPtr.Zero)
            {
                throw new ExternalException("Open Service Manager Error");
            }

            var serviceHandle = OpenService(
                scManagerHandle,
                svc.ServiceName,
                SERVICE_QUERY_CONFIG | SERVICE_CHANGE_CONFIG);

            if (serviceHandle == IntPtr.Zero)
            {
                throw new ExternalException("Open Service Error");
            }

            var result = ChangeServiceConfig(
                serviceHandle,
                SERVICE_NO_CHANGE,
                (uint)mode,
                SERVICE_NO_CHANGE,
                null,
                null,
                IntPtr.Zero,
                null,
                null,
                null,
                null);

            if (result == false)
            {
                throw new ExternalException("Could not change service start type");

            }

            CloseServiceHandle(serviceHandle);
            CloseServiceHandle(scManagerHandle);
        }
    }
}
