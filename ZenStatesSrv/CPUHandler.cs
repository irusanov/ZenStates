using OpenLibSys;
using System;
using System.Configuration;
using System.IO;
using System.Threading;

namespace ZenStatesSrv
{
    /// <summary>
    /// Description of CPUHandler.
    /// </summary>
    public class CPUHandler
    {
        public enum CPUType { Unsupported = 0, DEBUG = 1, SummitRidge = 2, Threadripper = 3, RavenRidge = 4, PinnacleRidge = 5, Picasso = 6, Matisse = 7, Rome = 8, Renoir = 9 };
        public enum PerfBias { Auto = 0, None, Cinebench_R11p5, Cinebench_R15, Geekbench_3 };
        //public enum PerfEnh { None = 0, Level1, Level2, Level3_OC, Level4_OC };
/*
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
        const UInt32 SMC_MSG_EnableSmuFeatures = 0x3;
        const UInt32 SMC_MSG_DisableSmuFeatures = 0x4; // Doesn't work with Matisse;
        const UInt32 SMC_MSG_SetTjMax = 0x23;
        const UInt32 SMC_MSG_EnableOverclocking = 0x24;
        const UInt32 SMC_MSG_DisableOverclocking = 0x25;
        const UInt32 SMC_MSG_SetOverclockFreqAllCores = 0x26;
        const UInt32 SMC_MSG_SetOverclockFreqPerCore = 0x27;
        const UInt32 SMC_MSG_SetOverclockVid = 0x28;
        const UInt32 SMC_MSG_SetBoostLimitFrequency = 0x29;
        const UInt32 SMC_MSG_SetBoostLimitFrequencyAllCores = 0x2B;
        const UInt32 SMC_MSG_GetOverclockCap = 0x2C;
        const UInt32 SMC_MSG_MessageCount = 0x2D;

        // Unknown/disabled in AGESA 1.0.0.2+
        const UInt32 SMC_MSG_SetPPTLimit = 0x31;
        const UInt32 SMC_MSG_TCTL_OFFSET = 0x3A;
        const UInt32 SMC_MSG_SetTDCLimit = 0x43;
        const UInt32 SMC_MSG_SetEDCLimit = 0x44;
        const UInt32 SMC_MSG_SetFITLimit = 0x45;
        // AGESA 1.0.0.2+
        const UInt32 SMC_MSG_SetFITLimitScalar = 0x2F;

        const UInt32 SMU_FeatureFlag_PPT = 0x04;
        const UInt32 SMU_FeatureFlag_TDC = 0x08;
        const UInt32 SMU_FeatureFlag_THERM = 0x10;

        // Legacy (Zen/Zen+)
        const UInt32 SMU_ADDR_MSG_ZEN1 = 0x03B10528;
        const UInt32 SMU_ADDR_RSP_ZEN1 = 0x03B10564;
        const UInt32 SMU_ADDR_ARG0_ZEN1 = 0x03B10598;
        const UInt32 SMU_ADDR_ARG1_ZEN1 = SMU_ADDR_ARG0_ZEN1 + 0x4;

        const UInt32 SMC_MSG_EnableSmuFeatures_ZEN1 = 0x09;
        const UInt32 SMC_MSG_DisableSmuFeatures_ZEN1 = 0x0A;
        const UInt32 SMC_MSG_EnableOverclocking_ZEN1 = 0x23;
        const UInt32 SMC_MSG_DisableOverclocking_ZEN1 = 0x24;
        const UInt32 SMC_MSG_SetOverclockFreqAllCores_ZEN1 = 0x26;
        const UInt32 SMC_MSG_SetOverclockFreqPerCore_ZEN1 = 0x27;
        const UInt32 SMC_MSG_SetOverclockVid_ZEN1 = 0x28;
        const UInt32 SMC_MSG_SetPPTLimit_ZEN1 = 0x31;
        const UInt32 SMC_MSG_TCTL_OFFSET_ZEN1 = 0x3A;
        const UInt32 SMC_MSG_SetTDCLimit_ZEN1 = 0x43;
        const UInt32 SMC_MSG_SetEDCLimit_ZEN1 = 0x44;
        const UInt32 SMC_MSG_SetFITLimit_ZEN1 = 0x45;
        const UInt32 SMC_MSG_SetTjMax_ZEN1 = 0x46;
        const UInt32 SMC_MSG_SetFITLimitScalar_ZEN1 = 0x48;
*/
        private Ols ols;
        private BaseCPUSettings cpuSettings;

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
        public UInt64 PstateOcAtStart;
        public bool ZenC6CoreAtStart = false;
        public bool ZenC6PackageAtStart = false;
        public bool ZenCorePerfBoostAtStart = false;
        public bool ZenOcAtStart = false;

