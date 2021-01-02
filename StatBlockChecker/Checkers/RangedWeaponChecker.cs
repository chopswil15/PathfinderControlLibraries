using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipmentBasic;
using EquipmentBusiness;
using PathfinderGlobals;
using Utilities;
namespace StatBlockChecker
{
    public class RangedWeaponChecker : IRangedWeaponChecker
    {
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        private string _baseAtk;
        private string _size;
        private int _sizeMod;
        private WeaponChecker _weaponChecker;
        private int _racialHDValue;
        private string _raceWeapons;
        private IEquipmentData _equipmentData;
        private INaturalWeaponBusiness _naturalWeaponBusiness;
        private IWeaponBusiness _weaponBusiness;

        public RangedWeaponChecker(ISBCheckerBaseInput sbCheckerBaseInput, IEquipmentData equipmentData, 
              ISizeData sizeData, INaturalWeaponBusiness naturalWeaponBusiness, IWeaponBusiness weaponBusiness)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _equipmentData = equipmentData;
            _naturalWeaponBusiness = naturalWeaponBusiness;
            _weaponBusiness = weaponBusiness;
            _baseAtk = _sbCheckerBaseInput.MonsterSB.BaseAtk;
            _size = _sbCheckerBaseInput.MonsterSB.Size;
            _sizeMod = sizeData.SizeMod;
            _racialHDValue = _sbCheckerBaseInput.Race_Base.RacialHDValue();
            _raceWeapons = _sbCheckerBaseInput.Race_Base.RaceWeapons();
            _weaponChecker = new WeaponChecker(_sbCheckerBaseInput, _equipmentData, _naturalWeaponBusiness, _weaponBusiness);
        }

