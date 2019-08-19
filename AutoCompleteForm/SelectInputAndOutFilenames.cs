using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dict1AndDict2;

namespace AutoCompleteForm
{
    public class SelectInputAndOutFiles
    {
        public static Dictionary<string, string> Action(WorkOrder.WorkOrder workOrder, string defaultValues, string ipmForm, string accForm)
        {
            Dictionary<string, string> forms = new Dictionary<string, string>();
            string selectedInputForm = string.Empty;
            string selectedOutputForm = string.Empty;
            string OutputIpmFile = ConstantString.IpmFormCompletedFolder + workOrder.ControlNumber + "_" + workOrder.ModelName + ".docx";
            string OutputAccFile = ConstantString.IpmFormCompletedFolder + "acc_" + workOrder.ControlNumber + "_" + workOrder.ModelName + ".docx";

            string[] splitAction;
            //string[] splitResults;
            //string[] splitData;
            //string[] splitDefaultValues;
            //string[] splitParameter;
            splitAction = workOrder.Action.Split(':');

            switch (splitAction[0].Trim())
            {
                case "INSPECTION FUNCTION & SAFETY A - I02":
                    {
                        selectedInputForm = ipmForm;
                        selectedOutputForm = OutputIpmFile;
                        if (splitAction.Length == 2)
                        {
                            workOrder.Action = Dict1Dict2ToString(splitAction[1].Trim(), defaultValues.Trim());
                            workOrder.Action = workOrder.Action.Substring(0, workOrder.Action.Length - 1);
                            //Console.WriteLine(workOrder.Action);
                            //Console.ReadLine();
                        }
                        else
                        {
                            workOrder.Action = defaultValues;
                        }
                        break;
                    }
                case "INSPECTION FUNCTION & SAFETY A":
                    {
                        selectedInputForm = ipmForm;
                        selectedOutputForm = OutputIpmFile;
                        if (splitAction.Length == 2)
                        {
                            workOrder.Action = Dict1Dict2ToString(splitAction[1].Trim(), defaultValues.Trim());
                            workOrder.Action = workOrder.Action.Substring(0, workOrder.Action.Length - 1);
                        }
                        else
                        {
                            workOrder.Action = defaultValues;
                        }
                        break;
                    }
                case "ACCEPTANCE TEST/COMMISSION":
                    {
                        selectedInputForm = accForm;
                        selectedOutputForm = OutputAccFile;
                        if (splitAction.Length == 2)
                        {
                            workOrder.Action = Dict1Dict2ToString(splitAction[1].Trim(), defaultValues.Trim());
                            workOrder.Action = workOrder.Action.Substring(0, workOrder.Action.Length - 1);
                        }
                        else
                        {
                            workOrder.Action = defaultValues;
                        }

                        break;
                    }
                default:
                    {
                        selectedInputForm = string.Empty;
                        selectedOutputForm = string.Empty;
                        break;
                    }

            }

            forms.Add(selectedInputForm, selectedOutputForm);
            return forms;
        }

        private static string Dict1Dict2ToString(string str1, string str2)
        {
            string workOrderAction = string.Empty;
            var dict1 = Dict1AndDict2.Dict1AndDict2.ConvertStringToDict(str1);
            var dict2 = Dict1AndDict2.Dict1AndDict2.ConvertStringToDict(str2);

            var d1d2 = Dict1AndDict2.Dict1AndDict2.MergeExclusively(dict1, dict2);

            foreach (var pair in d1d2)
            {
                workOrderAction = workOrderAction + pair.Key + "=" + pair.Value + ",";
            }
            return workOrderAction;
        }   
    }

    
}

