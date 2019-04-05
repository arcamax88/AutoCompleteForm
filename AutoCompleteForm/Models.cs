using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AutoCompleteForm
{
    public class Models
    {
        [XmlElement("model")]
        public List<Model> ListOfModel = new List<Model>();
    }
}
