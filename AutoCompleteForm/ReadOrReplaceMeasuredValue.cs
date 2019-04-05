using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCompleteForm
{
    public class ReadOrReplaceMeasuredValue
    {
        public static void Action(WorkOrder.WorkOrder workOrder, string parameters)
        {
            string[] splitAction;
            splitAction = workOrder.Action.Split(':');
            if (splitAction.Length == 1)
            {
                workOrder.Action = parameters;
            }
            else if (splitAction.Length == 2)
            {
                workOrder.Action = splitAction[1];
            }
            else
            {
                workOrder.Action = "THIRD-PARTY";
            }
        }
    }
}
