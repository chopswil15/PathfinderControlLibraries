using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using CommonInterFacesDD;

namespace StatBlockCommon.MagicItem_SB
{
    [Serializable]
    [XmlRootAttribute(ElementName = "MagicItem", IsNullable = false)]
    public class MagicItemStatBlock : IEquipment, IMagicItemStatBlock
    {       
        public bool Masterwork { get; set; }
        public bool Broken { get; set; }
        public string ExtraAbilities { get; set; }
        public EquipmentType EquipmentType
        {
            get { return EquipmentType.MagicItem; }
        }


        #region Properties

        public int id { get; set; }
        public string name { get; set; }
        public string Aura { get; set; }
        public string CL { get; set; }
        public string Slot { get; set; }
        public string Price { get; set; }
        public string Weight { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public string Cost { get; set; }
        public string Group { get; set; }
        public string Source { get; set; }
        public string AL { get; set; }
        public string Int { get; set; }
        public string Wis { get; set; }
        public string Cha { get; set; }
        public string Ego { get; set; }
        public string Communication { get; set; }
        public string Senses { get; set; }
        public string Powers { get; set; }
        public string SpecialPurpose { get; set; }
        public string DedicatedPowers { get; set; }
        public string MagicItems { get; set; }
        public string full_text { get; set; }
        public string Destruction { get; set; }
        public bool MinorArtifactFlag { get; set; }
        public bool MajorArtifactFlag { get; set; }
        public bool Abjuration { get; set; }
        public bool Conjuration { get; set; }
        public bool Divination { get; set; }
        public bool Enchantment { get; set; }
        public bool Evocation { get; set; }
        public bool Necromancy { get; set; }
        public bool Transmutation { get; set; }
        public string AuraStrength { get; set; }
        public double? WeightValue { get; set; }
        public int PriceValue { get; set; }
        public int CostValue { get; set; }
        public string Languages { get; set; }
        public string LinkText { get; set; }
        public string BaseItem { get; set; }
        public bool Mythic { get; set; }
        public bool LegendaryWeapon { get; set; }
        public bool Illusion { get; set; }
        public bool Universal { get; set; }
        public string Scaling { get; set; }

        public bool IsIntelligentItem { get; set; }

        #endregion Properties      

        public MagicItemStatBlock()
        {            
            name = string.Empty;
            Aura = string.Empty;
            CL = string.Empty;
            Slot = string.Empty;
            Price = string.Empty;
            Weight = string.Empty;
            Description = string.Empty;
            Requirements = string.Empty;
            Cost = string.Empty;
            Group = string.Empty;
            Source = string.Empty;
            full_text = string.Empty;
            AL = string.Empty;
            Int = string.Empty;
            Wis = string.Empty;
            Cha = string.Empty;
            Ego = string.Empty;
            Senses = string.Empty;
            Communication = string.Empty;
            Powers = string.Empty;
            SpecialPurpose = string.Empty;
            DedicatedPowers = string.Empty;
            MagicItems = string.Empty;
            Destruction = string.Empty;
            MinorArtifactFlag = false;
            MajorArtifactFlag = false;            
            AuraStrength = string.Empty;
            Abjuration = false;
            Conjuration = false;
            Divination = false;
            Enchantment = false;
            Evocation = false; Necromancy = false;
            Transmutation = false;
            WeightValue = 0;
            PriceValue = 0;
            CostValue = 0;
            Languages = string.Empty;
            BaseItem = string.Empty;
            Mythic = false;
            LegendaryWeapon = false;
            Illusion = false;
            Universal = false;
            Scaling = string.Empty;
            IsIntelligentItem = false;

            Masterwork = true;
        }       
    }
}
