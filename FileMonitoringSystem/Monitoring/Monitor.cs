using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FileMonitoringSystem.Repo;
using FileMonitoringSystem.Repo.ImplementRepo;
using log4net;
using FileMonitoringSystem.Common;

namespace FileMonitoringSystem.Monitoring
{
    public class MonitorSettings
    {
        public string[] MonitorFolders { get; set; }
        public string[] MonitorFileTypes { get; set; }

        public MonitorSettings() { }

        public MonitorSettings(string[] monitorFolders, string[] monitorFileTypes)
        {
            MonitorFolders = monitorFolders;
            MonitorFileTypes = monitorFileTypes;
        }
    }

    public class Monitor : IWorker
    {
        private ILog _log = LogManager.GetLogger(typeof(Monitor).Name);

        private MonitorSettings _settings;        
        private ChangesBuffer _buff;
        private FileSystemWatcher[] _watchers;


        public Monitor(ChangesBuffer buff, MonitorSettings settings)
        {
            _buff = buff;
            _settings = settings;
        }

        public void Start()
        {
            int countFileType = _settings.MonitorFileTypes.Length;
            int countfolder = _settings.MonitorFolders.Length;
            _watchers = new FileSystemWatcher[countFileType * countfolder];

            int i = 0;
            foreach (string path in _settings.MonitorFolders)
            {
                foreach (string type in _settings.MonitorFileTypes)
                {
                    _watchers[i] = MonitorNode(path, "*." + type);
                    i++;
                }
            }
        }

        public void Stop()
        {
            foreach (var watcher in _watchers)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
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
            _log.Info($"{e.FullPath} removed!");
            _buff.Deleted(e.FullPath);          
            
        }
    }

}