        public void CheckRangedWeapons(string rangedValues)
        {
            if (rangedValues.Length == 0) return;
            string[] temp = new string[] { " or" };
            List<string> Ranged = rangedValues.Split(temp, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (string rangedWeapon in Ranged)  //loop on "or" blocks
            {
                CheckOneRangedOrBlock(rangedWeapon);
            }
        }

        private void CheckOneRangedOrBlock(string rangedWeapon)
        {
            List<string> Found = new List<string>();
            List<string> Ranged = new List<string>();
            string[] temp2 = new string[] { " and" };
            if (rangedWeapon.Contains(" and "))
            {
                Ranged = rangedWeapon.Split(temp2, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            else if (rangedWeapon.Contains(", "))
            {
                temp2 = new string[] { ", " };
                Ranged = rangedWeapon.Split(temp2, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            else
            {
                Ranged.Add(rangedWeapon);
            }

            bool SimpleOne = false;

            foreach (string ranged in Ranged)
            {
                Weapon weapon = null;
                NaturalWeapon natural_weapon = null;
                WeaponCommon weaponCommon = new WeaponCommon(_sbCheckerBaseInput, _equipmentData, _naturalWeaponBusiness);
                bool MagicWeapon;
                bool GreaterMagicWeapon;

                if (weaponCommon.FindWeapon(ref weapon, ref natural_weapon, ranged.Trim(), out MagicWeapon, out GreaterMagicWeapon))
                {
                    Found.Add(ranged.Trim());
                    CheckOneRangedWeaponFound(weapon, ranged.Trim(), MagicWeapon, GreaterMagicWeapon, ref SimpleOne, _raceWeapons);
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("Ranged Attack", "Missing Weapon-" + rangedWeapon.Trim());
                }
            }
        }

        private void CheckOneRangedWeaponFound(Weapon weapon, string rangedWeapon, bool MagicWeapon, bool GreaterMagicWeapon,
                     ref bool SimpleOne, string _raceWeapons)
        {
            string holdRangedWeapon;
            List<string> Bonuses = null;
            string weaponsDamage = string.Empty;
            string weaponBonus = string.Empty;
            int weaponBonusComputed, AbilityBonus;
            int weaponBonusSB = 0;
            string formula = string.Empty;
            int RangeMod = 0;

            holdRangedWeapon = rangedWeapon;

            holdRangedWeapon = holdRangedWeapon.Replace("ranged touch", string.Empty);
            if (holdRangedWeapon.Contains("Rapid Shot"))
            {
                holdRangedWeapon = holdRangedWeapon.Replace("Rapid Shot", string.Empty).Trim();
                RangeMod -= 2;
            }

            _weaponChecker.ParseSingleWeapon(weapon, ref weaponBonus, ref weaponsDamage, ref holdRangedWeapon, ref Bonuses);
            _weaponChecker.CheckRangedWeaponDamage(weapon, weaponsDamage, _size, _sbCheckerBaseInput.AbilityScores, _sbCheckerBaseInput.MonsterSBSearch, _sbCheckerBaseInput.MessageXML, MagicWeapon, GreaterMagicWeapon, _sbCheckerBaseInput.IndvSB);

            AbilityBonus = _sbCheckerBaseInput.AbilityScores.DexMod;
            string AbilityUsed = " Dex Mod ";
            if (rangedWeapon.Contains("hand of the acolyte"))
            {
                AbilityBonus = _sbCheckerBaseInput.AbilityScores.WisMod;
                AbilityUsed = " Wis Mod (hand of the acolyte) ";

            }

            if (holdRangedWeapon.Contains("/+"))
            {
                if (!_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Quick Draw") && Utility.IsThrownWeapon(weapon.search_name.ToLower()))
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("Ranged Iterative Attacks", "No Quick Draw, so can't have Iterative Attacks for " + weapon.name);
                }
            }

            int BAB = Convert.ToInt32(Utility.GetNonParenValue(_baseAtk));

            if (holdRangedWeapon.Contains("flurry of blows"))
            {
                holdRangedWeapon = holdRangedWeapon.Replace("flurry of blows", string.Empty).Trim();
                BAB = _racialHDValue + _sbCheckerBaseInput.CharacterClasses.GetNonMonkBABValue() + _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Monk") - 2;
            }

            weaponBonusComputed = BAB + AbilityBonus + _sizeMod + RangeMod;
            formula += BAB + " _baseAtk +" + AbilityBonus.ToString() + AbilityUsed
                + CommonMethods.GetStringValue(_sizeMod) + " _sizeMod +" + RangeMod.ToString() + " RangeMod";

            if (weapon.name == "Sling" && _sbCheckerBaseInput.MonsterSBSearch.HasGear("stones"))
            {
                weaponBonusComputed--;
                formula += " -1 sling with stones";
            }

            if (weapon.name == "rock" && _sbCheckerBaseInput.MonsterSBSearch.HasSpecialAttackGeneral("rock throwing"))
            {
                weaponBonusComputed++;
                formula += " +1 rock throwing";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("crossbowman"))
            {
                int fighterLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("fighter");
                if (fighterLevel >= 5)
                {
                    int tempBonus = 1;
                    if (fighterLevel >= 9) tempBonus++;
                    if (fighterLevel >= 13) tempBonus++;
                    if (fighterLevel >= 17) tempBonus++;
                    weaponBonusComputed += tempBonus;
                    formula += " +" + tempBonus.ToString() + " crossbowman crossbow expert";
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("archer"))
            {
                int fighterLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("fighter");
                if (fighterLevel >= 5)
                {
                    int tempBonus = 1;
                    if (fighterLevel >= 9) tempBonus++;
                    if (fighterLevel >= 13) tempBonus++;
                    if (fighterLevel >= 17) tempBonus++;
                    weaponBonusComputed += tempBonus;
                    formula += " +" + tempBonus.ToString() + " Expert Archer";
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("spirit (champion)"))
            {
                int mediumLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("medium");
                int bonus = 1;
                if (mediumLevel >= 4) bonus++;
                if (mediumLevel >= 8) bonus++;
                if (mediumLevel >= 12) bonus++;
                if (mediumLevel >= 15) bonus++;
                if (mediumLevel >= 19) bonus++;

                weaponBonusComputed += bonus;
                formula += " +" + bonus.ToString() + " spirit (champion)";
            }

            string hold = null;

            if (weapon.NamedWeapon)
            {
                hold = weapon.BaseWeaponName.ToLower();
            }
            else
            {
                hold = weapon.search_name.ToLower();
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Weapon Focus (" + hold + PathfinderConstants.PAREN_RIGHT))
            {
                weaponBonusComputed++;
                formula += " +1 Weapon Focus";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Greater Weapon Focus (" + hold + PathfinderConstants.PAREN_RIGHT))
            {
                weaponBonusComputed++;
                formula += " +1 Greater Weapon Focus";
            }

            if (weapon.Masterwork && weapon.EnhancementBonus == 0)
            {
                weaponBonusComputed++;
                formula += " +1 Masterwork";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasSpecialAttackGeneral("weapon training"))
            {
                weaponBonusComputed += _sbCheckerBaseInput.MonsterSBSearch.GetWeaponsTrainingModifier(weapon.search_name, ref formula);
            }

            try
            {
                weaponBonusSB = Convert.ToInt32(Bonuses.FirstOrDefault());
            }
            catch
            {
                _sbCheckerBaseInput.MessageXML.AddFail("CheckOneRangedWeaponFound", "Failure to convert Bonus to Int from value of " + Bonuses.FirstOrDefault());
            }

            WeaponCommon weaponCommon = new WeaponCommon(_sbCheckerBaseInput, _equipmentData, _naturalWeaponBusiness);
            bool ignore = false;
            if (_sbCheckerBaseInput.IndvSB != null)
            {
                weaponCommon.GetOnGoingAttackMods(ref weaponBonusComputed, ref formula, MagicWeapon, GreaterMagicWeapon, out ignore);
            }

            if (weapon.EnhancementBonus > 0 && !ignore)
            {
                weaponBonusComputed += weapon.EnhancementBonus;
                formula += " +" + weapon.EnhancementBonus.ToString() + " Enhancement Bonus";
            }

            if (Bonuses.Count > 1)
            {
                if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Rapid Shot") && weaponBonusComputed - 2 == weaponBonusSB & weapon.name.Contains("bow"))
                {
                    weaponBonusComputed -= 2;
                }
            }

            if (weapon.name == "bomb" && _sbCheckerBaseInput.MonsterSBSearch.HasFeat("Throw Anything"))
            {
                weaponBonusComputed++;
                formula += " +1 Throw Anything";
            }

            weaponCommon.CheckWeaponProficiency(weapon, ref weaponBonusComputed, ref formula, ref SimpleOne, _raceWeapons);

            if (weaponBonusComputed == weaponBonusSB)
            {
                _sbCheckerBaseInput.MessageXML.AddPass("Ranged Attack Bonus " + weapon.Weapon_FullName(), formula);
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail("Ranged Attack Bonus " + weapon.Weapon_FullName(), weaponBonusComputed.ToString(), weaponBonusSB.ToString(), formula);
            }
        }
    }
}
