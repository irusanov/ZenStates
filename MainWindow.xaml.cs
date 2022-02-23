using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ZenStates.Core;
using ZenStates.ViewModels;
using ZenStates.Windows;
using Forms = System.Windows.Forms;

namespace ZenStates
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        internal readonly AppSettings settings = (Application.Current as App)?.appSettings;
        internal readonly Forms.NotifyIcon _notifyIcon;
        internal readonly Cpu cpu;

        public MainWindow()
        {
            try
            {
                SplashWindow.Loading("CPU");
                cpu = new Cpu();

                if (cpu.info.family.Equals(Cpu.Family.UNSUPPORTED))
                {
                    HandleError("CPU is not supported.");
                    ExitApplication();
                }
                else if (cpu.info.codeName.Equals(Cpu.CodeName.Unsupported))
                {
                    HandleError("CPU model is not supported.\n" +
                                "Please run a debug report and send to the developer.");
                }

                IconSource = GetIcon("pack://application:,,,/ZenStates;component/Resources/ZenStates.ico", 16);
                _notifyIcon = GetTrayIcon();

                DataContext = new MainViewModel();
                InitializeComponent();
            }
            catch (Exception ex)
            {
                HandleError(ex.Message);
                ExitApplication();
            }
        }

        private Forms.NotifyIcon GetTrayIcon()
        {
            string AssemblyProduct = ((AssemblyProductAttribute)Attribute.GetCustomAttribute(
                Assembly.GetExecutingAssembly(),
                typeof(AssemblyProductAttribute), false)).Product;

            string AssemblyVersion = ((AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(
                Assembly.GetExecutingAssembly(),
                typeof(AssemblyFileVersionAttribute), false)).Version;
            Forms.NotifyIcon notifyIcon = new Forms.NotifyIcon
            {
                Icon = Properties.Resources.ZenStates_16x16
            };

            notifyIcon.MouseClick += NotifyIcon_MouseClick;
            notifyIcon.ContextMenuStrip = new Forms.ContextMenuStrip();
            notifyIcon.ContextMenuStrip.Items.Add($"{AssemblyProduct} {AssemblyVersion}", null, OnAppContextMenuItemClick);
            notifyIcon.ContextMenuStrip.Items.Add("-");
            notifyIcon.ContextMenuStrip.Items.Add("Exit", null, (object sender, EventArgs e) => ExitApplication());

            return notifyIcon;
        }

        private void OnAppContextMenuItemClick(object sender, EventArgs e)
        {
            WindowState = WindowState.Normal;
        }

        private void NotifyIcon_MouseClick(object sender, Forms.MouseEventArgs e)
        {
            if (e.Button == Forms.MouseButtons.Left)
            {
                WindowState = WindowState.Normal;
                Activate();
            }

            // else, default = show context menu
        }

        public void SetWindowTitle()
        {
            string AssemblyTitle = ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(
                    Assembly.GetExecutingAssembly(),
                    typeof(AssemblyTitleAttribute), false)).Title;

            string AssemblyVersion = ((AssemblyFileVersionAttribute)Attribute.GetCustomAttribute(
                Assembly.GetExecutingAssembly(),
                typeof(AssemblyFileVersionAttribute), false)).Version;

            Title = $"{AssemblyTitle} {AssemblyVersion.Substring(0, AssemblyVersion.LastIndexOf('.'))}";
#if BETA
            Title = $@"{AssemblyTitle} {AssemblyVersion} beta";

            AdonisUI.Controls.MessageBox.Show("This is a BETA version of the application. Some functions might be working incorrectly.\n\n" +
                "Please report if something is not working as expected.", "Beta version", AdonisUI.Controls.MessageBoxButton.OK);
#else
#if DEBUG
            Title = $@"{AssemblyTitle} {AssemblyVersion} (debug)";
#endif
#endif
        }

        private void ExitApplication()
        {
            _notifyIcon?.Dispose();
            cpu?.Dispose();
            Application.Current?.Shutdown();
        }

        private ImageSource GetIcon(string iconSource, double width)
        {
            BitmapDecoder decoder = BitmapDecoder.Create(new Uri(iconSource),
                BitmapCreateOptions.DelayCreation,
                BitmapCacheOption.OnDemand);

            BitmapFrame result = decoder.Frames.SingleOrDefault(f => f.Width == width);
            if (result == default(BitmapFrame)) result = decoder.Frames.OrderBy(f => f.Width).First();

            return result;
        }

        public void HandleError(string message, string title = "Error")
        {
            AdonisUI.Controls.MessageBox.Show(
                message,
                title,
                AdonisUI.Controls.MessageBoxButton.OK,
                AdonisUI.Controls.MessageBoxImage.Error
            );
        }

        private void Restart()
        {
            settings.Save();
            Process.Start(Application.ResourceAssembly.Location);
            ExitApplication();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (settings.SaveWindowPosition)
            {
                settings.WindowLeft = Left;
                settings.WindowTop = Top;
            }

            settings.Save();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            SplashWindow.Stop();
            Application.Current.MainWindow = this;
            SetWindowTitle();

            if (settings.SaveWindowPosition)
            {
                WindowStartupLocation = WindowStartupLocation.Manual;
                Left = settings.WindowLeft;
                Top = settings.WindowTop;
            }

            if (settings.StartMinimized)
            {
                WindowState = WindowState.Minimized;
                ChangeState();
            }
        }

        private void AdonisWindow_Closed(object sender, EventArgs e) => ExitApplication();

        private void ChangeState()
        {
            if (WindowState == WindowState.Minimized)
            {
                if (settings.MinimizeToTray)
                {
                    _notifyIcon.Visible = true;
                    ShowInTaskbar = false;
                }
            }
            else
            {
                _notifyIcon.Visible = false;
                ShowInTaskbar = true;
            }
        }

        private void AdonisWindow_StateChanged(object sender, EventArgs e) => ChangeState();
    }
}
