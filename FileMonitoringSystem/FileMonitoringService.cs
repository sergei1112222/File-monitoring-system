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

namespace FileMonitoringSystem
{
    public partial class FileMonitoringService : ServiceBase
    {
        private FilesMonitor _myProg = new FilesMonitor();

        public FileMonitoringService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Task.Run(() => { _myProg.Work(); });                       
        }

        protected override void OnStop()
        {
            _myProg.Stop();
        }
    }
}
