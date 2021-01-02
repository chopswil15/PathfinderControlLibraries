using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using CommonInterFacesDD;
using AutoMapper;
using PathfinderDomains;
using PathfinderContext.Services;
using Utilities;

namespace EquipmentBasic
{
    public class Weapon : IEquipment
    {
      //  private static WeaponService _weaponService;

        public bool Masterwork { get; set; }
        public int EnhancementBonus { get; set; }
        public bool Broken { get; set; }
        public bool Integrated { get; set; }
        public WeaponSpecialMaterials WeaponSpecialMaterial { get; set; }
        public WeaponSpecialAbilities WeaponSpecialAbilities { get; set; }
        public ShieldSpecialAbilities ShieldSpecialAbilities { get; set; }
        public StatBlockInfo.SizeCategories WeaponSize { get; set; }
        private StatBlockInfo.WeaponProficiencies WeaponProficiencies { get; set; }
        public int CompositeBonus { get; set; }
        public bool NamedWeapon { get; set; }
        public string BaseWeaponName { get; set; }

        public EquipmentType EquipmentType
        {
            get { return EquipmentType.Weapon; }
        }

        #region Properties
        public int id { get; set; }
        public string group { get; set; }
        public string category { get; set; }
        public string name { get; set; }
        public string search_name { get; set; }
        public string cost { get; set; }
        public string damage_small { get; set; }
        public string damage_medium { get; set; }
        public string critical { get; set; }
        public string range { get; set; }
        public string weight { get; set; }
        public string type { get; set; }
        public string special { get; set; }
        public bool bludgeoning { get; set; }
        public bool piercing { get; set; }
        public bool slashing { get; set; }
        public bool brace { get; set; }
        public bool disarm { get; set; }
        public bool @double { get; set; }
        public bool monk { get; set; }
        public bool nonlethal { get; set; }
        public bool reach { get; set; }
        public bool sunder { get; set; }
        public bool trip { get; set; }
        public bool performance { get; set; }
        public bool deadly { get; set; }
        public bool distracting { get; set; }
        public bool uses_ammunition { get; set; }
        public string source { get; set; }
        public bool eastern { get; set; }
        public bool grapple { get; set; }
        public string misfire { get; set; }
        public int capacity { get; set; }
        public bool firearm { get; set; }
        public bool technological { get; set; }
        public string capacity_technological { get; set; }
        public string charge_usage { get; set; }

        #endregion Properties

        static Weapon()
        {
            Mapper.CreateMap<Weapon, weapon>();
            Mapper.CreateMap<weapon, Weapon>();
        }

        private static weapon MapThisToWeaponObject(Weapon SB)
        {
            return Mapper.Map<Weapon, weapon>(SB);
        }

        private static Weapon MapThisToWeaponObject(weapon Weapon)
        {
            return Mapper.Map<weapon, Weapon>(Weapon);
        }

        public Weapon()
        {
            WeaponSize = StatBlockInfo.SizeCategories.Medium; //default
            WeaponSpecialMaterial = WeaponSpecialMaterials.None;
            WeaponProficiencies = StatBlockInfo.WeaponProficiencies.None;
            CompositeBonus = 0;

            group = string.Empty;
            category = string.Empty;
            name = string.Empty;
            search_name = string.Empty;
            cost = string.Empty;
            damage_small = string.Empty;
            damage_medium = string.Empty;
            critical = string.Empty;
            range = string.Empty;
            weight = string.Empty;
            type = string.Empty;
            special = string.Empty;
            bludgeoning = false;
            piercing = false;
            slashing = false;
            brace = false;
            disarm = false;
            @double = false;
            monk = false;
            nonlethal = false;
            reach = false;
            trip = false;
            uses_ammunition = false;
            source = string.Empty;
            NamedWeapon = false;
            BaseWeaponName = string.Empty;
            grapple = false;
            firearm = false;
            misfire = string.Empty;
            capacity = 0;
            technological = false;
            capacity_technological = string.Empty;
            charge_usage = string.Empty;
        }

        public string Weapon_FullName()
        {
            string temp = string.Empty;
            if (EnhancementBonus != 0)
            {
                if (EnhancementBonus > 0)
                {
                    temp += "+" + EnhancementBonus.ToString() + Utility.SPACE;
                }
                else
                {
                    temp += EnhancementBonus.ToString() + Utility.SPACE;
                }
            }
            else if (Masterwork)
            {
                temp += "mwk ";
            }

            temp += WeaponSpecialAbilities.ToString();
            temp += Utility.SPACE + search_name.ToLower();
            return temp;
        }

        public StatBlockInfo.WeaponProficiencies GetWeaponCategory()
        {
            if (WeaponProficiencies == StatBlockInfo.WeaponProficiencies.None && group.Length > 0)
            {
                switch (group)
                {
                    case "Exotic Weapon":
                        WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Exotic;
                        break;
                    case "Martial Weapon":
                        WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Martial;
                        break;
                    case "Simple Weapon":
                        WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
                        break;
                    default:
                        WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.None;
                        break;
                }
            }

            return WeaponProficiencies;
        }


        public static Weapon GetWeaponByName(string name)
        {
            WeaponService _weaponService = new WeaponService(EquipmentGlobals.ConnectionString);
            weapon tempWeapon = _weaponService.GetWeaponByName(name);
            return MapThisToWeaponObject(tempWeapon);
        }
    }
}
