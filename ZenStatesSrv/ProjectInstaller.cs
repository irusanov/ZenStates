using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace ZenStatesSrv
{
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller serviceProcessInstaller;
        private ServiceInstaller serviceInstaller;

        public ProjectInstaller()
        {
            serviceProcessInstaller = new ServiceProcessInstaller();
            serviceInstaller = new ServiceInstaller();
            // Here you can set properties on serviceProcessInstaller or register event handlers
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;

            serviceInstaller.ServiceName = ZenStatesSrv.MyServiceName;
            this.Installers.AddRange(new Installer[] { serviceProcessInstaller, serviceInstaller });
        }
    }
}
