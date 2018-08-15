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

    public class FakeSender: ThreadWorker
    {
        private IRepository _repo;
        private ILog _log = LogManager.GetLogger(typeof(FakeSender).Name);

        public FakeSender(IRepository repo)
        {
            _repo = repo;
        }

        protected override void Work()
        {
            while (!CancelFlag.IsCancellationRequested)
            {
                FileData temp = getFIleState(10);
                if (temp != null)
                {
                    _log.Info("Send file state");
                    fakeSend(temp);
                }
                else
                    // поспим 5 секунд, чтоб не жрать процессор этим циклом, пока что у нас нет подходящих FileState
                    Sleep(1000);
            }
        }

        private FileData getFIleState(int intactSeconds)
        {
            if (_repo.Count != 0)
                lock (_repo)
                {
                    if (_repo.Count != 0)
                    {
                        var query = _repo.Query(f => f.TimeSpan < DateTime.Now.AddSeconds(-intactSeconds)).OrderBy(f => f.TimeSpan);
                        if (query.Count() > 0)
                        {
                            var fileState = query.First();

                            _log.Info($"Removed from DB: {fileState.Path}");
                            _repo.Remove(fileState);
                            return fileState;
                        }
                    }
                }
            return null;
        }

        private void fakeSend(FileData fd)
        {
            _log.Info($"Send file State {fd.Path}");
        }
    }
}
