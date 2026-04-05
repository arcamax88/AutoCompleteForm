using System.Collections.Generic;
using MedicalEquipmentTester;

namespace AutoCompleteForm
{
    public class ReadTesters
    {
        public static List<Tester> Run(string xmlFile)
        {
            Tester tester = new Tester();
            return tester.ReadXmlFile(xmlFile);
        }
    }
}
