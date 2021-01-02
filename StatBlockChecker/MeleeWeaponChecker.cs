using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipmentBasic;
using CommonInterFacesDD;
using Utilities;
using OnGoing;
using ClassManager;
using StatBlockCommon;
using MagicItemAbilityWrapper;
using CommonStatBlockInfo;
using StatBlockCommon.Individual_SB;

namespace StatBlockChecker
{
    public class MeleeWeaponChecker
    {
        private StatBlockMessageWrapper _messageXML;
        private MonSBSearch _monSBSearch;
        private ClassMaster CharacterClasses;
        private IndividualStatBlock_Combat _indvSB;
        private List<string> magicInEffect;
        private Dictionary<IEquipment, int> Weapons;
        private WeaponChecker _weaponChecker;
        private AbilityScores.AbilityScores _abilityScores;
        private int SizeMod;
        private string BaseAtk;
        private string Size;
        private int _acDefendingMod;
        private int RacialHDValue;
        private string RaceName;
        private bool DontUseRacialHD;
        private RaceBase.RaceType RaceBaseType;
        private bool HasRaceBase;
        private string RaceWeapons;


        public MeleeWeaponChecker(StatBlockMessageWrapper _messageXML, MonSBSearch _monSBSearch, ClassMaster CharacterClasses, 
                IndividualStatBlock_Combat _indvSB, List<string> magicInEffect, Dictionary<IEquipment, int> Weapons, string BaseAtk, string Size,
                int SizeMod,  AbilityScores.AbilityScores _abilityScores, int RacialHDValue, int _acDefendingMod,
                string RaceName, bool DontUseRacialHD, RaceBase.RaceType RaceBaseType, bool HasRaceBase, string RaceWeapons)
        {
            this._messageXML = _messageXML;
            this._monSBSearch = _monSBSearch;
            this.CharacterClasses = CharacterClasses;
            this._indvSB = _indvSB;
            this.magicInEffect = magicInEffect;
            this.Weapons = Weapons;
            this.BaseAtk = BaseAtk;
            this.Size = Size;
            this.SizeMod = SizeMod;            
            this._abilityScores = _abilityScores;
            this.RacialHDValue = RacialHDValue;
            this._acDefendingMod = _acDefendingMod;
            this.RaceName = RaceName;
            this.DontUseRacialHD = DontUseRacialHD;
            this.RaceBaseType = RaceBaseType;
            this.HasRaceBase = HasRaceBase;
            this.RaceWeapons = RaceWeapons;
            _weaponChecker = new WeaponChecker(CharacterClasses,magicInEffect,Weapons,RaceName,DontUseRacialHD,RaceBaseType,HasRaceBase,RacialHDValue);
        }

