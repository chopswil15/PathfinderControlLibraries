using CommonInterFacesDD;
using CommonStatBlockInfo;

using EquipmentBasic;
using EquipmentBusiness;
using MagicItemAbilityWrapper;
using PathfinderGlobals;
using StatBlockBusiness;
using StatBlockCommon;
using StatBlockCommon.MagicItem_SB;
using StatBlockCommon.ReflectionWrappers;
using StatBlockParsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities;

namespace StatBlockChecker
{
    public class EquipmentParser : IEquipmentParser
    {
        private ISizeData _sizeData;
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        private IArmorClassData _armorClassData;
        private IEquipmentData _equipmentData;
        private IMagicItemStatBlockBusiness _magicItemStatBlockBusiness;
        private IWeaponBusiness _weaponBusiness;
        private IArmorBusiness _armorBusiness;
        private IEquipmentGoodsBusiness _equipmentGoodsBusiness;

        public EquipmentParser(ISBCheckerBaseInput sbCheckerBaseInput, ISizeData sizeData, IArmorClassData armorClassData,
                IEquipmentData equipmentData, IMagicItemStatBlockBusiness magicItemStatBlockBusiness, IWeaponBusiness weaponBusiness,
                IArmorBusiness armorBusiness, IEquipmentGoodsBusiness equipmentGoodsBusiness)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _sizeData = sizeData;
            _equipmentData = equipmentData;
            _equipmentData.Weapons = new Dictionary<IEquipment, int>();
            _equipmentData.Armor = new Dictionary<IEquipment, int>();
            _equipmentData.EquipementRoster = new Dictionary<IEquipment, int>();
            _equipmentData.MagicItemAbilities = new List<MagicItemAbilitiesWrapper>();
            _armorClassData = armorClassData;
            _magicItemStatBlockBusiness = magicItemStatBlockBusiness;
            _weaponBusiness = weaponBusiness;
            _armorBusiness = armorBusiness;
            _equipmentGoodsBusiness = equipmentGoodsBusiness;
        }

