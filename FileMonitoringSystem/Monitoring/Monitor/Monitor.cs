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
    public class FileChangeListener
    {
        private Repository _repo;
        private Monitor[] Monitors;

        public FileChangeListener(Repository repo)
        {
            _repo = repo;
        }
    }
   
    public class Monitor: IMonitoring
    {
        private  FileSystemWatcher _fileSystemWatcher;
        

        public Monitor(Repository repo)
        {
            _repo = repo;
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
            _repo.OperationWithModFile(e.Name, 0);
            
        }

        private  void FileSystemWatcher_Renamed(object sender, FileSystemEventArgs e)
        {
            _repo.OperationWithModFile(e.Name, 1);
        }

        private  void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            _repo.OperationWithModFile(e.Name, 2);
        }
    }
}
