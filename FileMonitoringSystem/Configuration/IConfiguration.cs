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
    interface IConfiguration
    {
        
        SenderSetting GetServerSetting();

        MonitorSetting GetMonitorSetting();

        DbSetting GetDbSetting();
    }
}
