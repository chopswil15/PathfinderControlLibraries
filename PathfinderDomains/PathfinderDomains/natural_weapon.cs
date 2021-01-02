using System;
using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace PathfinderDomains {

    [Serializable]
    [XmlRootAttribute(ElementName = "NaturalWeapon", IsNullable = false)]
    public class natural_weapon {
        public virtual int id { get; set; }
        public virtual string name { get; set; }
        public virtual string damage_fine { get; set; }
        public virtual string damage_diminutive { get; set; }
        public virtual string damage_tiny { get; set; }
        public virtual string damage_small { get; set; }
        public virtual string damage_medium { get; set; }
        public virtual string damage_large { get; set; }
        public virtual string damage_huge { get; set; }
        public virtual string damage_gargantuan { get; set; }
        public virtual string damage_colossal { get; set; }
        public virtual string damage_type { get; set; }
        public virtual string attack_type { get; set; }
        public virtual bool piercing { get; set; }
        public virtual bool slashing { get; set; }
        public virtual bool bludgeoning { get; set; }
    }
}
