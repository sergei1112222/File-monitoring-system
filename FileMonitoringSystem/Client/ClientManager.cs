using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileMonitoringSystem.Repo.ImplementRepo;
using FileMonitoringSystem.Configuration;
using FileMonitoringSystem.Monitoring.Monitor;
using FileMonitoringSystem.Configuration.Configurator;
using FileMonitoringSystem.Monitoring;

namespace FileMonitoringSystem.Client
{
    
    class ClientManager
    {
      
        private IConfiguration _conf;
        private MonitorSetting monitorSetting;

        public ChangesBuffer changeBuf;
        public Repository Repo;
        public Monitor[] FileChangeListeners;


        public ClientManager()
        {
            _conf = new Configurator();
            monitorSetting = _conf.GetMonitorSetting();
            changeBuf = new ChangesBuffer();
        }

        public void InitializeListeners()
        {
            int countFileType = monitorSetting.MonitorFileTypes.Length;
            int countfolder = monitorSetting.MonitorFolders.Length;
            FileChangeListeners = new Monitor[countFileType * countfolder];    
        
            int i = 0;
            foreach (string path in monitorSetting.MonitorFolders)
            {
                
                foreach (string type in monitorSetting.MonitorFileTypes)
                {
                    FileChangeListeners[i] = new Monitor(changeBuf);
                    FileChangeListeners[i].MonitorNode(path, "*."+type);
                    i++;
                }
            }

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

