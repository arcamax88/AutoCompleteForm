using System.Collections.Generic;
using Dict1AndDict2;

namespace AutoCompleteForm
{
    public class SelectInputAndOutFiles
    {
        public static Dictionary<string, string> GetForms(WorkOrder.WorkOrder workOrder, string defaultValues, string ipmForm, string accForm)
        {
            string outputIpmFile = ConstantString.IpmFormCompletedFolder + workOrder.ControlNumber + "_" + workOrder.ModelName + ".docx";
            string outputAccFile = ConstantString.IpmFormCompletedFolder + "acc_" + workOrder.ControlNumber + "_" + workOrder.ModelName + ".docx";

            string[] splitAction = workOrder.Action.Split(':');
            string actionType = splitAction[0].Trim();
            string actionParams = splitAction.Length == 2 ? splitAction[1] : null;

            string selectedInputForm;
            string selectedOutputForm;

            switch (actionType)
            {
                case "INSPECTION FUNCTION & SAFETY A - I02":
                case "INSPECTION FUNCTION & SAFETY A":
                    selectedInputForm = ipmForm;
                    selectedOutputForm = outputIpmFile;
                    ApplyMergedParameters(workOrder, actionParams, defaultValues);
                    break;
                case "ACCEPTANCE TEST/COMMISSION":
                    selectedInputForm = accForm;
                    selectedOutputForm = outputAccFile;
                    ApplyMergedParameters(workOrder, actionParams, defaultValues);
                    break;
                default:
                    selectedInputForm = string.Empty;
                    selectedOutputForm = string.Empty;
                    break;
            }

            return new Dictionary<string, string> { { selectedInputForm, selectedOutputForm } };
        }

        private static void ApplyMergedParameters(WorkOrder.WorkOrder workOrder, string actionParams, string defaultValues)
        {
            if (!string.IsNullOrEmpty(actionParams))
            {
                string merged = Dict1Dict2ToString(actionParams.Trim(), defaultValues.Trim());
                workOrder.Action = merged.Length > 0 ? merged.Substring(0, merged.Length - 1) : string.Empty;
            }
            else
            {
                workOrder.Action = defaultValues;
            }
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
