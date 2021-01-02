using System.Collections.Generic; 
using System.Text; 
using System;
using System.Xml.Serialization; 


namespace PathfinderDomains {

    [Serializable]
    [XmlRootAttribute(ElementName = "Spell", IsNullable = false)]
    public class spell {
        public spell() { }
        public virtual int id { get; set; }
        public virtual string name { get; set; }
        public virtual string school { get; set; }
        public virtual string subschool { get; set; }
        public virtual string descriptor { get; set; }
        public virtual string spell_level { get; set; }
        public virtual string casting_time { get; set; }
        public virtual string components { get; set; }
        public virtual bool costly_components { get; set; }
        public virtual string range { get; set; }
        public virtual string area { get; set; }
        public virtual string effect { get; set; }
        public virtual string targets { get; set; }
        public virtual string duration { get; set; }
        public virtual bool dismissible { get; set; }
        public virtual bool shapeable { get; set; }
        public virtual string saving_throw { get; set; }
        public virtual string spell_resistence { get; set; }
        public virtual string description { get; set; }
        public virtual string description_formated { get; set; }
        public virtual string source { get; set; }
        public virtual string full_text { get; set; }
        public virtual bool verbal { get; set; }
        public virtual bool somatic { get; set; }
        public virtual bool material { get; set; }
        public virtual bool focus { get; set; }
        public virtual bool divine_focus { get; set; }
        public virtual System.Nullable<int> sor { get; set; }
        public virtual System.Nullable<int> wiz { get; set; }
        public virtual System.Nullable<int> cleric { get; set; }
        public virtual System.Nullable<int> druid { get; set; }
        public virtual System.Nullable<int> ranger { get; set; }
        public virtual System.Nullable<int> bard { get; set; }
        public virtual System.Nullable<int> paladin { get; set; }
        public virtual System.Nullable<int> alchemist { get; set; }
        public virtual System.Nullable<int> summoner { get; set; }
        public virtual System.Nullable<int> witch { get; set; }
        public virtual System.Nullable<int> inquisitor { get; set; }
        public virtual System.Nullable<int> oracle { get; set; }
        public virtual System.Nullable<int> magus { get; set; }
        public virtual System.Nullable<int> antipaladin { get; set; }
        public virtual System.Nullable<int> adept { get; set; }
        public virtual System.Nullable<int> SLA_Level { get; set; }
        public virtual string deity { get; set; }
        public virtual string linktext { get; set; }
        public virtual string domain { get; set; }
        public virtual string short_description { get; set; }
        public virtual System.Nullable<bool> acid { get; set; }
        public virtual System.Nullable<bool> air { get; set; }
        public virtual System.Nullable<bool> chaotic { get; set; }
        public virtual System.Nullable<bool> cold { get; set; }
        public virtual System.Nullable<bool> curse { get; set; }
        public virtual System.Nullable<bool> darkness { get; set; }
        public virtual System.Nullable<bool> death { get; set; }
        public virtual System.Nullable<bool> disease { get; set; }
        public virtual System.Nullable<bool> earth { get; set; }
        public virtual System.Nullable<bool> electricity { get; set; }
        public virtual System.Nullable<bool> emotion { get; set; }
        public virtual System.Nullable<bool> evil { get; set; }
        public virtual System.Nullable<bool> fear { get; set; }
        public virtual System.Nullable<bool> fire { get; set; }
        public virtual System.Nullable<bool> force { get; set; }
        public virtual System.Nullable<bool> good { get; set; }
        public virtual System.Nullable<bool> language_dependent { get; set; }
        public virtual System.Nullable<bool> lawful { get; set; }
        public virtual System.Nullable<bool> light { get; set; }
        public virtual System.Nullable<bool> mind_affecting { get; set; }
        public virtual System.Nullable<bool> pain { get; set; }
        public virtual System.Nullable<bool> poison { get; set; }
        public virtual System.Nullable<bool> shadow { get; set; }
        public virtual System.Nullable<bool> sonic { get; set; }
        public virtual System.Nullable<bool> water { get; set; }
        public virtual System.Nullable<int> material_costs { get; set; }
        public virtual string mythic_text { get; set; }
        public virtual bool mythic { get; set; }
        public virtual string augmented { get; set; }
        public virtual System.Nullable<int> bloodrager { get; set; }
        public virtual System.Nullable<int> shaman { get; set; }
        public virtual System.Nullable<int> psychic { get; set; }
        public virtual System.Nullable<int> medium { get; set; }
        public virtual System.Nullable<int> mesmerist { get; set; }
        public virtual System.Nullable<int> occultist { get; set; }
        public virtual System.Nullable<int> spiritualist { get; set; }
        public virtual System.Nullable<int> skald { get; set; }
        public virtual System.Nullable<int> investigator { get; set; }
        public virtual System.Nullable<int> hunter { get; set; }
        public virtual string haunt_statistics { get; set; }
        public virtual System.Nullable<bool> ruse { get; set; }
        public virtual System.Nullable<bool> draconic { get; set; }
        public virtual System.Nullable<bool> meditative { get; set; }
    }
}
