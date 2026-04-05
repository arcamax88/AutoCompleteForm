using System;
using WorkOrder;

namespace AutoCompleteForm
{
    public class ProcessWorkOrderOfLoanUnit
    {
        public static WorkOrder.WorkOrder Run(Work_Orders workOrderLoanUnit)
        {
            WorkOrder.WorkOrder wo = new WorkOrder.WorkOrder(
                workOrderLoanUnit.WO_NUMBER,
                workOrderLoanUnit.EMPLOYEE_DESC,
                workOrderLoanUnit.STAT_DATETIME,
                workOrderLoanUnit.TAG_NUMBER,
                workOrderLoanUnit.SERIAL_NUM,
                workOrderLoanUnit.MANUFACTURER_DESC,
                workOrderLoanUnit.MODEL_NUM,
                workOrderLoanUnit.BUILDING_DESC,
                workOrderLoanUnit.LOCATION_DESC,
                workOrderLoanUnit.ACTION);

            string[] splitAction = workOrderLoanUnit.ACTION.Split(':');
            if (splitAction.Length < 2)
            {
                Console.WriteLine($"Warning: Loan unit work order {workOrderLoanUnit.WO_NUMBER} has no equipment details in Action field.");
                return wo;
            }

            string[] splitEqDetails = splitAction[1].Split(',');
            foreach (var dict in splitEqDetails)
            {
                string[] splitDict = dict.Split('=');
                if (splitDict.Length < 2)
                    continue;

                string key = splitDict[0].Trim().ToUpper();
                string value = splitDict[1].Trim().ToUpper();

                Console.WriteLine(key);

                if (key == "MANUFACTURER")
                    wo.ManufacturerName = value;
                else if (key == "MODEL")
                    wo.ModelName = value;
                else if (key == "SN")
                    wo.SerialNumber = value;
            }

            wo.ControlNumber = "LOAN UNIT";
            return wo;
        }
    }
}
