using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileMonitoringSystem.Repo;
using FileMonitoringSystem.Repo.ImplementRepo;

namespace FileMonitoringSystem.Monitoring
{

    public class ChangesBuffer
    {
        private Dictionary<string, FileState> _files = new Dictionary<string, FileState>();
        private Repository _repo;

        public ChangesBuffer(Repository repo)
        {
            _repo = repo;
        }

        public void Created(string path)
        {
            //lock (_files)
                if (!_files.ContainsKey(path))
                    _files.Add(path, new FileState(path));
                else
                {
                    _files[path].IsDeleted = false;
                    _files[path].Update();
                }
        }

        public void Changed(string path)
        {
            lock (_files)
                if (!_files.ContainsKey(path))
                    _files.Add(path, new FileState(path));
                else
                    _files[path].Update();
        }

        public void Deleted(string path)
        {
            lock (_files)
            {
                if (!_files.ContainsKey(path))
                    _files.Add(path, new FileState(path));

                _files[path].IsDeleted = true;
                _files[path].Update();
            }
        }

        public void Renamed(string oldPath, string newPath)
        {
            lock (_files)
            {
                if (_files.ContainsKey(oldPath))
                    _files.Remove(oldPath);

                _files.Add(newPath, new FileState(newPath, oldPath));
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

                            Program._log.Info($"Removed from buffer: {file.Path}");
                            _repo.Insert(new FileData(Guid.NewGuid(), file.Path, file.TimeSpan));
                            _files.Remove(file.Path);
                            
                            return file;
                        }
                    }

            return null;
        }
    }
}
