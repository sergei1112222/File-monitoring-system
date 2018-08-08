using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMonitoringSystem.Monitoring
{
    public class FileState
    {
        public string Path { get; set; }
        public string OldPath { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime TimeSpan { get; set; }

        public FileState(string path)
        {
            Path = path;
            TimeSpan = DateTime.Now;
        }

        public FileState(string path, string oldPath)
            : this(path)
        {
            OldPath = oldPath;
        }

        public void Update()
        {
            TimeSpan = DateTime.Now;
        }
    }
}
