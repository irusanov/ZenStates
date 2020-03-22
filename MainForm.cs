using OpenLibSys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management;
using System.Threading;
using System.Windows.Forms;

namespace ZenStates
{
    public partial class AppWindow : Form
    {
        // MSR
        const uint MSR_PStateCurLim     = 0xC0010061; // [6:4] PstateMaxVal
        const uint MSR_PStateStat       = 0xC0010063; // [2:0] CurPstate
        const uint MSR_PStateDef0       = 0xC0010064; // [63] PstateEn [21:14] CpuVid [13:8] CpuDfsId [7:0] CpuFid
        const uint MSR_PMGT_MISC        = 0xC0010292; // [32] PC6En
        const uint MSR_PSTATE_BOOST     = 0xC0010293;
        const uint MSR_CSTATE_CONFIG    = 0xC0010296; // [22] CCR2_CC6EN [14] CCR1_CC6EN [6] CCR0_CC6EN
        const uint MSR_HWCR             = 0xC0010015;
        const uint MSR_PERFBIAS1        = 0xC0011020;
        const uint MSR_PERFBIAS2        = 0xC0011021;
        const uint MSR_PERFBIAS3        = 0xC001102B;
        const uint MSR_PERFBIAS4        = 0xC001102D;
        const uint MSR_PERFBIAS5        = 0xC0011093;

        // Thermal
        const uint THM_TCON_CUR_TMP     = 0x00059800;
        const uint THM_TCON_PROCHOT     = 0x00059804;
        const uint THM_TCON_THERM_TRIP  = 0x00059808;

        private enum PerfBias { Auto = 0, None, Cinebench_R11p5, Cinebench_R15, Geekbench_3, SuperPi };

        private static readonly Dictionary<PerfBias, string> PerfBiasDict = new Dictionary<PerfBias, string>
        {
            { PerfBias.Auto, "Auto" },
            { PerfBias.None, "None" },
            { PerfBias.Cinebench_R11p5, "Cinebench R11.5" },
            { PerfBias.Cinebench_R15, "Cinebench R15 / R20" },
            { PerfBias.Geekbench_3, "Geekbench 3 / AIDA64" },
            { PerfBias.SuperPi, "SuperPi" }
        };

        private readonly Ols ols;
        private SMU.CPUType cpuType = SMU.CPUType.Unsupported;
        private SMU smu;
        private SystemInfo si;
        private BackgroundWorker backgroundWorker;
        //private BindingSource siBindingSource;
        private readonly Mutex hMutexPci;
        private Components.PstateItem[] PstateItems;
        private int NUM_PSTATES = 3; // default set to 3, real active states are checked on a later stage

        private void HandleError(string message, string title = "Error")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void RunBackgroundTask(DoWorkEventHandler task, RunWorkerCompletedEventHandler completedHandler)
        {
            try
            {
                backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += task;
                backgroundWorker.RunWorkerCompleted += completedHandler;
                backgroundWorker.RunWorkerAsync();
            }
            catch (ApplicationException ex)
            {
                HandleError(ex.Message);
            }
        }


        private bool SmuWriteReg(uint addr, uint data)
        {
            if (ols.WritePciConfigDwordEx(smu.SMU_PCI_ADDR, smu.SMU_OFFSET_ADDR, addr) == 1)
            {
                return ols.WritePciConfigDwordEx(smu.SMU_PCI_ADDR, smu.SMU_OFFSET_DATA, data) == 1;
            }
            return false;
        }

        private bool SmuReadReg(uint addr, ref uint data)
        {
            if (ols.WritePciConfigDwordEx(smu.SMU_PCI_ADDR, smu.SMU_OFFSET_ADDR, addr) == 1)
            {
                return ols.ReadPciConfigDwordEx(smu.SMU_PCI_ADDR, smu.SMU_OFFSET_DATA, ref data) == 1;
            }
            return false;
        }

        private bool SmuWaitDone()
        {
            bool res = false;
            ushort timeout = 1000;
            uint data = 0;
            while ((!res || data != 1) && --timeout > 0)
            {
                res = SmuReadReg(smu.SMU_ADDR_RSP, ref data);
            }

            if (timeout == 0 || data != 1) res = false;

            return res;
        }

