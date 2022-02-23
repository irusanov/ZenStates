using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ZenStates.State.Navigators;
using ZenStates.ViewModels;

namespace ZenStates.Commands
{
    public class SettingsResetCommand : CommandBase
    {
        private readonly AppSettings _appSetting;
        public override void Execute(object parameter)
        {
            AdonisUI.Controls.MessageBoxResult result = AdonisUI.Controls.MessageBox.Show(
                "Do you want to reset settings to default values?",
                "Confirm",
                AdonisUI.Controls.MessageBoxButton.YesNo);

            if (result == AdonisUI.Controls.MessageBoxResult.No)
                return;

            try
            {
                _appSetting?.Reset();
            }
            catch (Exception ex)
            {
                AdonisUI.Controls.MessageBox.Show(
                    $"Could not reset settings: {ex.Message}",
                    "Error",
                    AdonisUI.Controls.MessageBoxButton.OK,
                    AdonisUI.Controls.MessageBoxImage.Error);
            }
        }

        public SettingsResetCommand(AppSettings settings)
        {
            _appSetting = settings;
        }
    }
}
