using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileMonitoringSystem.Repo;
using FileMonitoringSystem.Repo.ImplementRepo;
using log4net;

namespace FileMonitoringSystem.Monitoring
{
    public class ChangesBuffer
    {
        private Dictionary<string, FileState> _files = new Dictionary<string, FileState>();
        private ILog _log = LogManager.GetLogger(typeof(ChangesBuffer).Name);

        public ChangesBuffer() { }
        
        public void Created(string path, string name)
        {
            lock (_files)
                if (!_files.ContainsKey(path))
                {
                    _log.Info($"Add in change buffer (created){path}");
                    _files.Add(path, new FileState(path,name,false));
                }
                else
                {
                    _files[path].IsDeleted = false;
                    _files[path].Update();
                }
        }

        public void Changed(string path, string name)
        {
            lock (_files)
                if (!_files.ContainsKey(path))
                {
                    _files.Add(path, new FileState(path, name, false));
                    _log.Info($"Add in change buffer (changed){path}");
                }

                else
                {
                    _log.Info($"Add in change buffer (changed){path}");
                    _files[path].Update();
                }
                    
        }

        public void Deleted(string path, string name)
        {
            lock (_files)
            {
                if (!_files.ContainsKey(path))
                {
                    _files.Add(path, new FileState(path, name, true));
                    _log.Info($"Add in change buffer (deleted){path}");
                }
                    

                _files[path].IsDeleted = true;
                _files[path].Update();
            }
        }

        public void Renamed(string oldPath, string newPath, string name)
        {
            lock (_files)
            {
                if (_files.ContainsKey(oldPath))
                    _files.Remove(oldPath);

                _files.Add(newPath, new FileState(newPath, oldPath, name, false));
                _log.Info($"Add in change buffer (rename){oldPath} --> {newPath}");
            }
        }

        public FileState Dequeue(int intactSeconds)
        {
            if (_files.Count > 0)
                lock (_files)
                    if (_files.Count > 0)
                    {

                        var query = _files.Values
                            .Where(f => f.TimeSpan < DateTime.Now.AddSeconds(-intactSeconds))
                            .OrderBy(f => f.TimeSpan);
                        if (query.Count() > 0)
                        {
                            var file = query.First();

                            _log.Info($"Removed from buffer: {file.Path}");
                            _files.Remove(file.Path);
                            
                            return file;
                        }
                    }

            return null;
        }
    }
}
