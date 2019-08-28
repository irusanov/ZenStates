/*
// Feature Control Defines
#define FEATURE_CCLK_CONTROLLER_BIT    0
#define FEATURE_FAN_CONTROLLER_BIT     1
#define FEATURE_DATA_CALCULATION_BIT   2
#define FEATURE_PPT_BIT                3
#define FEATURE_TDC_BIT                4
#define FEATURE_THERMAL_BIT            5
#define FEATURE_FIT_BIT                6
#define FEATURE_QOS_BIT                7
#define FEATURE_CORE_CSTATES_BIT       8
#define FEATURE_PROCHOT_BIT            9
#define FEATURE_MCM_DATA_TRANSFER_BIT  10
#define FEATURE_DLWM_BIT               11
#define FEATURE_PC6_BIT                12
#define FEATURE_CSTATE_BOOST_BIT       13
#define FEATURE_VOLTAGE_CONTROLLER_BIT 14
#define FEATURE_HOT_PLUG_BIT           15
#define FEATURE_SPARE_16_BIT           16
#define FEATURE_FW_DEEPSLEEP_BIT       17
#define FEATURE_SPARE_18_BIT           18
#define FEATURE_SPARE_19_BIT           19
#define FEATURE_SPARE_20_BIT           20
#define FEATURE_SPARE_21_BIT           21
#define FEATURE_SPARE_22_BIT           22
#define FEATURE_SPARE_23_BIT           23
#define FEATURE_SPARE_24_BIT           24
#define FEATURE_SPARE_25_BIT           25
#define FEATURE_SPARE_26_BIT           26
#define FEATURE_SPARE_27_BIT           27
#define FEATURE_SPARE_28_BIT           28
#define FEATURE_SPARE_29_BIT           29
#define FEATURE_SPARE_30_BIT           30
#define FEATURE_SPARE_31_BIT           31
*/

