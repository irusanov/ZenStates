# ASUS ZenStates


## Prerequisites
* [WinRing driver](https://1drv.ms/u/s!Atmpv-6qHr_6r-YV1-C0ht0nCQiHKA)
* .Net Framework v4.0
* Supported OS: Windows XP/Vista/7/8/10

# SMU Commands

There's no public document describing the available commands, however I was able to guess some of them with trial and error and the help of the publicly released "worktool" app and the info provided by FlyGoat's repo: (https://github.com/FlyGoat/ryzen_nb_smu)

| ID | Name | Note |
| :------| :------ | :------ |
| 0x1 | TestMessage |  |
| 0x2 | GetSmuVersion |  |
| 0x24 | EnableOverclocking | Forces base clock and manual overclock mode. |
| 0x25 | DisableOverclocking |  |
| 0x26 | SetOverclockFreqAllCore | Sets all core frequency, EnableOverclocking first. |
| 0x27 | SetOverclockFreqPerCore | Always sets core #0, probably needs additional parameters. EnableOverclocking first.  |
| 0x28 | SetOverclockVid | Alters the VID (in HEX). EnableOverclocking first. |
| 0x29 | SetBoostLimitFreqAllCores | Probably sets fmax |
| 0x2B | ? | Sets maximum boost frequency |
| 0x2C | GetOverclockCap | ? |
| 0x2F | ? | With multi manually set to 40x, sets the multi to 39.50x |
