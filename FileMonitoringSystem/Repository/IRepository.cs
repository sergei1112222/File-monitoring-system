using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMonitoringSystem.Repository
{
    public class DbSetting
    {
        string _dbAddr;

        public DbSetting() { }

    }
    public class FileData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public bool IsSend { get; set; }
        public FileData() { }

        public FileData(Guid id, string name, string type, string path)
        {
            Id = id;
            Name = name;
            Type = type;
            Path = path;
        }
    }

    interface IRepository
    {
       
        void Insert(FileData entity);
        void Delete(FileData removeData);
        FileData GetFileDataForSend();
    }
}
