using System;
using System.Collections.Generic;

namespace ZenStatesSrv
{
    public abstract class BaseCPUSettings
    {
        // Zen/Zen+
        public BaseCPUSettings()
        {
            // MSR
            MSR_PStateStat = 0xC0010063;    // [2:0] CurPstate
            MSR_PStateDef0 = 0xC0010064;    // [63] PstateEn [21:14] CpuVid [13:8] CpuDfsId [7:0] CpuFid

            MSR_PMGT_MISC = 0xC0010292;     // [32] PC6En
            MSR_CSTATE_CONFIG = 0xC0010296; // [22] CCR2_CC6EN [14] CCR1_CC6EN [6] CCR0_CC6EN
            MSR_HWCR = 0xC0010015;

            MSR_PERFBIAS1 = 0xC0011020;
            MSR_PERFBIAS2 = 0xC0011021;
            MSR_PERFBIAS3 = 0xC001102B;
            MSR_PERFBIAS4 = 0xC001102D;
            MSR_PERFBIAS5 = 0xC0011093;

            // Thermal
            THM_TCON_CUR_TMP = 0x00059800;
            THM_TCON_THERM_TRIP = 0x00059808;

            // SMU
            SMU_PCI_ADDR = 0x00000000;
            SMU_OFFSET_ADDR = 0xB8;
            SMU_OFFSET_DATA = 0xBC;

            SMU_ADDR_MSG = 0x03B10528;
            SMU_ADDR_RSP = 0x03B10564;
            SMU_ADDR_ARG0 = 0x03B10598;
            SMU_ADDR_ARG1 = SMU_ADDR_ARG0 + 0x4;

            // SMU Messages
            SMC_MSG_TestMessage = 0x1;
            SMC_MSG_GetSmuVersion = 0x2;
            SMC_MSG_EnableSmuFeatures = 0x09;
            SMC_MSG_DisableSmuFeatures = 0x0A;
            SMC_MSG_EnableOverclocking = 0x23;
            SMC_MSG_DisableOverclocking = 0x24;
            SMC_MSG_SetOverclockFreqAllCores = 0x26;
            SMC_MSG_SetOverclockFreqPerCore = 0x27;
            SMC_MSG_SetOverclockVid = 0x28;
            SMC_MSG_SetBoostLimitFrequency = 0x29;
            SMC_MSG_SetBoostLimitFrequencyAllCores = 0x2B;
            SMC_MSG_SetPPTLimit = 0x31;
            SMC_MSG_GetTctlOffset = 0x3A;
            SMC_MSG_SetTDCLimit = 0x43;
            SMC_MSG_SetEDCLimit = 0x44;
            SMC_MSG_SetFITLimit = 0x45;
            SMC_MSG_SetTjMax = 0x46;
            SMC_MSG_SetFITLimitScalar = 0x48;
        }

        public UInt32 MSR_PStateStat { get; protected set; }
        public UInt32 MSR_PStateDef0 { get; protected set; }

        public UInt32 MSR_PMGT_MISC { get; protected set; }
        public UInt32 MSR_CSTATE_CONFIG { get; protected set; }
        public UInt32 MSR_HWCR { get; protected set; }

        public UInt32 MSR_PERFBIAS1 { get; protected set; }
        public UInt32 MSR_PERFBIAS2 { get; protected set; }
        public UInt32 MSR_PERFBIAS3 { get; protected set; }
        public UInt32 MSR_PERFBIAS4 { get; protected set; }
        public UInt32 MSR_PERFBIAS5 { get; protected set; }

        public UInt32 THM_TCON_CUR_TMP { get; protected set; }
        public UInt32 THM_TCON_THERM_TRIP { get; protected set; }

        public UInt32 SMU_PCI_ADDR { get; protected set; }
        public UInt32 SMU_OFFSET_ADDR { get; protected set; }
        public UInt32 SMU_OFFSET_DATA { get; protected set; }

        public UInt32 SMU_ADDR_MSG { get; protected set; }
        public UInt32 SMU_ADDR_RSP { get; protected set; }
        public UInt32 SMU_ADDR_ARG0 { get; protected set; }
        public UInt32 SMU_ADDR_ARG1 { get; protected set; }

