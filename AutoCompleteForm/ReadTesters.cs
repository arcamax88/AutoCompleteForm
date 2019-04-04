using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalEquipmentTester;

namespace AutoCompleteForm
{
    public class ReadTesters
    {
        public static List<Tester> Start (string xmlFile)
        {
            List<Tester> testers = new List<Tester>();

            //read the xml file which contains list of test equipment
            Tester tester = new Tester();
            testers = tester.ReadXmlFile(xmlFile);

            return testers;
        }
    }
}
