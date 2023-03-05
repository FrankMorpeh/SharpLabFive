using SharpLabFive.Controllers.WorkshopControllers;
using System;
using System.IO;
using System.Xml;

namespace SharpLabFive.XmlLoggers.FactoryLoggers
{
    public static class XmlFactoryLogger
    {
        public static void LogFactory(Factory factory)
        {
            string pathToLogger = MainWindow.initialLocation + "\\Loggers\\FactoryLogger.xml";
            if (!File.Exists(pathToLogger))
                CreateFactoryLoggerXmlFile(pathToLogger);
            LogFactoryToExistingFile(factory, pathToLogger);
        }
        private static void CreateFactoryLoggerXmlFile(string pathToLogger)
        {
            using (XmlWriter xmlWriter = XmlWriter.Create(pathToLogger))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Factories");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
                xmlWriter.Flush();
            }
        }
        private static void LogFactoryToExistingFile(Factory factory, string pathToLogger)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(pathToLogger);

            XmlNode factoryNode = xmlDocument.CreateElement("Factory");
            XmlNode moneyNode = xmlDocument.CreateElement("Money"); moneyNode.InnerText = Convert.ToString(factory.Money);
            XmlNode numberOfGoodsMadeode = xmlDocument.CreateElement("NumberOfGoodsMade"); numberOfGoodsMadeode.InnerText = 
                Convert.ToString(factory.NumberOfGoodsMade);
            XmlNode numberOfResourcesNode = xmlDocument.CreateElement("NumberOfResources"); numberOfResourcesNode.InnerText = 
                Convert.ToString(factory.NumberOfResources);
            XmlNode dateNode = xmlDocument.CreateElement("Date"); dateNode.InnerText = DateTime.Now.ToString("G");
            factoryNode.AppendChild(moneyNode);
            factoryNode.AppendChild(numberOfGoodsMadeode);
            factoryNode.AppendChild(numberOfResourcesNode);
            factoryNode.AppendChild(dateNode);

            xmlDocument.DocumentElement.AppendChild(factoryNode);
            xmlDocument.Save(pathToLogger);
        }
    }
}