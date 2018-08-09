using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using log4net;
using FileMonitoringSystem.Configuration.Configurator;
using FileMonitoringSystem.Monitoring.Monitor;
using FileMonitoringSystem.Monitoring;
using FileMonitoringSystem.Client;
using System.Diagnostics;

namespace FileMonitoringSystem
{
    static class Program
    {
        private static ILog _log;
        public static void ConfigLog()
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

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                ConfigLog();                
                sw.Stop();
                //_log.Info(sw.ElapsedMilliseconds);
                //Client.ClientManager client = new Client.ClientManager();
                //client.InitializeListeners();
                
                var args = Environment.GetCommandLineArgs();
                if (args.Length > 1 && args[1] == "console")
                {

                    _log.Info("Start as console");
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
                    
                }
                
                



            }
            catch(Exception ex)
            {
                System.IO.File.WriteAllText("D:\\test.txt", ex.ToString());

                _log.Error(ex);
            }

            /*
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
            
            
            var args = Environment.GetCommandLineArgs();
            if(args.Length > 1 && args[1] == "console")
            {
               

                FilesMonitor myProg = new FilesMonitor();
                myProg.Work();
            }
            else
            {
                _log.Info("Start as service");
                
            }*/
            /*ClientManager Client = new ClientManager();
            Client.InitializeListeners();
            while (true)
            {

            }*/
            
        }
    }
}
