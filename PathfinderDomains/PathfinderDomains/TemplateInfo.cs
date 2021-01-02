using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PathfinderDomains
{
    [Serializable]
    [XmlRootAttribute(ElementName = "TemplateInfo", IsNullable = false)]
    public class TemplateInfo
    {
        public TemplateInfo() { }
        public virtual int id { get; set; }
        public virtual string TemplateName { get; set; }
        public virtual int TemplateOrderTypeId { get; set; }
    }
}
