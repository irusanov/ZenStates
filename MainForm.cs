using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Windows.Forms;
using ZenStates.Components;
using ZenStates.Utils;

namespace ZenStates
{
    public partial class AppWindow : Form
    {
        //TODO: TlbCacheDis, MSRC001_1004
        // MSR
        private const uint MSR_PStateCurLim     = 0xC0010061; // [6:4] PstateMaxVal
        private const uint MSR_PStateStat       = 0xC0010063; // [2:0] CurPstate
        private const uint MSR_PStateDef0       = 0xC0010064; // [63] PstateEn [21:14] CpuVid [13:8] CpuDfsId [7:0] CpuFid
        private const uint MSR_PMGT_MISC        = 0xC0010292; // [32] PC6En
        private const uint MSR_PSTATE_BOOST     = 0xC0010293;
        private const uint MSR_CSTATE_CONFIG    = 0xC0010296; // [22] CCR2_CC6EN [14] CCR1_CC6EN [6] CCR0_CC6EN
        private const uint MSR_HWCR             = 0xC0010015;
        private const uint MSR_PERFBIAS1        = 0xC0011020;
        private const uint MSR_PERFBIAS2        = 0xC0011021;
        private const uint MSR_PERFBIAS3        = 0xC001102B;
        private const uint MSR_PERFBIAS4        = 0xC001102D;
        private const uint MSR_PERFBIAS5        = 0xC0011093;

        // Thermal
        private const uint THM_TCON_CUR_TMP     = 0x00059800;
        private const uint THM_TCON_PROCHOT     = 0x00059804;
        private const uint THM_TCON_THERM_TRIP  = 0x00059808;

        private enum PerfBias { Auto = 0, None, Cinebench_R11p5, Cinebench_R15, Geekbench_3, SuperPi };
        private enum PerfEnh { Auto = 0, Default, Level1, Level2, Level3_OC, Level4_OC };

        private static readonly Dictionary<PerfBias, string> PerfBiasDict = new Dictionary<PerfBias, string>
        {
            { PerfBias.Auto, "Auto" },
            { PerfBias.None, "None" },
            { PerfBias.Cinebench_R11p5, "Cinebench R11.5" },
            { PerfBias.Cinebench_R15, "Cinebench R15 / R20" },
            { PerfBias.Geekbench_3, "Geekbench 3 / AIDA64" },
            { PerfBias.SuperPi, "SuperPi" }
        };

        private static readonly Dictionary<PerfEnh, string> PerfEnhDict = new Dictionary<PerfEnh, string>
        {
            { PerfEnh.Auto, "Auto" },
            { PerfEnh.Default, "Default" },
            { PerfEnh.Level1, "Level 1" },
            { PerfEnh.Level2, "Level 2" },
            { PerfEnh.Level3_OC, "Level 3 (OC)" },
            { PerfEnh.Level4_OC, "Level 4 (OC)" }
        };

        private readonly SystemInfo si;
        private readonly Ops ops;
        private BackgroundWorker backgroundWorker;
        //private BindingSource siBindingSource;
        private PstateItem[] PstateItems;
        private int NUM_PSTATES = 3; // default set to 3, real active states are checked on a later stage

