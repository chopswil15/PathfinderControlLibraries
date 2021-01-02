using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace StatBlockCommon.Feat_SB
{
    [Serializable]
    [XmlRootAttribute(ElementName = "Feat", IsNullable = false)]
    public class FeatStatBlock : IFeatStatBlock
    {       

        #region Properties
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string description { get; set; }
        public string prerequisites { get; set; }
        public string prerequisite_feats { get; set; }
        public string benefit { get; set; }
        public string normal { get; set; }
        public string special { get; set; }
        public string source { get; set; }
        public string full_text { get; set; }
        public bool teamwork { get; set; }
        public bool critical { get; set; }
        public bool grit { get; set; }
        public bool style { get; set; }
        public bool peformance { get; set; }
        public bool mythic { get; set; }
        public bool racial { get; set; }
        public bool companionfamiliar { get; set; }
        public bool achievement { get; set; }
        public string race_name { get; set; }
        public string note { get; set; }
        public string goal { get; set; }
        public string completion_benefit { get; set; }
        public bool multiples { get; set; }
        public string suggested_traits { get; set; }
        public string prerequisite_skills { get; set; }
        public bool panache { get; set; }
        public bool betrayal { get; set; }
        public bool targeting { get; set; }
        public bool esoteric { get; set; }
        public bool stare { get; set; }
        public bool weapon_mastery { get; set; }
        public bool item_mastery { get; set; }
        public bool armor_mastery { get; set; }
        public bool shield_mastery { get; set; }
        public bool blood_hex { get; set; }
        public bool trick { get; set; }
        public bool money { get; set; }

        #endregion Properties

        public FeatStatBlock()
        {
            name = string.Empty;
            type = string.Empty;
            description = string.Empty;
            prerequisites = string.Empty;
            prerequisite_feats = string.Empty;
            benefit = string.Empty;
            normal = string.Empty;
            special = string.Empty;
            source = string.Empty;
            full_text = string.Empty;
            teamwork = false;
            critical = false;
            style = false;
            peformance = false;
            grit = false;
            racial = false;
            race_name = string.Empty;
            note = string.Empty;
            goal = string.Empty;
            completion_benefit = string.Empty;
            multiples = false;
            suggested_traits = string.Empty;
            prerequisite_skills = string.Empty;
            mythic = false;
            panache = false;
            betrayal = false;
            targeting = false;
            esoteric = false;
            stare = false;
            weapon_mastery = false;
            item_mastery = false;
            armor_mastery = false;
            shield_mastery = false;
            blood_hex = false;
            trick = false;
            money = false;
        }       
    }
}
