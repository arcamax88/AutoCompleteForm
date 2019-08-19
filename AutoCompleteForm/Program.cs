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
            //declare all variables
            AIMSExport workOrdersIpmFormUnfinished = new AIMSExport();
            List<Tester> testers = new List<Tester>();
            List<Model> models = new List<Model>();
            List<Work_Orders> listOfWorkOrdersToBeProcessed = new List<Work_Orders>();
            List<string> parameters = new List<string>();
            string strTestersXmlFile = ConstantString.MainFolder + "List_of_Currently_Used_Tester.xml";
            string strWorkOrdersXmlFile = ConstantString.MainFolder + "Work Orders.xml";
            string strModelsXmlFile = ConstantString.MainFolder + "models.xml";
            List<WorkOrder.WorkOrder> workOrders = new List<WorkOrder.WorkOrder>();
            Dictionary<string, string> files = new Dictionary<string, string>();
            WorkOrder.WorkOrder wo;
            bool flagModelSearchResult;

            //read the xml file which contains list of test equipment
            testers = ReadTesters.Start(strTestersXmlFile);

            //read the xml file which contains list of models
            models = ReadXmlFile.Models(strModelsXmlFile);

            //read the xml file which contains list of workorders
            listOfWorkOrdersToBeProcessed = ReadXmlFile.WorkOrders(strWorkOrdersXmlFile);
            int row_count = 0;
            foreach (Work_Orders item in listOfWorkOrdersToBeProcessed)
            {
                flagModelSearchResult = false;
                //display each pre process workorder in the console window
                row_count++;
                Console.WriteLine(row_count + ". " + item.WO_NUMBER + " " + item.TAG_NUMBER + " " + item.MANUFACTURER_DESC + " " +
                                item.MODEL_NUM + " " + item.SERIAL_NUM + " " + item.LOCATION_DESC + " " + item.EMPLOYEE_DESC);
                if (item.TAG_NUMBER != "NOEQU")
                {
                    //process workorder of registered unit
                    wo = new WorkOrder.WorkOrder(item.WO_NUMBER, item.EMPLOYEE_DESC, item.STAT_DATETIME, item.TAG_NUMBER, item.SERIAL_NUM,
                                                item.MANUFACTURER_DESC, item.MODEL_NUM, item.BUILDING_DESC, item.LOCATION_DESC, item.ACTION);

                    //search the wo.ModelName if it is already in the list of models
                    foreach (Model model in models)
                    {
                        if (wo.ModelName == model.ModelNumber.TrimStart().TrimEnd())
                        {
                            wo.ModelName = model.ModelName;
                            files = SelectInputAndOutFiles.Action(wo, model.ConstantParameters, ConstantString.IpmFormFolder + model.IpmForm,
                                                            ConstantString.AcceptanceFormFolder + model.AcceptanceForm);
                            foreach (var pair in files)
                            {
                                Console.WriteLine(pair.Key.ToString());
                                Console.WriteLine(pair.Value.ToString());

                                ////extract the data on the second statement or use the default
                                //ReadOrReplaceMeasuredValue.Action(wo, model.ConstantParameters);
                                wo.CreateIpmForm(pair.Key, pair.Value, testers);
                            }
                            flagModelSearchResult = true;
                        }
                    }
                    if (!flagModelSearchResult)
                    {
                        Console.WriteLine(wo.ModelName);
                        workOrdersIpmFormUnfinished.ListOfWorkOrders.Add(item);
                    }
                }
                else
                {
                    //process workorder of loan unit
                    wo = ProcessWorkOrderOfLoanUnit.Start(item);
                }
                //display each post process workorder in the console window
                Console.WriteLine(row_count + ". " + wo.WorkOrderNumber + " " + wo.ControlNumber + " " + wo.ManufacturerName + " " + wo.ModelName + " " + wo.SerialNumber + " " + wo.DepartmentName
                    + " " + wo.HospitalName + " " + wo.EmployeeName + " " + wo.Action);
            }
            Console.ReadLine();
            //replace the xml file with undone workorders
            WriteUnfinishedWorkOrders.Start(workOrdersIpmFormUnfinished, strWorkOrdersXmlFile);
            Console.ReadLine();
        }
    }
}
