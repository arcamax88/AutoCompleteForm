using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCompleteForm
{
    public class EqTypeSuctionRegulator
    {
        //on / off only
        public const string SuctioOnAndOffIpmForm = ConstantString.IpmFormFolder + "suction_regulator_with_on_and_off_only.docx";
        public const string SuctioOnAndOffParameters = "VAR=NA";
        public const string SuctioOnAndOffAcceptanceForm = ConstantString.AcceptanceFormFolder + "acc_suction_regulator_with_on_and_off_only.docx";

        //on / off with reg
        public const string SuctioOnAndOffWithRegIpmForm = ConstantString.IpmFormFolder + "suction_regulator_with_reg_off_and_full.docx";
        public const string SuctioOnAndOffWithRegParameters = "VAR=NA";
        public const string SuctioOnAndOffWithRegAcceptanceForm = ConstantString.AcceptanceFormFolder + "acc_suction_regulator_with_reg_off_and_full.docx";
    }
}
