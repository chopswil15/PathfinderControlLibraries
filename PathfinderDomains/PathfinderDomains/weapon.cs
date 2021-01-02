using System;
using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace PathfinderDomains {

    [Serializable]
    [XmlRootAttribute(ElementName = "Weapon", IsNullable = false)]
    public class weapon {
        public virtual int id { get; set; }
        public virtual string group { get; set; }
        public virtual string category { get; set; }
        public virtual string name { get; set; }
        public virtual string search_name { get; set; }
        public virtual string cost { get; set; }
        public virtual string damage_small { get; set; }
        public virtual string damage_medium { get; set; }
        public virtual string critical { get; set; }
        public virtual string range { get; set; }
        public virtual string weight { get; set; }
        public virtual string type { get; set; }
        public virtual string special { get; set; }
        public virtual bool bludgeoning { get; set; }
        public virtual bool piercing { get; set; }
        public virtual bool slashing { get; set; }
        public virtual bool brace { get; set; }
        public virtual bool disarm { get; set; }
        public virtual bool @double { get; set; }
        public virtual bool monk { get; set; }
        public virtual bool nonlethal { get; set; }
        public virtual bool reach { get; set; }
        public virtual bool sunder { get; set; }
        public virtual bool trip { get; set; }
        public virtual bool performance { get; set; }
        public virtual bool deadly { get; set; }
        public virtual bool distracting { get; set; }
        public virtual bool uses_ammunition { get; set; }
        public virtual string source { get; set; }
        public virtual bool eastern { get; set; }
        public virtual bool grapple { get; set; }
        public virtual string misfire { get; set; }
        public virtual int capacity { get; set; }
        public virtual bool firearm { get; set; }
        public virtual bool technological { get; set; }
        public virtual string capacity_technological { get; set; }
        public virtual string charge_usage { get; set; }
    }
}
