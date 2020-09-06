# ZenStates


## Prerequisites
* [WinRing driver](https://1drv.ms/u/s!Atmpv-6qHr_6r-YV1-C0ht0nCQiHKA)
* .Net Framework v4.0
* Supported OS: Windows XP/Vista/7/8/10

## How To
The application consists of two executables - the main GUI `ZenStates.exe` and a service `ZenStatesSrv.exe`.
An additional `uninstall.bat` is provided for an easy uninstall of the previous service version.

1. Extract the provided zip and place in a desired location on the disk
2. Run `uninstall.bat` if not running for a first time
3. Run `ZenStates.exe`


## Technical Information

### What have changed with Zen2
1. SMU mailbox message address changed from `0x03B10528` to `0x03B10530`
2. SMU mailbox response address changed from `0x03B10564` to `0x03B1057C`
3. ARG base address changed from `0x03B10998` to `0x03B109C4`

### SMU Commands

There's no public document describing the available commands, however I was able reverse-engineer some of them with the help of the publicly released "worktool" app, ReadWriteEverything, [CrystalCPUID](https://crystalmark.info/en/download/) and the info provided by [FlyGoat](https://github.com/FlyGoat/ryzen_nb_smu).

The research is based on SMU version 64.40.00.

| ID | Name | Description |
| :------| :------ | :------ |
| 0x1 | TestMessage | A test command to check if Mailbox responds. Returns 0x1 if successful. |
| 0x2 | GetSmuVersion | Gets the SMU Firmware version. |
| 0x3 | EnableSmuFeatures | |
| 0x4 | DisableSmuFeatures | The command is rejected (`0xFD`). Seems to be currently blocked by AMD |
| 0x23 | SetTjMax | Set TjMax temperature, probably in degrees C° |
| 0x24 | EnableOverclocking | Forces manual overclock mode. All limits, except overtemperature protection, are lifted. OC means FID != default. |
| 0x25 | DisableOverclocking | Reverts back to non-OC mode. |
| 0x26 | SetOverclockFreqAllCores | Sets all core OC frequency, depends on `0x24` |
| 0x27 | SetOverclockFreqPerCore | Set overclock frequency per core. Probably requires 2 arguments. Depends on 0x24. |
| 0x28 | SetOverclockVid | Alters the VID (in HEX). Depends on `0x24`. |
| 0x29 | etBoostLimitFrequency | Sets single-thread max boost frequency. |
| 0x2B | SetBoostLimitFrequencyAllCores | Sets maximum boost frequency for multi-thread applications. Still depends on PBO limits. |
| 0x2C | GetOverclockCap | Gets OC capability, which is unclear to me how it could be used and what does it mean exactly. |
| 0x2F | SetFITLimitScalar | Sets Scalar from 1 to 10 |
| 0x30 | MessageCount | Get current messages count in the queue |

### Projects used  
[RTCSharp (github)](https://github.com/tomrus88/RTCSharp)  
[ryzen_smu (gitlab)](https://gitlab.com/leogx9r/ryzen_smu/)  
[ryzen_nb_smu (github)](https://github.com/flygoat/ryzen_nb_smu)  
[zenpower (github)](https://github.com/ocerman/zenpower)  
[Linux kernel (github)](https://github.com/torvalds/linux)  
[AMD's public documentation](https://www.amd.com/en/support/tech-docs)  
