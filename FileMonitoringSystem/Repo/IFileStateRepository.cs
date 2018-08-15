using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMonitoringSystem.Repo
{
    public class DbSettings
    {
        string _dbAddr;

        public DbSettings() { }

    }
    public class FileData
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string OldPath { get; set; }
        public bool IsRemove { get; set; }
        public DateTime TimeSpan { get; set; }
        public string ContentHash { get; set; }
        public FileData() { }

        public FileData(Guid id, string path, string oldPath, bool isRemove, DateTime timeSpan, string contentHash)
        {
            Id = id;
            Path = path;
            OldPath = oldPath;
            IsRemove = isRemove;
            TimeSpan = timeSpan;
            ContentHash = contentHash;
        }
    }

    public interface IFileStateRepository
    {
        void Insert(FileData entity);
        void Remove(FileData removeData);

    }
}