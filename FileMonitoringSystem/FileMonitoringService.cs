using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using log4net;

namespace FileMonitoringSystem
{
    public partial class FileMonitoringService : ServiceBase
    {
        private FilesMonitor _myProg = new FilesMonitor();
        

        public FileMonitoringService()
        {
            InitializeComponent();
        }
        public void operation()
        {
            
        }

        protected override void OnStart(string[] args)
        {
            Client.ClientManager client = new Client.ClientManager();
            client.InitializeListeners();
            
        }

        protected override void OnStop()
        {
            _myProg.Stop();
        }
    }
}
