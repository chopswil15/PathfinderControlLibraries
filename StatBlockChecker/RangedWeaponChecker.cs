using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipmentBasic;
using Utilities;
using OnGoing;
using CommonStatBlockInfo;
using StatBlockCommon;
using CommonInterFacesDD;
using ClassManager;
using StatBlockCommon.Individual_SB;

namespace StatBlockChecker
{
    public class RangedWeaponChecker
    {
        private StatBlockMessageWrapper _messageXML;
        private MonSBSearch _monSBSearch;
        private ClassMaster CharacterClasses;
        private string RaceName;
        private IndividualStatBlock_Combat _indvSB;
        private List<string> magicInEffect;
        private Dictionary<IEquipment, int> Weapons;
        private string BaseAtk;
        private string Size;
        private int SizeMod;
        private WeaponChecker _weaponChecker;
        private AbilityScores.AbilityScores _abilityScores;
        private bool DontUseRacialHD;
        private RaceBase.RaceType RaceBaseType;
        private bool HasRaceBase;
        private int RacialHDValue;
        private string RaceWeapons;

        public RangedWeaponChecker(StatBlockMessageWrapper _messageXML, MonSBSearch _monSBSearch, ClassMaster CharacterClasses, string RaceName,
                IndividualStatBlock_Combat _indvSB, List<string> magicInEffect, Dictionary<IEquipment, int> Weapons, string BaseAtk, string Size,
                int SizeMod,  AbilityScores.AbilityScores _abilityScores, bool DontUseRacialHD, RaceBase.RaceType RaceBaseType,
                bool HasRaceBase, int RacialHDValue, string RaceWeapons)
        {
            this._messageXML = _messageXML;
            this._monSBSearch = _monSBSearch;
            this.CharacterClasses = CharacterClasses;
            this.RaceName = RaceName;
            this._indvSB = _indvSB;
            this.magicInEffect = magicInEffect;
            this.Weapons = Weapons;
            this.BaseAtk = BaseAtk;
            this.Size = Size;
            this.SizeMod = SizeMod;
            this._abilityScores = _abilityScores;
            this.DontUseRacialHD = DontUseRacialHD;
            this.RaceBaseType = RaceBaseType;
            this.HasRaceBase = HasRaceBase;
            this.RacialHDValue = RacialHDValue;
            this.RaceWeapons = RaceWeapons;
            _weaponChecker = new WeaponChecker(CharacterClasses, magicInEffect, Weapons, RaceName, DontUseRacialHD, RaceBaseType, HasRaceBase, RacialHDValue);
        }