        public void ParseEquipment(IMonSBSearch _monSBSearch)
        {
            string gear = _sbCheckerBaseInput.MonsterSB.Gear.Replace("*", string.Empty).Trim();
            string otherGear = _sbCheckerBaseInput.MonsterSB.OtherGear.Replace("*", string.Empty).Trim();
            Utility.ParenCommaFix(ref otherGear);
            Utility.ParenCommaFix(ref gear);

            if (gear.Contains("CL "))
            {
                // HandleCLCommas(ref gear);
            }

            List<string> gearHold = gear.Split(',').ToList();
            if (otherGear != null)
            {
                gearHold.AddRange(otherGear.Split(',').ToList());
            }

            if (_sbCheckerBaseInput.MonsterSB.Environment.Length > 0)
            {
                if (_sbCheckerBaseInput.MonsterSB.Treasure.Contains("NPC Gear"))
                {
                    gear = _sbCheckerBaseInput.MonsterSB.Treasure;
                    int Pos = gear.IndexOf(PathfinderConstants.PAREN_LEFT);
                    if (Pos != -1)
                    {
                        gear = gear.Replace(gear.Substring(0, Pos - 1), string.Empty);
                        Pos = gear.IndexOf(PathfinderConstants.PAREN_RIGHT);
                        gear = gear.Substring(0, Pos);
                        gear = gear.Replace(PathfinderConstants.PAREN_LEFT, string.Empty);
                        gearHold.AddRange(gear.Split(',').ToList());
                    }
                }
                if (_sbCheckerBaseInput.MonsterSB.Treasure.Contains("standard ("))
                {
                    gear = _sbCheckerBaseInput.MonsterSB.Treasure;
                    int Pos = gear.IndexOf(PathfinderConstants.PAREN_LEFT);
                    gear = gear.Replace(gear.Substring(0, Pos - 1), string.Empty);
                    Pos = gear.IndexOf(PathfinderConstants.PAREN_RIGHT);
                    gear = gear.Substring(0, Pos);
                    gear = gear.Replace(PathfinderConstants.PAREN_LEFT, string.Empty);
                    gearHold.AddRange(gear.Split(',').ToList());
                }
                else if (_sbCheckerBaseInput.MonsterSB.Treasure.Contains(" ("))
                {
                    gear = _sbCheckerBaseInput.MonsterSB.Treasure;
                    int Pos = gear.IndexOf(PathfinderConstants.PAREN_LEFT);
                    gear = gear.Replace(gear.Substring(0, Pos - 1), string.Empty);
                    Pos = gear.IndexOf(PathfinderConstants.PAREN_RIGHT);
                    gear = gear.Substring(0, Pos);
                    gear = gear.Replace(PathfinderConstants.PAREN_LEFT, string.Empty);
                    gearHold.AddRange(gear.Split(',').ToList());
                }
            }


            gearHold.ForEach(x => x = x.Trim());
            gearHold.RemoveAll(x => x == string.Empty);

            foreach (string super in _sbCheckerBaseInput.SourceSuperScripts)
            {
                for (int a = 0; a < gearHold.Count; a++)
                {
                    gearHold[a] = gearHold[a].Replace(super, string.Empty);
                }
            }

            try
            {
                _equipmentData.Weapons = Equipment_Parse.ParseWeaponsSimple(gearHold, _weaponBusiness, _magicItemStatBlockBusiness);
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("ParseEquipment", ex.Message);
            }

            Weapon weapon = Equipment_Parse.GetUnarmedStrikeWeapon(_weaponBusiness);  //everyone can do unarmed strike

            if(_sbCheckerBaseInput.MonsterSB.Melee.Contains("incorporeal touch"))
            {
                IncorporealTouch incorporealTouch = Equipment_Parse.GetIncorporealTouchWeapon();
                int posIncorporealTouchStart = _sbCheckerBaseInput.MonsterSB.Melee.IndexOf("incorporeal touch");
                int posIncorporealTouchStop = _sbCheckerBaseInput.MonsterSB.Melee.IndexOf(PathfinderConstants.PAREN_RIGHT, posIncorporealTouchStart);
                string incorporealTouchString = _sbCheckerBaseInput.MonsterSB.Melee.Substring(posIncorporealTouchStart, posIncorporealTouchStop - posIncorporealTouchStart);
                incorporealTouch.DameageStrings = incorporealTouchString.Split(new string[] { " plus " }, StringSplitOptions.RemoveEmptyEntries).ToList();

                _equipmentData.Weapons.Add(incorporealTouch, 1);
            }


            if (_sbCheckerBaseInput.MonsterSB.Class.Contains("monk") || _sbCheckerBaseInput.MonsterSB.Class.Contains("brawler"))
            {
                int naturalWeaponMod = 0;
                string naturalWeaponString;
                if (_monSBSearch.HasGear("amulet of mighty fists"))
                {
                    naturalWeaponString = _monSBSearch.GetGearString("amulet of mighty fists");
                    if (naturalWeaponString.Contains("+"))
                    {
                        List<string> specialAbilities = CommonMethods.GetAmuletOfMightFistsSpecialAbilities();
                        foreach (string ability in specialAbilities)
                        {
                            if (naturalWeaponString.Contains(ability)) naturalWeaponString = naturalWeaponString.Replace(ability, string.Empty);
                        }
                        int Pos = naturalWeaponString.IndexOf("+");
                        naturalWeaponMod = int.Parse(naturalWeaponString.Substring(Pos + 1));
                    }
                }
                Weapon weapon2 = Equipment_Parse.GetUnarmedStrikeWeapon(_weaponBusiness);
                StatBlockInfo.HDBlockInfo MonkUAD = FindSpecialClassUnarmedDamage(_sizeData.SizeCat);
                weapon2.damage_medium = MonkUAD.ToString();
                weapon2.damage_small = MonkUAD.ToString();
                weapon2.WeaponSize = _sizeData.SizeCat;
                naturalWeaponString = "flurry of blows";
                if (naturalWeaponMod > 0)
                {
                    naturalWeaponString = "+" + naturalWeaponMod.ToString() + PathfinderConstants.SPACE + naturalWeaponString;
                    weapon2.EnhancementBonus = naturalWeaponMod;
                    weapon2.Masterwork = true;
                }
                weapon2.name = naturalWeaponString;
                weapon2.search_name = "flurry of blows";

                _equipmentData.Weapons.Add(weapon2, 1);

                int monkLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Monk");
                if (monkLevel == 0) monkLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("brawler");
                if (_monSBSearch.HasGear("monk's robe")) monkLevel += 5;
                StatBlockInfo.HDBlockInfo MonkUAD2 = StatBlockInfo.GetSpecialClassUnarmedDamage(monkLevel, _sizeData.SizeCat);
                naturalWeaponString = "unarmed strike";
                if (naturalWeaponMod > 0)
                {
                    naturalWeaponString = "+" + naturalWeaponMod.ToString() + PathfinderConstants.SPACE + naturalWeaponString;
                    weapon.EnhancementBonus = naturalWeaponMod;
                    weapon.Masterwork = true;
                }
                weapon.name = naturalWeaponString;
                weapon.search_name = "unarmed strike";
                StatBlockInfo.HDBlockInfo tempUAD = StatBlockInfo.GetSpecialClassUnarmedDamage(monkLevel, StatBlockInfo.SizeCategories.Medium);
                weapon.damage_medium = tempUAD.ToString();
                tempUAD = StatBlockInfo.GetSpecialClassUnarmedDamage(monkLevel, StatBlockInfo.SizeCategories.Small);
                weapon.damage_small = tempUAD.ToString();
                weapon.WeaponSize = _sizeData.SizeCat;
            }

            _equipmentData.Weapons.Add(weapon, 1);

            if (_sbCheckerBaseInput.MonsterSB.Class.Contains("barbarian"))
            {
                if (_sbCheckerBaseInput.MonsterSB.Melee.Contains("bite"))
                    _equipmentData.Weapons.Add(Equipment_Parse.GetBarbarianBite(), 1);
            }

            if (_sbCheckerBaseInput.MonsterSB.Class.Contains("alchemist"))
            {
                if (_sbCheckerBaseInput.MonsterSB.Ranged.Contains("bomb"))
                    _equipmentData.Weapons.Add(Equipment_Parse.GetBomb(), 1);
            }

            if (_sbCheckerBaseInput.MonsterSB.SpecialAttacks.Contains("rock throwing"))
                _equipmentData.Weapons.Add(Equipment_Parse.AddRockAsWeapon(), 1);


            _equipmentData.Armor = Equipment_Parse.ParseArmorSimple(gearHold, _armorBusiness);
            _monSBSearch.Armor = _equipmentData.Armor;
                        
            StatBlockInfo.ArmorProficiencies tempArmorPro = _sbCheckerBaseInput.CharacterClasses.GetAllKnownArmorProficiencies();
            StatBlockInfo.ShieldProficiencies tempShieldPro = _sbCheckerBaseInput.CharacterClasses.GetAllKnownShieldProficiencies();

            if (_monSBSearch.HasFeat("Light _equipmentData.Armor Proficiency")) tempArmorPro |= StatBlockInfo.ArmorProficiencies.Light;
            if (_monSBSearch.HasFeat("Medium _equipmentData.Armor Proficiency")) tempArmorPro |= StatBlockInfo.ArmorProficiencies.Medium;
            if (_monSBSearch.HasFeat("Heavy _equipmentData.Armor Proficiency")) tempArmorPro |= StatBlockInfo.ArmorProficiencies.Heavy;

            if (_monSBSearch.HasFeat("Shield Proficiency")) tempShieldPro |= StatBlockInfo.ShieldProficiencies.Shield;
            if (_monSBSearch.HasFeat("Tower Shield Proficiency")) tempShieldPro |= StatBlockInfo.ShieldProficiencies.Tower;

            Armor armor;
            foreach (KeyValuePair<IEquipment, int> kvp in _equipmentData.Armor)
            {
                if (kvp.Key is Armor)
                {
                    armor = (Armor)kvp.Key;
                    if (armor.category == "shield")
                    {
                        StatBlockInfo.ShieldProficiencies shieldCategory = armor.GetShieldCategory();
                        if ((!tempShieldPro.HasFlag(shieldCategory) && (_sbCheckerBaseInput.MonsterSB.DontUseRacialHD || (_sbCheckerBaseInput.Race_Base != null && _sbCheckerBaseInput.Race_Base.RaceBaseType == RaceBase.RaceType.Race))))
                        {
                            if (tempShieldPro.HasFlag(StatBlockInfo.ShieldProficiencies.Extra))
                            {
                                List<string> extraShield = _sbCheckerBaseInput.CharacterClasses.GetAllShieldProficienciesExtra();
                                if (!extraShield.Contains(armor.name))
                                    _sbCheckerBaseInput.MessageXML.AddFail("Missing Shield Proficiency", armor.name + " needs " + shieldCategory.ToString());
                            }
                            else
                                _sbCheckerBaseInput.MessageXML.AddFail("Missing Shield Proficiency", armor.name + " needs " + shieldCategory.ToString());
                        }
                    }
                    if (armor.category.Contains("armor"))
                    {
                        StatBlockInfo.ArmorProficiencies armorCategory = armor.GetArmorCategory();
                        if ((tempArmorPro & armorCategory) != armorCategory && (_sbCheckerBaseInput.MonsterSB.DontUseRacialHD || (_sbCheckerBaseInput.Race_Base != null && _sbCheckerBaseInput.Race_Base.RaceBaseType == RaceBase.RaceType.Race)))
                            _sbCheckerBaseInput.MessageXML.AddFail("Missing _equipmentData.Armor Proficiency", armor.name + " needs " + armorCategory.ToString());

                    }
                }
                else if (kvp.Key is Weapon) //shield as weapon
                    _equipmentData.Weapons.Add(kvp.Key, kvp.Value);
            }

            _equipmentData.EquipementRoster = Equipment_Parse.ParseMagicItems(gearHold, _equipmentData.EquipementRoster,
                _magicItemStatBlockBusiness, _weaponBusiness, _armorBusiness, _equipmentGoodsBusiness);

            foreach (KeyValuePair<IEquipment, int> kvp in _equipmentData.EquipementRoster)
            {
                IEquipment hold = kvp.Key;
                if (hold.EquipmentType == EquipmentType.MagicItem)
                    ApplyMagicItem(hold);
            }

            foreach (string missing in gearHold)
            {
                _sbCheckerBaseInput.MessageXML.AddInfo("Missing Gear Info: " + missing);
            }           
        }

