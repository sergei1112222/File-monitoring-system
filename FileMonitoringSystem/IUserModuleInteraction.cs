using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMonitoringSystem
{
    interface IUserModuleInteraction
    {
        void Initializeconfig();
        void InitializeMonitoring();
        void InitializeRepository();
        void InitializeSender();
    }
}
