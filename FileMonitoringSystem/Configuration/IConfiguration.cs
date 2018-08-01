using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FileMonitoringSystem.Configuration
{
    interface IConfiguration
    {
        string GetServerAdress();

        string[] GetMonitoringFolders();

        string[] GetMonitoringFileTypes();
    }
}