        //public bool ZenTscWorkaround = true;

        public bool TrayIconAtStart = false;
        public bool ApplyAtStart = false;
        public bool P80Temp = false;

        public UInt64[] Pstate = new UInt64[NumPstates];
        public UInt64[] BoostFreq = new UInt64[3];
        public UInt64 PstateOc = 0x0;

        public bool ZenC6Core = false;
        public bool ZenC6Package = false;
        public bool ZenCorePerfBoost = false;
        public bool ZenOc = false;
        public int ZenPPT = 0;
        public int ZenTDC = 0;
        public int ZenEDC = 0;
        public int ZenScalar = 1;

        //public CPUHandler.PerfEnh PerformanceEnhancer = 0;
        public CPUHandler.PerfBias PerformanceBias = PerfBias.Auto;

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
                    this.cpuType = CPUType.SummitRidge;
                    break;
                case 0x00800F80: // CPU \ Zen + \ Pinnacle Ridge \ 12nm
                    this.cpuType = CPUType.PinnacleRidge;
                    break;
                case 0x00810F80: // APU \ Zen + \ Picasso \ 12nm
                    this.cpuType = CPUType.Picasso;
                    break;
                case 0x00810F00: // APU \ Zen \ Raven Ridge \ RV - A0 \ 14nm
                case 0x00810F10: // APU \ Zen \ Raven Ridge \ 14nm
                case 0x00820F00: // APU \ Zen \ Raven Ridge 2 \ RV2 - A0 \ 14nm
                    this.cpuType = CPUType.RavenRidge;
                    break;
                case 0x00870F10: // CPU \ Zen2 \ Matisse \ MTS - B0 \ 7nm + 14nm I/ O Die
                case 0x00870F00: // CPU \ Zen2 \ Matisse \ MTS - A0 \ 7nm + 14nm I/ O Die
                    this.cpuType = CPUType.Matisse;
                    break;
                case 0x00830F00:
                case 0x00830F10: // CPU \ Epyc 2 \ Rome \ Treadripper 2 \ Castle Peak 7nm
                    this.cpuType = CPUType.Rome;
                    break;
                case 0x00850F00:
                case 0x00850F10: // APU \ Renoir \ Fenghuang
                case 0x00860F00: // APU \ Zen2 \ Renoir \ 4000 mobile \ 7nm
                    this.cpuType = CPUType.Renoir;
                    break;
                default:
                    this.cpuType = CPUType.Unsupported;
#if DEBUG
                    this.cpuType = CPUType.DEBUG;
#endif
                    break;
            }

            //this.cpuType = CPUType.Matisse;

            cpuSettings = GetMaintainedCPUSettings.GetByType(this.cpuType);

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

                for (int i = 0; i < 3; i++)
                {
                    BoostFreq[i] = SettingsStore.BoostFreq[i];
                }

                PstateOc = SettingsStore.PstateOc;

                ZenC6Core = SettingsStore.ZenC6Core;
                ZenC6Package = SettingsStore.ZenC6Package;
                ZenCorePerfBoost = SettingsStore.ZenCorePerfBoost;
                ZenOc = SettingsStore.ZenOc;
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
                PstateOcAtStart = 0x0;

                uint edx = 0, eax = 0;
                
                for (uint i = 0; i < NumPstates; i++)
                {
                    if (ols.RdmsrTx(cpuSettings.MSR_PStateDef0 + i, ref eax, ref edx, (UIntPtr)(1)) == 1)
                    {
                        PstateAtStart[i] = ((UInt64)edx << 32) | eax;
                        if (Pstate[i] == 0)
                        {
                            Pstate[i] = PstateAtStart[i];
                        }
                    }
                }

                BoostFreq[0] = Pstate[0];
                BoostFreq[1] = Pstate[0];
                BoostFreq[2] = Pstate[2];

                PstateOcAtStart = PstateAtStart[0];
                if (PstateOc == 0) PstateOc = PstateOcAtStart;

