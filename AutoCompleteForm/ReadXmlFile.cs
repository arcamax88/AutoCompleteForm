using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using WorkOrder;

namespace AutoCompleteForm
{
    public class ReadXmlFile
    {
        public static List<Work_Orders> WorkOrders(string xmlFile)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(AIMSExport));
            using (TextReader reader = new StreamReader(xmlFile))
            {
                AIMSExport xmlData = (AIMSExport)deserializer.Deserialize(reader);
                return xmlData.ListOfWorkOrders;
            }
        }

        public static List<Model> Models(string xmlFile)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(Models));
            using (TextReader reader = new StreamReader(xmlFile))
            {
                Models xmlData = (Models)deserializer.Deserialize(reader);
                return xmlData.ListOfModel;
            }
        }
    }
}
