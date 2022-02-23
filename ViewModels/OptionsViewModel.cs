using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ZenStates.Commands;

namespace ZenStates.ViewModels
{
    public class OptionsViewModel : ViewModelBase
    {
        public ICommand ResetSettings { get; }
        public bool DarkMode { get; set; }
        public bool QCodeTemperatureDisplay { get; set; }
        public bool MinimizeToTray { get; set; }
        public bool StartMinimized { get; set; }
        public bool StartWithWindows { get; set; }
        public bool SaveWindowPosition { get; set; }

        public OptionsViewModel(AppSettings appSettings)
        {
            DarkMode = appSettings.DarkMode;
            QCodeTemperatureDisplay = appSettings.QCodeTemperatureDisplay;
            MinimizeToTray = appSettings.MinimizeToTray;
            StartMinimized = appSettings.StartMinimized;
            StartWithWindows = appSettings.StartWithWindows;
            SaveWindowPosition = appSettings.SaveWindowPosition;

            ResetSettings = new SettingsResetCommand(appSettings);
        }
    }
}