                // Get current C-state settings
                if (ols.RdmsrTx(cpuSettings.MSR_PMGT_MISC, ref eax, ref edx, (UIntPtr)(1)) == 1)
                {
                    ZenC6PackageAtStart = Convert.ToBoolean(edx & 1);
                    if (SettingsStore.SettingsReset) ZenC6Package = ZenC6PackageAtStart;
                }
                if (ols.RdmsrTx(cpuSettings.MSR_CSTATE_CONFIG, ref eax, ref edx, (UIntPtr)(1)) == 1)
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
                if (ols.RdmsrTx(cpuSettings.MSR_HWCR, ref eax, ref edx, (UIntPtr)(1)) == 1)
                {
                    ZenCorePerfBoostAtStart = !Convert.ToBoolean((eax >> 25) & 1);
                    if (SettingsStore.SettingsReset) ZenCorePerfBoost = ZenCorePerfBoostAtStart;
                }

                // Get OC Mode
                if (ols.RdmsrTx(cpuSettings.MSR_PStateStat, ref eax, ref edx, (UIntPtr)(1)) == 1)
                {
                    ZenOcAtStart = Convert.ToBoolean((eax >> 1) & 1);
                    if (SettingsStore.SettingsReset) ZenOc = ZenOcAtStart;
                }

