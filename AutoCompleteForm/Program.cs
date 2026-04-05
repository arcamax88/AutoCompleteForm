using System;
using System.Collections.Generic;
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
            string strTestersXmlFile    = ConstantString.MainFolder + "List_of_Currently_Used_Tester.xml";
            string strWorkOrdersXmlFile = ConstantString.MainFolder + "Work Orders.xml";
            string strModelsXmlFile     = ConstantString.MainFolder + "models.xml";

            ValidateFilesExist(strTestersXmlFile, strWorkOrdersXmlFile, strModelsXmlFile);

            List<Tester>      testers    = ReadTesters.Run(strTestersXmlFile);
            List<Model>       models     = ReadXmlFile.Models(strModelsXmlFile);
            List<Work_Orders> workOrders = ReadXmlFile.WorkOrders(strWorkOrdersXmlFile);

            // Build O(1) model lookup keyed on trimmed ModelNumber
            Dictionary<string, Model> modelsByNumber = new Dictionary<string, Model>();
            foreach (Model m in models)
                modelsByNumber[m.ModelNumber.Trim()] = m;

            AIMSExport unfinished = new AIMSExport();
            int rowCount = 0;

            foreach (Work_Orders item in workOrders)
            {
                rowCount++;
                Console.WriteLine(rowCount + ". " + item.WO_NUMBER + " " + item.TAG_NUMBER + " " +
                    item.MANUFACTURER_DESC + " " + item.MODEL_NUM + " " + item.SERIAL_NUM + " " +
                    item.LOCATION_DESC + " " + item.EMPLOYEE_DESC);

                WorkOrder.WorkOrder wo;
                try
                {
                    if (item.TAG_NUMBER != "NOEQU")
                    {
                        wo = new WorkOrder.WorkOrder(item.WO_NUMBER, item.EMPLOYEE_DESC, item.STAT_DATETIME,
                            item.TAG_NUMBER, item.SERIAL_NUM, item.MANUFACTURER_DESC, item.MODEL_NUM,
                            item.BUILDING_DESC, item.LOCATION_DESC, item.ACTION);

                        Model model;
                        if (modelsByNumber.TryGetValue(wo.ModelName, out model))
                        {
                            wo.ModelName = model.ModelName;
                            Dictionary<string, string> forms = SelectInputAndOutFiles.GetForms(
                                wo, model.ConstantParameters,
                                ConstantString.IpmFormFolder + model.IpmForm,
                                ConstantString.AcceptanceFormFolder + model.AcceptanceForm);

                            foreach (var pair in forms)
                            {
                                Console.WriteLine(pair.Key);
                                Console.WriteLine(pair.Value);
                                wo.CreateIpmForm(pair.Key, pair.Value, testers);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No model match found for: " + wo.ModelName);
                            unfinished.ListOfWorkOrders.Add(item);
                        }
                    }
                    else
                    {
                        wo = ProcessWorkOrderOfLoanUnit.Run(item);
                    }

                    Console.WriteLine(rowCount + ". " + wo.WorkOrderNumber + " " + wo.ControlNumber + " " +
                        wo.ManufacturerName + " " + wo.ModelName + " " + wo.SerialNumber + " " +
                        wo.DepartmentName + " " + wo.HospitalName + " " + wo.EmployeeName + " " + wo.Action);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error processing work order " + item.WO_NUMBER + ": " + ex.Message);
                    unfinished.ListOfWorkOrders.Add(item);
                }
            }

            Console.ReadLine();
            WriteUnfinishedWorkOrders.Run(unfinished, strWorkOrdersXmlFile);
            Console.ReadLine();
        }

        private static void ValidateFilesExist(params string[] paths)
        {
            foreach (string path in paths)
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException("Required input file not found: " + path);
            }
        }
    }
}
