/*
 * Created by SharpDevelop.
 * User: Jon_Sandstrom
 * Date: 2016-03-30
 * Time: 12:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
//#define _PHYSICAL_MEMORY_SUPPORT

using System;
using System.Runtime.InteropServices;
using OpenLibSys;
using System.Windows.Forms;
using Microsoft.Win32;

namespace AsusTC
{
	/// <summary>
	/// Description of CPUHandler.
	/// </summary>
	public class CPUHandler
	{
		const UInt16 MSR_OVERCLOCKING_MAILBOX = 0x150;
		const UInt16 MSR_TURBO_RATIO_LIMIT = 0x1AD; // Core 1-8
		const UInt16 MSR_TURBO_RATIO_LIMIT1 = 0x1AE; // Core 9-16
		const UInt16 MSR_TURBO_RATIO_LIMIT2 = 0x1AF; // HWE core 17-18 + semaphore bit, BWE Core 17-24
		const UInt16 MSR_TURBO_RATIO_LIMIT3 = 0x1AC; // BWE semaphore bit
		const UInt16 MSR_TEMPERATURE_TARGET = 0x1A2;
		const UInt16 MSR_IA32_THERM_STATUS = 0x19C;
		const UInt16 MSR_IA32_PACKAGE_THERM_STATUS = 0x1B1;
		const UInt16 MSR_CORE_THREAD_COUNT = 0x35;
		
		public const int maxRatio = 50;
		public const int minRatio = 12;
		public const int maxTurboVoltage = 1529;
		public const int minTurboVoltage = 829;
		public const int maxOffsetVoltage = 200;
		public const int minOffsetVoltage = -200;
		public const int minTemp = 0;
		
		private Ols ols;
		
		public int Tjmax;
		public int Cores;
		public int Threads;
		
		public string cpuModel;

		private UInt64 defaultOCMB;
		private UInt32 defaultTRL_EAX;
		private UInt32 defaultTRL_EDX;
		private UInt32 defaultTRL1_EAX;
		private UInt32 defaultTRL1_EDX;
		private UInt32 defaultTRL2_EAX;
		private UInt32 defaultTRL2_EDX;
		
		public int upperTemperature;
		public int lowerTemperature;
		
		public int defaultMaxRatio;
		public int throttleRatio;
		
		private int defaultTurboVoltage;
		public int throttleTurboVoltage;
		
		private int defaultOffsetVoltage;
		public int throttleOffsetVoltage;
		
		public bool overrideMode = false;
		public bool thermalThrottleEnabled = false;
		public bool settingsReset = false;
		public bool startupEnabled = false;
		public bool systemStartupEnabled = false;
		public bool throttleState = false;
		
		public bool isBWE = false;
		public bool unsupportedCpu = false;
		
		public CPUHandler()
		{
			ols = new Ols();
			
            // Check support library sutatus
            switch(ols.GetStatus())
            {
                case (uint)Ols.Status.NO_ERROR:
                    break;
                case (uint)Ols.Status.DLL_NOT_FOUND:
                    throw new System.ApplicationException("WinRing DLL_NOT_FOUND");
                    break;
                case (uint)Ols.Status.DLL_INCORRECT_VERSION:
                    throw new System.ApplicationException("WinRing DLL_INCORRECT_VERSION");
                    break;
                case (uint)Ols.Status.DLL_INITIALIZE_ERROR:
                    throw new System.ApplicationException("WinRing DLL_INITIALIZE_ERROR");
                    break;
            }
            
			// Check WinRing0 status
            switch (ols.GetDllStatus())
            {
                case (uint)Ols.OlsDllStatus.OLS_DLL_NO_ERROR:
                    break;
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED:
                    throw new System.ApplicationException("WinRing OLS_DRIVER_NOT_LOADED");
                    break;
                case (uint)Ols.OlsDllStatus.OLS_DLL_UNSUPPORTED_PLATFORM:
                    throw new System.ApplicationException("WinRing OLS_UNSUPPORTED_PLATFORM");
                    break;
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_FOUND:
                    throw new System.ApplicationException("WinRing OLS_DLL_DRIVER_NOT_FOUND");
                    break;
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_UNLOADED:
                    throw new System.ApplicationException("WinRing OLS_DLL_DRIVER_UNLOADED");
                    break;
                case (uint)Ols.OlsDllStatus.OLS_DLL_DRIVER_NOT_LOADED_ON_NETWORK:
                    throw new System.ApplicationException("WinRing DRIVER_NOT_LOADED_ON_NETWORK");
                    break;
                case (uint)Ols.OlsDllStatus.OLS_DLL_UNKNOWN_ERROR:
                    throw new System.ApplicationException("WinRing OLS_DLL_UNKNOWN_ERROR");
                    break;
            }
            
            // CPU Check. Compare family, model, ext family, ext model. Ignore stepping/revision
			switch(getCpuInfo()&0xFFFFFFF0) { 
				case 0x000306F0: //Haswell-E
					this.isBWE = false;
					break;
				case 0x000406F0: //Broadwell-E
					this.isBWE = true;
					break;
				default:
					this.unsupportedCpu = true;
					break;
			}
            
            if(!this.unsupportedCpu) {
            	// Load saved settings
	            
	            UInt64 settingsOCMB = AsusTC.Default.OCMB;
	            UInt32 settingsTRL_EAX = AsusTC.Default.TRL_EAX;
	            UInt32 settingsTRL_EDX = AsusTC.Default.TRL_EDX;
	            UInt32 settingsTRL1_EAX = AsusTC.Default.TRL1_EAX;
	            UInt32 settingsTRL1_EDX = AsusTC.Default.TRL1_EDX;
	            UInt32 settingsTRL2_EAX = AsusTC.Default.TRL2_EAX;
	            UInt32 settingsTRL2_EDX = AsusTC.Default.TRL2_EDX;
	            this.startupEnabled = AsusTC.Default.startupEnabled;
	            this.systemStartupEnabled = AsusTC.Default.systemStartupEnabled;
	            this.overrideMode = AsusTC.Default.overrideMode;
	            this.throttleRatio = AsusTC.Default.throttleRatio;
	            this.throttleTurboVoltage = AsusTC.Default.throttleTurboVoltage;
	            this.throttleOffsetVoltage = AsusTC.Default.throttleOffsetVoltage;
	            this.upperTemperature = AsusTC.Default.upperTemperature;
	            this.lowerTemperature = AsusTC.Default.lowerTemperature;
	            
	            // Get number of cores
	            getCores();
	            
	            // Get and store Tjmax
	            getTjmax();
	            
	            // Get current OCMB
	            defaultOCMB = ReadOCMB(0x10,0x00);
	            if(defaultOCMB == 0xFFFFFFFFFFFFFFFF) throw new System.ApplicationException("Unsupported platform (1)");
	            //else if(defaultOCMB == 0) throw new System.ApplicationException("System not overclocked");
	            else if(defaultOCMB != settingsOCMB) settingsReset = true; settingsOCMB = defaultOCMB;
	            
	            // Get current turbo ratios
	            uint edx = 0, eax = 0;
	            if(ols.Rdmsr(MSR_TURBO_RATIO_LIMIT,ref eax, ref edx) != 1) throw new System.ApplicationException("Unsupported platform (2)");
	            defaultTRL_EAX = eax;
	            defaultTRL_EDX = edx;
	            if(defaultTRL_EAX != settingsTRL_EAX) settingsReset = true; settingsTRL_EAX = defaultTRL_EAX;
	            if(defaultTRL_EDX != settingsTRL_EDX) settingsReset = true; settingsTRL_EDX = defaultTRL_EDX;
	            
	            if(ols.Rdmsr(MSR_TURBO_RATIO_LIMIT1,ref eax, ref edx) != 1) {
	            	// TRL1 not supported
	            	if(Cores > 8) throw new System.ApplicationException("Error reading MSR");
	            	defaultTRL1_EAX = 0;
	            	defaultTRL1_EDX = 0;
	            	defaultTRL2_EAX = 0;
	            	defaultTRL2_EDX = 0;
	            } else {
	            	defaultTRL1_EAX = eax;
	            	defaultTRL1_EDX = edx;
	            	
	            	if(ols.Rdmsr(MSR_TURBO_RATIO_LIMIT2,ref eax, ref edx) != 1) { 
	            		// TRL2 not supported
	            		if(Cores > 16) throw new System.ApplicationException("Error reading MSR");
	            		defaultTRL2_EAX = 0;
	            		defaultTRL2_EDX = 0;
	            	} else {
	            		defaultTRL2_EAX = eax;
	            		defaultTRL2_EDX = edx;
	            	}
	            }
	            
	            if(ols.Rdmsr(MSR_TURBO_RATIO_LIMIT3, ref eax, ref edx) == 1) isBWE = true;
	            
	            if(defaultTRL1_EAX != settingsTRL1_EAX) settingsReset = true; settingsTRL1_EAX = defaultTRL1_EAX;
	            if(defaultTRL1_EDX != settingsTRL1_EDX) settingsReset = true; settingsTRL1_EDX = defaultTRL1_EDX;
	            if(defaultTRL2_EAX != settingsTRL2_EAX) settingsReset = true; settingsTRL2_EAX = defaultTRL2_EAX;
	            if(defaultTRL2_EDX != settingsTRL2_EDX) settingsReset = true; settingsTRL2_EDX = defaultTRL2_EDX;
	            
	            // Max ratio values & clipping
	            defaultMaxRatio = (byte)(defaultOCMB&0xFF);
	            if(defaultMaxRatio > maxRatio) defaultMaxRatio = maxRatio;
	            else if(defaultMaxRatio < minRatio) defaultMaxRatio = maxRatio; //throw new System.ApplicationException("Error reading ratio");
	            
	            #region X-core ratio limit
	            
	            byte[] ratio = new byte[Cores];
	            
	            for(byte i = 0;i<Cores;i++) {
	            	if(i < 4) ratio[i] = (byte)((defaultTRL_EAX>>((i)*8))&0xFF);
	            	else if(i < 8) ratio[i] = (byte)((defaultTRL_EDX>>((i-4)*8))&0xFF);
	            	else if(i < 12) ratio[i] = (byte)((defaultTRL1_EAX>>((i-8)*8))&0xFF);
	            	else if(i < 16) ratio[i] = (byte)((defaultTRL1_EDX>>((i-12)*8))&0xFF);
	            	else if(i < 20) ratio[i] = (byte)((defaultTRL2_EAX>>((i-16)*8))&0xFF);
	            	else if(i < 23) ratio[i] = (byte)((defaultTRL2_EDX>>((i-20)*8))&0xFF);
	            }
	            
	            if(ratio[Cores-1] < defaultMaxRatio) defaultMaxRatio = ratio[Cores-1];
	            
	            #endregion
	            
	            // Turbo Voltage values & clipping
	            defaultTurboVoltage = (int)(defaultOCMB>>8)&0xFFF;
	            if(defaultTurboVoltage > maxTurboVoltage) defaultTurboVoltage = maxTurboVoltage;
	            else if(defaultTurboVoltage < minTurboVoltage) defaultTurboVoltage = minTurboVoltage;//throw new System.ApplicationException("Error reading voltage");
	            
	            // Offset voltage values, clipping & negative value handling
	            defaultOffsetVoltage = (int)((defaultOCMB>>21)&0x7FF);
	            
	            if(defaultOffsetVoltage > 1023) defaultOffsetVoltage = defaultOffsetVoltage-2048;
	            
	            if(defaultOffsetVoltage > 200) defaultOffsetVoltage = 200;
	            else if(defaultOffsetVoltage < -200) defaultOffsetVoltage = -200;
	            
	            // If reset
	            if(settingsReset) {
	            	// set all values to defaults
	            	throttleRatio = defaultMaxRatio;
	            	throttleTurboVoltage = defaultTurboVoltage;
	            	throttleOffsetVoltage = defaultOffsetVoltage;
	            	upperTemperature = Tjmax-20;
	            	lowerTemperature = Tjmax-50;
	            	if((defaultOCMB&(1<<20)) != 0) overrideMode = true;
	            	
	            	// Store current defaults
		            AsusTC.Default.OCMB = defaultOCMB;
		            AsusTC.Default.TRL_EAX = defaultTRL_EAX;
		            AsusTC.Default.TRL_EDX = defaultTRL_EDX;
		            AsusTC.Default.TRL1_EAX = defaultTRL1_EAX;
		            AsusTC.Default.TRL1_EDX = defaultTRL1_EDX;
		            AsusTC.Default.TRL2_EAX = defaultTRL2_EAX;
		            AsusTC.Default.TRL2_EDX = defaultTRL2_EDX;
		            
		            AsusTC.Default.throttleRatio = throttleRatio;
		            AsusTC.Default.throttleTurboVoltage = throttleTurboVoltage;
		            AsusTC.Default.throttleOffsetVoltage = throttleOffsetVoltage;
		            AsusTC.Default.upperTemperature = upperTemperature;
		            AsusTC.Default.lowerTemperature = lowerTemperature;
		            
		            // Disable auto-enable
		            AsusTC.Default.startupEnabled = false;
		            
		            AsusTC.Default.Save();
	            } else if(this.startupEnabled) {
	            	this.thermalThrottleEnabled = true;
	            	setSystemStartup(this.systemStartupEnabled);
	            }
            }
            
		}
		
		public void setSystemStartup(bool enable) {
			RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
			 if (rkApp.GetValue(Application.ProductName) == null) {
                // The value doesn't exist, the application is not set to run at startup
                if(enable) {
                	rkApp.SetValue(Application.ProductName, Application.ExecutablePath);
                	this.systemStartupEnabled = true;
                } else {
                	this.systemStartupEnabled = false;
                }
			} else if(!enable) {
				// The value exists, delete it
				rkApp.DeleteValue(Application.ProductName, false);
				this.systemStartupEnabled = false;
			}
		}
		
		public UInt64 ReadOCMB(byte cmd, byte domain) {
			uint eax = 0, edx = 0;
			if(ols.Wrmsr(MSR_OVERCLOCKING_MAILBOX, 0, (uint)(0x80000000|domain<<8|cmd)) == 1) {
				if(ols.Rdmsr(MSR_OVERCLOCKING_MAILBOX,ref eax,ref edx) == 1) {
					return edx<<32|eax;
				}
			}
			// else
			return 0xFFFFFFFFFFFFFFFF;
		}
		
		public bool WriteOCMB(byte cmd, byte domain, uint data) {
			uint eax = 0, edx = 0;
			if(ols.Wrmsr(MSR_OVERCLOCKING_MAILBOX, data, (uint)(0x80000000|domain<<8|cmd)) == 1) {
				return true;
			} else {
				return false;
			}
		}
		
		public int getTjmax() {
			uint eax = 0, edx = 0;
			if(ols.Rdmsr(MSR_TEMPERATURE_TARGET,ref eax, ref edx) == 1) {
				this.Tjmax = (int)(eax>>16&0xFF);
			}
			return Tjmax;
		}
		
		public int getPackageTemp() {
			uint eax = 0, edx = 0;
			if(ols.Rdmsr(MSR_IA32_PACKAGE_THERM_STATUS,ref eax, ref edx) == 1) {
				int temp = Tjmax-(int)(eax>>16&0x7F);
				
				// Handle temperature rules
				
				if(thermalThrottleEnabled) assertState(temp);
				
				return temp;
			} else {
				return 0;
			}
		}
		
		public int getCores() {
			uint eax = 0, edx = 0;
			if(ols.Rdmsr(MSR_CORE_THREAD_COUNT,ref eax, ref edx) == 1) {
				this.Cores = (int)(eax>>16&0xFFFF);
				this.Threads = (int)(eax&0xFFFF);
			}
			return Cores;
		}
		
		public uint getCpuInfo() {
			uint eax = 0, ebx = 0, ecx = 0, edx = 0;
			ols.CpuidPx(0x00000000, ref eax, ref ebx, ref ecx, ref edx, (UIntPtr)1);
			if(ols.CpuidPx(0x00000001, ref eax, ref ebx, ref ecx, ref edx, (UIntPtr)1) == 1) {
				return eax;
			}
			return 0;
		}
		
		public string getCpuString() {
			uint eax = 0, ebx = 0, ecx = 0, edx = 0;
			ols.CpuidPx(0x80000000, ref eax, ref ebx, ref ecx, ref edx, (UIntPtr)1);
			
			
			string[] temp = new string[12];
			
			for(int index = 0; index<3;index++) {
				if(ols.CpuidPx((uint)(0x80000002+index), ref eax, ref ebx, ref ecx, ref edx, (UIntPtr)1) == 1) {
					for(int i = 0; i<4; i++) {
						temp[index*4+0] += Convert.ToChar(eax>>(i*8)&0xFF);
						temp[index*4+1] += Convert.ToChar(ebx>>(i*8)&0xFF);
						temp[index*4+2] += Convert.ToChar(ecx>>(i*8)&0xFF);
						temp[index*4+3] += Convert.ToChar(edx>>(i*8)&0xFF);
					}
				}
			}
			return temp[0]+temp[1]+temp[2]+temp[3]+temp[4]+temp[5]+temp[6]+temp[7]+temp[8]+temp[9]+temp[10]+temp[11];
		}
		
		public bool setCpuSettings(int ratio, int turboVoltage, int offsetVoltage, bool overrideMode) {
			
			UInt32 mb;
			if(overrideMode) {
				mb = (uint)((offsetVoltage&0x7FF)<<21|(1<<20)|(turboVoltage&0xFFF)<<8|ratio&0xFF);
			} else {
				mb = (uint)((offsetVoltage&0x7FF)<<21|(turboVoltage&0xFFF)<<8|ratio&0xFF);
			}
			uint ratios = (uint)((ratio&0xFF)<<24|(ratio&0xFF)<<16|(ratio&0xFF)<<8|(ratio&0xFF));
			
			//MessageBox.Show(mb.ToString("X8"));
			//throttleState = true;
			//return true;
			
			// Ratio first
			if(isBWE) {
				// Broadwell-E, set TRL+TRL1+TRL2 + semaphore bit in TRL3
				if(ols.Wrmsr(MSR_TURBO_RATIO_LIMIT,ratios,ratios) == 1) {
					if(ols.Wrmsr(MSR_TURBO_RATIO_LIMIT1,ratios,ratios) == 1) {
						if(ols.Wrmsr(MSR_TURBO_RATIO_LIMIT2, ratios, ratios) == 1) {
							// Set BWE semaphore bit
							if(ols.Wrmsr(MSR_TURBO_RATIO_LIMIT3, 0, 0x80000000) == 1) {
								if(WriteOCMB(0x11,0,mb)) {
									throttleState = true;
									return true;
								}
							}
						}
					}
				}
			}
			else {
				// Haswell-E, set TRL+TRL1+ core17+18 & semaphore bit in TRL2
				if(ols.Wrmsr(MSR_TURBO_RATIO_LIMIT,ratios,ratios) == 1) {
					if(ols.Wrmsr(MSR_TURBO_RATIO_LIMIT1,ratios,ratios) == 1) {
						// TRL2 + semaphore bit
						if(ols.Wrmsr(MSR_TURBO_RATIO_LIMIT2,(defaultTRL2_EAX&0xFFFF0000)|(ratios&0x0000FFFF),defaultTRL2_EDX|0x80000000) == 1) {
							if(WriteOCMB(0x11,0,mb)) {
								//MessageBox.Show(mb.ToString("X8"));
								throttleState = true;
								return true;
							}
						}
					}
				}
			}
			
			
			return false;
		}
		
		public bool setDefault() {
			
			//MessageBox.Show(defaultOCMB.ToString("X8"));
			//throttleState = false;
			//return true;
			
			// Going up, voltage first
			if(WriteOCMB(0x11,0,(uint)defaultOCMB&0xFFFFFFFF)) {
				if(isBWE) {
					// Broadwell-E, set TRL+TRL1+TRL2 + semaphore bit in TRL3
					if(ols.Wrmsr(MSR_TURBO_RATIO_LIMIT,defaultTRL_EAX,defaultTRL_EDX) == 1) {
						if(ols.Wrmsr(MSR_TURBO_RATIO_LIMIT1,defaultTRL1_EAX,defaultTRL1_EDX) == 1) {
							if(ols.Wrmsr(MSR_TURBO_RATIO_LIMIT2, defaultTRL2_EAX, defaultTRL2_EDX) == 1) {
								// Set BWE semaphore bit
								if(ols.Wrmsr(MSR_TURBO_RATIO_LIMIT3, 0, 0x80000000) == 1) {
									throttleState = false;
									return true;
								}
							}
						}
					}
				}
				else {
					// Haswell-E, set TRL+TRL1+ core17+18 & semaphore bit in TRL2
					if(ols.Wrmsr(MSR_TURBO_RATIO_LIMIT,defaultTRL_EAX,defaultTRL_EDX) == 1) {
						if(ols.Wrmsr(MSR_TURBO_RATIO_LIMIT1,defaultTRL1_EAX,defaultTRL1_EDX) == 1) {
							// TRL2 + semaphore bit
							if(ols.Wrmsr(MSR_TURBO_RATIO_LIMIT2,defaultTRL2_EAX,defaultTRL2_EDX|0x80000000) == 1) {
								throttleState = false;
								return true;
							}
						}
					}
				}
			}
			return false;
		}
		
		public void assertState(int temp) {
			if(temp >= upperTemperature && !throttleState) {
				// Throttle down
				if(setCpuSettings(throttleRatio, throttleTurboVoltage, throttleOffsetVoltage, overrideMode)) throttleState = true;
			} else if(temp <= lowerTemperature && throttleState) {
				//if(setCpuSettings(defaultMaxRatio, defaultTurboVoltage, defaultOffsetVoltage, true)) throttleState = false;
				if(setDefault()) throttleState = false;
			}
		}
		
		public void unload() {
			ols.Dispose();
		}
		
		/*public unsafe bool isAsus() {
			return true;
			
			UIntPtr SMBIOS_ENTRY_ADDR = (UIntPtr)0x000F0570;
			UIntPtr SMBIOS_ST_POINTER = (UIntPtr)0x000F0588;
			UInt32 SMBIOS_ST1_NAME_OFFSET = (UInt32)0x5D;
			
			byte* bArr = (byte*)Marshal.AllocHGlobal(4);
			
			if(ols.ReadPhysicalMemory(SMBIOS_ST_POINTER,bArr,4,1) != 4) return false;
			
			UInt32 smbios_addr = (UInt32)(bArr[3]<<24|bArr[2]<<16|bArr[1]<<8|bArr[0]);
			
			
			if(ols.ReadPhysicalMemory((UIntPtr)(smbios_addr+SMBIOS_ST1_NAME_OFFSET),bArr,4,1) != 4) return false;
			
			if(bArr[0] == 0x41 && bArr[1] == 0x53 && bArr[2] == 0x55 && bArr[3] == 0x53) {
				return true;
			} else {
				return false;
			}
			
		}*/
	}
}
