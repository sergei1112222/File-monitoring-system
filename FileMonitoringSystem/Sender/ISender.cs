using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    interface ISender
    {
        
    }
}