        public void CheckRangedWeapons(string RangedValues)
        {
            if (RangedValues.Length == 0) return;
            string[] temp = new string[] { " or" };
            List<string> Ranged = RangedValues.Split(temp, StringSplitOptions.RemoveEmptyEntries).ToList<string>();


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
                Ranged = rangedWeapon.Split(temp2, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            }
            else if (rangedWeapon.Contains(", ") )
            {
                temp2 = new string[] { ", " };
                Ranged = rangedWeapon.Split(temp2, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
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
                WeaponCommon weaponCommon = new WeaponCommon(magicInEffect, Weapons, _indvSB, _messageXML, _monSBSearch, CharacterClasses, RaceName, DontUseRacialHD, RaceBaseType, HasRaceBase, RacialHDValue);
                bool MagicWeapon;
                bool GreaterMagicWeapon;

                if (weaponCommon.FindWeapon(ref weapon, ref natural_weapon, ranged.Trim(), out MagicWeapon, out GreaterMagicWeapon))
                {
                    Found.Add(ranged.Trim());
                    CheckOneRangedWeaponFound(weapon, ranged.Trim(), MagicWeapon, GreaterMagicWeapon, ref SimpleOne, RaceWeapons);
                }
                else
                {
                    _messageXML.AddFail("Ranged Attack", "Missing Weapon-" + rangedWeapon.Trim());
                }
            }
        }

        private void CheckOneRangedWeaponFound(Weapon weapon, string rangedWeapon, bool MagicWeapon, bool GreaterMagicWeapon,
                     ref bool SimpleOne, string RaceWeapons)
        {
            string holdRangedWeapon = string.Empty;
            List<string> Bonuses = null;
            string weaponsDamage = string.Empty;
            string weaponBonus = string.Empty;
            int weaponBonusComputed = 0;
            int weaponBonusSB = 0;
            int AbilityBonus = 0;
            int RangeMod = 0;
            string formula = string.Empty;
            RangeMod = 0;

            holdRangedWeapon = rangedWeapon;

            holdRangedWeapon = holdRangedWeapon.Replace("ranged touch", string.Empty);
            if (holdRangedWeapon.IndexOf("Rapid Shot") >= 0)
            {
                holdRangedWeapon = holdRangedWeapon.Replace("Rapid Shot", string.Empty).Trim();
                RangeMod -= 2;
            }

            _weaponChecker.ParseSingleWeapon(weapon, ref weaponBonus, ref weaponsDamage, ref holdRangedWeapon, ref Bonuses);
            _weaponChecker.CheckRangedWeaponDamage(weapon, weaponsDamage, Size, _abilityScores, _monSBSearch, _messageXML,MagicWeapon,GreaterMagicWeapon,_indvSB);

            AbilityBonus = _abilityScores.DexMod;
            string AbilityUsed = " Dex Mod ";
            if (rangedWeapon.Contains("hand of the acolyte"))
            {
                AbilityBonus = _abilityScores.WisMod;
                AbilityUsed = " Wis Mod (hand of the acolyte) ";

            }

            if (holdRangedWeapon.Contains("/+"))
            {
                if (!_monSBSearch.HasFeat("Quick Draw") && Utility.IsThrownWeapon(weapon.search_name.ToLower()))
                {
                    _messageXML.AddFail("Ranged Iterative Attacks", "No Quick Draw, so can't have Iterative Attacks for " + weapon.name);
                }
            }

            int BAB = Convert.ToInt32(Utility.GetNonParenValue(BaseAtk));

            if (holdRangedWeapon.Contains("flurry of blows"))
            {
                holdRangedWeapon = holdRangedWeapon.Replace("flurry of blows", string.Empty).Trim();
                BAB = RacialHDValue + CharacterClasses.GetNonMonkBABValue() + CharacterClasses.FindClassLevel("Monk") - 2;
            }

            weaponBonusComputed = BAB + AbilityBonus + SizeMod + RangeMod;
            formula += BAB + " BaseAtk +" + AbilityBonus.ToString() + AbilityUsed 
                + Utility.GetStringValue(SizeMod) + " SizeMod +" + RangeMod.ToString() + " RangeMod";

            if (weapon.name == "Sling" && _monSBSearch.HasGear("stones"))
            {
                weaponBonusComputed--;
                formula += " -1 sling with stones";
            }

            if (weapon.name == "rock" && _monSBSearch.HasSpecialAttackGeneral("rock throwing"))
            {
                weaponBonusComputed++;
                formula += " +1 rock throwing";
            }

            if (_monSBSearch.HasClassArchetype("crossbowman"))
            {
                int fighterLevel = CharacterClasses.FindClassLevel("fighter");
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

            if (_monSBSearch.HasClassArchetype("archer"))
            {
                int fighterLevel = CharacterClasses.FindClassLevel("fighter");
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

            if (_monSBSearch.HasSQ("spirit (champion)"))
            {
                int mediumLevel = CharacterClasses.FindClassLevel("medium");
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

            if (_monSBSearch.HasFeat("Weapon Focus (" + hold + ")"))
            {
                weaponBonusComputed++;
                formula += " +1 Weapon Focus";
            }

            if (_monSBSearch.HasFeat("Greater Weapon Focus (" + hold + ")"))
            {
                weaponBonusComputed++;
                formula += " +1 Greater Weapon Focus";
            }

            if (weapon.Masterwork && weapon.EnhancementBonus == 0)
            {
                weaponBonusComputed++;
                formula += " +1 Masterwork";
            }           

            if (_monSBSearch.HasSpecialAttackGeneral("weapon training"))
            {
                weaponBonusComputed += _monSBSearch.GetWeaponsTrainingModifier(weapon.search_name, ref formula);
            }

            try
            {
                weaponBonusSB = Convert.ToInt32(Bonuses.FirstOrDefault());
            }
            catch
            {
                _messageXML.AddFail("CheckOneRangedWeaponFound", "Failure to convert Bonus to Int from value of " + Bonuses.FirstOrDefault());
            }

            WeaponCommon weaponCommon = new WeaponCommon(magicInEffect, Weapons, _indvSB, _messageXML, _monSBSearch, CharacterClasses, RaceName, DontUseRacialHD, RaceBaseType, HasRaceBase, RacialHDValue);
            bool ignore = false;
            if (_indvSB != null)
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
                if (_monSBSearch.HasFeat("Rapid Shot") && weaponBonusComputed - 2 == weaponBonusSB & weapon.name.Contains("bow"))
                {
                    weaponBonusComputed -= 2;
                }
            }

            if (weapon.name == "bomb" && _monSBSearch.HasFeat("Throw Anything"))
            {
                weaponBonusComputed++;
                formula += " +1 Throw Anything";
            }

            weaponCommon.CheckWeaponProficiency(weapon, ref weaponBonusComputed, ref formula, ref SimpleOne, RaceWeapons);

            if (weaponBonusComputed == weaponBonusSB)
            {
                _messageXML.AddPass("Ranged Attack Bonus " + weapon.Weapon_FullName(), formula);
            }
            else
            {
                _messageXML.AddFail("Ranged Attack Bonus " + weapon.Weapon_FullName(), weaponBonusComputed.ToString(), weaponBonusSB.ToString(), formula);
            }
        } 

    }


}
