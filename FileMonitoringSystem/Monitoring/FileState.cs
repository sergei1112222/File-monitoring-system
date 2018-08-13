using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMonitoringSystem.Monitoring
{
    public class FileState
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string OldPath { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime TimeSpan { get; set; }

        public FileState(string path, string name, bool isDeleted)
        {
            Path = path;
            TimeSpan = DateTime.Now;
            Name = name;
            IsDeleted = isDeleted;
        }

        public FileState(string path, string oldPath, string name, bool isDeleted)
            : this(path,name, isDeleted)
        {
            OldPath = oldPath;
        }

        public void Update()
        {
            TimeSpan = DateTime.Now;
        }
    }
}