                // Get Tctl offset
                GetTctlOffset(ref TctlOffset);
            }
            else if (cpuType == CPUType.DEBUG)
            {
                Pstate[0] = Convert.ToUInt64("80000000000408A0", 16); //unchecked((UInt64)((1 & 1) << 63 | (0x10 & 0xFF) << 14 | (0x08 & 0xFF) << 8 | 0xA0 & 0xFF));
                Pstate[1] = Convert.ToUInt64("8000000000080A90", 16); //unchecked((UInt64)((1 & 1) << 63 | (0x20 & 0xFF) << 14 | (0x0A & 0xFF) << 8 | 0x90 & 0xFF));
                Pstate[2] = Convert.ToUInt64("8000000000100C80", 16); //unchecked((UInt64)((1 & 1) << 63 | (0x30 & 0xFF) << 14 | (0x0C & 0xFF) << 8 | 0x80 & 0xFF));
                BoostFreq[0] = Pstate[0];
                BoostFreq[1] = Pstate[0];
                BoostFreq[2] = Pstate[2];
                PstateOc = Pstate[0];
                ZenC6Core = false;
                ZenC6CoreAtStart = false;
                ZenC6Package = false;
                ZenC6PackageAtStart = false;
                ZenCorePerfBoost = true;
                ZenCorePerfBoostAtStart = true;
                ZenOc = false;
                ZenOcAtStart = false;
                TctlOffset = 0;
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
                 res = ols.RdmsrTx(cpuSettings.MSR_HWCR, ref eax, ref edx, (UIntPtr)(((UInt64)1) << j));
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
                     res = ols.WrmsrTx(cpuSettings.MSR_HWCR, eax, edx, (UIntPtr)(((UInt64)1) << j));

                     eax = (UInt32)(data & 0xFFFFFFFF);
                     edx = (UInt32)(data >> 32) & 0xFFFFFFFF;

                     // Write P-state
                     res = ols.WrmsrTx((uint)(cpuSettings.MSR_PStateDef0 + pstate), eax, edx, (UIntPtr)(((UInt64)1) << j));

                    if (res == 1)
                    {
                    	Pstate[pstate] = data;
                    	if (pstate == 2) BoostFreq[pstate] = Pstate[pstate];
                   	}
                }
            }

            return res == 1;
        }

        public bool setOverclockFrequencyAllCores(UInt64 data)
        {
            bool res = false;
            uint eax = 0, edx = 0;

            for (int j = 0; j < Threads; j++)
            {

                // P0 fix C001_0015 HWCR[21]=1
                if (ols.RdmsrTx(cpuSettings.MSR_HWCR, ref eax, ref edx, (UIntPtr)(((UInt64)1) << j)) == 1)
                {
                    eax |= 0x200000;
                    if (ols.WrmsrTx(cpuSettings.MSR_HWCR, eax, edx, (UIntPtr)(((UInt64)1) << j)) == 1)
                    {
                        byte fid = Convert.ToByte(data & 0xFF);
                        byte did = Convert.ToByte((data >> 8) & 0x3F);
                        byte vid = Convert.ToByte((data >> 14) & 0xFF);
                        double freq = (25 * fid / (did * 12.5)) * 100;

                        if (cpuType >= CPUType.Matisse)
                        {
                            if (SmuWrite(cpuSettings.SMC_MSG_SetOverclockFreqAllCores, (uint)freq))
                            {
                                if (SmuWrite(cpuSettings.SMC_MSG_SetOverclockVid, vid)) res = true;
                            }
                        }
                        else
                        {
                            /*if (SmuWrite(SMC_MSG_SetOverclockFreqAllCores_ZEN1, (uint)freq))
                            {
                                if (SmuWrite(SMC_MSG_SetOverclockVid_ZEN1, vid)) res = true;
                            }*/
                            res = true;
                        }
                    }
                }
            }

            /*if (cpuType >= CPUType.Matisse)
            {
                WritePstate(0, data);
                WritePstate(1, data);
            }*/

            if (res)
            {
                PstateOc = data;
                WritePstate(0, data);
                WritePstate(1, data);
            }

            return res;
        }

        public bool setCmdTemp(uint msg, uint data)
        {
            return SmuWrite(msg, data);
        }

        public bool setBoostFrequencySingleCore(UInt64 data)
        {
            bool res = false;
            uint eax = 0, edx = 0;

            for (int j = 0; j < Threads; j++)
            {

                // P0 fix C001_0015 HWCR[21]=1
                if (ols.RdmsrTx(cpuSettings.MSR_HWCR, ref eax, ref edx, (UIntPtr)(((UInt64)1) << j)) == 1)
                {
                    eax |= 0x200000;
                    if (ols.WrmsrTx(cpuSettings.MSR_HWCR, eax, edx, (UIntPtr)(((UInt64)1) << j)) == 1)
                    {
                        byte fid = Convert.ToByte(data & 0xFF);
                        byte did = Convert.ToByte((data >> 8) & 0x3F);
                        double freq = (25 * fid / (did * 12.5)) * 100;

                        if (SmuWrite(cpuSettings.SMC_MSG_SetBoostLimitFrequency, (uint)freq)) res = true;
                    }
                }
            }

            if (res) BoostFreq[0] = data;

            WritePstate(0, data);
            WritePstate(1, data);

            return res;
        }

        public bool setBoostFrequencyAllCores(UInt64 data)
        {
            bool res = false;
            uint eax = 0, edx = 0;

            for (int j = 0; j < Threads; j++)
            {

                // P0 fix C001_0015 HWCR[21]=1
                if (ols.RdmsrTx(cpuSettings.MSR_HWCR, ref eax, ref edx, (UIntPtr)(((UInt64)1) << j)) == 1)
                {
                    eax |= 0x200000;
                    if (ols.WrmsrTx(cpuSettings.MSR_HWCR, eax, edx, (UIntPtr)(((UInt64)1) << j)) == 1)
                    {
                        byte fid = Convert.ToByte(data & 0xFF);
                        byte did = Convert.ToByte((data >> 8) & 0x3F);
                        double freq = (25 * fid / (did * 12.5)) * 100;

                        if (SmuWrite(cpuSettings.SMC_MSG_SetBoostLimitFrequencyAllCores, (uint)freq)) res = true;
                    }
                }
            }

            if (res) BoostFreq[1] = data;

            return res;
        }

        public bool SetC6Core(bool en)
        {
            uint eax = 0, edx = 0;
            int res = 1;

            for (int j = 0; j < Threads; j++)
            {

                // Read current settings
                res = ols.RdmsrTx(cpuSettings.MSR_CSTATE_CONFIG, ref eax, ref edx, (UIntPtr)(((UInt64)1) << j));
                if (res == 1)
                {

                    if (en) eax = eax | 0x404040;
                    else eax = eax & 0xFFBFBFBF;
                    //edx = (UInt32)(data>>32)&0xFFFFFFFF;

                    // Rewrite settings
                    res = ols.WrmsrTx(cpuSettings.MSR_CSTATE_CONFIG, eax, edx, (UIntPtr)(((UInt64)1) << j));
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
            res = ols.Rdmsr(cpuSettings.MSR_PMGT_MISC, ref eax, ref edx);
            if (res == 1)
            {

                if (en) edx = edx | 0x1;
                else edx = edx & 0xFFFFFFFE;

                // Rewrite settings
                res = ols.Wrmsr(cpuSettings.MSR_PMGT_MISC, eax, edx);

            }

            if (res == 1) ZenC6Package = en;

            return res == 1;
        }

        public bool SetCpb(bool en)
        {
            uint eax = 0, edx = 0;
            int res = 1;

            // Read current settings
            res = ols.Rdmsr(cpuSettings.MSR_HWCR, ref eax, ref edx);
            if (res == 1)
            {
                if (!en) eax = eax | 1 << 25;
                else eax = eax & 0xFDFFFFFF;

                // Rewrite settings
                res = ols.Wrmsr(cpuSettings.MSR_HWCR, eax, edx);
            }

            if (res == 1) ZenCorePerfBoost = en;

            return res == 1;
        }

        public bool SetPerfBias(PerfBias pb)
        {
            uint pb1_eax = 0, pb1_edx = 0, pb2_eax = 0, pb2_edx = 0, pb3_eax = 0, pb3_edx = 0, pb4_eax = 0, pb4_edx = 0, pb5_eax = 0, pb5_edx = 0;

            // Read current settings
            if (ols.RdmsrTx(cpuSettings.MSR_PERFBIAS1, ref pb1_eax, ref pb1_edx, (UIntPtr)1) != 1) return false;
            if (ols.RdmsrTx(cpuSettings.MSR_PERFBIAS2, ref pb2_eax, ref pb2_edx, (UIntPtr)1) != 1) return false;
            if (ols.RdmsrTx(cpuSettings.MSR_PERFBIAS3, ref pb3_eax, ref pb3_edx, (UIntPtr)1) != 1) return false;
            if (ols.RdmsrTx(cpuSettings.MSR_PERFBIAS4, ref pb4_eax, ref pb4_edx, (UIntPtr)1) != 1) return false;
            if (ols.RdmsrTx(cpuSettings.MSR_PERFBIAS5, ref pb5_eax, ref pb5_edx, (UIntPtr)1) != 1) return false;

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
                case PerfBias.Auto:
                default:
                    return false;
            }

            // Rewrite
            for (int i = 0; i < Threads; i++)
            {
                if (ols.WrmsrTx(cpuSettings.MSR_PERFBIAS1, pb1_eax, pb1_edx, (UIntPtr)(((UInt64)1) << i)) != 1) return false;
                if (ols.WrmsrTx(cpuSettings.MSR_PERFBIAS2, pb2_eax, pb2_edx, (UIntPtr)(((UInt64)1) << i)) != 1) return false;
                if (ols.WrmsrTx(cpuSettings.MSR_PERFBIAS3, pb3_eax, pb3_edx, (UIntPtr)(((UInt64)1) << i)) != 1) return false;
                if (ols.WrmsrTx(cpuSettings.MSR_PERFBIAS4, pb4_eax, pb4_edx, (UIntPtr)(((UInt64)1) << i)) != 1) return false;
                if (ols.WrmsrTx(cpuSettings.MSR_PERFBIAS5, pb5_eax, pb5_edx, (UIntPtr)(((UInt64)1) << i)) != 1) return false;
            }

            PerformanceBias = pb;

            return true;
        }

        public bool SetPPT(int ppt)
        {
            bool res;

            res = SmuWrite(cpuSettings.SMC_MSG_SetPPTLimit, (UInt32)ppt * 1000);

            if (res) ZenPPT = ppt;

            return res;
        }

        public bool SetTDC(int tdc)
        {
            bool res;

            res = SmuWrite(cpuSettings.SMC_MSG_SetTDCLimit, (UInt32)tdc * 1000);

            if (res) ZenTDC = tdc;

            return res;
        }

        public bool SetEDC(int edc)
        {
            bool res;

            res = SmuWrite(cpuSettings.SMC_MSG_SetEDCLimit, (UInt32)edc * 1000);

            if (res) ZenEDC = edc;

            return res;
        }

        public bool SetScalar(int scalar)
        {
            bool res;
            res = SmuWrite(cpuSettings.SMC_MSG_SetFITLimitScalar, (UInt32)scalar);

            if (res) ZenScalar = scalar;

            return res;
        }

        public bool SetOcMode(bool manual)
        {
            bool res;

            if (manual)
            {
                if (cpuType >= CPUType.Matisse)
                    res = SmuWrite(cpuSettings.SMC_MSG_EnableOverclocking, 0);
                else
                    res = true; //SmuWrite(SMC_MSG_EnableOverclocking_ZEN1, 0);
            }
            else
            {
                if (cpuType >= CPUType.Matisse)
                    res = SmuWrite(cpuSettings.SMC_MSG_DisableOverclocking, 0);
                else
                    res = true; //SmuWrite(SMC_MSG_DisableOverclocking_ZEN1, 0);
            }

            if (res) ZenOc = manual;

            return res;
        }

        private bool SmuWriteReg(uint addr, uint data)
        {
            if (ols.WritePciConfigDwordEx(cpuSettings.SMU_PCI_ADDR, cpuSettings.SMU_OFFSET_ADDR, addr) == 1)
            {
                return ols.WritePciConfigDwordEx(cpuSettings.SMU_PCI_ADDR, cpuSettings.SMU_OFFSET_DATA, data) == 1;
            }
            return false;
        }

        private bool SmuReadReg(uint addr, ref uint data)
        {
            if (ols.WritePciConfigDwordEx(cpuSettings.SMU_PCI_ADDR, cpuSettings.SMU_OFFSET_ADDR, addr) == 1)
            {
                return ols.ReadPciConfigDwordEx(cpuSettings.SMU_PCI_ADDR, cpuSettings.SMU_OFFSET_DATA, ref data) == 1;
            }
            return false;
        }

        private bool SmuWaitDone()
        {
            bool res = false;
            UInt16 timeout = 1000;
            UInt32 data = 0;
            while ((!res || data != 1) && --timeout > 0)
            {
               res = SmuReadReg(cpuSettings.SMU_ADDR_RSP, ref data);
            }

            if (timeout == 0 || data != 1) res = false;

            return res;
        }

        private bool SmuRead(uint msg, ref uint data)
        {
            if (SmuWriteReg(cpuSettings.SMU_ADDR_RSP, 0))
            {
                if (SmuWriteReg(cpuSettings.SMU_ADDR_MSG, msg))
                {
                    if (SmuWaitDone())
                    {
                        return SmuReadReg(cpuSettings.SMU_ADDR_ARG0, ref data);
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
                if (SmuWriteReg(cpuSettings.SMU_ADDR_RSP, 0))
                {
                    // Write data
                    if (SmuWriteReg(cpuSettings.SMU_ADDR_ARG0, value))
                    {
                        SmuWriteReg(cpuSettings.SMU_ADDR_ARG1, 0);
                    }

                    // Send message
                    if (SmuWriteReg(cpuSettings.SMU_ADDR_MSG, msg))
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
            ols.WritePciConfigDword(cpuSettings.SMU_PCI_ADDR, (byte)cpuSettings.SMU_OFFSET_ADDR, value);
            return ols.ReadPciConfigDword(cpuSettings.SMU_PCI_ADDR, (byte)cpuSettings.SMU_OFFSET_DATA);
        }

        public bool GetTctlOffset(ref UInt32 offset)
        {
            return SmuRead(cpuSettings.SMC_MSG_GetTctlOffset, ref offset);
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

            res = SmuReadReg(cpuSettings.THM_TCON_CUR_TMP, ref thmData);
            if (res)
            {
                // Range sel = 0 to 255C (Temp = Tctl - offset)
                Temp = (thmData >> 21) * 0.125 - TctlOffset;

                // THMx000[31:21] = CUR_TEMP, THMx000[19] = CUR_TEMP_RANGE_SEL
                if ((thmData & (1 << 19)) != 0)
                {
                    // Range sel = -49 to 206C (Temp = Tctl - offset - 49)
                    Temp -= 49;
                }
            }

            return res;
        }

        public uint GetSmuVersion()
        {
            uint version = 0;
            if (SmuRead(cpuSettings.SMC_MSG_GetSmuVersion, ref version))
            {
                return version;
            }
            return 0;
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
                Pstate[i] = PstateAtStart[i];
            }

            PstateOc = PstateOcAtStart;

            BoostFreq[0] = Pstate[0];
            BoostFreq[1] = Pstate[0];
            BoostFreq[2] = Pstate[2];

            //ZenOc = ZenOcAtStart;

            // C-states
            ZenC6Core = ZenC6CoreAtStart;
            ZenC6Package = ZenC6PackageAtStart;
            ZenCorePerfBoost = ZenCorePerfBoostAtStart;

            ZenPPT = 0;
            ZenEDC = 0;
            ZenTDC = 0;
            ZenScalar = 1;

            // Perf Bias
            PerformanceBias = PerfBias.Auto;
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

            for (int i = 0; i < NumPstates; i++)
            {
                SettingsStore.BoostFreq[i] = BoostFreq[i];
            }

            SettingsStore.PstateOc = PstateOc;
            SettingsStore.ZenC6Core = ZenC6Core;
            SettingsStore.ZenC6Package = ZenC6Package;
            SettingsStore.ZenCorePerfBoost = ZenCorePerfBoost;
            SettingsStore.ZenPPT = ZenPPT;
            SettingsStore.ZenTDC = ZenTDC;
            SettingsStore.ZenEDC = ZenEDC;
            SettingsStore.ZenScalar = ZenScalar;
            SettingsStore.ZenOc = ZenOc;

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