        private void HandleError(string message, string title = "Error")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SetStatus(string status)
        {
            statusText.Text = status;
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

        private void InitSystemInfo(object sender, DoWorkEventArgs e)
        {
            si.CpuId = ops.GetCpuId();
            si.ExtendedModel = ((si.CpuId & 0xff) >> 4) + ((si.CpuId >> 12) & 0xf0);
            si.PackageType = ops.GetPkgType();
            si.PatchLevel = ops.GetPatchLevel();
            si.SmuVersion = ops.smu.Version;
            int[] coreCount = ops.GetCoreCount();
            si.FusedCoreCount = coreCount[0];
            si.Threads = coreCount[1];
            si.CpuName = ops.GetCpuName();

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            foreach (ManagementObject obj in searcher.Get())
            {
                si.MbVendor = ((string)obj["Manufacturer"]).Trim();
                si.MbName = ((string)obj["Product"]).Trim();
            }
            if (searcher != null) searcher.Dispose();

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
            foreach (ManagementObject obj in searcher.Get())
            {
                si.BiosVersion = ((string)obj["SMBIOSBIOSVersion"]).Trim();
            }
            if (searcher != null) searcher.Dispose();

            manualOverclockItem.Cores = si.PhysicalCoreCount;
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
            cpuIdLabel.Text = $"{si.GetCpuIdString()} ({ops.cpuType})";
            microcodeInfoLabel.Text = Convert.ToString(si.PatchLevel, 16).ToUpper();
        }

        private void InitSystemInfo_Complete(object sender, RunWorkerCompletedEventArgs e)
        {
            // PopulatePstates();
            // InitManualOc();

            // Resize main form
            int pstatesHeight = groupBoxPstates.Height;
            int expectedPstateHeight = 115;

            if (pstatesHeight > expectedPstateHeight)
            {
                var formHeight = ActiveForm.Height;
                ActiveForm.Height = formHeight + pstatesHeight - expectedPstateHeight;
            }

            comboBoxPerfBias.DataSource = PerfBiasDict.ToList();
            comboBoxPerfBias.ValueMember = "Key";
            comboBoxPerfBias.DisplayMember = "Value";

            comboBoxPerfEnh.DataSource = PerfEnhDict.ToList();
            comboBoxPerfEnh.ValueMember = "Key";
            comboBoxPerfEnh.DisplayMember = "Value";

            PopulateInfoTab();
            SetStatus("Ready.");

            //Enabled = true;

            /*MessageBox.Show($"OC Mode: {ops.GetOcMode()}");

            MessageBox.Show($"Fused: {si.FusedCoreCount}\n" +
                $"Thread: {si.Threads}\n" +
                $"SMT: {si.SMT}\n" +
                $"CCD: {si.CCDCount}\n" +
                $"CCX: {si.CCXCount}\n" +
                $"Active cores in CCX : {si.NumCoresInCCX}");*/

        }

        private double GetCurrentMulti(bool ocmode = false)
        {
            uint msr = ocmode ? MSR_PSTATE_BOOST : MSR_PStateDef0;
            uint eax = default, edx = default;

            if (ops.ols.Rdmsr(msr, ref eax, ref edx) != 1)
            {
                HandleError("Error getting current multiplier!");
                return 0;
            }

            double multi = 25 * (eax & 0xFF) / (12.5 * (eax >> 8 & 0x3F));

            // OC mode only supports multipliers in 0.25 increment
            return Math.Round(multi * 4, MidpointRounding.ToEven) / 4;
        }

        private double GetCoreMulti(int index)
        {
            uint eax = default, edx = default;
            if (ops.ols.RdmsrTx(MSR_PSTATE_BOOST, ref eax, ref edx, (UIntPtr)(1 << index)) != 1)
            {
                //HandleError($"Error getting core{index} frequency");
                return 0;
            }

            double multi = 25 * (eax & 0xFF) / (12.5 * (eax >> 8 & 0x3F));
            return Math.Round(multi * 4, MidpointRounding.ToEven) / 4;
        }

        private byte GetCurrentVid(bool ocmode = false)
        {
            uint msr = ocmode ? MSR_PSTATE_BOOST : MSR_PStateDef0;
            uint eax = default, edx = default;

            if (ops.ols.Rdmsr(msr, ref eax, ref edx) != 1)
            {
                HandleError("Error getting current VID!");
                return 0;
            }

            return (byte)(eax >> 14 & 0xFF);
        }

        private bool GetCPB()
        {
            uint eax = 0, edx = 0;
            if (ops.ols.Rdmsr(MSR_HWCR, ref eax, ref edx) != 1)
            {
                HandleError("Error getting CPB MSR!");
                return false;
            }

            return ops.GetBits(eax, 25, 1) == 0;
        }

        private void SetCPB(bool en) 
        {
            uint eax = 0, edx = 0;
            bool res = (ops.ols.Rdmsr(MSR_HWCR, ref eax, ref edx) == 1);

            if (res)
            {
                eax = ops.SetBits(eax, 25, 1, en ? 0 : 1U);
                res = (ops.ols.Wrmsr(MSR_HWCR, eax, edx) == 1);
            }

            if (!res)
                HandleError("Error setting CPB!");
        }

        // Package C6-State
        private bool GetC6Package()
        {
            uint eax = 0, edx = 0;
            if (ops.ols.Rdmsr(MSR_PMGT_MISC, ref eax, ref edx) != 1)
            {
                HandleError("Error getting Package C6-State MSR!");
                return false;
            }

            return ops.GetBits(edx, 0, 1) == 1;
        }

        private void SetC6Package(bool en)
        {
            uint eax = 0, edx = 0;
            bool res = ops.ols.Rdmsr(MSR_PMGT_MISC, ref eax, ref edx) == 1;

            if (res)
            {
                uint val = en ? 1U : 0;

                edx = ops.SetBits(edx, 0, 1, val);
                res = (ops.ols.Wrmsr(MSR_PMGT_MISC, eax, edx) == 1);
            }

            if (!res)
            {
                HandleError("Error setting Package C6-State!");
                return;
            }
        }

        // Core C6-State
        private bool GetC6Core()
        {
            uint eax = default, edx = default;
            if (ops.ols.Rdmsr(MSR_CSTATE_CONFIG, ref eax, ref edx) != 1)
            {
                HandleError("Error getting Core C6-State!");
                return false;
            }

            bool CCR0_CC6EN = Convert.ToBoolean(ops.GetBits(eax, 6, 1));
            bool CCR1_CC6EN = Convert.ToBoolean(ops.GetBits(eax, 14, 1));
            bool CCR2_CC6EN = Convert.ToBoolean(ops.GetBits(eax, 22, 1));

            return CCR0_CC6EN && CCR1_CC6EN && CCR2_CC6EN;
        }

        private void SetC6Core(bool en)
        {
            uint eax = 0, edx = 0;
            bool res = ops.ols.Rdmsr(MSR_CSTATE_CONFIG, ref eax, ref edx) == 1;

            if (res)
            {
                uint val = en ? 1U : 0;

                eax = ops.SetBits(eax, 6, 1, val);
                eax = ops.SetBits(eax, 14, 1, val);
                eax = ops.SetBits(eax, 22, 1, val);

                res = (ops.ols.Wrmsr(MSR_CSTATE_CONFIG, eax, edx) == 1);
            }

            if (!res)
            {
                HandleError("Error setting Core C6-State!");
                return;
            }
        }


        #region UI Functions

        private void PopulatePstates()
        {
            uint eax = default, edx = default;

            ops.ols.Rdmsr(MSR_PStateCurLim, ref eax, ref edx);
            NUM_PSTATES = Convert.ToInt32((eax >> 4) & 0x7) + 1;
            PstateItems = new PstateItem[NUM_PSTATES];

            for (int i = 0; i < NUM_PSTATES; i++)
            {
                ops.ols.Rdmsr(MSR_PStateDef0 + Convert.ToUInt32(i), ref eax, ref edx);
                PstateItems[i] = new PstateItem
                {
                    Label = $"P{i}",
                    Pstate = (ulong)edx << 32 | eax
                };
                tableLayoutPanel3.Controls.Add(PstateItems[i], 0, i);
            }
        }

        private void RefreshState()
        {
            uint eax = default, edx = default;

            InitManualOc();

            for (int i = 0; i < NUM_PSTATES; i++)
            {
                ops.ols.Rdmsr(MSR_PStateDef0 + Convert.ToUInt32(i), ref eax, ref edx);
                PstateItems[i].Pstate = (ulong)edx << 32 | eax;
            }

            SetStatus("Refresh OK.");
        }

        private void InitManualOc()
        {
            bool ocmode = ops.GetOcMode();
            manualOverclockItem.OCmode = ocmode;
            manualOverclockItem.Vid = GetCurrentVid(ocmode);
            manualOverclockItem.Multi = GetCurrentMulti(ocmode);
            manualOverclockItem.ProchotEnabled = ops.IsProchotEnabled();
            // manualOverclockItem.Cores = si.PhysicalCoreCount; // si.FusedCoreCount;
        }

        private void InitPowerTab()
        {
            checkBoxCPB.Checked = GetCPB();
            checkBoxC6Core.Checked = GetC6Core();
            checkBoxC6Package.Checked = GetC6Package();
        }

        // P0 fix C001_0015 HWCR[21]=1
        // Fixes timer issues when not using HPET
        private bool ApplyTscWorkaround()
        {
            uint eax = 0, edx = 0;

            if (ops.ols.Rdmsr(MSR_HWCR, ref eax, ref edx) != -1)
            {
                eax |= 0x200000;
                return ops.ols.Wrmsr(MSR_HWCR, eax, edx) != -1;
            }
            return false;
        }

        private bool SetFrequencyAllCore(int frequency)
        {
            // TODO: Add Manual OC mode
            if (!ops.SmuWrite(ops.smu.SMU_MSG_SetOverclockFrequencyAllCores, Convert.ToUInt32(frequency)))
            {
                HandleError("Error setting frequency!");
                return false;
            }
            return true;
        }

        private bool SetFrequencySingleCore(int mask, int frequency)
        {
            if (!ops.SmuWrite(ops.smu.SMU_MSG_SetOverclockFrequencyPerCore, Convert.ToUInt32(mask | frequency & 0xFFFFF)))
            {
                HandleError("Error setting core frequency!");
                return false;
            }
            return true;
        }

        private bool SetFrequencyMultipleCores(uint mask, int frequency, int count)
        {
            // ((i.CCD << 4 | i.CCX % 2 & 0xF) << 4 | i.CORE % 4 & 0xF) << 20;
            for (uint i = 0; i <= count; i++)
            {
                mask = ops.SetBits(mask, 20, 2, i);
                if (!SetFrequencySingleCore((int)mask, frequency))
                    return false;
            }
            return true;
        }

        private bool SetFrequencyCCX(uint mask, int frequency)
        {
            bool ret = SetFrequencyMultipleCores(mask, frequency, si.NumCoresInCCX);
            if (!ret)
                HandleError("Error setting CCX frequency!");
            return ret;
        }

        private bool SetFrequencyCCD(uint mask, int frequency)
        {
            bool ret = true;
            for (uint i = 0; i <= si.CCXCount / si.CCDCount; i++)
            {
                mask = ops.SetBits(mask, 24, 1, i);
                ret = SetFrequencyCCX(mask, frequency);
            }
            if (!ret)
                HandleError("Error setting CCD frequency!");
            return ret;
        }

        private bool SetOCVid(byte vid)
        {
            if (!ops.SmuWrite(ops.smu.SMU_MSG_SetOverclockCpuVid, Convert.ToUInt32(vid)))
            {
                HandleError("Error setting CPU Overclock VID!");
                return false;
            }
            return true;
        }

        private bool SetOcMode(bool enabled, uint arg = 0U)
        {
            uint cmd = enabled ? ops.smu.SMU_MSG_EnableOcMode : ops.smu.SMU_MSG_DisableOcMode;
            if (!ops.SmuWrite(cmd, arg))
            {
                HandleError("Error setting OC mode!");
                return false;
            }
            return true;
        }

        private bool SetProchot(bool enabled = true)
        {
            uint arg = enabled ? 0U : 0x1000000;
            bool res = SetOcMode(!enabled, arg);

            // Re-enable OC mode
            if (enabled) res = SetOcMode(true);

            return res;
        }

        private void ApplyManualOcSettings()
        {
            bool ret = false;
            var item = manualOverclockItem;
            int targetFreq = (int)(item.Multi * 100.00);
            int currentFreq = (int)(GetCurrentMulti(true) * 100.00);

            if (targetFreq >= currentFreq)
                SetOCVid(item.Vid);

            if (item.AllCores)
            {
                ret = SetFrequencyAllCore(targetFreq);
            } 
            else
            {
                switch (item.ControlMode)
                {
                    case 0:
                        if (SetFrequencyAllCore(550))
                        ret = SetFrequencySingleCore(item.CoreMask, targetFreq);
                        break;
                    case 1:
                        ret = SetFrequencyCCX((uint)item.CoreMask, targetFreq);
                        break;
                    case 2:
                        ret = SetFrequencyCCD((uint)item.CoreMask, targetFreq);
                        break;
                    default:
                        break;
                }
            }

            if (targetFreq < currentFreq)
                SetOCVid(item.Vid);

            if (ret)
            {
                item.UpdateState();
                SetStatus("OK.");
            }
            else
            {
                item.Reset();
                SetStatus("Error.");
            }
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

                    if (ops.ols.Wrmsr(MSR_PStateDef0 + Convert.ToUInt32(i), eax, edx) != 1)
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
            if (ops.ols.Rdmsr(MSR_PERFBIAS1, ref pb1_eax, ref pb1_edx) != 1) return false;
            if (ops.ols.Rdmsr(MSR_PERFBIAS2, ref pb2_eax, ref pb2_edx) != 1) return false;
            if (ops.ols.Rdmsr(MSR_PERFBIAS3, ref pb3_eax, ref pb3_edx) != 1) return false;
            if (ops.ols.Rdmsr(MSR_PERFBIAS4, ref pb4_eax, ref pb4_edx) != 1) return false;
            if (ops.ols.Rdmsr(MSR_PERFBIAS5, ref pb5_eax, ref pb5_edx) != 1) return false;

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
                if (ops.ols.Wrmsr(MSR_PERFBIAS1, pb1_eax, pb1_edx) != 1) return false;
                if (ops.ols.Wrmsr(MSR_PERFBIAS2, pb2_eax, pb2_edx) != 1) return false;
                if (ops.ols.Wrmsr(MSR_PERFBIAS3, pb3_eax, pb3_edx) != 1) return false;
                if (ops.ols.Wrmsr(MSR_PERFBIAS4, pb4_eax, pb4_edx) != 1) return false;
                if (ops.ols.Wrmsr(MSR_PERFBIAS5, pb5_eax, pb5_edx) != 1) return false;
            }

            return true;
        }
        #endregion



