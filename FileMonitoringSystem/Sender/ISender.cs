using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMonitoringSystem.Sender
{
    public class SenderSetting
    {
        string _serverAdress;
        int _port;

        public SenderSetting() { }

        public SenderSetting(string serverAddr, int port)
        {
            _serverAdress = serverAddr;
            _port = port;
        }
    }
    interface ISender
    {
        
    }
}
