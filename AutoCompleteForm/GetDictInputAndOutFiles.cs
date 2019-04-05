using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCompleteForm
{
    public class GetDictInputAndOuputFiles
    {
        public static Dictionary<string, string> Action(WorkOrder.WorkOrder workOrder, List<Model> models)
        {
            var dictInputAndOuputFilenames = new Dictionary<string, string>();
            string modelName = workOrder.ModelName;
            foreach (Model model in models)
            {
                if(modelName == model.ModelNumber.TrimStart().TrimEnd())
                {
                    dictInputAndOuputFilenames = SelectInputAndOutFiles.Action(workOrder, ConstantString.IpmFormFolder + model.IpmForm,
                                                    ConstantString.AcceptanceFormFolder + model.AcceptanceForm);
                    ReadOrReplaceMeasuredValue.Action(workOrder, model.ConstantParameters);
                }
            }
            return dictInputAndOuputFilenames;
        }
    }
}
