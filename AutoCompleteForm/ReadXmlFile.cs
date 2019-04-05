using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using WorkOrder;

namespace AutoCompleteForm
{
    public class ReadXmlFile
    {
        public static List<Work_Orders> WorkOrders(string xmlFile)
        {
            //read the xml file which contains list of workorders
            XmlSerializer deserializer = new XmlSerializer(typeof(AIMSExport));
            TextReader reader = new StreamReader(xmlFile);
            object obj = deserializer.Deserialize(reader);
            AIMSExport xmlData = (AIMSExport)obj;
            reader.Close();

            return xmlData.ListOfWorkOrders;
        }

        public static List<Model> Models(string xmlFile)
        {
            //read the xml file which contains list of workorders
            XmlSerializer deserializer = new XmlSerializer(typeof(Models));
            TextReader reader = new StreamReader(xmlFile);
            object obj = deserializer.Deserialize(reader);
            Models xmlData = (Models)obj;
            reader.Close();

            return xmlData.ListOfModel;
        }
    }
}
