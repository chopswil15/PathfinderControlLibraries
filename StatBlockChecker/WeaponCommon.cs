using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipmentBasic;
using CommonInterFacesDD;
using OnGoing;
using CommonStatBlockInfo;
using StatBlockCommon;
using MagicItemAbilityWrapper;
using StatBlockCommon.Individual_SB;

using Utilities;
using EquipmentBusiness;
using PathfinderGlobals;
using StatBlockParsing;

namespace StatBlockChecker
{
    public class WeaponCommon
    { 
        private string RaceName;
        private bool DontUseRacialHD;
        private RaceBase.RaceType RaceBaseType;
        private bool HasRaceBase;
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        private IEquipmentData _equipmentData;
        private INaturalWeaponBusiness _naturalWeaponBusiness;
        private const string GREATER_MAGIC_WEAPON = "Greater Magic Weapon";
        private const string MAGIC_WEAPON = "Magic Weapon";

        public WeaponCommon(ISBCheckerBaseInput sbCheckerBaseInput, IEquipmentData equipmentData, INaturalWeaponBusiness naturalWeaponBusiness)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _equipmentData = equipmentData;
            _naturalWeaponBusiness = naturalWeaponBusiness;
            RaceName = _sbCheckerBaseInput.Race_Base.Name();
            DontUseRacialHD = _sbCheckerBaseInput.MonsterSB.DontUseRacialHD;
            RaceBaseType = _sbCheckerBaseInput.Race_Base.RaceBaseType;
            HasRaceBase = _sbCheckerBaseInput.Race_Base == null ? false : true;
        }

