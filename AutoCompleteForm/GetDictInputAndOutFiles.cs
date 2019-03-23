using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCompleteForm
{
    public class GetDictInputAndOuputFiles
    {
        public static Dictionary<string, string> Action(WorkOrder.WorkOrder workOrder)
        {
            var dictInputAndOuputFilenames = new Dictionary<string, string>();
            string modelName = workOrder.ModelName;

            //1.Laerdal Suction Pump
            if (modelName == "LSU")
            {
                dictInputAndOuputFilenames.Add(string.Empty, string.Empty);
                //dictInputAndOuputFilenames = ConstantString.Aspirator;
            }
            //2.Radiant warmer
            else if (modelName == "IW932AEA" || modelName == "IW950" || modelName == "IW990AEA")
            {
                dictInputAndOuputFilenames.Add(string.Empty, string.Empty);
                //dictInputAndOuputFilenames = ConstantString.RadiantWarmer;
            }
            //3.Phototherapy
            else if (modelName == "3006-BTP")
            {
                dictInputAndOuputFilenames.Add(string.Empty, string.Empty);
                //dictInputAndOuputFilenames = ConstantString.Phototheraphy;
            }
            //4.Philips MX series monitor
            else if (modelName == "M3002A" || modelName == "866062")
            {
                dictInputAndOuputFilenames.Add(string.Empty, string.Empty);
                //dictInputAndOuputFilenames = ConstantString.PhilipsMX;
            }
            //5.Karl Storz Stack and operating theatre generic devices
            else if (modelName == "UP-DR80MD" || modelName == "WD250" || modelName == "EJ-MLA26EK1" || modelName == "20090519" || modelName == "TC300" ||
                modelName == "TC200EN" || modelName == "264320-20" || modelName == "20133120" || modelName == "KU.2422.902" ||
                modelName == "PROXENON 350" || modelName == "QUANTUM" || modelName == "VP4826" || modelName == "9826NB" || modelName == "20133120-1" ||
                 modelName == "TC200" || modelName == "UI 500 S1" || modelName == "20133101-1" ||
                 modelName == "18-98001" || modelName == "1898001" || modelName == "E9000")
            {
                dictInputAndOuputFilenames = SelectInputAndOutFiles.Action(workOrder, EqTypeGeneralDevices.GeneralDevicesIpmForm, EqTypeGeneralDevices.GeneralDevicesAcceptanceForm);
                ReadOrReplaceMeasuredValue.Action(workOrder, EqTypeGeneralDevices.GeneralDevicesParameters);
            }
            //6.breast pump
            else if (modelName == "SYMPHONY" || modelName == "PLATINUM")
            {
                dictInputAndOuputFilenames.Add(string.Empty, string.Empty);
                //dictInputAndOuputFilenames = ConstantString.BreastPump;
            }
            //7.syringe pumps
            else if (modelName == "NIKI T34" || modelName == "NIKI T34L")
            {
                dictInputAndOuputFilenames.Add(string.Empty, string.Empty);
                //dictInputAndOuputFilenames = ConstantString.SyringePumpNikiT34;
            }
            //8.general devices
            else if (modelName == "ACCUMAX" || modelName == "PT101AN" || modelName == "PT101AZ" || modelName == "4500" ||
                modelName == "AP-206000" || modelName == "1037632" || modelName == "ICEMAN CLEAR" || modelName == "48744")
            {
                dictInputAndOuputFilenames = SelectInputAndOutFiles.Action(workOrder, EqTypeGeneralDevices.GeneralDevicesIpmForm, EqTypeGeneralDevices.GeneralDevicesAcceptanceForm);
                ReadOrReplaceMeasuredValue.Action(workOrder, EqTypeGeneralDevices.GeneralDevicesParameters);
            }
            //9.infusion pump nutricia infinity flocare
            //new
            else if (modelName == "FLOCARE INFINITY")
            {
                dictInputAndOuputFilenames = SelectInputAndOutFiles.Action(workOrder, EqTypeFeedingPumps.InfusionPumpNutriciaFlocareInfinityIpmForm, EqTypeFeedingPumps.InfusionPumpNutriciaFlocareInfinityAcceptanceForm);
                ReadOrReplaceMeasuredValue.Action(workOrder, EqTypeFeedingPumps.InfusionPumpNutriciaFlocareInfinityParameters);
            }
            //10.infusion pump plum a
            else if (modelName == "PLUM A+")
            {
                dictInputAndOuputFilenames = SelectInputAndOutFiles.Action(workOrder, EqTypeVolumetricInfusionPumps.InfusionPumpPlumAIpmForm, EqTypeVolumetricInfusionPumps.InfusionPumpPlumAAcceptanceForm);
                ReadOrReplaceMeasuredValue.Action(workOrder, EqTypeVolumetricInfusionPumps.InfusionPumpPlumAParameters);
            }
            //11.infusion pump cadd solis
            else if (modelName == "CADD-SOLIS")
            {
                dictInputAndOuputFilenames.Add(string.Empty, string.Empty);
                //dictInputAndOuputFilenames = ConstantString.InfusionPumpCaddSolis;
            }
            //12.welch allyn connex vital sign monitors
            else if (modelName == "75CT-6" || modelName == "68NXTX" || modelName == "75CT-B")
            {
                dictInputAndOuputFilenames = SelectInputAndOutFiles.Action(workOrder, EqTypeVitalSignMonitors.WelchAllynConnexIpmForm, EqTypeVitalSignMonitors.WelchAllynConnexAcceptanceForm);
                ReadOrReplaceMeasuredValue.Action(workOrder, EqTypeVitalSignMonitors.WelchAllynConnexParameters);
            }
            //13.alaris asena syringe pumps
            else if (modelName == "ASENA GH" || modelName == "8002MED01-G" || modelName == "9002MED01-G" || modelName == "80053UN01")
            {
                dictInputAndOuputFilenames.Add(string.Empty, string.Empty);
                //dictInputAndOuputFilenames = ConstantString.CarefusionAsenadictInputAndOuputFilenames;
            }
            //14.GE Carescape monitors and modules
            else if (modelName == "B450" || modelName == "B40" || modelName == "B650")
            {
                dictInputAndOuputFilenames = SelectInputAndOutFiles.Action(workOrder, EqTypeBedsideMonitors.GeCarescapeMonitorIpmForm, EqTypeBedsideMonitors.GeCarescapeMonitorAcceptanceForm);
                ReadOrReplaceMeasuredValue.Action(workOrder, EqTypeBedsideMonitors.GeCarescapeMonitorParameters);
            }
            //15.patient warmers
            else if (modelName == "MEDITHERM" || modelName == "5016000" || modelName == "501-5900" || modelName == "WT6000")
            {
                dictInputAndOuputFilenames.Add(string.Empty, string.Empty);
                //dictInputAndOuputFilenames = ConstantString.PatientWarmer;
            }
            //16.pro-tens
            //new
            else if (modelName == "PROTENS" || modelName == "PRO-TENS")
            {
                dictInputAndOuputFilenames.Add(string.Empty, string.Empty);
                //dictInputAndOuputFilenames = ConstantString.ProTens;
            }
            //17.pulse oximeters
            else if (modelName == "RAD-5" || modelName == "NPB-85" || modelName == "N-65")
            {
                dictInputAndOuputFilenames.Add(string.Empty, string.Empty);
                // dictInputAndOuputFilenames = ConstantString.PulseOximeter;
            }
            //18.ctg
            else if (modelName == "FM30" || modelName == "173")
            {
                dictInputAndOuputFilenames.Add(string.Empty, string.Empty);
                //dictInputAndOuputFilenames = ConstantString.Ctg;
            }
            //19.ecg
            else if (modelName == "TC50" || modelName == "TC70")
            {
                //dictInputAndOuputFilenames.Add(string.Empty, string.Empty);
                dictInputAndOuputFilenames = SelectInputAndOutFiles.Action(workOrder, EqTypeECGMachines.PhilipsTC50IpmForm, EqTypeECGMachines.PhilipsTC50AcceptanceForm);
                ReadOrReplaceMeasuredValue.Action(workOrder, EqTypeECGMachines.PhilipsTC50Parameters);
            }
            //20.telemetry
            else if (modelName == "APEXPRO CH")
            {
                dictInputAndOuputFilenames.Add(string.Empty, string.Empty);
                //dictInputAndOuputFilenames = ConstantString.Telemetry;
            }
            //21.nebuliser
            else if (modelName == "PORTA NEB" || modelName == "PORTA-NEB" || modelName == "INNOSPIRE DELUXE")
            {
                dictInputAndOuputFilenames.Add(string.Empty, string.Empty);
                //dictInputAndOuputFilenames = ConstantString.Nebuliser;
            }
            //22.fetal heart monitor
            else if (modelName == "FD3" || modelName == "D930" || modelName == "SONICAID ONE" || modelName == "FD1")
            {
                dictInputAndOuputFilenames.Add(string.Empty, string.Empty);
                //dictInputAndOuputFilenames = ConstantString.FetalHeartMonitor;
            }
            //23.humidier
            else if (modelName == "MR850")
            {
                dictInputAndOuputFilenames.Add(string.Empty, string.Empty);
                //dictInputAndOuputFilenames = ConstantString.FisherAndPaykelMR850;
            }
            //24.blood warmer
            else if (modelName == "BW685S")
            {
                dictInputAndOuputFilenames.Add(string.Empty, string.Empty);
                //dictInputAndOuputFilenames = ConstantString.BloodAndFluidWarmer;
            }

            //26.electronic thermometer
            else if (modelName == "692" || modelName == "PRO 4000" || modelName == "SURETEMP PLUS" || modelName == "690" || modelName == "PRO6000" || modelName == "THERMOSCAN PRO 4000")
            {
                dictInputAndOuputFilenames = SelectInputAndOutFiles.Action(workOrder, EqTypeElectronicThermometer.ElectronicThermometerIpmForm, EqTypeElectronicThermometer.ElectronicThermometerAcceptanceForm);
                ReadOrReplaceMeasuredValue.Action(workOrder, EqTypeElectronicThermometer.ElectronicThermometerParameter);
            }
            //27.Covidien AV Impulse SCD
            else if (modelName == "AV 6000")
            {
                dictInputAndOuputFilenames = SelectInputAndOutFiles.Action(workOrder, EqTypeSequentialCompressionDevices.CovidienAvImpulseIpmForm, EqTypeSequentialCompressionDevices.CovidienAvImpulseAcceptanceForm);
                ReadOrReplaceMeasuredValue.Action(workOrder, EqTypeSequentialCompressionDevices.CovidienAvImpulseParameters);
            }
            //28.Covidien SCD 700
            else if (modelName == "SCD 700")
            {
                dictInputAndOuputFilenames = SelectInputAndOutFiles.Action(workOrder, EqTypeSequentialCompressionDevices.CovidienScd700IpmForm, EqTypeSequentialCompressionDevices.CovidienScd700AcceptanceForm);
                ReadOrReplaceMeasuredValue.Action(workOrder, EqTypeSequentialCompressionDevices.CovidienScd700Parameters);
            }
            //loan unit class 1 
            else if (modelName == "RDS7A")
            {
                dictInputAndOuputFilenames = SelectInputAndOutFiles.Action(workOrder, EqTypeGeneralDeviceClass1.GeneralDevicesIpmForm, EqTypeGeneralDeviceClass1.GeneralDevicesAcceptanceForm);
                ReadOrReplaceMeasuredValue.Action(workOrder, EqTypeGeneralDeviceClass1.GeneralDevicesParameters);
            }
            //loan unit class 2
            else if (modelName == "SAMPLE")
            {
                dictInputAndOuputFilenames = SelectInputAndOutFiles.Action(workOrder, EqTypeSequentialCompressionDevices.CovidienScd700IpmForm, EqTypeSequentialCompressionDevices.CovidienScd700AcceptanceForm);
                ReadOrReplaceMeasuredValue.Action(workOrder, EqTypeSequentialCompressionDevices.CovidienScd700Parameters);
            }
            //
            else
            {
                dictInputAndOuputFilenames.Add(string.Empty, string.Empty);
            }

            return dictInputAndOuputFilenames;
        }
    }
}