        public AppWindow()
        {
            InitializeComponent();
            ops = new Ops();
            si = new SystemInfo();

            //siBindingSource = new BindingSource();
            ToolTip toolTip = new ToolTip();

            //Enabled = false;

            trayMenuItemApp.Text = Application.ProductName + " " + Application.ProductVersion.Substring(0, Application.ProductVersion.LastIndexOf('.'));

            try
            {
                ops.CheckOlsStatus();

                if (ops.cpuType == SMU.CPUType.Unsupported)
                {
                    HandleError("CPU is not supported");

                    if (Application.MessageLoop)
                    {
                        Application.Exit();
                    }
                    else
                    {
                        Environment.Exit(1);
                    }
                }

                SetStatus("Initializing...");

                if (ops.smu.Version == 0)
                {
                    HandleError("Error getting SMU version!\n" +
                        "Default SMU addresses are not responding to commands.");
                }

                PopulatePstates();
                InitManualOc();
                InitPowerTab();
                RunBackgroundTask(InitSystemInfo, InitSystemInfo_Complete);

                //Enabled = true;
            }
            catch (ApplicationException ex)
            {
                HandleError(ex.Message);
                Dispose();
                Application.Exit();
            }
        }

        private void ButtonApply_Click(object sender, EventArgs e)
        {
            var selectedTab = tabControl1.SelectedTab;

            if (selectedTab == cpuTabOC)
            {
                if (manualOverclockItem.ModeChanged) {
                    if (SetOcMode(manualOverclockItem.OCmode))
                    {
                        manualOverclockItem.UpdateState();
                    }
                    else
                    {
                        manualOverclockItem.Reset();
                        return;
                    }
                }

                if (manualOverclockItem.OCmode)
                    ApplyManualOcSettings();
                else
                    ApplyPstates();
            }

            if (selectedTab == tabTweaks)
                ApplyPerfBias((PerfBias)comboBoxPerfBias.SelectedIndex);

            if (selectedTab == tabPower)
            {
                SetCPB(checkBoxCPB.Checked);
                SetC6Core(checkBoxC6Core.Checked);
                SetC6Package(checkBoxC6Package.Checked);
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
                trayIcon.Visible = true;
            }
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            trayIcon.Visible = false;
        }