        public bool FindWeapon(ref Weapon weapon, ref NaturalWeapon naturalWeapon, string weaponText,
                out bool isMagicWeapon, out bool isGreaterMagicWeapon)
        {
            isMagicWeapon = false;
            isGreaterMagicWeapon = false;

            try
            {
                weaponText = weaponText.Replace(Environment.NewLine, PathfinderConstants.SPACE);                
                int enchancement = 0;
                int weaponCount = 1;
                bool isMasterwork = weaponText.Contains("mwk") || weaponText.Contains("masterwork");
                if (!isMasterwork && weaponText.Contains(" of ") && !weaponText.Contains("flurry of blows")) isMasterwork = true;
                weaponText = weaponText.Replace("hand of the apprentice", string.Empty).Trim();
                int Pos = weaponText.IndexOf(PathfinderConstants.SPACE);
                string temp = string.Empty;
                string temp2 = weaponText.Substring(0, 2);

                if ((Pos == 1 || Pos == 2) && !temp2.Contains("+")) //get rid of weapon count
                {
                    weaponCount = int.Parse(weaponText.Substring(0, Pos));
                    weaponText = weaponText.Substring(Pos).Trim();
                }
                Pos = weaponText.IndexOf("+");
                if (Pos == 0)
                {
                    Pos = weaponText.IndexOf(PathfinderConstants.SPACE);
                    temp = weaponText.Substring(0, Pos);
                    Pos = temp.IndexOf("/");
                    if (Pos != -1) temp = temp.Substring(0, Pos);
                    enchancement = Convert.ToInt32(temp);
                    if (enchancement > 0) isMasterwork = true;
                }

                if (weaponText.Contains("flurry of blows"))
                {
                    weaponText = weaponText.Replace("flurry of blows", string.Empty);
                    if (!weaponText.Contains("unarmed strike"))
                    {
                        weaponText = "unarmed strike " + weaponText.Trim();
                        _sbCheckerBaseInput.MessageXML.AddFail("Missing Unarmed Strike Text", "Missing 'Unarmed Strike' in flurry of blows text");
                    }
                }

                bool foundWeapon = FindWeaponInList(ref weapon, weaponText, enchancement, isMasterwork, ref temp, 0, false);
                if (foundWeapon) return foundWeapon;

                foreach (string magic in _sbCheckerBaseInput.MagicInEffect)
                {
                    if (magic.Contains("Magic Weapon, Greater"))
                    {
                        Pos = magic.IndexOf("CL");
                        temp = magic.Substring(Pos);
                        List<OnGoingStatBlockModifier> mods = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockMods();
                        var attackMod = from x in mods where x.Name == "Greater Magic Weapon- Attack Bonus" select x.Modifier;

                        foundWeapon = FindWeaponInList(ref weapon, weaponText, enchancement, isMasterwork, ref temp, attackMod.FirstOrDefault(), true);
                        if (foundWeapon)
                        {
                            isGreaterMagicWeapon = true;
                            return foundWeapon;
                        }
                    }
                    else if (magic.Contains(MAGIC_WEAPON))
                    {
                        Pos = magic.IndexOf("CL");
                        temp = magic.Substring(Pos);
                        List<OnGoingStatBlockModifier> mods = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockMods();
                        var attackMod = from x in mods where x.Name == "Magic Weapon- Attack Bonus" select x.Modifier;

                        foundWeapon = FindWeaponInList(ref weapon, weaponText, enchancement, isMasterwork, ref temp, attackMod.FirstOrDefault(), true);
                        if (foundWeapon)
                        {
                            isMagicWeapon = true;
                            return foundWeapon;
                        }
                    }
                }

                Pos = weaponText.IndexOf("+");
                if (Pos >= 0)
                {
                    temp = weaponText.Substring(0, Pos).Trim();
                    temp = temp.Replace("incorporeal", string.Empty).Replace("claws", "claw").Replace("tentacles", "tentacle")
                          .Replace("slams", "slam").Replace("wings", "wing").Replace("stomp", "stomps").Replace("bites", "bite").Trim();
                    NaturalWeapon natural_weapon = _naturalWeaponBusiness.GetNaturalWeaponByName(temp);
                    if (natural_weapon != null)
                    {
                        _equipmentData.Weapons.Add(natural_weapon, weaponCount);
                        naturalWeapon = (NaturalWeapon)natural_weapon;
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("FindWeapon", ex.Message);
                return false;
            }
        }

        public void CheckWeaponProficiency(Weapon weapon, ref int weaponBonusComputed, ref string formula, 
               ref bool SimpleOne, string raceWeaponsText)
        {
            bool isProficient = false;
            StatBlockInfo.WeaponProficiencies knownWeaponsProficiencies =  _sbCheckerBaseInput.CharacterClasses.GetAllKnownWeaponsProficiencies();

            StatBlockInfo.WeaponProficiencies weaponCat = weapon.GetWeaponCategory();

            if (SimpleOne) knownWeaponsProficiencies ^= StatBlockInfo.WeaponProficiencies.SimpleOne;
           
            if (RaceName != null)
            {
                switch (RaceName)
                {
                    case "Half-Elf":
                        if (raceWeaponsText.Contains(weapon.search_name.ToLower())) isProficient = true;
                        break;
                    case "Elf":                    
                        if (raceWeaponsText.Contains(weapon.search_name.ToLower())) isProficient = true;
                        if (weapon.name.Contains("elven")) weaponCat = StatBlockInfo.WeaponProficiencies.Martial;
                        break;
                    case "Gnome":
                         if (weapon.name.Contains("gnome")) weaponCat = StatBlockInfo.WeaponProficiencies.Martial;
                        break;
                    case "Half-Orc":                     
                        if (raceWeaponsText.Contains(weapon.search_name.ToLower())) isProficient = true;
                        if (weapon.name.Contains("orc")) weaponCat = StatBlockInfo.WeaponProficiencies.Martial;
                        break;
                    case "Halfling":                       
                        if (raceWeaponsText.Contains(weapon.search_name.ToLower())) isProficient = true;
                        if (weapon.name.Contains("halfling")) weaponCat = StatBlockInfo.WeaponProficiencies.Martial;
                        break;
                    case "Dwarf":
                        if (weapon.name.Contains("dwarven"))  weaponCat = StatBlockInfo.WeaponProficiencies.Martial;                     
                        if (raceWeaponsText.Contains(weapon.search_name.ToLower())) isProficient = true;
                        break;
                    default:
                        if (raceWeaponsText.Contains(weapon.search_name.ToLower())) isProficient = true;
                        break; 
                }
            }

            if (weapon.group == "Exotic Weapon")
            {
                string tempWeaponName = weapon.search_name.ToLower();
                if (tempWeaponName.Contains("scorpion")) tempWeaponName = tempWeaponName.Replace("scorpion", string.Empty).Trim();
                if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Exotic Weapon Proficiency (" + tempWeaponName + PathfinderConstants.PAREN_RIGHT)) isProficient = true;
            }
            if (weapon.group == "Martial Weapon")
            {
                if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Martial Weapon Proficiency (" + weapon.search_name.ToLower() + PathfinderConstants.PAREN_RIGHT)) isProficient = true;
            }

            if (!isProficient && ( _sbCheckerBaseInput.CharacterClasses.HasClass("hellknight signifer")))
            {
               //hell knight order
            }

            if (!isProficient && ( _sbCheckerBaseInput.CharacterClasses.HasClass("cleric") ||   _sbCheckerBaseInput.CharacterClasses.HasClass("inquisitor")))
            {
                isProficient = GetDeityProficienccy(weapon);
            }

            if (knownWeaponsProficiencies.HasFlag(weaponCat)) isProficient = true;
            if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("skill at arms")) isProficient = true; //battle mystery

            if (!isProficient)
            {
                if ((knownWeaponsProficiencies & weaponCat) != weaponCat 
                    && (DontUseRacialHD || (HasRaceBase && RaceBaseType != RaceBase.RaceType.BestiaryStatBlock)))
                {
                    bool ExtraPro = false;
                    if (knownWeaponsProficiencies.HasFlag(StatBlockInfo.WeaponProficiencies.Extra))
                    {
                        List<string> Extra =  _sbCheckerBaseInput.CharacterClasses.GetAllWeaponProficienciesExtra();
                        if (Extra.Contains(weapon.search_name.ToLower())) ExtraPro = true;
                        if (weapon.BaseWeaponName.Length > 0 && !ExtraPro)
                        {
                            if (Extra.Contains(weapon.BaseWeaponName.ToLower())) ExtraPro = true;
                        }
                    }

                    if ((knownWeaponsProficiencies & StatBlockInfo.WeaponProficiencies.SimpleOne) == StatBlockInfo.WeaponProficiencies.SimpleOne)
                    {
                        if (weapon.group == "Simple Weapon")
                        {
                            // ExtraPro = true;
                            SimpleOne = true;
                        }
                    }


                    if (!ExtraPro && !_sbCheckerBaseInput.MonsterSBSearch.HasOnlyClassHitdice())
                    {
                        weaponBonusComputed -= 4;
                        formula += " -4 no weapon Proficiency";
                        _sbCheckerBaseInput.MessageXML.AddFail("Weapon Proficiency Missing", weapon.name + " needs " + weaponCat.ToString());
                    }
                }
                else
                {
                    weaponBonusComputed -= 4;
                    formula += " -4 no weapon Proficiency";
                    _sbCheckerBaseInput.MessageXML.AddFail("Weapon Proficiency Missing", weapon.name + " needs " + weaponCat.ToString());
                }
            }
        }

        private bool GetDeityProficienccy(Weapon weapon)
        {
            bool isProficient = false;
            string deity =  _sbCheckerBaseInput.CharacterClasses.FindDiety();
            if (deity.Length > 0)
            {
                string deityWeapon;
                Dictionary<string, string> DeityProficiencies =  CommonMethods.GetDeityProficiencies();
                if (DeityProficiencies.TryGetValue(deity.ToLower(), out deityWeapon))
                {
                    if (weapon.search_name.ToLower() == deityWeapon) isProficient = true;
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("CheckWeaponProficiency", "Diety undefined - " + deity);
                }          
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail("CheckWeaponProficiency", "No Deity for cleric");
            }
            return isProficient;
        }

        private bool FindWeaponInList(ref Weapon weapon, string weaponText, int Enchanment, bool Masterwork,
                      ref string temp, int MagicWeaponMod, bool IngoreMasterwork)
        {
            foreach (KeyValuePair<IEquipment, int> kvp in _equipmentData.Weapons)
            {
                IEquipment oneWeapon = kvp.Key;
                var weaponTextLower = weaponText.ToLower();

                if (oneWeapon is Weapon)
                {
                    Weapon weaponTemp = oneWeapon as Weapon;
                    var searchNameLower = weaponTemp.search_name.ToLower();
                    int EnhancementHold = weaponTemp.EnhancementBonus;
                    if (MagicWeaponMod > 0) EnhancementHold = MagicWeaponMod;  //Enhancement doesn't stack
                    if (weaponTextLower.Contains(searchNameLower) 
                        && (Enchanment == EnhancementHold || weaponTemp.NamedWeapon) 
                        && (weaponTemp.Masterwork == Masterwork || IngoreMasterwork))
                    {
                        weapon = weaponTemp;
                        return true;
                    }
                    else if (weaponTextLower.Contains(searchNameLower) && weaponTemp.NamedWeapon)
                    {
                        weapon = weaponTemp;
                        return true;
                    }
                    if (weaponTextLower.Contains("spiked"))
                    {
                        if (weaponTextLower.Contains("light")) temp = "light spiked shield";
                        if (weaponTextLower.Contains("heavy")) temp = "heavy spiked shield";
                        if (temp.Contains(searchNameLower) 
                              && (Enchanment == weaponTemp.EnhancementBonus || weaponTemp.NamedWeapon))
                        {
                            weapon = weaponTemp;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void GetOnGoingDamageMods(bool MagicWeapon, bool GreaterMagicWeapon, IndividualStatBlock_Combat IndvSB, ref string formula, ref StatBlockInfo.HDBlockInfo damageComputed, ref bool ignoreEnhancement)
        {
            List<OnGoingStatBlockModifier> Mods = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockMods();
            foreach (OnGoingStatBlockModifier mod in Mods)
            {
                if (mod.ModType == OnGoingStatBlockModifier.StatBlockModifierTypes.Damage && mod.SubType == OnGoingStatBlockModifier.StatBlockModifierSubTypes.None)
                {
                    bool ignore = false;
                    if (mod.Name.Contains(GREATER_MAGIC_WEAPON) && GreaterMagicWeapon) ignoreEnhancement = true;
                    if (mod.Name.Contains(MAGIC_WEAPON) && MagicWeapon && !mod.Name.Contains(GREATER_MAGIC_WEAPON)) ignoreEnhancement = true;

                    if (mod.Name.Contains(GREATER_MAGIC_WEAPON) && !GreaterMagicWeapon)
                    {
                        ignore = true;
                    }
                    else if (mod.Name.Contains(MAGIC_WEAPON) && !MagicWeapon && !mod.Name.Contains(GREATER_MAGIC_WEAPON)) ignore = true;

                    if (!ignore)
                    {
                        damageComputed.Modifier += mod.Modifier;
                        formula += " +" + mod.Modifier.ToString() + PathfinderConstants.SPACE + mod.Name;
                    }
                }
            }
        }


        public void GetOnGoingAttackMods(ref int weaponBonusComputed, ref string formula, bool MagicWeapon, bool GreaterMagicWeapon, out  bool ignoreEnhancement)
        {
            ignoreEnhancement = false;
            List<OnGoingStatBlockModifier> Mods = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockMods();
            foreach (OnGoingStatBlockModifier mod in Mods)
            {
                if (mod.ModType == OnGoingStatBlockModifier.StatBlockModifierTypes.Attack)
                {
                    if (mod.Name.Contains(GREATER_MAGIC_WEAPON) && GreaterMagicWeapon) ignoreEnhancement = true;
                    if (mod.Name.Contains(MAGIC_WEAPON) && MagicWeapon && !mod.Name.Contains(GREATER_MAGIC_WEAPON)) ignoreEnhancement = true;
                    bool ignore = false;
                    if (mod.Name.Contains(GREATER_MAGIC_WEAPON) && !GreaterMagicWeapon)
                    {
                        ignore = true;
                    }
                    else if (mod.Name.Contains(MAGIC_WEAPON) && !MagicWeapon && !mod.Name.Contains(GREATER_MAGIC_WEAPON)) ignore = true;

                    if (!ignore)
                    {
                        weaponBonusComputed += mod.Modifier;
                        formula += " +" + mod.Modifier.ToString() + PathfinderConstants.SPACE + mod.Name;
                    }
                }
            }

            List<MagicItemAbilitiesWrapper> magicItemAbilities = _sbCheckerBaseInput.IndvSB.GetMagicItemAbilities();
            foreach (MagicItemAbilitiesWrapper ability in magicItemAbilities)
            {
                List<IOnGoing> abilityMods = ability.OnGoingStatBlockModifiers;
                foreach (IOnGoing mod in abilityMods)
                {
                    if (mod.OnGoingType == OnGoingType.StatBlock)
                    {
                        OnGoingStatBlockModifier Mod = (OnGoingStatBlockModifier)mod;
                        if (Mod.ModType == OnGoingStatBlockModifier.StatBlockModifierTypes.Attack)
                        {
                            if (Mod.Name.Contains(GREATER_MAGIC_WEAPON) && GreaterMagicWeapon) ignoreEnhancement = true;
                            if (Mod.Name.Contains(MAGIC_WEAPON) && MagicWeapon && !Mod.Name.Contains(GREATER_MAGIC_WEAPON)) ignoreEnhancement = true;
                 
                            bool ignore = false;
                            if (Mod.Name.Contains(GREATER_MAGIC_WEAPON) && GreaterMagicWeapon)
                            {
                                ignore = true;
                            }
                            else if (Mod.Name.Contains(MAGIC_WEAPON) && MagicWeapon) ignore = true;

                            if (!ignore)
                            {
                                weaponBonusComputed += Mod.Modifier;
                                formula += " +" + Mod.Modifier.ToString() + PathfinderConstants.SPACE + Mod.Name;
                            }
                        }
                    }
                }
            }
        }

    }
}
