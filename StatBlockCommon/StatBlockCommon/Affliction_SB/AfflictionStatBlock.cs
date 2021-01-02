using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace StatBlockCommon.Affliction_SB
{
    [Serializable]
    [XmlRootAttribute(ElementName = "Affliction", IsNullable = false)]
    public class AfflictionStatBlock 
    {

        #region Properties
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string onset { get; set; }
        public string frequency { get; set; }
        public string effect { get; set; }
        public string cure { get; set; }
        public string source { get; set; }
        public string fulltext { get; set; }
        public string cost { get; set; }
        public string save { get; set; }
        public bool poison { get; set; }
        public bool disease { get; set; }
        public bool curse { get; set; }
        public System.Nullable<int> save_value { get; set; }
        public string initial_effect { get; set; }
        public string secondary_effect { get; set; }
        public string description { get; set; }
        public string addiction { get; set; }
        public bool drug { get; set; }
        public string damage { get; set; }
        public string spell_effect { get; set; }
        public string special { get; set; }

        #endregion Properties      

        public AfflictionStatBlock()
        {
            name = string.Empty;
            type = string.Empty;
            onset = string.Empty;
            frequency = string.Empty;
            effect = string.Empty;
            cure = string.Empty;
            source = string.Empty;
            fulltext = string.Empty;
            cost = string.Empty;
            save = string.Empty;
            poison = false;
            curse = false;
            disease = false;
            initial_effect = string.Empty;
            secondary_effect = string.Empty;
            save_value = -1;
            description = string.Empty;
            addiction = string.Empty;
            drug = false;
            damage = string.Empty;
            spell_effect = string.Empty;
            special = string.Empty;
        }    
    }
}
