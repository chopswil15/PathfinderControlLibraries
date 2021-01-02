using System;
using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace PathfinderDomains {

    [Serializable]
    [XmlRootAttribute(ElementName = "Armor", IsNullable = false)]
    public class armor {
        public virtual int id { get; set; }
        public virtual string name { get; set; }
        public virtual string category { get; set; }
        public virtual string cost { get; set; }
        public virtual short? bonus { get; set; }
        public virtual short? max_dex_bonus { get; set; }
        public virtual short? armor_check_penalty { get; set; }
        public virtual short? spell_failure_percent { get; set; }
        public virtual short? speed_30 { get; set; }
        public virtual short? speed_20 { get; set; }
        public virtual short? weight { get; set; }
        public virtual string source { get; set; }
        public virtual bool eastern { get; set; }
    }
}
