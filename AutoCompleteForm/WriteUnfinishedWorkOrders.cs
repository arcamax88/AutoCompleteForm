using System.IO;
using System.Xml.Serialization;
using WorkOrder;

namespace AutoCompleteForm
{
    public class WriteUnfinishedWorkOrders
    {
        public static void Run(AIMSExport wo, string xmlFile)
        {
            string tempFile = xmlFile + ".tmp";
            string backupFile = xmlFile + ".bak";

            XmlSerializer serializer = new XmlSerializer(typeof(AIMSExport));
            using (TextWriter writer = new StreamWriter(tempFile))
            {
                serializer.Serialize(writer, wo);
            }

            // Atomically replace the original file; previous version saved as .bak
            File.Replace(tempFile, xmlFile, backupFile);
        }
    }
}
