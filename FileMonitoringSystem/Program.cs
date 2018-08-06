using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;
using FileMonitoringSystem.Configuration.Configurator;
using FileMonitoringSystem.Monitoring.Monitor;
using FileMonitoringSystem.Monitoring;
using FileMonitoringSystem.Client;

namespace FileMonitoringSystem
{
    static class Program
    {
        private static ILog _log;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            /*log4net.GlobalContext.Properties["tab"] = "\t";
            log4net.Config.XmlConfigurator.Configure();
            _log = LogManager.GetLogger(typeof(Program).Name);

            var args = Environment.GetCommandLineArgs();
            if(args.Length > 1 && args[1] == "console")
            {
                _log.Info("Start as console");

                FilesMonitor myProg = new FilesMonitor();
                myProg.Work();
            }
            else
            {
                _log.Info("Start as service");
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new FileMonitoringService()
                };
                ServiceBase.Run(ServicesToRun);
            }*/
            ClientManager Client = new ClientManager();
            Client.InitializeListeners();
        }
    }
}
