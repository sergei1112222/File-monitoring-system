using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace FileMonitoringSystem
{
    public class FilesMonitor
    {
        private ILog _log = LogManager.GetLogger(typeof(FilesMonitor).Name);
        private bool _stop;

        public void Work()
        {
            while (!_stop)
            {
                Thread.Sleep(2000);
                _log.Info("I'm working!");
            }
        }

        public void Stop()
        {
            _stop = true;
        }
    }
}
