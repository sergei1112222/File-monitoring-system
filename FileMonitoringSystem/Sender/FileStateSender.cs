using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileMonitoringSystem.Common;
using FileMonitoringSystem.Repo;
using log4net;

namespace FileMonitoringSystem.Sender
{
    public class SenderSettings
    {
        string _serverAdress;
        int _port;

        public SenderSettings() { }

        public SenderSettings(string serverAddr, int port)
        {
            _serverAdress = serverAddr;
            _port = port;
        }
    }

    public class FileStateSender: ThreadWorker
    {
        private IFileStateRepository _repo;
        private ILog _log = LogManager.GetLogger(typeof(FileStateSender).Name);
        private int _timeOut;

        public FileStateSender(IFileStateRepository repo)
        {
            _repo = repo;
        }

        protected override void Work()
        {
            while (!CancelFlag.IsCancellationRequested)
            {
                try
                {

                }
                cathc(){

                }
            }
        }

        private FileData GetFIleState(int intactSeconds)
        {
            /*f (_repo.Count != 0)
                lock (_repo)
                {
                    if (_repo.Count != 0)
                    {
                        //var query = _repo.Query(f => f.TimeSpan < DateTime.Now.AddSeconds(-intactSeconds)).OrderBy(f => f.TimeSpan);
                        /*if (query.Count() > 0)
                        {
                            var fileState = query.First();

                            _log.Info($"Removed from DB: {fileState.Path}");
                            _repo.Remove(fileState);
                            return fileState;
                        }
                    }
                }*/
            return null;
        }

        private void FakeSend(FileData fd)
        {
            _log.Info($"Send file State {fd.Path}");
        }
    }
}
