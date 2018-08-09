using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMonitoringSystem.Repo
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
        public bool IsSynh { get; set; }
        public DateTime Date { get; set; }
        public FileData() { }

        public FileData(Guid id, string name, DateTime date)
        {
            Id = id;
            Name = name;
            Date = date;
        }
    }

    interface IRepository
    {
       
        void Insert(FileData entity);
        void Delete(FileData removeData);
        FileData GetFileDataForSend();
    }
}
