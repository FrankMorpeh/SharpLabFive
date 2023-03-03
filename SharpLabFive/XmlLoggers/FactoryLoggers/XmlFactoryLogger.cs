using SharpLabFive.Controllers.WorkshopControllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace SharpLabFive.XmlLoggers.FactoryLoggers
{
    public static class XmlFactoryLogger
    {
        public static void LogFactory(Factory factory)
        {
            if (!File.Exists(MainWindow.initialLocation + "\\Loggers\\FactoryLogger.xml"))
            {
                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;
                xmlWriterSettings.NewLineOnAttributes = true;
                using (XmlWriter xmlWriter = XmlWriter.Create("FactoryLogger.xml", xmlWriterSettings))
                {
                    xmlWriter.WriteStartDocument();

                    xmlWriter.WriteStartElement("Factory");
                    xmlWriter.WriteElementString("Money", Convert.ToString(factory.Money));
                    xmlWriter.WriteElementString("NumberOfGoodsMade", Convert.ToString(factory.NumberOfGoodsMade));
                    xmlWriter.WriteElementString("NumberOfResources", Convert.ToString(factory.NumberOfResources));
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndDocument();

                    xmlWriter.Flush();
                    xmlWriter.Close();
                }
            }
        }
    }
}