using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMonitoringSystem.Repository.Repository
{
    public class Repository: IRepository
    {
        private List<FileData> _db;

        public Repository()
        {
            _db = new List<FileData>();
        }

        public void Insert(FileData entity)
        {

        }

        public void Delete(int id)
        {

        }

        public FileData GetFileDataForSend()
        {
            return new FileData();
        }
    }
}
