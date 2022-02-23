using System;
using System.Threading;
using System.Windows;
using ZenStates.Core;
using ZenStates.Windows;

namespace ZenStates
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal const string mutexName = "Local\\ZenStates";
        internal static Mutex instanceMutex;
        internal bool createdNew;
        public readonly AppSettings appSettings = new AppSettings().Load();

        protected override void OnStartup(StartupEventArgs e)
        {
            instanceMutex = new Mutex(true, mutexName, out createdNew);

            if (!createdNew)
            {
                // App is already running! Exit the application and show the other window.
                InteropMethods.PostMessage((IntPtr)InteropMethods.HWND_BROADCAST, InteropMethods.WM_SHOWME,
                    IntPtr.Zero, IntPtr.Zero);
                Current.Shutdown();
                Environment.Exit(0);
            }

            GC.KeepAlive(instanceMutex);
            SplashWindow.Start();
            base.OnStartup(e);
        }
    }
}
