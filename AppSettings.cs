using AdonisUI;
using System;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace ZenStates
{
    [Serializable]
    public sealed class AppSettings
    {
        private const int VERSION_MAJOR = 1;
        private const int VERSION_MINOR = 0;

        private const string filename = "settings.xml";

        public AppSettings Create()
        {
            SettingsVersion = $"{VERSION_MAJOR}.{VERSION_MINOR}";
            DarkMode = true;
            ShowDisclaimer = true;
            QCodeTemperatureDisplay = false;
            MinimizeToTray = false;
            StartMinimized = false;
            StartWithWindows = false;
            SaveWindowPosition = false;
            WindowLeft = 0;
            WindowTop = 0;

            Save();

            return this;
        }

        public void Save()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(filename))
                {
                    XmlSerializer xmls = new XmlSerializer(typeof(AppSettings));
                    xmls.Serialize(sw, this);
                }
            }
            catch (Exception ex)
            {
                AdonisUI.Controls.MessageBox.Show(
                    $"Could not save settings to file!\n{ex.Message}",
                    "Error",
                    AdonisUI.Controls.MessageBoxButton.OK,
                    AdonisUI.Controls.MessageBoxImage.Error);
            }
        }

        public AppSettings Reset() => Create();

        public AppSettings Load()
        {
            if (File.Exists(filename))
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    try
                    {
                        XmlSerializer xmls = new XmlSerializer(typeof(AppSettings));
                        return xmls.Deserialize(sr) as AppSettings;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        sr.Close();
                        MessageBox.Show(
                            "Invalid settings file!\nSettings will be reset to defaults.",
                            "Error",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return Create();
                    }
                }
            }
            else
            {
                return Create();
            }
        }

        public void ChangeTheme()
        {
            Uri DarkColorScheme = new Uri("pack://application:,,,/ZenStates;component/Themes/Dark.xaml",
                UriKind.Absolute);
            Uri LightColorScheme = new Uri("pack://application:,,,/ZenStates;component/Themes/Light.xaml",
                UriKind.Absolute);

            if (DarkMode)
                ResourceLocator.SetColorScheme(Application.Current.Resources, DarkColorScheme);
            else
                ResourceLocator.SetColorScheme(Application.Current.Resources, LightColorScheme);

            //DarkMode = !DarkMode;
        }

        public string SettingsVersion { get; set; } = $"{VERSION_MAJOR}.{VERSION_MINOR}";
        public bool DarkMode { get; set; } = true;
        public bool ShowDisclaimer { get; set; } = true;
        public bool QCodeTemperatureDisplay { get; set; }
        public bool MinimizeToTray { get; set; }
        public bool StartMinimized { get; set; }
        public bool StartWithWindows { get; set; }
        public bool SaveWindowPosition { get; set; }
        public double WindowLeft { get; set; }
        public double WindowTop { get; set; }
    }
}
