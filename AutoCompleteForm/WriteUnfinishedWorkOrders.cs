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
    public class WriteUnfinishedWorkOrders
    {
        public static void Start(AIMSExport wo, string xmlFile)
        {
            //replace the xml file with undone workorders
            XmlSerializer serializer = new XmlSerializer(typeof(AIMSExport));
            TextWriter writer = new StreamWriter(xmlFile);
            serializer.Serialize(writer, wo);
            writer.Close();
        }
    }
}
