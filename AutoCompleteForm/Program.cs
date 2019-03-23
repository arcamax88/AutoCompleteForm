using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using WorkOrder;
using MedicalEquipmentTester;

namespace AutoCompleteForm
{
    class Program
    {
        static void Main(string[] args)
        {
            AIMSExport workOrdersIpmFormUnfinished = new AIMSExport();
            List<Tester> testers = new List<Tester>();
            List<string> parameters = new List<string>();
            string xmlFile = ConstantString.MainFolder + "Work Orders.xml";
            List<WorkOrder.WorkOrder> workOrders = new List<WorkOrder.WorkOrder>();
            Dictionary<string, string> files = new Dictionary<string, string>();
            WorkOrder.WorkOrder wo;

            //read the xml file which contains list of test equipment
            Tester tester = new Tester();
            testers = tester.ReadXmlFile(ConstantString.MainFolder + "List_of_Currently_Used_Tester.xml");

            //read the xml file which contains list of workorders
            XmlSerializer deserializer = new XmlSerializer(typeof(AIMSExport));
            TextReader reader = new StreamReader(xmlFile);
            object obj = deserializer.Deserialize(reader);
            AIMSExport xmlData = (AIMSExport)obj;
            reader.Close();

            int row_count = 0;
            foreach (Work_Orders item in xmlData.ListOfWorkOrders)
            {
                //display each pre process workorder in the console window
                row_count++;
                Console.WriteLine(row_count + ". " + item.WO_NUMBER + " " + item.TAG_NUMBER + " " + item.MANUFACTURER_DESC + " " + item.MODEL_NUM + " " + item.SERIAL_NUM + " " + item.LOCATION_DESC
                    + " " + item.EMPLOYEE_DESC);
                if (item.TAG_NUMBER != "NOEQU")
                {
                    wo = new WorkOrder.WorkOrder(item.WO_NUMBER, item.EMPLOYEE_DESC, item.STAT_DATETIME, item.TAG_NUMBER, item.SERIAL_NUM, item.MANUFACTURER_DESC,
                                                                        item.MODEL_NUM, item.BUILDING_DESC, item.LOCATION_DESC, item.ACTION);
                }
                else
                {
                    wo = new WorkOrder.WorkOrder(item.WO_NUMBER, item.EMPLOYEE_DESC, item.STAT_DATETIME, item.TAG_NUMBER, item.SERIAL_NUM, item.MANUFACTURER_DESC,
                                                                        item.MODEL_NUM, item.BUILDING_DESC, item.LOCATION_DESC, item.ACTION);

                    string[] splitAction = item.ACTION.Split(':');
                    Console.WriteLine(splitAction[1]);
                    string[] splitEqDetails = splitAction[1].Split(',');
                    foreach(var dict in splitEqDetails)
                    {
                        string[] splitDict = dict.Split('=');
                        Console.WriteLine(splitDict[0]);
                        string strKeyPair = splitDict[0].TrimStart().TrimEnd().ToUpper();
                        string strValuePair = splitDict[1].TrimStart().TrimEnd().ToUpper();
                        if (strKeyPair == "MANUFACTURER")
                        {
                            wo.ManufacturerName = strValuePair;
                        }
                        if (strKeyPair == "MODEL")
                        {
                            wo.ModelName = strValuePair;
                        }
                        if (strKeyPair == "SN")
                        {
                            wo.SerialNumber = strValuePair;
                        }
                        wo.ControlNumber = "LOAN UNIT";
                    }
                }
                //display each post process workorder in the console window
                Console.WriteLine(row_count + ". " + wo.WorkOrderNumber + " " + wo.ControlNumber + " " + wo.ManufacturerName + " " + wo.ModelName + " " + wo.SerialNumber + " " + wo.DepartmentName
                    + " " + wo.HospitalName + " " + wo.EmployeeName);
                Console.ReadLine();
                //add specific form (input file) to each model and inject default values to each parameter
                files = GetDictInputAndOuputFiles.Action(wo);
                foreach (var pair in files)
                {
                    if (pair.Key != string.Empty)
                    {
                        wo.CreateIpmForm(pair.Key, pair.Value, testers);
                    }
                    else
                    {
                        workOrdersIpmFormUnfinished.ListOfWorkOrders.Add(item);
                    }
                }
            }
            Console.ReadLine();

            //replace the xml file with undone workorders
            XmlSerializer serializer = new XmlSerializer(typeof(AIMSExport));
            TextWriter writer = new StreamWriter(xmlFile);
            serializer.Serialize(writer, workOrdersIpmFormUnfinished);
            writer.Close();
        }
    }
}
