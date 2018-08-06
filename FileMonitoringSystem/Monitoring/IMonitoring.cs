using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMonitoringSystem.Monitoring
{
    public class MonitorSetting
    {
        public string[] MonitorFolders { get; set; }
        public string[] MonitorFileTypes { get; set; }

        public MonitorSetting() { }

        public MonitorSetting(string[] monitorFolders, string[] monitorFileTypes)
        {
            MonitorFolders = monitorFolders;
            MonitorFileTypes = monitorFileTypes;
        } 

        
    }
    interface IMonitoring
    {
        void StartMonitor();
        void EndMonitor();
    }
}