        private bool SmuRead(uint msg, ref uint data)
        {
            if (SmuWriteReg(smu.SMU_ADDR_RSP, 0))
            {
                if (SmuWriteReg(smu.SMU_ADDR_MSG, msg))
                {
                    if (SmuWaitDone())
                    {
                        return SmuReadReg(smu.SMU_ADDR_ARG, ref data);
                    }
                }
            }

            return false;
        }

        private bool SmuWrite(uint msg, uint value)
        {
            bool res = false;
            // Mutex
            if (hMutexPci.WaitOne(5000))
            {
                // Clear response
                if (SmuWriteReg(smu.SMU_ADDR_RSP, 0))
                {
                    // Write data
                    if (SmuWriteReg(smu.SMU_ADDR_ARG, value))
                    {
                        SmuWriteReg(smu.SMU_ADDR_ARG + 4, 0);
                    }

                    // Send message
                    if (SmuWriteReg(smu.SMU_ADDR_MSG, msg))
                    {
                        res = SmuWaitDone();
                    }
                }
            }

            hMutexPci.ReleaseMutex();
            return res;
        }

        private uint ReadDword(uint value)
        {
            ols.WritePciConfigDword(smu.SMU_PCI_ADDR, (byte)smu.SMU_OFFSET_ADDR, value);
            return ols.ReadPciConfigDword(smu.SMU_PCI_ADDR, (byte)smu.SMU_OFFSET_DATA);
        }




        private void CheckOlsStatus()
        {
            // Check support library status
            switch (ols.GetStatus())
            {
                case (uint)Ols.Status.NO_ERROR:
                    break;
                case (uint)Ols.Status.DLL_NOT_FOUND:
                    throw new ApplicationException("WinRing DLL_NOT_FOUND");
                case (uint)Ols.Status.DLL_INCORRECT_VERSION:
                    throw new ApplicationException("WinRing DLL_INCORRECT_VERSION");
                case (uint)Ols.Status.DLL_INITIALIZE_ERROR:
                    throw new ApplicationException("WinRing DLL_INITIALIZE_ERROR");
            }

            // Check WinRing0 status
            switch (ols.GetDllStatus())
            {
                case (uint)Ols.OlsDllStatus.OLS_DLL_NO_ERROR:
                    break;
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED:
                    throw new ApplicationException("WinRing OLS_DRIVER_NOT_LOADED");
                case (uint)Ols.OlsDllStatus.OLS_DLL_UNSUPPORTED_PLATFORM:
                    throw new ApplicationException("WinRing OLS_UNSUPPORTED_PLATFORM");
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_FOUND:
                    throw new ApplicationException("WinRing OLS_DLL_DRIVER_NOT_FOUND");
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_UNLOADED:
                    throw new ApplicationException("WinRing OLS_DLL_DRIVER_UNLOADED");
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED_ON_NETWORK:
                    throw new ApplicationException("WinRing DRIVER_NOT_LOADED_ON_NETWORK");
                case (uint)Ols.OlsDllStatus.OLS_DLL_UNKNOWN_ERROR:
                    throw new ApplicationException("WinRing OLS_DLL_UNKNOWN_ERROR");
            }
        }

        private uint GetCpuInfo()
        {
            uint eax = 0, ebx = 0, ecx = 0, edx = 0;
            ols.CpuidPx(0x00000000, ref eax, ref ebx, ref ecx, ref edx, (UIntPtr)1);
            if (ols.CpuidPx(0x00000001, ref eax, ref ebx, ref ecx, ref edx, (UIntPtr)1) == 1)
            {
                return eax;
            }
            return 0;
        }

        private uint GetSmuVersion()
        {
            uint version = 0;
            if (SmuRead(smu.SMU_MSG_GetSmuVersion, ref version))
            {
                return version;
            }
            return 0;
        }

        private uint GetPatchLevel()
        {
            uint eax = 0, edx = 0;
            if (ols.RdmsrTx(0x8b, ref eax, ref edx, (UIntPtr)(1)) != 1)
            {
                return 0;
            }
            return eax;
        }

