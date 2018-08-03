﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileMonitoringSystem.Monitoring;
using FileMonitoringSystem.Repository;
using FileMonitoringSystem.Sender;
using System.Xml;

namespace FileMonitoringSystem.Configuration.Configurator
{
    public class Configurator: IConfiguration
    {
        private const string _configPath = "config.xml";

        public SenderSetting GetServerSetting()
        {
            return new SenderSetting();
        }

        public MonitorSetting GetMonitorSetting()
        {
            string monitoringDirArr = "";
            string monitoringTypeArr = "";
            XmlElement xElement = loadConfig("monitoringConf");
            foreach (XmlElement xNode in xElement)
            {
                if (xNode.Name == "monitoringDirectoryes")
                {
                    monitoringDirArr = xNode.InnerText;
                }
                if (xNode.Name == "monitoringFileTypes")
                {
                    monitoringTypeArr = xNode.InnerText;
                }
            }
            return new MonitorSetting(monitoringDirArr.Split(','), monitoringTypeArr.Split(','));
        }

        public DbSetting GetDbSetting()
        {
            return new DbSetting();
        }

        private XmlElement loadConfig(string unitName)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(_configPath);
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlElement node in xRoot)
            {
                if (node.Name == unitName)
                    return node;
            }
            return null;
        }
    }
}
