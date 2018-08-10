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

namespace FileMonitoringSystem.Client
{
    
    public class ClientManager
    {
      
        private IConfiguration _conf;
        private MonitorSetting monitorSetting;

        public ChangesBuffer changeBuf;
        public IRepository Repo;
        public Monitor FileChangeListeners;


        public ClientManager(IConfiguration config, IRepository repository)
        {
            _conf = config;
            Repo = new Repository();
            monitorSetting = _conf.GetMonitorSetting();
            changeBuf = new ChangesBuffer();
        }
        public void Start()
        {
            //Start monitoring
            //Start Change handle
            //Start sender
        }

        public void Stop()
        {

        }

        public void InitializeListeners()
        {
            FileChangeListeners = new Monitor(changeBuf, monitorSetting.MonitorFileTypes, monitorSetting.MonitorFolders);
            new System.Threading.Thread(operationListener).Start();
        }

        private void operationListener()
        {
            while (true)
            {
                changeBuf.Dequeue(5);
            }
        }
        public void OperationWithModFile()
        {

        }

    }
}