        private bool GetOcMode()
        {
            uint eax = 0;
            uint edx = 0;

            if (ols.RdmsrTx(MSR_PStateStat, ref eax, ref edx, (UIntPtr)(1)) == 1)
            {
                return Convert.ToBoolean((eax >> 1) & 1);
            }
            return false;
        }

        private void InitSystemInfo(object sender, DoWorkEventArgs e)
        {
            si.CpuId = GetCpuInfo();
            si.PatchLevel = GetPatchLevel();

            // CPU Check. Compare family, model, ext family, ext model. Ignore stepping/revision
            switch (si.CpuId & 0xFFFFFFF0)
            {
                case 0x00800F10: // CPU \ Zen \ Summit Ridge \ ZP - B0 \ 14nm
                case 0x00800F00: // CPU \ Zen \ Summit Ridge \ ZP - A0 \ 14nm
                    cpuType = SMU.CPUType.SummitRidge;
                    break;
                case 0x00800F80: // CPU \ Zen + \ Pinnacle Ridge \ Colfax 12nm
                    cpuType = SMU.CPUType.PinnacleRidge;
                    break;
                case 0x00810F80: // APU \ Zen + \ Picasso \ 12nm
                    cpuType = SMU.CPUType.Picasso;
                    break;
                case 0x00810F00: // APU \ Zen \ Raven Ridge \ RV - A0 \ 14nm
                case 0x00810F10: // APU \ Zen \ Raven Ridge \ 14nm
                case 0x00820F00: // APU \ Zen \ Raven Ridge 2 \ RV2 - A0 \ 14nm
                    cpuType = SMU.CPUType.RavenRidge;
                    break;
                case 0x00870F10: // CPU \ Zen2 \ Matisse \ MTS - B0 \ 7nm + 14nm I/ O Die
                case 0x00870F00: // CPU \ Zen2 \ Matisse \ MTS - A0 \ 7nm + 14nm I/ O Die
                    cpuType = SMU.CPUType.Matisse;
                    break;
                case 0x00830F00:
                case 0x00830F10: // CPU \ Epyc 2 \ Rome \ Treadripper 2 \ Castle Peak 7nm
                    cpuType = SMU.CPUType.Rome;
                    break;
                case 0x00850F00:
                    cpuType = SMU.CPUType.Fenghuang;
                    break;
                case 0x00850F10: // APU \ Renoir
                    cpuType = SMU.CPUType.Renoir;
                    break;
                default:
                    cpuType = SMU.CPUType.Unsupported;
#if DEBUG
                    cpuType = SMU.CPUType.DEBUG;
#endif
                    break;
            }

            // SMU Init
            smu = GetMaintainedSettings.GetByType(cpuType);
            smu.Version = GetSmuVersion();
            if (smu.Version == 0)
            {
                HandleError("Error getting SMU version!\n" +
                    "Default SMU addresses are not responding to commands.");
            }
            si.SmuVersion = smu.Version;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            foreach (ManagementObject obj in searcher.Get())
            {
                si.MbVendor = ((string)obj["Manufacturer"]).Trim();
                si.MbName = ((string)obj["Product"]).Trim();
            }
            if (searcher != null) searcher.Dispose();

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            foreach (ManagementObject obj in searcher.Get())
            {
                var cpuName = (string)obj["Name"];
                cpuName = cpuName.Replace("(R)", "");
                cpuName = cpuName.Replace("(TM)", "");
                cpuName = cpuName.Trim();
                si.CpuName = cpuName;
                si.FusedCoreCount = int.Parse(obj["NumberOfCores"].ToString());
            }
            if (searcher != null) searcher.Dispose();

            /*
            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
            foreach (ManagementObject obj in searcher.Get())
            {
                si.FusedCoreCount = int.Parse(obj["NumberOfLogicalProcessors"].ToString());
            }
            if (searcher != null) searcher.Dispose();
            */

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
            foreach (ManagementObject obj in searcher.Get())
            {
                si.BiosVersion = ((string)obj["SMBIOSBIOSVersion"]).Trim();
            }
            if (searcher != null) searcher.Dispose();

            si.Threads = Convert.ToInt32(Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS"));
        }

        private void PopulateInfoTab()
        {
            /*siBindingSource = new BindingSource();
            siBindingSource.DataSource = si;
            cpuInfoLabel.DataBindings.Add("Text", siBindingSource, "CpuName", true);
            cpuIdLabel.DataBindings.Add("Text", siBindingSource, "CpuId", true);*/

            cpuInfoLabel.Text = si.CpuName;
            mbVendorInfoLabel.Text = si.MbVendor;
            mbModelInfoLabel.Text = si.MbName;
            biosInfoLabel.Text = si.BiosVersion;
            smuInfoLabel.Text = si.GetSmuVersionString();
            cpuIdLabel.Text = $"{si.GetCpuIdString()} ({cpuType.ToString()})";
            microcodeInfoLabel.Text = Convert.ToString(si.PatchLevel, 16).ToUpper();
        }

        private void InitSystemInfo_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            // Resize main form
            int pstatesHeight = groupBoxPstates.Height;
            int expectedPstateHeight = 115;

            if (pstatesHeight > expectedPstateHeight)
            {
                var formHeight = ActiveForm.Height;
                ActiveForm.Height = formHeight + pstatesHeight - expectedPstateHeight;
            }

            PopulateFrequencyList(comboBoxCpuFreq.Items);
            PopulateCCDList(comboBoxCore.Items);

            comboBoxCore.SelectedIndex = si.PhysicalCoreCount;

            int index = (int)((GetCurrentMulti() - 5.50) / 0.25);
            if (index < 0) index = 0;
            comboBoxCpuFreq.SelectedIndex = index;

            comboBoxPerfBias.DataSource = PerfBiasDict.ToList();
            comboBoxPerfBias.ValueMember = "Key";
            comboBoxPerfBias.DisplayMember = "Value";

            PopulateInfoTab();

            this.Enabled = true;
            /*
            MessageBox.Show($"Fused: {si.FusedCoreCount}\n" +
                $"Thread: {si.Threads}\n" +
                $"SMT: {si.SMT}\n" +
                $"CCD: {si.CCDCount}\n" +
                $"CCX: {si.CCXCount}\n" +
                $"Active cores in CCX : {si.NumCoresInCCX}");
            */
        }

