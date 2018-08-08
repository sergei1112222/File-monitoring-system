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
    public class Monitor
    {
        private  FileSystemWatcher _fileSystemWatcher;
        private ChangesBuffer _buff;

        public Monitor(ChangesBuffer buff)
        {
            _buff = buff;
        }
        
        public  void MonitorNode(string Name, string  monitoringFileType)
        {
            _fileSystemWatcher = new FileSystemWatcher();
            _fileSystemWatcher.Path = Name;
            _fileSystemWatcher.Filter = monitoringFileType;
            _fileSystemWatcher.Created += FileSystemWatcher_Created;
            _fileSystemWatcher.Changed += FileSystemWatcher_Changed;
            _fileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
            _fileSystemWatcher.Deleted += FileSystemWatcher_Deleted;
            _fileSystemWatcher.EnableRaisingEvents = true;
            //_fileSystemWatcher.IncludeSubdirectories = true;
        }

        public  void StartMonitor()
        {
           
        }
        public  void EndMonitor()
        {
            _fileSystemWatcher.EnableRaisingEvents = false;
        }
        private  void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            _buff.Created(e.FullPath);
            Program._log.Info($"{e.FullPath} created!");
        }
        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            _buff.Changed(e.FullPath);
            Program._log.Info($"{e.FullPath} changed!");
        }

        private  void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            _buff.Renamed(e.OldFullPath, e.FullPath);
            Program._log.Info($"{e.OldFullPath} renamed to {e.FullPath}!");
        }

        private  void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            _buff.Deleted(e.FullPath);
            Program._log.Info($"{e.FullPath} removed!");
        }
    }
}
