using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCompleteForm
{
    public class SelectInputAndOutFiles
    {
        public static Dictionary<string, string> Action(WorkOrder.WorkOrder workOrder, string ipmForm, string accForm)
        {
            Dictionary<string, string> forms = new Dictionary<string, string>();
            string selectedInputForm = string.Empty;
            string selectedOutputForm = string.Empty;
            string OutputIpmFile = ConstantString.IpmFormCompletedFolder + workOrder.ManufacturerName + "_" + workOrder.ModelName + "_" + workOrder.ControlNumber + "_" + workOrder.WorkOrderNumber + ".docx";
            string OutputAccFile = ConstantString.IpmFormCompletedFolder + "acc_" + workOrder.ManufacturerName + "_" + workOrder.ModelName + "_" + workOrder.ControlNumber + "_" + workOrder.WorkOrderNumber + ".docx";

            string[] splitAction;
            splitAction = workOrder.Action.Split(':');
            if (splitAction[0].TrimStart().TrimEnd() == "INSPECTION FUNCTION & SAFETY A - I02" || (splitAction[0].TrimStart().TrimEnd() == "INSPECTION FUNCTION & SAFETY A"))
            {
                selectedInputForm = ipmForm;
                selectedOutputForm = OutputIpmFile;
            }
            else if (splitAction[0].TrimStart().TrimEnd() == "ACCEPTANCE TEST/COMMISSION")
            {
                selectedInputForm = accForm;
                selectedOutputForm = OutputAccFile;
            }
            else
            {
                selectedInputForm = string.Empty;
                selectedOutputForm = string.Empty;
            }

            forms.Add(selectedInputForm, selectedOutputForm);
            return forms;
        }
    }
}
