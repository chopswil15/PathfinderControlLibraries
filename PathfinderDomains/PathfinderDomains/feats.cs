using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace PathfinderDomains
{
    [Serializable]
    [XmlRootAttribute(ElementName = "Feats", IsNullable = false)]
    public class feats
    {
        public feats() { }
        public virtual int id { get; set; }
        public virtual string name { get; set; }
        public virtual string type { get; set; }
        public virtual string description { get; set; }
        public virtual string prerequisites { get; set; }
        public virtual string prerequisite_feats { get; set; }
        public virtual string benefit { get; set; }
        public virtual string normal { get; set; }
        public virtual string special { get; set; }
        public virtual string source { get; set; }
        public virtual string full_text { get; set; }
        public virtual bool teamwork { get; set; }
        public virtual bool critical { get; set; }
        public virtual bool grit { get; set; }
        public virtual bool style { get; set; }
        public virtual bool performance { get; set; }
        public virtual bool racial { get; set; }
        public virtual bool companionfamiliar { get; set; }
        public virtual bool achievement { get; set; }
        public virtual string race_name { get; set; }
        public virtual string note { get; set; }
        public virtual string goal { get; set; }
        public virtual string completion_benefit { get; set; }
        public virtual bool multiples { get; set; }
        public virtual string suggested_traits { get; set; }
        public virtual bool mythic { get; set; }
        public virtual string prerequisite_skills { get; set; }
        public virtual bool panache { get; set; }
        public virtual bool betrayal { get; set; }
        public virtual bool targeting { get; set; }
        public virtual bool esoteric { get; set; }
        public virtual bool stare { get; set; }
        public virtual bool weapon_mastery { get; set; }
        public virtual bool item_mastery { get; set; }
        public virtual bool armor_mastery { get; set; }
        public virtual bool shield_mastery { get; set; }
        public virtual bool blood_hex { get; set; }
        public virtual bool trick { get; set; }
        public virtual bool money { get; set; }
    }
}