        public UInt32 SMC_MSG_TestMessage { get; protected set; }
        public UInt32 SMC_MSG_GetSmuVersion { get; protected set; }
        public UInt32 SMC_MSG_EnableSmuFeatures { get; protected set; }
        public UInt32 SMC_MSG_DisableSmuFeatures { get; protected set; }
        public UInt32 SMC_MSG_EnableOverclocking { get; protected set; }
        public UInt32 SMC_MSG_DisableOverclocking { get; protected set; }
        public UInt32 SMC_MSG_SetOverclockFreqAllCores { get; protected set; }
        public UInt32 SMC_MSG_SetOverclockFreqPerCore { get; protected set; }
        public UInt32 SMC_MSG_SetOverclockVid { get; protected set; }
        public UInt32 SMC_MSG_SetBoostLimitFrequency { get; protected set; }
        public UInt32 SMC_MSG_SetBoostLimitFrequencyAllCores { get; protected set; }
        public UInt32 SMC_MSG_SetPPTLimit { get; protected set; }
        public UInt32 SMC_MSG_GetTctlOffset { get; protected set; }
        public UInt32 SMC_MSG_SetTDCLimit { get; protected set; }
        public UInt32 SMC_MSG_SetEDCLimit { get; protected set; }
        public UInt32 SMC_MSG_SetFITLimit { get; protected set; }
        public UInt32 SMC_MSG_SetTjMax { get; protected set; }
        public UInt32 SMC_MSG_SetFITLimitScalar { get; protected set; }
    }

    // inherit the base class and define the new values in ctor

    public class SummitRidgeCPUSettings : BaseCPUSettings
    {
        public SummitRidgeCPUSettings(){}
    }

    public class Zen2CPUSettings : BaseCPUSettings
    {
        public Zen2CPUSettings()
        {
            SMU_ADDR_MSG = 0x03B10524;
            SMU_ADDR_RSP = 0x03B10570;
            SMU_ADDR_ARG0 = 0x03B10A40;
            SMU_ADDR_ARG1 = SMU_ADDR_ARG0 + 0x4;

            SMC_MSG_EnableSmuFeatures = 0x3;
            SMC_MSG_DisableSmuFeatures = 0x4; // Doesn't work with Matisse;
            SMC_MSG_SetTjMax = 0x23;
            SMC_MSG_EnableOverclocking = 0x5A;
            SMC_MSG_DisableOverclocking = 0x5B;
            SMC_MSG_SetOverclockFreqAllCores = 0x5C;
            SMC_MSG_SetOverclockFreqPerCore = 0x5D;
            SMC_MSG_SetOverclockVid = 0x61;
            SMC_MSG_SetBoostLimitFrequency = 0x29;
            SMC_MSG_SetBoostLimitFrequencyAllCores = 0x2B;
            SMC_MSG_GetOverclockCap = 0x2C;
            // SMC_MSG_MessageCount = 0x2D;
            SMC_MSG_SetFITLimitScalar = 0x6E;

            SMC_MSG_SetPPTLimit = 0x53;
            SMC_MSG_GetTctlOffset = 0x70;
            SMC_MSG_SetTDCLimit = 0x54;
            SMC_MSG_SetEDCLimit = 0x55;
        }

        public UInt32 SMC_MSG_GetOverclockCap { get; protected set; }
        // public UInt32 SMC_MSG_MessageCount { get; protected set; }
    }

    // Matisse, Renoir, CastlePeak and Rome share the same settings
    // CastlePeak (Threadripper 3000 series) shares the same CPUID as sthe server counterpart Rome
    public static class GetMaintainedCPUSettings
    {
        static Dictionary<CPUHandler.CPUType, BaseCPUSettings> settings = new Dictionary<CPUHandler.CPUType, BaseCPUSettings>()
        {
            { CPUHandler.CPUType.SummitRidge, new SummitRidgeCPUSettings() },
            { CPUHandler.CPUType.Matisse, new Zen2CPUSettings() },
            { CPUHandler.CPUType.Rome, new Zen2CPUSettings() },
            { CPUHandler.CPUType.Renoir, new Zen2CPUSettings() },
        };

        public static BaseCPUSettings GetByType(CPUHandler.CPUType type)
        {
            BaseCPUSettings output;
            if (!settings.TryGetValue(type, out output))
            {
                throw new NotImplementedException();
            }
            return output;
        }
    }
}
