using System.Collections.Generic; 
using System.Text; 
using System;
using System.Xml.Serialization; 


namespace PathfinderDomains {

    [Serializable]
    [XmlRootAttribute(ElementName = "Affliction", IsNullable = false)]
    public class affliction {
        public affliction() { }
        public virtual int id { get; set; }
        public virtual string name { get; set; }
        public virtual string type { get; set; }
        public virtual string onset { get; set; }
        public virtual string frequency { get; set; }
        public virtual string effect { get; set; }
        public virtual string cure { get; set; }
        public virtual string source { get; set; }
        public virtual string fulltext { get; set; }
        public virtual string cost { get; set; }
        public virtual string save { get; set; }
        public virtual bool poison { get; set; }
        public virtual bool disease { get; set; }
        public virtual bool curse { get; set; }
        public virtual System.Nullable<int> save_value { get; set; }
        public virtual string initial_effect { get; set; }
        public virtual string secondary_effect { get; set; }
        public virtual string description { get; set; }
        public virtual string addiction { get; set; }
        public virtual bool drug { get; set; }
        public virtual string damage { get; set; }
        public virtual string spell_effect { get; set; }
        public virtual string special { get; set; }
    }
}
