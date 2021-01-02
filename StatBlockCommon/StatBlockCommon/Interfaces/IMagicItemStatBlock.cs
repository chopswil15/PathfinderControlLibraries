using System;
using CommonInterFacesDD;
namespace StatBlockCommon
{
    public interface IMagicItemStatBlock
    {
        bool Abjuration { get; set; }
        string AL { get; set; }
        string Aura { get; set; }
        string AuraStrength { get; set; }
        string BaseItem { get; set; }
        bool Broken { get; set; }
        string Cha { get; set; }
        string CL { get; set; }
        string Communication { get; set; }
        bool Conjuration { get; set; }
        string Cost { get; set; }
        int CostValue { get; set; }
        string DedicatedPowers { get; set; }
        string Description { get; set; }
        string Destruction { get; set; }
        bool Divination { get; set; }
        string Ego { get; set; }
        bool Enchantment { get; set; }
        EquipmentType EquipmentType { get; }
        bool Evocation { get; set; }
        string ExtraAbilities { get; set; }
        string full_text { get; set; }
        string Group { get; set; }
        int id { get; set; }
        bool Illusion { get; set; }
        string Int { get; set; }
        bool IsIntelligentItem { get; set; }
        string Languages { get; set; }
        bool LegendaryWeapon { get; set; }
        string LinkText { get; set; }
        string MagicItems { get; set; }
        bool MajorArtifactFlag { get; set; }
        bool Masterwork { get; set; }
        bool MinorArtifactFlag { get; set; }
        bool Mythic { get; set; }
        string name { get; set; }
        bool Necromancy { get; set; }
        string Powers { get; set; }
        string Price { get; set; }
        int PriceValue { get; set; }
        string Requirements { get; set; }
        string Scaling { get; set; }
        string Senses { get; set; }
        string Slot { get; set; }
        string Source { get; set; }
        string SpecialPurpose { get; set; }
        bool Transmutation { get; set; }
        bool Universal { get; set; }
        string Weight { get; set; }
        double? WeightValue { get; set; }
        string Wis { get; set; }
    }
}
