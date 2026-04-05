using System.Collections.Generic;
using System.Xml.Serialization;

namespace AutoCompleteForm
{
    public class Models
    {
        [XmlElement("model")]
        public List<Model> ListOfModel { get; set; } = new List<Model>();
    }
}
