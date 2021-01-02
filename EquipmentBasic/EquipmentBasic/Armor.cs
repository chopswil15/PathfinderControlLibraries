using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonInterFacesDD;
using CommonStatBlockInfo;
using PathfinderDomains;
using AutoMapper;
using PathfinderContext.Services;

namespace EquipmentBasic
{
    public class Armor : IEquipment
    {
        //private static ArmorService _armorService;

        public bool Masterwork { get; set; }
        public bool Broken { get; set; }
        public int EnhancementBonus { get; set; }
        public ArmorSpecialAbilities ArmorSpecialAbilities { get; set; }
        public ShieldSpecialAbilities ShieldSpecialAbilities { get; set; }
        public StatBlockInfo.SizeCategories ArmorSize { get; set; }
        private StatBlockInfo.ArmorProficiencies ArmorProficiencies { get; set; }
        private StatBlockInfo.ShieldProficiencies ShieldProficiencies { get; set; }
        public bool NamedArmor { get; set; }
        public string BaseArmorName { get; set; }
        public bool Spiked { get; set; }
        public bool Mithral { get; set; }

        public EquipmentType EquipmentType 
        {
            get { return EquipmentType.Armor; }
        }

        #region Properties
        public int id { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public string cost { get; set; }
        public short? bonus { get; set; }
        public short? max_dex_bonus { get; set; }
        public short? armor_check_penalty { get; set; }
        public short? spell_failure_percent { get; set; }
        public short? speed_30 { get; set; }
        public short? speed_20 { get; set; }
        public short? weight { get; set; }
        public string source { get; set; }
        public bool eastern { get; set; }
        #endregion Properties

        static Armor()
        {
            Mapper.CreateMap<Armor, armor>();
            Mapper.CreateMap<armor, Armor>();
        }

        private static armor MapThisToArmorObject(Armor SB)
        {
            return Mapper.Map<Armor, armor>(SB);
        }

        private static Armor MapThisToArmorObject(armor Armor)
        {
            return Mapper.Map<armor, Armor>(Armor);
        }

        public Armor()
        {
            ArmorSize = StatBlockInfo.SizeCategories.Medium; //default
            Broken = false;

            name = string.Empty;
            category = string.Empty;
            cost = string.Empty;
            bonus = null;
            max_dex_bonus = null;
            armor_check_penalty = null;
            spell_failure_percent = null;
            speed_30 = null;
            speed_20 = null;
            weight = null;
            source = string.Empty;
        }

        public StatBlockInfo.ArmorProficiencies GetArmorCategory()
        {
            if (ArmorProficiencies == StatBlockInfo.ArmorProficiencies.None && category.Length > 0)
            {
                switch (category)
                {
                    case "light armor":
                        ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
                        break;
                    case "medium armor":
                        ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Medium;
                        break;
                    case "heavy armor":
                        ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Heavy;
                        break;
                    default:
                        ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.None;
                        break;
                }
            }

            return ArmorProficiencies;
        }

        public StatBlockInfo.ShieldProficiencies GetShieldCategory()
        {
            if (ShieldProficiencies == StatBlockInfo.ShieldProficiencies.None && category.Length > 0)
            {
                switch (category)
                {
                    case "shield":
                        if (name == "tower shield")
                        {
                            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Tower;
                        }
                        else
                        {
                            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Shield;
                        }
                        break;
                                    
                    default:
                        ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.None;
                        break;
                }
            }

            return ShieldProficiencies;
        }

        public static Armor GetArmorByName(string name)
        {
            ArmorService _armorService = new ArmorService(EquipmentGlobals.ConnectionString);
            armor tempArmor = _armorService.GetArmorByName(name);
            return MapThisToArmorObject(tempArmor);
        }

        //private static void SetArmorService()
        //{
        //    ArmorService _armorService = new ArmorService(EquipmentGlobals.ConnectionString);
        //}

    }
}
