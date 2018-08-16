using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileMonitoringSystem.Repo;

namespace FileMonitoringSystem.Sender
{
    interface ISender
    {
        void Connect();
        void Send(FileData fileData);
        void Receive();
        bool ConnectionStatus();
    }
}
