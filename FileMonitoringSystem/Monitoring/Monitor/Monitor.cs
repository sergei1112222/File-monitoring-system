using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FileMonitoringSystem.Repo;
using FileMonitoringSystem.Repo.ImplementRepo;
using log4net;

namespace FileMonitoringSystem.Monitoring.Monitor
{
    public class Monitor
    {
        private  FileSystemWatcher [] _fileSystemWatcher;
        private ChangesBuffer _buff;
        private ILog _log = LogManager.GetLogger(typeof(Monitor).Name);

        public Monitor(ChangesBuffer buff, string[] MonitorFileTypes, string[] MonitorFolders)
        {
            _buff = buff;
            int countFileType = MonitorFileTypes.Length;
            int countfolder = MonitorFolders.Length;
            _fileSystemWatcher = new FileSystemWatcher[countFileType * countfolder];

            int i = 0;
            foreach (string path in MonitorFolders)
            { 
                foreach (string type in MonitorFileTypes)
                {
                    _fileSystemWatcher[i] = MonitorNode(path, "*." + type);
                    i++;
                }
            }
        }
        
        private FileSystemWatcher MonitorNode(string Name, string  monitoringFileType)
        {
            FileSystemWatcher _fsw = new FileSystemWatcher();
            _fsw.Path = Name;
            _fsw.Filter = monitoringFileType;
            _fsw.Created += FileSystemWatcher_Created;
            _fsw.Changed += FileSystemWatcher_Changed;
            _fsw.Renamed += FileSystemWatcher_Renamed;
            _fsw.Deleted += FileSystemWatcher_Deleted;
            _fsw.EnableRaisingEvents = true;
            return _fsw;
        }

        public  void StartMonitor()
        {
           
        }
        public  void EndMonitor()
        {
         //   _fileSystemWatcher.EnableRaisingEvents = false;
        }
        private  void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            _log.Info($"{e.FullPath} created!");
            _buff.Created(e.FullPath);
        }
        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            _log.Info($"{e.FullPath} changed!");
            _buff.Changed(e.FullPath);
        }

        private  void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            _buff.Renamed(e.OldFullPath, e.FullPath);
            _log.Info($"{e.OldFullPath} renamed to {e.FullPath}!");
        }

        private  void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Program._log.Info($"{e.FullPath} removed!");
            _buff.Deleted(e.FullPath);
            
            
        }
    }
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
}