        private string ApplyMagicItem(IEquipment MagicItem)
        {
            StatBlockInfo.ACMods acMods_Computed = new StatBlockInfo.ACMods();
            MagicItemStatBlock MI = (MagicItemStatBlock)MagicItem;
            try
            {
                MagicItemAbilitiesWrapper wrapper = MagicItemAbilityReflectionWrapper.GetMagicItemAbility(MI.name, MI.ExtraAbilities);

                _sbCheckerBaseInput.IndvSB.ApplyMagicItem(MagicItem);

                if (wrapper.EquimentBaseString.Length > 0)
                {
                    Dictionary<IEquipment, int> tempArmor = Equipment_Parse.ParseArmorSimple(new List<string> { wrapper.EquimentBaseString },_armorBusiness);

                    if (tempArmor.Keys.First().EquipmentType == EquipmentType.Armor)
                    {
                        double Bonus = 0;
                        Armor armor = (Armor)tempArmor.Keys.First();
                        if (armor.category == "shield")
                        {
                            if (armor.bonus.HasValue)
                            {
                                Bonus = Convert.ToInt32(armor.bonus);
                                if (armor.Broken)
                                    Bonus = Math.Floor(Bonus / 2);

                                acMods_Computed.Shield += Convert.ToInt32(Bonus) + armor.EnhancementBonus;
                            }
                        }
                        if (armor.category.Contains("armor"))
                        {
                            if (armor.bonus.HasValue)
                            {
                                Bonus = Convert.ToInt32(armor.bonus);
                                if (armor.Broken)
                                    Bonus = Math.Floor(Bonus / 2);

                                acMods_Computed.Armor += Convert.ToInt32(Bonus) + armor.EnhancementBonus;
                            }
                        }
                    }
                }

                _equipmentData.MagicItemAbilities.Add(wrapper);
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("SBChecker-ApplyMagicItem", ex.Message + "--- " + ex.InnerException);
            }

            _armorClassData.ACMods_Computed = acMods_Computed;

            return MI.name + " abilities added.";
        }

        private StatBlockInfo.HDBlockInfo FindSpecialClassUnarmedDamage(StatBlockInfo.SizeCategories ClassSize)
        {
            int CLassLevel = 0;
            if (_sbCheckerBaseInput.CharacterClasses.HasClass("monk")) CLassLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Monk");
            else if (_sbCheckerBaseInput.CharacterClasses.HasClass("brawler")) CLassLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("brawler");

            return StatBlockInfo.GetSpecialClassUnarmedDamage(CLassLevel, ClassSize);
        }
    }   
}
