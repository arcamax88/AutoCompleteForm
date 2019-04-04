using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOrder;

namespace AutoCompleteForm
{
    public class ProcessWorkOrderOfLoanUnit
    {
        public static WorkOrder.WorkOrder Start(Work_Orders workOrderLoanUnit)
        {
            WorkOrder.WorkOrder wo = new WorkOrder.WorkOrder(workOrderLoanUnit.WO_NUMBER,
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
            Console.WriteLine(splitAction[1]);
            string[] splitEqDetails = splitAction[1].Split(',');
            foreach (var dict in splitEqDetails)
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

            return wo;
        }
    }
}