        private void TrayMenuItemExit_Click(object sender, EventArgs e)
        {
            Dispose();
            Application.Exit();
        }

        static void MinimizeFootprint()
        {
            NativeMethods.EmptyWorkingSet(Process.GetCurrentProcess().Handle);
        }

        // Get rid of flicker on form when painting child user controls
        int originalExStyle = -1;
        bool enableFormLevelDoubleBuffering = false;

        protected override CreateParams CreateParams
        {
            get
            {
                if (originalExStyle == -1)
                    originalExStyle = base.CreateParams.ExStyle;

                CreateParams cp = base.CreateParams;
                if (enableFormLevelDoubleBuffering)
                    cp.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED
                else
                    cp.ExStyle = originalExStyle;

                return cp;
            }
        }

        private void TurnOffFormLevelDoubleBuffering()
        {
            enableFormLevelDoubleBuffering = false;
            MinimizeBox = true;
        }

        private void AppWindow_Shown(object sender, EventArgs e)
        {
            //Show();
            MinimizeFootprint();
        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            RefreshState();
        }

        private void ManualOverclockItem_SlowModeClicked(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            int cores = si.Threads;
            int step = si.NumCoresInCCX;
            int index = 0;

            if (si.SMT)
                step *= 2;

            double[] ccx_frequencies = new double[si.CCXCount];

            if (cb.Checked)
            {
                for (var i = 0; i < cores; i+=step)
                {
                    ccx_frequencies[index] = GetCoreMulti(i);
                    ++index;
                }
                Storage.Add($"ccx_frequencies", ccx_frequencies);
                Storage.Add("oc_vid", manualOverclockItem.Vid);

                for (var i = 0; i < si.CCXCount; ++i)
                {
                    Console.WriteLine($"ccx{i}: " + Storage.Get<double[]>($"ccx_frequencies")[i].ToString());
                }

                SetFrequencyAllCore(550);
                SetOCVid(0x98);
            } 
            else
            {
                // Single core mode
                if (manualOverclockItem.ControlMode == 0 && !manualOverclockItem.AllCores)
                {
                    ApplyManualOcSettings();
                }
                else
                {
                    int[] masks = new int[si.CCXCount];

                    for (var i = 0; i < si.PhysicalCoreCount; i+= 4)
                    {
                        int ccd = i / 8;
                        int ccx = i / 4 - 2 * ccd;
                        masks[index] = (ccd << 4 | ccx) << 24;
                        ++index;
                    }

                    SetOCVid(Storage.Get<byte>($"oc_vid"));

                    for (var i = 0; i < si.CCXCount; ++i)
                    {
                        int targetFreq = (int)(Storage.Get<double[]>($"ccx_frequencies")[i] * 100.00);
                        SetFrequencyCCX((uint)masks[i], targetFreq);
                    }
                    //RestoreManualOcSettings();
                }
            }
        }

        private void ManualOverclockItem_ProchotClicked(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            bool res = SetProchot(cb.Checked);
            if (res) SetStatus("PROCHOT " + (cb.Checked ? "enabled." : "disabled."));
        }
    }
}
