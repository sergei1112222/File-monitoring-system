using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileMonitoringSystem.Monitoring.Monitor
{
   
    public class Monitor: IMonitoring
    {
        private  FileSystemWatcher _fileSystemWatcher;

        public  void MonitorNode(string Name)
        {
            _fileSystemWatcher = new FileSystemWatcher();
            _fileSystemWatcher.Path = Name;
            _fileSystemWatcher.Created += FileSystemWatcher_Created;
            _fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
            _fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;
            _fileSystemWatcher.EnableRaisingEvents = true;
            _fileSystemWatcher.IncludeSubdirectories = true;

        }

        public  void StartMonitor()
        {

        }
        public  void EndMonitor()
        {

        }
        private  void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File created: {0}", e.Name);
        }

        private  void FileSystemWatcher_Renamed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File renamed: {0}", e.Name);
        }

        private  void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("File deleted: {0}", e.Name);
        }
    }
}
