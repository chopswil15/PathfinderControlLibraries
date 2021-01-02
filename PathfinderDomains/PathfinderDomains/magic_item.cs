using System.Collections.Generic; 
using System.Text; 
using System;
using System.Xml.Serialization; 


namespace PathfinderDomains {

    [Serializable]
    [XmlRootAttribute(ElementName = "MagicItem", IsNullable = false)]
    public class magic_item {
        public magic_item() { }
        public virtual int id { get; set; }
        public virtual string name { get; set; }
        public virtual string Aura { get; set; }
        public virtual string CL { get; set; }
        public virtual string Slot { get; set; }
        public virtual string Price { get; set; }
        public virtual string Weight { get; set; }
        public virtual string Description { get; set; }
        public virtual string Requirements { get; set; }
        public virtual string Cost { get; set; }
        public virtual string Group { get; set; }
        public virtual string Source { get; set; }
        public virtual string AL { get; set; }
        public virtual string Int { get; set; }
        public virtual string Wis { get; set; }
        public virtual string Cha { get; set; }
        public virtual string Ego { get; set; }
        public virtual string Communication { get; set; }
        public virtual string Senses { get; set; }
        public virtual string Powers { get; set; }
        public virtual string SpecialPurpose { get; set; }
        public virtual string DedicatedPowers { get; set; }
        public virtual string MagicItems { get; set; }
        public virtual string full_text { get; set; }
        public virtual string Destruction { get; set; }
        public virtual bool MinorArtifactFlag { get; set; }
        public virtual bool MajorArtifactFlag { get; set; }
        public virtual bool Abjuration { get; set; }
        public virtual bool Conjuration { get; set; }
        public virtual bool Divination { get; set; }
        public virtual bool Enchantment { get; set; }
        public virtual bool Evocation { get; set; }
        public virtual bool Necromancy { get; set; }
        public virtual bool Transmutation { get; set; }
        public virtual string AuraStrength { get; set; }
        public virtual System.Nullable<double> WeightValue { get; set; }
        public virtual int PriceValue { get; set; }
        public virtual int CostValue { get; set; }
        public virtual string Languages { get; set; }
        public virtual string LinkText { get; set; }
        public virtual string BaseItem { get; set; }
        public virtual bool Mythic { get; set; }
        public virtual bool LegendaryWeapon { get; set; }
        public virtual bool Illusion { get; set; }
        public virtual bool Universal { get; set; }
        public virtual string Scaling { get; set; }
        public virtual bool IsIntelligentItem { get; set; }
    }
}
