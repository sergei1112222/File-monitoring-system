using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileMonitoringSystem.Repo
{
    public class QueueBuffer
    {
        private List<FileData> _qBuffer;

        public QueueBuffer()
        {
            _qBuffer = new List<FileData>();
            Thread thread = new Thread(BufTimer);
            thread.Start();
        }
        public void AddRenamedFile(RenamedEventArgs data)
        {
            int ind = _qBuffer.FindIndex(el => el.Name == data.OldName);
            if (ind != -1)
            {
                _qBuffer[ind].Name = data.Name;
                _qBuffer[ind].Date = DateTime.Now;
            }
            else
                AddCreatedFile(data);

        }

        public void AddCreatedFile(FileSystemEventArgs data)
        {
            Guid id = Guid.NewGuid();
            string[] fileData = data.Name.Split('.');
            //_qBuffer.Add(new FileData(id, data.Name, fileData[fileData.Length - 1], DateTime.Now));
        }

        public void AddChangedFile(FileSystemEventArgs data)
        {
            //compare hash
            
        }
        public void DeletedFile(FileSystemEventArgs data)
        {
            //import message
        }

        private void BufTimer()
        {
            while (true)
            {
                foreach (var elem in _qBuffer)
                {
                    if ((DateTime.Now - elem.Date).Seconds > 30)
                    {
                        //SendData
                    }
                }
            }
        }
    }
}
