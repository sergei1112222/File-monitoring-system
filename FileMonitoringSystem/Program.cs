using System;
using System.ServiceProcess;
using System.Threading;
using System.Xml;
using log4net;

namespace FileMonitoringSystem
{
    static class Program
    {
        private static ILog _log;

        private static void ConfigLog()
        {
            string _configPath = "config.xml";
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(_configPath);
            XmlElement xRoot = xDoc.DocumentElement;
            XmlElement logConf = null;
            foreach (XmlElement el in xRoot)
            {
                if (el.Name == "log4net")
                {
                    logConf = el;
                }
            }
            log4net.GlobalContext.Properties["tab"] = "\t";
            log4net.Config.XmlConfigurator.Configure(logConf);

            
            _log = LogManager.GetLogger(typeof(Program).Name);   
        
        }

        private static ClientManager CreateClient()
        {
            // инициализация зависимостей. Может быть реализовано через Ninject, но для простоты пишем сами
            Repo.IRepository repo = new Repo.ImplementRepo.Repository();
            Configuration.IConfiguration conf = new Configuration.Configurator.Configurator();

            // создание и инициализация клиента
            var client = new ClientManager(conf, repo);
            client.InitializeWorkers();
            return client;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
            try
            {
                ConfigLog();
                var client = CreateClient();
                client.Start();
                Thread.Sleep(60000);
                client.Stop();
                /* var args = Environment.GetCommandLineArgs();
                 if (args.Length > 1 && args[1] == "console")
                 {
                     client.Start();
                     _log.Info("Start as console");
                     // stop after 1 minute
                     Thread.Sleep(60000);
                     client.Stop();
                 }
                 else
                 {
                     _log.Info("Start as service");
                     ServiceBase[] ServicesToRun;
                     ServicesToRun = new ServiceBase[]
                     {
                         new FileMonitoringService(client)
                     };
                     ServiceBase.Run(ServicesToRun);

                 }*/

                
                var args = Environment.GetCommandLineArgs();
                if (args.Length > 1 && args[1] == "console")
                {
                    client.Start();
                    _log.Info("Start as console");
                    // stop after 1 minute
                    Thread.Sleep(60000);
                    client.Stop();
                }
                else
                {
                    _log.Info("Start as service");
                    ServiceBase[] ServicesToRun;
                    ServicesToRun = new ServiceBase[]
                    {
                        new FileMonitoringService(client)
                    };
                    ServiceBase.Run(ServicesToRun);
                    
                }

            }
            catch(Exception ex)
            {
                System.IO.File.WriteAllText("D:\\test.txt", ex.ToString());

                _log.Error(ex);
            }           
        }
    }
}