using OpenLibSys;
using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace AsusZsSrv
{
    /// <summary>
    /// Description of CPUHandler.
    /// </summary>
    public class CPUHandler
    {
        public enum CPUType { Unsupported = 0, DEBUG = 1, Summit_Ridge, Threadripper, Raven_Ridge, Pinnacle_Ridge, Matisse, Picasso, Rome };
        public enum PerfBias { None = 0, Cinebench_R15, Cinebench_R11p5, Geekbench_3 };
        //public enum PerfEnh { None = 0, Level1, Level2, Level3_OC, Level4_OC };

        // MSR
        const UInt32 MSR_PStateStat = 0xC0010063; // [2:0] CurPstate
        const UInt32 MSR_PStateDef0 = 0xC0010064; // [63] PstateEn [21:14] CpuVid [13:8] CpuDfsId [7:0] CpuFid
        const UInt32 MSR_PStateDef1 = 0xC0010065; // [63] PstateEn [21:14] CpuVid [13:8] CpuDfsId [7:0] CpuFid
        const UInt32 MSR_PStateDef2 = 0xC0010066; // [63] PstateEn [21:14] CpuVid [13:8] CpuDfsId [7:0] CpuFid
        const UInt32 MSR_PStateDef3 = 0xC0010067; // [63] PstateEn [21:14] CpuVid [13:8] CpuDfsId [7:0] CpuFid
        const UInt32 MSR_PStateDef4 = 0xC0010068; // [63] PstateEn [21:14] CpuVid [13:8] CpuDfsId [7:0] CpuFid
        const UInt32 MSR_PStateDef5 = 0xC0010069; // [63] PstateEn [21:14] CpuVid [13:8] CpuDfsId [7:0] CpuFid
        const UInt32 MSR_PStateDef6 = 0xC001006A; // [63] PstateEn [21:14] CpuVid [13:8] CpuDfsId [7:0] CpuFid
        const UInt32 MSR_PStateDef7 = 0xC001006B; // [63] PstateEn [21:14] CpuVid [13:8] CpuDfsId [7:0] CpuFid

        const UInt32 MSR_PMGT_MISC = 0xC0010292; // [32] PC6En
        const UInt32 MSR_CSTATE_CONFIG = 0xC0010296; // [22] CCR2_CC6EN [14] CCR1_CC6EN [6] CCR0_CC6EN
        const UInt32 MSR_HWCR = 0xC0010015;

        const UInt32 MSR_PERFBIAS1 = 0xC0011020;
        const UInt32 MSR_PERFBIAS2 = 0xC0011021;
        const UInt32 MSR_PERFBIAS3 = 0xC001102B;
        const UInt32 MSR_PERFBIAS4 = 0xC001102D;
        const UInt32 MSR_PERFBIAS5 = 0xC0011093;

        const UInt32 SMU_PCI_ADDR = 0x00000000;

        const UInt32 SMU_OFFSET_ADDR = 0xB8;
        const UInt32 SMU_OFFSET_DATA = 0xBC;

        //const UInt32 SMU_ADDR_MSG = 0x03B10528;
        const UInt32 SMU_ADDR_MSG = 0x03B10530; // Matisse;
        // const UInt32 SMU_ADDR_RSP = 0x03B10564;
        const UInt32 SMU_ADDR_RSP = 0x03B1057C; // Matisse;
        //const UInt32 SMU_ADDR_ARG0 = 0x03B10998;
        const UInt32 SMU_ADDR_ARG0 = 0x03B109C4; // Matisse
        const UInt32 SMU_ADDR_ARG1 = SMU_ADDR_ARG0 + 0x4;

        const UInt32 THM_TCON_CUR_TMP = 0x00059800;
        const UInt32 THM_TCON_THERM_TRIP = 0x00059808;

        const UInt32 SMC_MSG_TestMessage = 0x1;
        const UInt32 SMC_MSG_GetSmuVersion = 0x2;
        const UInt32 SMC_MSG_EnableSmuFeatures = 0x3; // Matisse 0x09;
        const UInt32 SMC_MSG_DisableSmuFeatures = 0x4; // Matisse 0x0A;
        const UInt32 SMC_MSG_EnableOverclocking = 0x24;
        const UInt32 SMC_MSG_DisableOverclocking = 0x25;
        const UInt32 SMC_MSG_SetOverclockFreqAllCore = 0x27;
        const UInt32 SMC_MSG_SetOverclockVid = 0x28;
        const UInt32 SMC_MSG_BoostLimitFreqAllCore = 0x29;
        const UInt32 SMC_MSG_GetOverclockCap = 0x2C;
        const UInt32 SMC_MSG_MessageCount = 0x2D;

        const UInt32 SMC_MSG_SetPPTLimit = 0x31;
        const UInt32 SMC_MSG_TCTL_OFFSET = 0x3A;
        const UInt32 SMC_MSG_SetTDCLimit = 0x43;
        const UInt32 SMC_MSG_SetEDCLimit = 0x44;
        const UInt32 SMC_MSG_SetFITLimit = 0x45;
        const UInt32 SMC_MSG_SetTjMax = 0x46;
        const UInt32 SMC_MSG_SetFITLimitScalar = 0x48;

        const UInt32 SMU_FeatureFlag_PPT = 0x04;
        const UInt32 SMU_FeatureFlag_TDC = 0x08;
        const UInt32 SMU_FeatureFlag_THERM = 0x10;

        private Ols ols;

        public int Threads;

        public string cpuModel;

        public CPUType cpuType = CPUType.Unsupported;
        public UInt32 TctlOffset = 0;

        Mutex hMutexPci;

        public Settings SettingsStore;

        public bool SettingsSaved = false;
        public bool ShutdownUnclean = false;

        public static int NumPstates = 3;

        public UInt64[] PstateAtStart;
        public bool ZenC6CoreAtStart = false;
        public bool ZenC6PackageAtStart = false;
        public bool ZenCorePerfBoostAtStart = false;

        //public bool ZenTscWorkaround = true;


        public bool TrayIconAtStart = false;
        public bool ApplyAtStart = false;
        public bool P80Temp = false;

        public UInt64[] Pstate = new UInt64[CPUHandler.NumPstates];

        public bool ZenC6Core = false;
        public bool ZenC6Package = false;
        public bool ZenCorePerfBoost = false;
        public int ZenPPT = 0;
        public int ZenTDC = 0;
        public int ZenEDC = 0;
        public int ZenScalar = 1;

        //public CPUHandler.PerfEnh PerformanceEnhancer = 0;
        public CPUHandler.PerfBias PerformanceBias = 0;

        public CPUHandler()
        {

            ols = new Ols();

            // Check support library sutatus
            switch (ols.GetStatus())
            {
                case (uint)Ols.Status.NO_ERROR:
                    break;
                case (uint)Ols.Status.DLL_NOT_FOUND:
                    throw new System.ApplicationException("WinRing DLL_NOT_FOUND");
                case (uint)Ols.Status.DLL_INCORRECT_VERSION:
                    throw new System.ApplicationException("WinRing DLL_INCORRECT_VERSION");
                case (uint)Ols.Status.DLL_INITIALIZE_ERROR:
                    throw new System.ApplicationException("WinRing DLL_INITIALIZE_ERROR");
            }

            // Check WinRing0 status
            switch (ols.GetDllStatus())
            {
                case (uint)Ols.OlsDllStatus.OLS_DLL_NO_ERROR:
                    break;
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED:
                    throw new System.ApplicationException("WinRing OLS_DRIVER_NOT_LOADED");
                case (uint)Ols.OlsDllStatus.OLS_DLL_UNSUPPORTED_PLATFORM:
                    throw new System.ApplicationException("WinRing OLS_UNSUPPORTED_PLATFORM");
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_FOUND:
                    throw new System.ApplicationException("WinRing OLS_DLL_DRIVER_NOT_FOUND");
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_UNLOADED:
                    throw new System.ApplicationException("WinRing OLS_DLL_DRIVER_UNLOADED");
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED_ON_NETWORK:
                    throw new System.ApplicationException("WinRing DRIVER_NOT_LOADED_ON_NETWORK");
                case (uint)Ols.OlsDllStatus.OLS_DLL_UNKNOWN_ERROR:
                    throw new System.ApplicationException("WinRing OLS_DLL_UNKNOWN_ERROR");
            }

            // CPU Check. Compare family, model, ext family, ext model. Ignore stepping/revision
            switch (GetCpuInfo() & 0xFFFFFFF0)
            {
                case 0x00800F10: // CPU \ Zen \ Summit Ridge \ ZP - B0 \ 14nm
                case 0x00800F00: // CPU \ Zen \ Summit Ridge \ ZP - A0 \ 14nm
                    this.cpuType = CPUType.Summit_Ridge;
                    break;
                case 0x00800F80: // CPU \ Zen + \ Pinnacle Ridge \ 12nm
                    this.cpuType = CPUType.Pinnacle_Ridge;
                    break;
                case 0x00810F80: // APU \ Zen + \ Picasso \ 12nm
                    this.cpuType = CPUType.Picasso;
                    break;
                case 0x00810F00: // APU \ Zen \ Raven Ridge \ RV - A0 \ 14nm
                case 0x00810F10: // APU \ Zen \ Raven Ridge \ 14nm
                case 0x00820F00: // APU \ Zen \ Raven Ridge 2 \ RV2 - A0 \ 14nm
                    this.cpuType = CPUType.Raven_Ridge;
                    break;
                case 0x00870F10: // CPU \ Zen2 \ Matisse \ MTS - B0 \ 7nm + 14nm I/ O Die
                case 0x00870F00: // CPU \ Zen2 \ Matisse \ MTS - A0 \ 7nm + 14nm I/ O Die
                    this.cpuType = CPUType.Matisse;
                    break;
                case 0x00830F00:
                case 0x00830F10: // CPU \ Epyc 2 \ Rome \ 7nm
                    this.cpuType = CPUType.Rome;
                    break;
                default:
                    this.cpuType = CPUType.Unsupported;
#if DEBUG
                    this.cpuType = CPUType.DEBUG;
#endif
                    break;
            }

            // Get number of threads
            this.Threads = Environment.ProcessorCount;

            // Create mutex handles
            hMutexPci = new Mutex(false, "Global\\Access_PCI");
            SettingsStore = new Settings();

            if (!SettingsStore.SettingsReset) SettingsSaved = true;

            if (cpuType != CPUType.Unsupported)
            {

                TrayIconAtStart = SettingsStore.TrayIconAtStart;
                ApplyAtStart = SettingsStore.ApplyAtStart;
                P80Temp = SettingsStore.P80Temp;

                for (int i = 0; i < NumPstates; i++)
                {
                    Pstate[i] = SettingsStore.Pstate[i];
                }

                ZenC6Core = SettingsStore.ZenC6Core;
                ZenC6Package = SettingsStore.ZenC6Package;
                ZenCorePerfBoost = SettingsStore.ZenCorePerfBoost;
                ZenPPT = SettingsStore.ZenPPT;
                ZenTDC = SettingsStore.ZenTDC;
                ZenEDC = SettingsStore.ZenEDC;
                ZenScalar = SettingsStore.ZenScalar;

                //PerformanceEnhancer = SettingsStore.PerformanceEnhancer;
                PerformanceBias = SettingsStore.PerformanceBias;

                // Safety check
                if (SettingsStore.LastState != 0) ShutdownUnclean = true;

                SettingsStore.LastState = 0x01;
                SettingsStore.Save();

                // Get current P-states
                PstateAtStart = new UInt64[NumPstates];

                uint edx = 0, eax = 0;

                if (cpuType == CPUType.DEBUG)
                {
                    Pstate[0] = Convert.ToUInt64("80000000000408A0", 16); //unchecked((UInt64)((1 & 1) << 63 | (0x10 & 0xFF) << 14 | (0x08 & 0xFF) << 8 | 0xA0 & 0xFF));
                    Pstate[1] = Convert.ToUInt64("8000000000080A90", 16); //unchecked((UInt64)((1 & 1) << 63 | (0x20 & 0xFF) << 14 | (0x0A & 0xFF) << 8 | 0x90 & 0xFF));
                    Pstate[2] = Convert.ToUInt64("8000000000100C80", 16); //unchecked((UInt64)((1 & 1) << 63 | (0x30 & 0xFF) << 14 | (0x0C & 0xFF) << 8 | 0x80 & 0xFF));
                    ZenC6Core = false;
                    ZenC6CoreAtStart = false;
                    ZenC6Package = false;
                    ZenC6PackageAtStart = false;
                    ZenCorePerfBoost = true;
                    ZenCorePerfBoostAtStart = true;
                    TctlOffset = 0;
                }
                else if (cpuType != CPUType.Unsupported)
                {
                    for (uint i = 0; i < NumPstates; i++)
                    {
                        if (ols.RdmsrTx(MSR_PStateDef0 + i, ref eax, ref edx, (UIntPtr)(1)) == 1)
                        {
                            PstateAtStart[i] = ((UInt64)edx << 32) | eax;
                            if (Pstate[i] == 0)
                            {
                                Pstate[i] = PstateAtStart[i];
                            }
                        }
                    }

                    // Get current C-state settings
                    if (ols.RdmsrTx(MSR_PMGT_MISC, ref eax, ref edx, (UIntPtr)(1)) == 1)
                    {
                        ZenC6PackageAtStart = Convert.ToBoolean(edx & 1);
                        if (SettingsStore.SettingsReset) ZenC6Package = ZenC6PackageAtStart;
                    }
                    if (ols.RdmsrTx(MSR_CSTATE_CONFIG, ref eax, ref edx, (UIntPtr)(1)) == 1)
                    {
                        bool CCR0_CC6EN = Convert.ToBoolean((eax >> 6) & 1);
                        bool CCR1_CC6EN = Convert.ToBoolean((eax >> 14) & 1);
                        bool CCR2_CC6EN = Convert.ToBoolean((eax >> 22) & 1);
                        if (CCR0_CC6EN && CCR1_CC6EN && CCR2_CC6EN)
                        {
                            ZenC6CoreAtStart = true;
                        }
                        else
                        {
                            ZenC6CoreAtStart = false;
                        }
                        if (SettingsStore.SettingsReset) ZenC6Core = ZenC6CoreAtStart;
                    }

                    // Get current CPB
                    if (ols.RdmsrTx(MSR_HWCR, ref eax, ref edx, (UIntPtr)(1)) == 1)
                    {
                        ZenCorePerfBoostAtStart = !Convert.ToBoolean((eax >> 25) & 1);
                        if (SettingsStore.SettingsReset) ZenCorePerfBoost = ZenCorePerfBoostAtStart;
                    }

                    // Get Tctl offset
                    GetTctlOffset(ref TctlOffset);
                }

            }
        }

        public uint GetCpuInfo()
        {
            uint eax = 0, ebx = 0, ecx = 0, edx = 0;
            ols.CpuidPx(0x00000000, ref eax, ref ebx, ref ecx, ref edx, (UIntPtr)1);
            if (ols.CpuidPx(0x00000001, ref eax, ref ebx, ref ecx, ref edx, (UIntPtr)1) == 1)
            {
                return eax;
            }
            return 0;
        }

        public string GetCpuString()
        {
            uint eax = 0, ebx = 0, ecx = 0, edx = 0;
            ols.CpuidPx(0x80000000, ref eax, ref ebx, ref ecx, ref edx, (UIntPtr)1);

            string[] temp = new string[12];

            for (int index = 0; index < 3; index++)
            {
                if (ols.CpuidPx((uint)(0x80000002 + index), ref eax, ref ebx, ref ecx, ref edx, (UIntPtr)1) == 1)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        temp[index * 4 + 0] += Convert.ToChar(eax >> (i * 8) & 0xFF);
                        temp[index * 4 + 1] += Convert.ToChar(ebx >> (i * 8) & 0xFF);
                        temp[index * 4 + 2] += Convert.ToChar(ecx >> (i * 8) & 0xFF);
                        temp[index * 4 + 3] += Convert.ToChar(edx >> (i * 8) & 0xFF);
                    }
                }
            }
            return temp[0] + temp[1] + temp[2] + temp[3] + temp[4] + temp[5] + temp[6] + temp[7] + temp[8] + temp[9] + temp[10] + temp[11];
        }

        public bool WritePstate(int pstate, UInt64 data)
        {
            uint eax = 0, edx = 0;
            int res = 1;

            for (int j = 0; j < Threads; j++)
            {

                // P0 fix C001_0015 HWCR[21]=1
                res = ols.RdmsrTx(MSR_HWCR, ref eax, ref edx, (UIntPtr)(((UInt64)1) << j));
                if (res == 1)
                {
                    /*if (P0TscWorkaround && (eax & 0x200000) != 0x200000)
                    {
                        eax |= 0x200000;
                        res = ols.WrmsrTx(MSR_HWCR, eax, edx, (UIntPtr)(1 << j));
                    }
                    else if ((eax & 0x200000) != 0)
                    {
                        eax &= 0xFFDFFFFF;
                        res = ols.WrmsrTx(MSR_HWCR, eax, edx, (UIntPtr)(1 << j));
                    }*/
                    eax |= 0x200000;
                    res = ols.WrmsrTx(MSR_HWCR, eax, edx, (UIntPtr)(((UInt64)1) << j));
                }
            }

            for (int j = 0; j < Threads; j++)
            {
                if (res == 1)
                {
                    eax = (UInt32)(data & 0xFFFFFFFF);
                    edx = (UInt32)(data >> 32) & 0xFFFFFFFF;

                    // Write P-state
                    res = ols.WrmsrTx((uint)(MSR_PStateDef0 + pstate), eax, edx, (UIntPtr)(((UInt64)1) << j));
                }
            }

            if (res == 1)
            {
                Pstate[pstate] = data;
            }

            return res == 1;
        }

        public bool SetC6Core(bool en)
        {
            uint eax = 0, edx = 0;
            int res = 1;

            for (int j = 0; j < Threads; j++)
            {

                // Read current settings
                res = ols.RdmsrTx(MSR_CSTATE_CONFIG, ref eax, ref edx, (UIntPtr)(((UInt64)1) << j));
                if (res == 1)
                {

                    if (en) eax = eax | 0x404040;
                    else eax = eax & 0xFFBFBFBF;
                    //edx = (UInt32)(data>>32)&0xFFFFFFFF;

                    // Rewrite settings
                    res = ols.WrmsrTx(MSR_CSTATE_CONFIG, eax, edx, (UIntPtr)(((UInt64)1) << j));
                }
            }

            if (res == 1) ZenC6Core = en;

            return res == 1;
        }

        public bool SetC6Package(bool en)
        {
            uint eax = 0, edx = 0;
            int res = 1;

            // Read current settings
            res = ols.Rdmsr(MSR_PMGT_MISC, ref eax, ref edx);
            if (res == 1)
            {

                if (en) edx = edx | 0x1;
                else edx = edx & 0xFFFFFFFE;

                // Rewrite settings
                res = ols.Wrmsr(MSR_PMGT_MISC, eax, edx);

            }

            if (res == 1) ZenC6Package = en;

            return res == 1;
        }
        public bool SetCpb(bool en)
        {
            uint eax = 0, edx = 0;
            int res = 1;

            // Read current settings
            res = ols.Rdmsr(MSR_HWCR, ref eax, ref edx);
            if (res == 1)
            {

                if (!en) eax = eax | 1 << 25;
                else eax = eax & 0xFDFFFFFF;

                // Rewrite settings
                res = ols.Wrmsr(MSR_HWCR, eax, edx);

            }

            if (res == 1) ZenCorePerfBoost = en;

            return res == 1;
        }

        public bool SetPerfBias(PerfBias pb)
        {

            uint pb1_eax = 0, pb1_edx = 0, pb2_eax = 0, pb2_edx = 0, pb3_eax = 0, pb3_edx = 0, pb4_eax = 0, pb4_edx = 0, pb5_eax = 0, pb5_edx = 0;

            // Read current settings
            if (ols.RdmsrTx(MSR_PERFBIAS1, ref pb1_eax, ref pb1_edx, (UIntPtr)1) != 1) return false;
            if (ols.RdmsrTx(MSR_PERFBIAS2, ref pb2_eax, ref pb2_edx, (UIntPtr)1) != 1) return false;
            if (ols.RdmsrTx(MSR_PERFBIAS3, ref pb3_eax, ref pb3_edx, (UIntPtr)1) != 1) return false;
            if (ols.RdmsrTx(MSR_PERFBIAS4, ref pb4_eax, ref pb4_edx, (UIntPtr)1) != 1) return false;
            if (ols.RdmsrTx(MSR_PERFBIAS5, ref pb5_eax, ref pb5_edx, (UIntPtr)1) != 1) return false;

            // Clear by default
            pb1_eax &= 0xFFFFFFEF;
            pb2_eax &= 0xFF83FFFF;
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
                    break;
                case PerfBias.Cinebench_R15:
                    pb2_eax |= (3 & 0x1F) << 18;
                    pb3_eax |= (6 & 0x7);
                    pb5_eax |= 1;
                    break;
                case PerfBias.Cinebench_R11p5:
                    pb3_eax |= (7 & 0x7);
                    pb4_eax |= 0x60010;
                    break;
                case PerfBias.Geekbench_3:
                    pb2_eax |= (4 & 0x1F) << 18;
                    pb3_eax |= (7 & 0x7);
                    break;
                default:
                    return false;
            }

            // Rewrite
            for (int i = 0; i < Threads; i++)
            {
                if (ols.WrmsrTx(MSR_PERFBIAS1, pb1_eax, pb1_edx, (UIntPtr)(((UInt64)1) << i)) != 1) return false;
                if (ols.WrmsrTx(MSR_PERFBIAS2, pb2_eax, pb2_edx, (UIntPtr)(((UInt64)1) << i)) != 1) return false;
                if (ols.WrmsrTx(MSR_PERFBIAS3, pb3_eax, pb3_edx, (UIntPtr)(((UInt64)1) << i)) != 1) return false;
                if (ols.WrmsrTx(MSR_PERFBIAS4, pb4_eax, pb4_edx, (UIntPtr)(((UInt64)1) << i)) != 1) return false;
                if (ols.WrmsrTx(MSR_PERFBIAS5, pb5_eax, pb5_edx, (UIntPtr)(((UInt64)1) << i)) != 1) return false;
            }

            PerformanceBias = pb;

            return true;
        }

        public bool SetPPT(int ppt)
        {
            bool res;

            res = SmuWrite(SMC_MSG_SetPPTLimit, (UInt32)ppt * 1000);

            if (res) ZenPPT = ppt;

            return res;
        }

        public bool SetTDC(int tdc)
        {
            bool res;

            res = SmuWrite(SMC_MSG_SetTDCLimit, (UInt32)tdc * 1000);

            if (res) ZenTDC = tdc;

            return res;
        }

        public bool SetEDC(int edc)
        {
            bool res;
            res = SmuWrite(SMC_MSG_SetEDCLimit, (UInt32)edc * 1000);

            if (res) ZenEDC = edc;

            return res;
        }

        public bool SetScalar(int scalar)
        {
            bool res;
            res = SmuWrite(SMC_MSG_SetFITLimitScalar, (UInt32)scalar);

            if (res) ZenScalar = scalar;

            return res;
        }

        /*public bool SetPerfEnhancer(PerfEnh pe) {
            bool res = true;
            //if(pe == PerformanceEnhancer) return true;

            // Mutex
            res = hMutexPci.WaitOne(5000);

            // Level 1/2/3/4
            if(res && cpuType == CPUType.Pinnacle_Ridge) {
                UInt32 ppt = 0, tdc = 0, edc = 0, scalar = 0;
                switch(pe) {
                    case PerfEnh.None:
                        ppt = 0;
                        tdc = 0;
                        edc = 0;
                        scalar = 1;
                        break;
                    case PerfEnh.Level1:
                        ppt = 1000000;
                        tdc = 1000000;
                        edc = 150000;
                        scalar = 10;
                        break;
                    case PerfEnh.Level2:
                        ppt = 1000000;
                        tdc = 1000000;
                        edc = 1000000;
                        scalar = 10;
                        break;
                    case PerfEnh.Level3_OC:
                        ppt = 1000000;
                        tdc = 1000000;
                        edc = 150000;
                        scalar = 1;
                        break;
                    case PerfEnh.Level4_OC:
                        ppt = 1000000;
                        tdc = 1000000;
                        edc = 1000000;
                        scalar = 1;
                        break;
                    default:
                        break;
                }

                // Clear response
                res = SmuWriteReg(SMU_ADDR_RSP, 0);
                if(res) {
                    // Set arg0
                    res = SmuWriteReg(SMU_ADDR_ARG0, ppt);
                    if(res) {
                        // Send message
                        res = SmuWriteReg(SMU_ADDR_MSG, SMC_MSG_SetPPTLimit);
                        if(res) {
                            // Wait for completion
                            res = SmuWaitDone();
                            if(res) {
                                res = SmuWriteReg(SMU_ADDR_RSP, 0);
                                if(res) {
                                    res = SmuWriteReg(SMU_ADDR_ARG0, tdc);
                                    if(res) {
                                        res = SmuWriteReg(SMU_ADDR_MSG, SMC_MSG_SetTDCLimit);
                                        if(res) {
                                            res = SmuWaitDone();
                                            if(res) {
                                                res = SmuWriteReg(SMU_ADDR_RSP, 0);
                                                if(res) {
                                                    res = SmuWriteReg(SMU_ADDR_ARG0, edc);
                                                    if(res) {
                                                        res = SmuWriteReg(SMU_ADDR_MSG, SMC_MSG_SetEDCLimit);
                                                        if(res) {
                                                            res = SmuWaitDone();
                                                            if(res) {
                                                                res = SmuWriteReg(SMU_ADDR_RSP, 0);
                                                                if(res) {
                                                                    res = SmuWriteReg(SMU_ADDR_ARG0, scalar);
                                                                    if(res) {
                                                                        res = SmuWriteReg(SMU_ADDR_MSG, SMC_MSG_SetFITLimitScalar);
                                                                        if(res) {
                                                                            res = SmuWaitDone();
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // Level 3/4
            /*if(res) {
                // Clear response
                res = SmuWriteReg(SMU_ADDR_RSP, 0);
                if(res) {

                    // Arg0 0x3B10598
                    res = SmuWriteReg(SMU_ADDR_ARG0, 0x1C);
                    if(res) {

                        // Arg1 0x03B1059C
                        for(int i = 0; i < 5 && res; i++) {
                            res = SmuWriteReg(SMU_ADDR_ARG1, 0);
                        }

                        if(res) {
                            switch(pe) {
                                case PerfEnh.Level3_OC:
                                case PerfEnh.Level4_OC:
                                    res = SmuWriteReg(SMU_ADDR_MSG, SMC_MSG_DisableSmuFeatures); break;
                                default:
                                    res = SmuWriteReg(SMU_ADDR_MSG, SMC_MSG_EnableSmuFeatures); break;
                            }

                            if(res) {
                                res = SmuWaitDone();
                            }

                        }
                    }
                }
            }

            hMutexPci.ReleaseMutex();

            if(res) PerformanceEnhancer = pe;

            return res;

        }*/

        private bool SmuWriteReg(UInt32 addr, UInt32 data)
        {
            int res = 0;

            // Clear response
            res = ols.WritePciConfigDwordEx(SMU_PCI_ADDR, SMU_OFFSET_ADDR, addr);
            if (res == 1)
            {
                res = ols.WritePciConfigDwordEx(SMU_PCI_ADDR, SMU_OFFSET_DATA, data);
            }

            return (res == 1);
        }

        private bool SmuReadReg(UInt32 addr, ref UInt32 data)
        {
            int res = 0;

            // Clear response
            res = ols.WritePciConfigDwordEx(SMU_PCI_ADDR, SMU_OFFSET_ADDR, addr);
            if (res == 1)
            {
                res = ols.ReadPciConfigDwordEx(SMU_PCI_ADDR, SMU_OFFSET_DATA, ref data);
            }

            return (res == 1);
        }

        private bool SmuWaitDone()
        {
            bool res = false;
            UInt16 timeout = 1000;
            UInt32 data = 0;
            while ((!res || data != 1) && --timeout > 0)
            {
                res = SmuReadReg(SMU_ADDR_RSP, ref data);
            }

            if (timeout == 0 || data != 1) res = false;

            return res;
        }

        private bool SmuRead(UInt32 msg, ref UInt32 data)
        {
            bool res;

            // Clear response
            res = SmuWriteReg(SMU_ADDR_RSP, 0);
            if (res)
            {
                // Send message
                res = SmuWriteReg(SMU_ADDR_MSG, msg);
                if (res)
                {
                    // Check completion
                    res = SmuWaitDone();

                    if (res)
                    {
                        res = SmuReadReg(SMU_ADDR_ARG0, ref data);
                    }
                }
            }

            return res;
        }

        private bool SmuWrite(UInt32 msg, UInt32 data)
        {
            bool res;

            // Mutex
            res = hMutexPci.WaitOne(5000);

            // Clear response
            if (res) res = SmuWriteReg(SMU_ADDR_RSP, 0);
            if (res)
            {
                // Write data
                res = SmuWriteReg(SMU_ADDR_ARG0, data);
                if (res)
                {
                    SmuWriteReg(SMU_ADDR_ARG1, 0);
                }
                // Send message
                res = SmuWriteReg(SMU_ADDR_MSG, msg);
                if (res)
                {
                    res = SmuWaitDone();
                }
            }

            hMutexPci.ReleaseMutex();

            return res;
        }

        public bool GetTctlOffset(ref UInt32 offset)
        {
            return SmuRead(SMC_MSG_TCTL_OFFSET, ref offset);
        }

        public bool GetThermTrip(ref double ThermTrip)
        {
            return false;
        }

        public bool GetTemp(ref double Temp)
        {
            if (cpuType == CPUType.DEBUG)
            {
                Temp = new Random().Next(0, 100);
                return true;
            }

            bool res;
            UInt32 thmData = 0;

            res = SmuReadReg(THM_TCON_CUR_TMP, ref thmData);
            if (res)
            {
                // THMx000[31:21] = CUR_TEMP, THMx000[19] = CUR_TEMP_RANGE_SEL
                if ((thmData & (1 << 19)) == 0)
                {
                    // Range sel = 0 to 255C (Temp = Tctl - offset)
                    Temp = (thmData >> 21) * 0.125 - TctlOffset;
                }
                else
                {
                    // Range sel = -49 to 206C (Temp = Tctl - offset - 49)
                    Temp = (thmData >> 21) * 0.125 - TctlOffset - 49;
                }
            }

            return res;
        }

        public bool WritePort80Temp(double temp)
        {
            if (temp > 99) temp = 99;
            else if (temp < 0) temp = 0;

            int temp8 = (int)Math.Round(temp);

            int tens = temp8 / 10; // 1x = 1, 2x = 2, 3x = 3 ...
            int ones = temp8 - tens * 10; // 11-10 = 1 ...
            int temp_out = (tens << 4) | ones;

            return (ols.WriteIoPortByteEx(0x80, (byte)temp_out) == 1);

        }

        public void Restore()
        {

            // P-states
            for (int i = 0; i < NumPstates; i++)
            {
                WritePstate(i, PstateAtStart[i]);
            }

            // C-states
            SetC6Core(ZenC6CoreAtStart);
            SetC6Package(ZenC6PackageAtStart);
            SetCpb(ZenCorePerfBoostAtStart);

            SetPPT(0);
            SetTDC(0);
            SetEDC(0);
            SetScalar(1);

            // Perf Bias
            SetPerfBias(PerfBias.None);
        }

        public void SaveSettings()
        {
            SettingsStore.TrayIconAtStart = TrayIconAtStart;
            SettingsStore.ApplyAtStart = ApplyAtStart;
            SettingsStore.P80Temp = P80Temp;

            for (int i = 0; i < NumPstates; i++)
            {
                SettingsStore.Pstate[i] = Pstate[i];
            }

            SettingsStore.ZenC6Core = ZenC6Core;
            SettingsStore.ZenC6Package = ZenC6Package;
            SettingsStore.ZenCorePerfBoost = ZenCorePerfBoost;
            SettingsStore.ZenPPT = ZenPPT;
            SettingsStore.ZenTDC = ZenTDC;
            SettingsStore.ZenEDC = ZenEDC;
            SettingsStore.ZenScalar = ZenScalar;

            SettingsStore.PerformanceBias = PerformanceBias;

            SettingsStore.SettingsReset = false;

            SettingsStore.Save();

            SettingsSaved = true;
        }

        public void Unload()
        {
            ols.DeinitializeOls();
        }
    }
}
