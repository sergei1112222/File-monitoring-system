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
        public string Name { get; set; }
        public string OldPath { get; set; }
        public bool IsRemove { get; set; }
        public DateTime TimeSpan { get; set; }
        public string ContentHash { get; set; }
        public FileData() { }

        public FileData(Guid id, string path, string name, string oldPath,bool isRemove, DateTime timeSpan, string contentHash)
        {
            Id = id;
            Path = path;
            Name = name;
            OldPath = oldPath;
            IsRemove = isRemove;
            TimeSpan = timeSpan;
            ContentHash = contentHash;
        }
    }

    public interface IRepository
    {
       
        void Insert(FileData entity);
        void Delete(FileData removeData);
        List<FileData> ReturnDataBase();
    }
}
