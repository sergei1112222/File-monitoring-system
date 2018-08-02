using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMonitoringSystem.Monitoring
{
    interface IMonitoring
    {
        void StartMonitor();
        void EndMonitor();
    }
}
