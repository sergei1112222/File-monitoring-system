using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FileMonitoringSystem.Repo;
using FileMonitoringSystem.Repo.ImplementRepo;

namespace FileMonitoringSystem.Monitoring.Monitor
{
    public class Monitor: IMonitoring
    {
        private  FileSystemWatcher _fileSystemWatcher;
        private QueueBuffer _buff;

        public Monitor(QueueBuffer  buff)
        {
            _buff = buff;
        }
        
        public  void MonitorNode(string Name, string  monitoringFileType)
        {
            _fileSystemWatcher = new FileSystemWatcher();
            _fileSystemWatcher.Path = Name;
            _fileSystemWatcher.Filter = monitoringFileType;
            _fileSystemWatcher.Created += FileSystemWatcher_Created;
            _fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
            _fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;
            _fileSystemWatcher.IncludeSubdirectories = true;
        }

        public  void StartMonitor()
        {
            _fileSystemWatcher.EnableRaisingEvents = true;
        }
        public  void EndMonitor()
        {
            _fileSystemWatcher.EnableRaisingEvents = false;
        }
        private  void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            _buff.AddCreatedFile(e);
        }

        private  void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            _buff.AddRenamedFile(e);
        }

        private  void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            //_repo.OperationWithModFile(e.Name, 2);
        }
    }
}
