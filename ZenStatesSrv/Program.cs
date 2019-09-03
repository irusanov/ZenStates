using System.ServiceProcess;

namespace ZenStatesSrv
{
    static class Program
    {

        /// <summary>
        /// This method starts the service.
        /// </summary>
        ///
        static void Main()
        {
            // To run more than one service you have to add them here
            ServiceBase.Run(new ServiceBase[] { new ZenStatesSrv() });
        }

    }
}
