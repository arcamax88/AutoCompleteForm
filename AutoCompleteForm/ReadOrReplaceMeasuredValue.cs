using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCompleteForm
{
    public class ReadOrReplaceMeasuredValue
    {
        public static void Action(WorkOrder.WorkOrder workOrder, string _defaultValues)
        {
            string[] splitAction;
            string[] splitData;
            splitAction = workOrder.Action.Split(',');
            Console.WriteLine(splitAction[0].Trim());

            switch (splitAction.Length)
            {
                case 1:
                    {
                        splitData = splitAction[0].Split('=');
                        if (splitData[0].Trim() == "BTY")
                        {
                            workOrder.Action = workOrder.Action + "," + _defaultValues;
                        }
                        else
                        {
                            workOrder.Action = splitAction[0];
                        }
                        break;
                    }
                case 2:
                    {
                        workOrder.Action = splitAction[1];
                        break;
                    }
                case 3:
                    {
                        workOrder.Action = splitAction[1];
                        break;
                    }
                default:
                    {
                        workOrder.Action = _defaultValues;
                        break;
                    }

            }
        }
    }
}
