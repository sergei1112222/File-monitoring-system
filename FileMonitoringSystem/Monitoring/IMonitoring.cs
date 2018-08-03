using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMonitoringSystem.Monitoring
{
    public class MonitorSetting
    {
        string[] _monitorFolders;
        string[] _monitorFileTypes;

        public MonitorSetting() { }

        public MonitorSetting(string[] monitorFolders, string[] monitorFileTypes)
        {
            _monitorFolders = monitorFolders;
            _monitorFileTypes = monitorFileTypes;
        }
    }
    interface IMonitoring
    {
        void StartMonitor();
        void EndMonitor();
    }
}