        // TODO: Detect OC Mode and return PState multi if on auto
        private double GetCurrentMulti()
        {
            uint eax = default, edx = default;
            if (ols.RdmsrTx(MSR_PSTATE_BOOST, ref eax, ref edx, (UIntPtr)(1)) != 1)
            {
                HandleError("Error getting current multiplier!");
                return 0;
            }

            return 25 * (eax & 0xFF) / (12.5 * (eax >> 8 & 0x3F));
        }

        private void PopulateFrequencyList(ComboBox.ObjectCollection l)
        {
            for (double multi = 5.5; multi <= 70; multi += 0.25)
            {
                l.Add(new FrequencyListItem(multi, string.Format("x{0:0.00}", multi)));
            }
        }

        /*private void PopulateCCDList(ComboBox.ObjectCollection l)
        {
            for (int core = 0; core < si.FusedCoreCount; ++core)
            {
                Console.WriteLine($"ccd: {core / (si.NumCoresInCCX * 2)}, ccx: {core / si.NumCoresInCCX}, core: {core}");
                l.Add(new CoreListItem(core / si.NumCoresInCCX / 2, core / si.NumCoresInCCX, core));
            }

            l.Add("All Cores");
        }*/

        private void PopulateCCDList(ComboBox.ObjectCollection l)
        {
            for (int core = 0; core < si.PhysicalCoreCount; ++core)
            {
                Console.WriteLine($"ccd: {core / 8}, ccx: {core / 4}, core: {core}");
                l.Add(new CoreListItem(core / 8, core / 4, core));
            }

            l.Add("All Cores");
        }




        #region UI Functions

        // P0 fix C001_0015 HWCR[21]=1
        // Fixes timer issues when not using HPET
        public bool ApplyTscWorkaround()
        {
            uint eax = 0, edx = 0;

            if (ols.Rdmsr(MSR_HWCR, ref eax, ref edx) != -1)
            {
                eax |= 0x200000;
                return ols.Wrmsr(MSR_HWCR, eax, edx) != -1;
            }
            return false;
        }

        private void ApplyFrequencyAllCoreSetting(int frequency)
        {
            if (!SmuWrite(smu.SMU_MSG_SetOverclockFrequencyAllCores, Convert.ToUInt32(frequency)))
                HandleError("Error setting frequency!");
        }

