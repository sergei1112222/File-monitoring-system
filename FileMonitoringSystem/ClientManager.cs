using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileMonitoringSystem.Repo.ImplementRepo;
using FileMonitoringSystem.Configuration;
using FileMonitoringSystem.Monitoring;
using FileMonitoringSystem.Configuration.Configurator;
using FileMonitoringSystem.Repo;
using FileMonitoringSystem.Common;
using log4net;

namespace FileMonitoringSystem
{
    
    public class ClientManager
    {
        private ILog _log = LogManager.GetLogger(typeof(ClientManager).Name);
        private IConfiguration _conf;
        private IRepository _repo;
        private IEnumerable<IWorker> _workers;

        public ClientManager(IConfiguration config, IRepository repository)
        {
            _conf = config;
            _repo = new Repository();
        }

        public void InitializeWorkers()
        {
            ChangesBuffer buffer = new ChangesBuffer();
            _log.Info("Initialize workers");
            LogManager.Flush(1);
            List<IWorker> workers = new List<IWorker>();
            workers.Add(new Monitor(buffer, _conf.GetMonitorSettings()));
            workers.Add(new ChangeHandler(buffer, _repo));
            //TODO: add Sender Worker

            _workers = workers;
        }

        public void Start()
        {
            
            foreach (var worker in _workers)
                worker.Start();
        }

        public void Stop()
        {
            foreach (var worker in _workers)
                worker.Stop();
        }       

    }
}

