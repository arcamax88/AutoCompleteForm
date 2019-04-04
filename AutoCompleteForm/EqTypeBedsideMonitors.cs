using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCompleteForm
{
    public class EqTypeBedsideMonitors
    {
        //GE Carescape Series
        public const string GeCarescapeMonitorIpmForm = ConstantString.IpmFormFolder + "ge_carescape_monitors.docx";
        public const string GeCarescapeMonitorParameters = "PARTNER=NA";
        public const string GeCarescapeMonitorAcceptanceForm = ConstantString.AcceptanceFormFolder + "acc_ge_patient_monitor_with_mms.docx";

        //Philips Intellivue Series
        public const string PhilipsIntellivueMonitorIpmForm = ConstantString.IpmFormFolder + "philips_intellivue_monitor.docx";
        public const string PhilipsIntellivueX2IpmForm = ConstantString.IpmFormFolder + "philips_intellivue_X2.docx";
        public const string PhilipsIntellivueCO2IpmForm = ConstantString.IpmFormFolder + "philips_intellivue_CO2.docx";
        public const string PhilipsIntellivueCO2Parameters = "MON=NA, ACC=38";
        public const string PhilipsIntellivueMonitorAcceptanceForm = ConstantString.AcceptanceFormFolder + "acc_philips_intellivue_monitor.docx";
    }
}