        public void CheckMeleeWeapons(string MeleeValues)
        {
            if (MeleeValues.Length == 0) return;
            string holdMelee = MeleeValues.Replace(Environment.NewLine, " ");
            string[] temp = new string[] { " or " };
            List<string> Melee = holdMelee.Split(temp, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            List<string> Found = new List<string>();

            foreach (string meleeWeapon in Melee) //loop on "or" blocks
            {
                CheckOneMeleeOrBlock(Found, meleeWeapon);
            }
        }

        private void CheckOneMeleeOrBlock(List<string> Found, string meleeWeapon)
        {
            NaturalWeapon natural_weapon = null;
            Weapon weapon = null;
            bool TwoWeaponFighting = false;
            bool MultipleWepons = false;
            bool NaturalMultipleWepons = false;
            bool LightWeapon = false;
            bool BiteAttack = false;
            List<string> Melee2 = new List<string>();
            string[] temp2 = new string[] { " and" };
            Utility.ParenCommaFix(ref meleeWeapon);

            if (FindNonParenAnd(meleeWeapon))
            {
                Melee2 = meleeWeapon.Split(temp2, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            }
            else if (meleeWeapon.IndexOf(", ") >= 0)
            {
                temp2 = new string[] { ", " };
                Melee2 = meleeWeapon.Split(temp2, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            }
            else
            {
                Melee2.Add(meleeWeapon);
            }

            LightWeapon = false;
            BiteAttack = false;
            if (Melee2.Count > 1)
            {
                MultipleWepons = true;
                foreach (string weap in Melee2)
                {
                    if (weap.IndexOf("bite") >= 0)
                    {
                        BiteAttack = true;
                    }
                    if (FindLightWeapon(weap))
                    {
                        LightWeapon = true;
                    }
                }
                if (NonNaturalWeaponCount(Melee2) != 1) TwoWeaponFighting = true;
            }

            Found.Add(meleeWeapon);
            if(HasNonNaturalWeapon(Melee2))
            {
                NaturalMultipleWepons = true;
            }
            int weaponIndex = 0;
            bool SimpleOne = false;

            foreach (string MW in Melee2) // loop on "and" blocks
            {
                weapon = null;
                natural_weapon = null;
                weaponIndex++;
                bool MagicWeapon;
                bool GreaterMagicWeapon;

                WeaponCommon weaponCommon = new WeaponCommon(magicInEffect, Weapons, _indvSB, _messageXML, _monSBSearch, CharacterClasses, RaceName,DontUseRacialHD,RaceBaseType,HasRaceBase,RacialHDValue);
                if (weaponCommon.FindWeapon(ref weapon, ref natural_weapon, MW.Trim(), out MagicWeapon, out GreaterMagicWeapon))
                {
                    Found.Add(MW);
                    CheckOneMeleeWeaponFound(natural_weapon, weapon, TwoWeaponFighting, MultipleWepons, LightWeapon,
                            BiteAttack, Melee2.Count, weaponIndex, MW, MagicWeapon, GreaterMagicWeapon, ref SimpleOne, RaceWeapons, NaturalMultipleWepons);
                }
                else
                {
                    _messageXML.AddFail("Melee Attack", "Missing Weapon-" + MW.Trim());
                }
            }
        }

        private bool FindLightWeapon(string weaponText)
        {
            //for Two-Handed weapons pairs
            foreach (KeyValuePair<IEquipment, int> kvp in Weapons)
            {
                IEquipment hold = kvp.Key;
                if (hold is Weapon)
                {
                    Weapon weapon = (Weapon)hold;
                    if (weaponText.IndexOf(weapon.search_name.ToLower()) >= 0)
                    {
                        if (weapon.category == "Light Melee Weapon" || weapon.name == "Whip" || weapon.@double)
                        {
                            return true;
                        }
                    }
                }
                else if (hold is NaturalWeapon)
                {
                    return true; // natural weapons are always light
                }
            }
            return false;
        }

        private bool FindNonParenAnd(string meleeWeapon)
        {
            ParenAndFix(ref meleeWeapon);
            if (!meleeWeapon.Contains("and ")) return false;

            return true; // between ()
        }

        private void ParenAndFix(ref string Block)
        {
            int LeftParenPos = Block.IndexOf("(");
            if (LeftParenPos == -1) return;
            int RightParenPos = Block.IndexOf(")");
            int CommaPos = Block.IndexOf("and ", LeftParenPos);


            string temp = Block;
            string hold = string.Empty;
            while (LeftParenPos >= 0)
            {
                if ((CommaPos > LeftParenPos) && (CommaPos < RightParenPos))
                {
                    temp = Block.Substring(LeftParenPos, RightParenPos - LeftParenPos);
                    hold = temp.Replace("and ", "|");
                    Block = Block.Replace(temp, hold);
                }
                LeftParenPos = Block.IndexOf("(", LeftParenPos + 1);

                if (LeftParenPos >= 0)
                {
                    RightParenPos = Block.IndexOf(")", LeftParenPos);
                    CommaPos = Block.IndexOf("and ", LeftParenPos);
                }
            }
        }

        private void CheckOneMeleeWeaponFound(NaturalWeapon natural_weapon, Weapon weapon, bool TwoWeaponFighting, bool MultipleWepons, 
             bool LightWeapon, bool BiteAttack, int Melee2Count, int weaponIndex, string MW,bool MagicWeapon,
             bool GreaterMagicWeapon, ref bool SimpleOne, string RaceWeapons, bool NaturalMultipleWepons)
        {
            List<string> Bonuses = new List<string>();
            int weaponBonusComputed = 0;
            int weaponBonusSB = 0;
            string formula = string.Empty;

            if (weapon != null)
                CheckOneWeaponMeleeNonNatural(weapon, ref weaponBonusComputed, TwoWeaponFighting, LightWeapon, BiteAttack, ref Bonuses, MW, Melee2Count, weaponIndex, ref formula, MagicWeapon, GreaterMagicWeapon);
            if (natural_weapon != null)
                CheckOneWeaponMeleeNatural(natural_weapon, ref weaponBonusComputed, ref Bonuses, MW, NaturalMultipleWepons, ref formula, Melee2Count);

            weaponBonusSB = Convert.ToInt32(Bonuses.First());

            if (Bonuses.Count > 1)
            {
                bool containsHaste = magicInEffect.Exists(y => y.Contains("Haste")) || magicInEffect.Exists(y => y.Contains("haste"));
                for (int w = 0; w < Bonuses.Count - 1; w++)
                {
                    if (Convert.ToInt32(Bonuses[w]) != Convert.ToInt32(Bonuses[w + 1]) + 5 && !weapon.name.Contains("flurry of blows") && !weapon.name.Contains("unarmed strike") && !containsHaste)
                    {
                        _messageXML.AddFail("Melee Multiple Attack Bonus-", "not 5 difference");
                        break;
                    }
                }
            }

            if (weapon != null)
            {
                WeaponCommon weaponCommon = new WeaponCommon(magicInEffect, Weapons, _indvSB, _messageXML, _monSBSearch, CharacterClasses, RaceName, DontUseRacialHD, RaceBaseType, HasRaceBase, RacialHDValue);
                weaponCommon.CheckWeaponProficiency(weapon, ref weaponBonusComputed, ref formula, ref SimpleOne, RaceWeapons);
            }


            if (weaponBonusComputed == weaponBonusSB)
            {
                if (weapon != null)
                    _messageXML.AddPass("Melee Attack Bonus-" + weapon.Weapon_FullName(), formula);
                else
                    _messageXML.AddPass("Melee Attack Bonus-" + natural_weapon.name, formula);
            }
            else
            {
                if (weapon != null)
                    _messageXML.AddFail("Melee Attack Bonus-" + weapon.Weapon_FullName(), weaponBonusComputed.ToString(), weaponBonusSB.ToString(), formula);
                else
                    _messageXML.AddFail("Melee Attack Bonus-" + natural_weapon.name, weaponBonusComputed.ToString(), weaponBonusSB.ToString(), formula);
            }
        }

        private void CheckOneWeaponMeleeNatural(NaturalWeapon natural_weapon, ref int weaponBonusComputed,
               ref List<string> Bonuses, string MW, bool NaturalMultipleWepons, ref string formula, int MeleeWeaponCount)
        {
            int BAB = 0;
            string weaponBonus = string.Empty;
            string weaponsDamage = string.Empty;
            string holdMeleeWeapon = string.Empty;
            int AbilityBonus = _abilityScores.StrMod;

            BAB = Convert.ToInt32(Utility.GetNonParenValue(BaseAtk));

            if (_monSBSearch.HasDefensiveAbility("incorporeal"))
            {
                AbilityBonus = _abilityScores.DexMod;
            }

            if (_monSBSearch.HasFeat("Weapon Finesse")) //Natural weapons are considered light weapons
            {
                AbilityBonus = _abilityScores.DexMod;
            }           

            weaponBonusComputed = BAB + AbilityBonus + SizeMod;
            formula = BAB.ToString() + " BAB +" + AbilityBonus.ToString() + " AbilityBonus " + Utility.GetStringValue(SizeMod) + " SizeMod";
            int MultiWeaponPenalty = -5;

            if (_monSBSearch.HasFeat("Multiattack"))
            {
                MultiWeaponPenalty = -2;
                formula += " -2 Multiattack";
            }

            if (!_monSBSearch.HasSQ("multiweapon mastery"))
            {
                if ((natural_weapon.attack_type == "Secondary" && MeleeWeaponCount != 1) || NaturalMultipleWepons)
                {
                    weaponBonusComputed += MultiWeaponPenalty;
                    formula += " " + MultiWeaponPenalty.ToString() + " Secondary";
                }
            }

            //if (_monSBSearch.HasFeat("Combat Expertise"))
            //{
            //    int mod = -1;
            //    mod -= BAB / 4;
            //    weaponBonusComputed += mod;
            //    formula += " -" + mod.ToString() + "  Combat Expertise";
            //}

            int WeaponFocusMod = 0;
            string weaponName = natural_weapon.name.ToLower();
            if (weaponName == "talons") weaponName = "talon";

            if (_monSBSearch.HasFeat("Weapon Focus (" + weaponName + ")"))
            {
                weaponBonusComputed++;
                WeaponFocusMod++;
                formula += " +1 Weapon Focus";
            }

            if (_monSBSearch.HasFeat("Greater Weapon Focus (" + weaponName + ")"))
            {
                weaponBonusComputed++;
                WeaponFocusMod++;
                formula += " +1 Greater Weapon Focus";
            }

            if (_monSBSearch.IsMythic)
            {
                if (_monSBSearch.HasMythicFeat("Weapon Focus (" + weaponName + ")"))
                {
                    weaponBonusComputed -= WeaponFocusMod;
                    weaponBonusComputed += WeaponFocusMod * 2;
                    formula += " +" + ((WeaponFocusMod * 2) - WeaponFocusMod).ToString() + " Mythic Weapon Focus";
                }
            }           

            if (_indvSB != null)
            {
                weaponBonusComputed += _indvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.NaturalAttack,
                                             OnGoingStatBlockModifier.StatBlockModifierSubTypes.None, false ,ref formula);
            }

            holdMeleeWeapon = MW.Trim();
            _weaponChecker.ParseSingleNaturalWeapon(natural_weapon, ref weaponBonus, ref weaponsDamage, ref holdMeleeWeapon, ref Bonuses);
        }

        private void CheckOneWeaponMeleeNonNatural(Weapon weapon, ref int weaponBonusComputed, bool TwoWeaponFighting, bool LightWeapon,
              bool BiteAttack, ref List<string> Bonuses, string MW, int weaponCount, int weaponIndex, ref string formula, bool MagicWeapon, bool GreaterMagicWeapon)
        {
            string weaponBonus = string.Empty;
            string weaponsDamage = string.Empty;
            string holdMeleeWeapon = string.Empty;

            int SizeDifference = 0;
            StatBlockInfo.SizeCategories MonSize = StatBlockInfo.GetSizeEnum(Size);
            StatBlockInfo.SizeCategories WeaponSize = weapon.WeaponSize;
            if (WeaponSize == StatBlockInfo.SizeCategories.Medium && MonSize != WeaponSize) WeaponSize = MonSize;
            if (MonSize != WeaponSize) SizeDifference = Math.Abs(StatBlockInfo.GetSizeDifference(MonSize, WeaponSize));

            holdMeleeWeapon = MW.Trim();
            int Pos = holdMeleeWeapon.IndexOf(" ");
            if (Pos == 1)
            {
                holdMeleeWeapon = holdMeleeWeapon.Substring(Pos).Trim();
            }

            int BAB = Convert.ToInt32(Utility.GetNonParenValue(BaseAtk));

            if (holdMeleeWeapon.Contains("flurry of blows"))
            {
                holdMeleeWeapon = holdMeleeWeapon.Replace("flurry of blows", string.Empty).Trim();
                BAB = RacialHDValue + CharacterClasses.GetNonMonkBABValue() + CharacterClasses.FindClassLevel("Monk") - 2;
            }


            _weaponChecker.ParseSingleWeapon(weapon, ref weaponBonus, ref weaponsDamage, ref holdMeleeWeapon, ref Bonuses);

            try
            {
                _weaponChecker.CheckMeleeWeaponDamage(weapon, weaponsDamage, TwoWeaponFighting, BiteAttack, weaponCount, weaponIndex,
                     Size, _abilityScores, _monSBSearch, _messageXML, CharacterClasses.FindClassLevel("fighter"), _acDefendingMod, MagicWeapon, GreaterMagicWeapon, _indvSB);
            }
            catch (Exception ex)
            {
                _messageXML.AddFail("CheckOneWeaponMeleeNonNatural - CheckMeleeWeaponDamage", ex.Message);
            }
            int AbilityBonus = _abilityScores.StrMod;

            if (_monSBSearch.HasFeat("Weapon Finesse"))
            {
                if (IsWeaponFinesseCategory(weapon))
                {
                    AbilityBonus = _abilityScores.DexMod;
                }
            }

            if (MW.Contains("dueling sword") && _monSBSearch.HasFeat("Weapon Finesse") && _monSBSearch.HasFeat("Exotic Weapon Proficiency (Aldori dueling sword)"))
            {
                AbilityBonus = _abilityScores.DexMod;
            }

            if (_monSBSearch.HasDefensiveAbility("incorporeal"))
            {
                AbilityBonus = _abilityScores.DexMod;
            }

            weaponBonusComputed = BAB + AbilityBonus + SizeMod;
            formula += BAB.ToString() + " BAB +" + AbilityBonus.ToString() + " AbilityBonus +" + SizeMod.ToString() + " SizeMod";

            if (weapon.Broken)
            {
                weaponBonusComputed -= 2;
                formula += " -2 Broken";
            }
            if (BiteAttack && weapon.name == "bite")
            {
                if (_monSBSearch.HasFeat("Multiattack"))
                {
                    weaponBonusComputed -= 2;
                    formula += " -2 bite Multiattack ";
                }
                else
                {
                    weaponBonusComputed -= 5;
                    formula += " -5 bite";
                }
            }

            if (weapon.name.ToLower() == "unarmed strike")
            {
                weaponBonusComputed += _monSBSearch.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.NaturalAttack,
                                             OnGoingStatBlockModifier.StatBlockModifierSubTypes.None, ref formula);
            }

            if ((weapon.WeaponSpecialAbilities.WeaponSpecialAbilitiesValue & WeaponSpecialAbilitiesEnum.Furious) == WeaponSpecialAbilitiesEnum.Furious)
            {
                weaponBonusComputed += 2;
                formula += " +2 furious";
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

            if (SizeDifference > 0)
            {
                //assume small CreatureTypeFoundation has small weapon
                if (!(MonSize == StatBlockInfo.SizeCategories.Small && WeaponSize == StatBlockInfo.SizeCategories.Medium))
                {
                    weaponBonusComputed -= SizeDifference * 2;
                    formula += "-" + (SizeDifference * 2).ToString() + " weapon size difference";
                }
            }

            weaponBonusComputed += _weaponChecker.PoleArmTraingMods(weapon, _monSBSearch, CharacterClasses.FindClassLevel("fighter"), ref formula);

            string hold = null;

            if (weapon.NamedWeapon)
            {
                hold = weapon.BaseWeaponName.ToLower();
            }
            else
            {
                hold = weapon.search_name.ToLower();
            }


            if (hold.Contains("aldori"))
            {
                hold = hold.Replace("aldori", "Aldori");
            }

            if (hold == "flurry of blows")
            {
                hold = "unarmed strike"; //flurry of blows is a bunch of unarmed strikes
            }

            if (_monSBSearch.HasDefensiveAbility("weapon training"))
            {
                int fighterLevel = CharacterClasses.FindClassLevel("fighter");
                int harshMod = 0;
                if (fighterLevel >= 5) harshMod++;
                if (fighterLevel >= 9) harshMod++;
                if (fighterLevel >= 13) harshMod++;
                if (fighterLevel >= 17) harshMod++;
                if (harshMod > 0)
                {
                    weaponBonusComputed += harshMod;
                    formula += " +" + harshMod.ToString() + " weapon training";
                }
            }

            if (_monSBSearch.HasSQ("classic duelist") && (weapon.name.ToLower() == "rapier" || weapon.search_name.ToLower() == "short sword" || weapon.name.ToLower() == "cutlass"))
            {
                weaponBonusComputed++;
                formula += " +1 classic duelist";
            }

            string holdFocus = hold.Replace("scorpion", string.Empty).Trim();
            if (_monSBSearch.HasFeat("Weapon Focus (" + holdFocus + ")"))
            {
                weaponBonusComputed++;
                formula += " +1 Weapon Focus";
            }

            if (_monSBSearch.HasFeat("Greater Weapon Focus (" + holdFocus + ")"))
            {
                weaponBonusComputed++;
                formula += " +1 Greater Weapon Focus";
            }

            if (!BiteAttack)
            {
                if (TwoWeaponFighting && !_monSBSearch.HasSQ("multiweapon mastery"))
                {
                    if (_monSBSearch.HasFeat("Two-Weapon Fighting"))
                    {
                        if (LightWeapon)
                        {
                            weaponBonusComputed += -2;
                            formula += " -2 Two-Weapon Fighting Feat-light";
                        }
                        else
                        {
                            weaponBonusComputed += -4;
                            formula += " -4 Two-Weapon Fighting Feat-not light";
                        }
                    }
                    else
                    {
                        if (LightWeapon)
                        {
                            weaponBonusComputed += -4;
                            formula += " -4 Two-Weapon Fighting-light";
                        }
                        else
                        {
                            weaponBonusComputed += -6;
                            formula += " -6 Two-Weapon Fighting-not light";
                        }
                    }
                }
            }
            

            string hold2 = null;
            if (weapon.NamedWeapon)
            {
                hold2 = weapon.BaseWeaponName;
            }
            else
            {
                hold2 = weapon.search_name;
            }

            if (_monSBSearch.HasSpecialAttackGeneral("weapon training"))
            {
                weaponBonusComputed += _monSBSearch.GetWeaponsTrainingModifier(hold2, ref formula);
            }

            if (weapon.Masterwork && weapon.EnhancementBonus == 0)
            {
                weaponBonusComputed++;
                formula += " +1 masterwork";
            }

            if (_monSBSearch.HasFeat("Shield Master") && weapon.name.Contains("shield"))
            {
                formula += " +" + weapon.EnhancementBonus.ToString() + " Shield Master";
                weaponBonusComputed += weapon.EnhancementBonus;
            }  

            if (_monSBSearch.HasClassArchetype("weapon master"))
            {
                int level = CharacterClasses.FindClassLevel("Fighter");
                int count = 0;
                if (level >= 3)
                {
                    weaponBonusComputed++;
                    count++;
                }
                if (level >= 7)
                {
                    weaponBonusComputed++;
                    count++;
                }
                if (level >= 11)
                {
                    weaponBonusComputed++;
                    count++;
                }
                if (level >= 15)
                {
                    weaponBonusComputed++;
                    count++;
                }
                if (level >= 19)
                {
                    weaponBonusComputed++;
                    count++;
                }
                formula += " +" + count.ToString() + " weapon master";
            }

            if (_monSBSearch.HasTemplate("graveknight"))
            {
                weaponBonusComputed += 2;
                formula += " +2 Sacrilegious Aura";
            }

            bool ignore = false;
            if (_indvSB != null)
            {
                WeaponCommon weaponCommon = new WeaponCommon(magicInEffect, Weapons, _indvSB, _messageXML, _monSBSearch, CharacterClasses, RaceName, DontUseRacialHD, RaceBaseType, HasRaceBase, RacialHDValue);
                weaponCommon.GetOnGoingAttackMods(ref weaponBonusComputed, ref formula, MagicWeapon, GreaterMagicWeapon, out ignore);
            }
            bool amuletOfMightFists = formula.Contains("Amulet of Mighty Fists");

            //shield bash get no Enchantment Bonus
            if (weapon.EnhancementBonus > 0 && _acDefendingMod == 0 && !weapon.name.Contains("shield") && !amuletOfMightFists && !ignore)
            {
                weaponBonusComputed += weapon.EnhancementBonus;
                formula += " +" + weapon.EnhancementBonus.ToString() + " Enhancement Bonus";
            }
        }      

        private bool HasNonNaturalWeapon(List<string> WeaponsList)
        {
            Weapon weapon = null;
            NaturalWeapon natural_weapon = null;

            foreach (string MW in WeaponsList)
            {
                weapon = null;
                natural_weapon = null;
                bool MagicWeapon;
                bool GreaterMagicWeapon;

                WeaponCommon weaponCommon = new WeaponCommon(magicInEffect, Weapons, _indvSB, _messageXML, _monSBSearch, CharacterClasses, RaceName, DontUseRacialHD, RaceBaseType, HasRaceBase, RacialHDValue);
                if (weaponCommon.FindWeapon(ref weapon, ref natural_weapon, MW.Trim(), out MagicWeapon, out GreaterMagicWeapon))
                {
                    if (weapon != null)
                    {
                        return true;
                    }                   
                }
            }
            return false;
        }

        private bool IsWeaponFinesseCategory(Weapon weapon)
        {
            if (weapon.category == "Light Melee Weapon") return true;
            List<string> FinesseWeaponsNames = new List<string> { "scarf, bladed", "flurry of blows", "unarmed strike", "rapier", 
                                "whip", "chain, spiked", "elven curve blade" };
            if (FinesseWeaponsNames.Contains(weapon.search_name.ToLower())) return true;
            if (FinesseWeaponsNames.Contains(weapon.BaseWeaponName.ToLower())) return true;

            return false;
        }

        private int NonNaturalWeaponCount(List<string> WeaponsList)
        {
            int count = 0;
            Weapon weapon = null;
            NaturalWeapon natural_weapon = null;

            foreach (string MW in WeaponsList)
            {
                weapon = null;
                natural_weapon = null;
                bool MagicWeapon;
                bool GreaterMagicWeapon;

                WeaponCommon weaponCommon = new WeaponCommon(magicInEffect, Weapons, _indvSB, _messageXML, _monSBSearch, CharacterClasses, RaceName, DontUseRacialHD, RaceBaseType, HasRaceBase, RacialHDValue);
                if (weaponCommon.FindWeapon(ref weapon, ref natural_weapon, MW.Trim(), out MagicWeapon, out GreaterMagicWeapon))
                {
                    if (weapon != null && weapon.name != "bite")
                    {
                        count++;
                    }
                    if (natural_weapon != null)
                    {
                        
                    }
                }
            }
            return count;
        }       
    }
}
