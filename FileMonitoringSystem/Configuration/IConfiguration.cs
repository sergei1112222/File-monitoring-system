using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using FileMonitoringSystem.Monitoring;
using FileMonitoringSystem.Repo;
using FileMonitoringSystem.Sender;

namespace FileMonitoringSystem.Configuration
{
    public interface IConfiguration
    {        
        SenderSettings GetServerSettings();

        MonitorSettings GetMonitorSettings();

        DbSettings GetDbSettings();
    }
}