        /*private void ApplyFrequencySingleCoreSetting(CoreListItem i, int frequency)
        {
            Console.WriteLine($"SET - ccd: {i.CCD}, ccx: {i.CCX }, core: {i.CORE % si.NumCoresInCCX }");
            if (!SmuWrite(smu.SMU_MSG_SetOverclockFrequencyPerCore, Convert.ToUInt32(((i.CCD << 4 | i.CCX % 2 & 0xF) << 4 | i.CORE % si.NumCoresInCCX & 0xF) << 20 | frequency & 0xFFFFF)))
                HandleError("Error setting frequency!");
        }*/

        private void ApplyFrequencySingleCoreSetting(CoreListItem i, int frequency)
        {
            Console.WriteLine($"SET - ccd: {i.CCD}, ccx: {i.CCX }, core: {i.CORE % 4 }");
            if (!SmuWrite(smu.SMU_MSG_SetOverclockFrequencyPerCore, Convert.ToUInt32(((i.CCD << 4 | i.CCX % 2 & 0xF) << 4 | i.CORE % 4 & 0xF) << 20 | frequency & 0xFFFFF)))
                HandleError("Error setting frequency!");
        }

        private void ApplyPstates()
        {
            for (int i = 0; i < NUM_PSTATES; i++)
            {
                var item = PstateItems[i];
                if (item.Changed)
                {
                    if (!ApplyTscWorkaround()) return;

                    uint eax = Convert.ToUInt32(item.Pstate & 0xFFFFFFFF);
                    uint edx = Convert.ToUInt32(item.Pstate >> 32);

                    if (ols.Wrmsr(MSR_PStateDef0 + Convert.ToUInt32(i), eax, edx) != 1)
                    {
                        Console.WriteLine($"Could not update Pstate{i}");
                        item.Reset();
                        return;
                    }

                    item.UpdateState();
                }
            }
        }

        private bool ApplyPerfBias(PerfBias pb)
        {
            uint pb1_eax = 0, pb1_edx = 0, pb2_eax = 0, pb2_edx = 0, pb3_eax = 0, pb3_edx = 0, pb4_eax = 0, pb4_edx = 0, pb5_eax = 0, pb5_edx = 0;

            // Read current settings
            if (ols.Rdmsr(MSR_PERFBIAS1, ref pb1_eax, ref pb1_edx) != 1) return false;
            if (ols.Rdmsr(MSR_PERFBIAS2, ref pb2_eax, ref pb2_edx) != 1) return false;
            if (ols.Rdmsr(MSR_PERFBIAS3, ref pb3_eax, ref pb3_edx) != 1) return false;
            if (ols.Rdmsr(MSR_PERFBIAS4, ref pb4_eax, ref pb4_edx) != 1) return false;
            if (ols.Rdmsr(MSR_PERFBIAS5, ref pb5_eax, ref pb5_edx) != 1) return false;

            // Clear by default
            pb1_eax &= 0xFFFFFFEF;
            pb2_eax &= 0xFF83FFFF;
            pb2_eax |= 0x02000000;
            pb3_eax &= 0xFFFFFFF8;
            pb4_eax &= 0xFFF9FFEF;
            pb5_eax &= 0xFFFFFFFE;

            // Specific settings
            switch (pb)
            {
                case PerfBias.None:
                    pb1_eax |= 0x10;
                    pb2_eax |= (8 & 0x1F) << 18;
                    pb3_eax |= (7 & 0x7);
                    pb4_eax |= 0x10;
                    break;
                case PerfBias.Cinebench_R11p5:
                    pb2_eax &= 0xF1FFFFEF;
                    pb3_eax |= (7 & 0x7);
                    pb4_eax |= 0x60010;
                    break;
                /*case PerfBias.Cinebench_R15:
                    pb2_eax |= (3 & 0x1F) << 18;
                    pb2_eax &= 0xF1FFFFEF;
                    pb3_eax |= (6 & 0x7);
                    pb4_eax |= 0x10;
                    pb5_eax |= 1;
                    break;*/
                case PerfBias.Cinebench_R15:
                    pb1_edx &= 0xFFF00F0F;
                    pb2_eax |= (3 & 0x1F) << 18;
                    pb2_eax |= 0x40;
                    pb2_eax &= 0xF1FFFFEF;
                    pb3_eax |= (7 & 0x7);
                    pb4_eax |= 0x15;
                    pb4_edx &= 0x0;
                    pb5_eax |= 1;
                    break;
                case PerfBias.Geekbench_3:
                    //pb2_eax &= 0xF1FFFFEF;
                    pb2_eax |= (4 & 0x1F) << 18;
                    pb3_eax |= (7 & 0x7);
                    pb4_eax |= 0x10;
                    break;
                case PerfBias.SuperPi:
                    pb2_eax |= (1 & 0x1F) << 18;
                    pb3_eax |= (9 & 0x7);
                    break;
                case PerfBias.Auto:
                default:
                    return false;
            }

            // Rewrite
            for (int i = 0; i < si.Threads; i++)
            {
                if (ols.Wrmsr(MSR_PERFBIAS1, pb1_eax, pb1_edx) != 1) return false;
                if (ols.Wrmsr(MSR_PERFBIAS2, pb2_eax, pb2_edx) != 1) return false;
                if (ols.Wrmsr(MSR_PERFBIAS3, pb3_eax, pb3_edx) != 1) return false;
                if (ols.Wrmsr(MSR_PERFBIAS4, pb4_eax, pb4_edx) != 1) return false;
                if (ols.Wrmsr(MSR_PERFBIAS5, pb5_eax, pb5_edx) != 1) return false;
            }

            return true;
        }
        #endregion



