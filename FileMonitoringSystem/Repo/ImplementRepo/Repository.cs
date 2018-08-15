using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileMonitoringSystem.Repo;
using log4net;

namespace FileMonitoringSystem.Repo.ImplementRepo
{
    public class FakeRepository: IFileStateRepository
    {
        private List<FileData> _db;
        private ILog _log = LogManager.GetLogger(typeof(FakeRepository).Name);

        public int Count { get { return _db.Count; } }

        public FakeRepository()
        {
            _db = new List<FileData>();
        }

        public void Insert(FileData fileData)
        {
            _db.Add(fileData);
        }

        public void Remove(FileData removeData)
        {
            lock (_db)
            {
                _db.Remove(removeData);
            } 
        }
    }
}
