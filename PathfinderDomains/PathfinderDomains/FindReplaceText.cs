using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PathfinderDomains
{
    [Serializable]
    [XmlRootAttribute(ElementName = "FindReplaceText", IsNullable = false)]
    public class FindReplaceText
    {
        public FindReplaceText() { }
        public virtual int Id { get; set; }
        public virtual string FindText { get; set; }
        public virtual string ReplaceText { get; set; }
    }
}