        public AppWindow()
        {
            InitializeComponent();

            ols = new Ols();
            hMutexPci = new Mutex();
            si = new SystemInfo();
            //siBindingSource = new BindingSource();
            ToolTip toolTip = new ToolTip();

            this.Enabled = false;

            trayMenuItemApp.Text = Application.ProductName + " " + Application.ProductVersion.Substring(0, Application.ProductVersion.LastIndexOf('.'));
            toolTip.SetToolTip(comboBoxCore, "All physical cores are listed. The app can't enumerate active cores only.");

            // P-States initialization temp
            uint eax = default, edx = default;

            ols.Rdmsr(MSR_PStateCurLim, ref eax, ref edx);
            NUM_PSTATES = Convert.ToInt32((eax >> 4) & 0x7) + 1;
            PstateItems = new Components.PstateItem[NUM_PSTATES];

            for (int i = 0; i < NUM_PSTATES; i++)
            {
                ols.Rdmsr(MSR_PStateDef0 + Convert.ToUInt32(i), ref eax, ref edx);
                PstateItems[i] = new Components.PstateItem
                {
                    Label = $"P{i}",
                    Pstate = (ulong)edx << 32 | eax
                };
                tableLayoutPanel3.Controls.Add(PstateItems[i], 0, i);
            }

            try
            {
                CheckOlsStatus();
                RunBackgroundTask(InitSystemInfo, InitSystemInfo_Complete);
            }
            catch (ApplicationException ex)
            {
                HandleError(ex.Message);
                Dispose();
                Application.Exit();
            }
        }

        private void ApplyCpuTabSettings()
        {
            int frequency = (int)(((FrequencyListItem)comboBoxCpuFreq.SelectedItem).Multi * 100.00);

            // All cores
            if (comboBoxCore.SelectedIndex == si.PhysicalCoreCount)
            {
                ApplyFrequencyAllCoreSetting(frequency);
            }
            else
            {
                ApplyFrequencyAllCoreSetting(550);
                ApplyFrequencySingleCoreSetting((CoreListItem)comboBoxCore.SelectedItem, frequency);
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            var selectedTab = tabControl1.SelectedTab;

            if (selectedTab == cpuTabOC)
            {
                ApplyCpuTabSettings();
                ApplyPstates();
            }

            if (selectedTab == tabPerfBias)
                ApplyPerfBias((PerfBias)comboBoxPerfBias.SelectedIndex);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                trayIcon.Visible = true;
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            trayIcon.Visible = false;
        }

        private void trayMenuItemExit_Click(object sender, EventArgs e)
        {
            Dispose();
            Application.Exit();
        }
    }
}
