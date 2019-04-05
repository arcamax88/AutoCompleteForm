using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCompleteForm
{
    public class Model
    {
        private string _equipmentType;
        private string _modelNumber;
        private string _modelName;
        private string _constantParameters;
        private string _ipmForm;
        private string _acceptanceForm;

        public string EquipmentType
        {
            get { return _equipmentType; }
            set { _equipmentType = value; }
        }
        public string ModelNumber
        {
            get { return _modelNumber; }
            set { _modelNumber = value; }
        }
        public string ModelName
        { 
            get { return _modelName; }
            set { _modelName = value; }
        }
        public string ConstantParameters
        {
            get { return _constantParameters; }
            set { _constantParameters = value; }
        }
        public string IpmForm
        {
            get { return _ipmForm; }
            set { _ipmForm = value; }
        }
        public string AcceptanceForm
        {
            get { return _acceptanceForm; }
            set { _acceptanceForm = value; }
        }
    }
}
