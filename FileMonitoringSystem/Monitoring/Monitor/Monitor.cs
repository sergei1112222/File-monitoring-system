using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileMonitoringSystem.Monitoring.Monitor
{
    public static class Monitor
    {
        private static FileSystemWatcher _fileSystemWatcher;

        public static void MonitorNode(string Name)
        {
            _fileSystemWatcher = new FileSystemWatcher();

        }

    }
}
