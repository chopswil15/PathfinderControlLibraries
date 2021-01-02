using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PathfinderDomains
{
    [Serializable]
    [XmlRootAttribute(ElementName = "TemplateRaceOverride", IsNullable = false)]
    public class TemplateRaceOverride
    {
        public TemplateRaceOverride() { }
        public virtual int Id { get; set; }
        public virtual string RaceName { get; set; }
        public virtual string TemplateName { get; set; }
        public virtual int OverrideActionTypeId { get; set; }
    }
}
