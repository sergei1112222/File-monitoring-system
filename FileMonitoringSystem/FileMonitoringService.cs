using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.IO.Compression;
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
        private ClientManager _manager;

        public FileMonitoringService(ClientManager manager)
        {
            InitializeComponent();

            _manager = manager;
        }

        protected override void OnStart(string[] args)
        {
            _manager.Start();            
        }

        protected override void OnStop()
        {
            _manager.Stop();
        }
    }
}
