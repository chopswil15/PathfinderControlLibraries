using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EquipmentBasic;
using CommonInterFacesDD;
using Utilities;
using OnGoing;
using CommonStatBlockInfo;
using EquipmentBusiness;
using PathfinderGlobals;

namespace StatBlockChecker
{
    public class MeleeWeaponChecker
    {
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        private WeaponChecker _weaponChecker;
        private int _sizeMod;
        private string _size;
        private int _acDefendingMod;
        private int _racialHDValue;
        private string _raceWeapons;
        private IEquipmentData _equipmentData;
        private INaturalWeaponBusiness _naturalWeaponBusiness;
        private IWeaponBusiness _weaponBusiness;

        public MeleeWeaponChecker(ISBCheckerBaseInput sbCheckerBaseInput, IEquipmentData equipmentData, 
                ISizeData sizeData, int acDefendingMod, INaturalWeaponBusiness naturalWeaponBusiness, IWeaponBusiness weaponBusiness)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _equipmentData = equipmentData;
            _naturalWeaponBusiness = naturalWeaponBusiness;
            _weaponBusiness = weaponBusiness;
            _size = _sbCheckerBaseInput.MonsterSB.Size;
            _sizeMod = sizeData.SizeMod;            
            _racialHDValue = _sbCheckerBaseInput.Race_Base.RacialHDValue();
            _acDefendingMod = acDefendingMod;
            _raceWeapons = _sbCheckerBaseInput.Race_Base.RaceWeapons();
            _weaponChecker = new WeaponChecker(_sbCheckerBaseInput, _equipmentData, _naturalWeaponBusiness, _weaponBusiness);
        }

        public void CheckMeleeWeapons(string meleeValues)
        {
            if (meleeValues.Length == 0) return;
            string holdMelee = meleeValues.Replace(Environment.NewLine, PathfinderConstants.SPACE);
            string[] temp = new string[] { " or " };
            List<string> meleeOrBlocks = holdMelee.Split(temp, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> foundWeapons = new List<string>();

            foreach (string meleeWeapon in meleeOrBlocks) //loop on "or" blocks
            {
                CheckOneMeleeOrBlock(foundWeapons, meleeWeapon);
            }
        }

        private void CheckOneMeleeOrBlock(List<string> foundWeapons, string meleeWeapon)
        {
            NaturalWeapon natural_weapon = null;
            Weapon weapon = null;
            bool twoWeaponFighting = false;
            bool multipleWepons = false;
            bool naturalMultipleWepons = false;
            bool lightWeapon;
            bool biteAttack;
            List<string> meleeList = new List<string>();
            string[] splitFind = new string[] { " and" };
            meleeWeapon = meleeWeapon.Replace('\n', ' ' );

            //find "and"s in parenths
            int posAnd = meleeWeapon.IndexOf("and");

            if (posAnd > 0)
            {
                int startPos = 0; ;
                while (posAnd <= meleeWeapon.Length || posAnd == -1)
                {
                    int openparen = meleeWeapon.IndexOf(PathfinderConstants.PAREN_LEFT, startPos);
                    if (openparen < 0) break;
                   
                    int closeparen = meleeWeapon.IndexOf(PathfinderConstants.PAREN_RIGHT, openparen);
                    if (posAnd > openparen && openparen < closeparen)
                    {
                        meleeWeapon = meleeWeapon.Insert(posAnd, "|");
                    }
                    posAnd = meleeWeapon.IndexOf("and", closeparen);
                    startPos = closeparen;    
                }
            }

            Utility.ParenCommaFix(ref meleeWeapon);

            if (FindNonParenAnd(meleeWeapon))
            {
                meleeList = meleeWeapon.Split(splitFind, StringSplitOptions.RemoveEmptyEntries).ToList();
                for (int a = 0; a <= meleeList.Count - 1; a++)
                {
                    meleeList[a] = meleeList[a].Replace("|", string.Empty);
                }
            }
            else if (meleeWeapon.Contains(", "))
            {
                splitFind = new string[] { ", " };
                meleeList = meleeWeapon.Split(splitFind, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            else
            {
                meleeList.Add(meleeWeapon);
            }

            lightWeapon = false;
            biteAttack = false;
            if (meleeList.Any())
            {
                multipleWepons = true;
                foreach (string weap in meleeList)
                {
                    if (weap.Contains("bite"))  biteAttack = true;
                    if (FindLightWeapon(weap))  lightWeapon = true;
                }
                if (NonNaturalWeaponCount(meleeList) != 1) twoWeaponFighting = true;
            }

            foundWeapons.Add(meleeWeapon);
            if(HasNonNaturalWeapon(meleeList))  naturalMultipleWepons = true;
            int weaponIndex = 0;
            bool SimpleOne = false;

            foreach (string MW in meleeList) // loop on "and" blocks
            {
                weapon = null;
                natural_weapon = null;
                weaponIndex++;
                bool MagicWeapon;
                bool GreaterMagicWeapon;

                WeaponCommon weaponCommon = new WeaponCommon(_sbCheckerBaseInput, _equipmentData, _naturalWeaponBusiness);
                if (weaponCommon.FindWeapon(ref weapon, ref natural_weapon, MW.Trim(), out MagicWeapon, out GreaterMagicWeapon))
                {
                    foundWeapons.Add(MW);
                    CheckOneMeleeWeaponFound(natural_weapon, weapon, twoWeaponFighting, multipleWepons, lightWeapon,
                            biteAttack, meleeList.Count, weaponIndex, MW, MagicWeapon, GreaterMagicWeapon, ref SimpleOne, _raceWeapons, naturalMultipleWepons);
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("Melee Attack", "Missing Weapon-" + MW.Trim());
                }
            }
        }

        private bool FindLightWeapon(string weaponText)
        {
            //for Two-Handed weapons pairs
            foreach (KeyValuePair<IEquipment, int> kvp in _equipmentData.Weapons)
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
            int LeftParenPos = Block.IndexOf(PathfinderConstants.PAREN_LEFT);
            if (LeftParenPos == -1) return;
            int RightParenPos = Block.IndexOf(PathfinderConstants.PAREN_RIGHT);
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
                LeftParenPos = Block.IndexOf(PathfinderConstants.PAREN_LEFT, LeftParenPos + 1);

                if (LeftParenPos >= 0)
                {
                    RightParenPos = Block.IndexOf(PathfinderConstants.PAREN_RIGHT, LeftParenPos);
                    CommaPos = Block.IndexOf("and ", LeftParenPos);
                }
            }
        }

        private void CheckOneMeleeWeaponFound(NaturalWeapon natural_weapon, Weapon weapon, bool TwoWeaponFighting, bool MultipleWepons, 
             bool LightWeapon, bool BiteAttack, int Melee2Count, int weaponIndex, string MW,bool MagicWeapon,
             bool GreaterMagicWeapon, ref bool SimpleOne, string _raceWeapons, bool NaturalMultipleWepons)
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
                bool containsHaste = _sbCheckerBaseInput.MagicInEffect.Exists(y => y.Contains("Haste")) || _sbCheckerBaseInput.MagicInEffect.Exists(y => y.Contains("haste"));
                for (int w = 0; w < Bonuses.Count - 1; w++)
                {
                    if (Convert.ToInt32(Bonuses[w]) != Convert.ToInt32(Bonuses[w + 1]) + 5 && !weapon.name.Contains("flurry of blows") && !weapon.name.Contains("unarmed strike") && !containsHaste)
                    {
                        _sbCheckerBaseInput.MessageXML.AddFail("Melee Multiple Attack Bonus-", "not 5 difference");
                        break;
                    }
                }
            }

            if (weapon != null)
            {
                WeaponCommon weaponCommon = new WeaponCommon(_sbCheckerBaseInput, _equipmentData, _naturalWeaponBusiness);
                weaponCommon.CheckWeaponProficiency(weapon, ref weaponBonusComputed, ref formula, ref SimpleOne, _raceWeapons);
            }


            if (weaponBonusComputed == weaponBonusSB)
            {
                if (weapon != null)
                    _sbCheckerBaseInput.MessageXML.AddPass("Melee Attack Bonus-" + weapon.Weapon_FullName(), formula);
                else
                    _sbCheckerBaseInput.MessageXML.AddPass("Melee Attack Bonus-" + natural_weapon.name, formula);
            }
            else
            {
                if (weapon != null)
                    _sbCheckerBaseInput.MessageXML.AddFail("Melee Attack Bonus-" + weapon.Weapon_FullName(), weaponBonusComputed.ToString(), weaponBonusSB.ToString(), formula);
                else
                    _sbCheckerBaseInput.MessageXML.AddFail("Melee Attack Bonus-" + natural_weapon.name, weaponBonusComputed.ToString(), weaponBonusSB.ToString(), formula);
            }
        }

        private void CheckOneWeaponMeleeNatural(NaturalWeapon natural_weapon, ref int weaponBonusComputed,
               ref List<string> Bonuses, string MW, bool NaturalMultipleWepons, ref string formula, int MeleeWeaponCount)
        {
            string weaponBonus = string.Empty;
            string weaponsDamage = string.Empty;
            string holdMeleeWeapon;
            int abilityBonus = _sbCheckerBaseInput.AbilityScores.StrMod;

            int BAB = Convert.ToInt32(Utility.GetNonParenValue(_sbCheckerBaseInput.MonsterSB.BaseAtk));

            if (_sbCheckerBaseInput.MonsterSBSearch.HasDefensiveAbility("incorporeal"))
            {
                abilityBonus = _sbCheckerBaseInput.AbilityScores.DexMod;
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Weapon Finesse")) //Natural weapons are considered light weapons
            {
                abilityBonus = _sbCheckerBaseInput.AbilityScores.DexMod;
            }           

            weaponBonusComputed = BAB + abilityBonus + _sizeMod;
            formula = BAB.ToString() + " BAB +" + abilityBonus.ToString() + " abilityBonus " + CommonMethods.GetStringValue(_sizeMod) + " _sizeMod";
            int multiWeaponPenalty = -5;

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Multiattack"))
            {
                multiWeaponPenalty = -2;
                formula += " -2 Multiattack";
            }

            if (!_sbCheckerBaseInput.MonsterSBSearch.HasSQ("multiweapon mastery"))
            {
                if ((natural_weapon.attack_type == "Secondary" && MeleeWeaponCount != 1) || NaturalMultipleWepons)
                {
                    weaponBonusComputed += multiWeaponPenalty;
                    formula += PathfinderConstants.SPACE + multiWeaponPenalty.ToString() + " Secondary";
                }
            }

            //if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Combat Expertise"))
            //{
            //    int mod = -1;
            //    mod -= BAB / 4;
            //    weaponBonusComputed += mod;
            //    formula += " -" + mod.ToString() + "  Combat Expertise";
            //}

            int weaponFocusMod = 0;
            string weaponName = natural_weapon.name.ToLower();
            if (weaponName == "talons") weaponName = "talon";

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Weapon Focus (" + weaponName + PathfinderConstants.PAREN_RIGHT))
            {
                weaponBonusComputed++;
                weaponFocusMod++;
                formula += " +1 Weapon Focus";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Greater Weapon Focus (" + weaponName + PathfinderConstants.PAREN_RIGHT))
            {
                weaponBonusComputed++;
                weaponFocusMod++;
                formula += " +1 Greater Weapon Focus";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.IsMythic)
            {
                if (_sbCheckerBaseInput.MonsterSBSearch.HasMythicFeat("Weapon Focus (" + weaponName + PathfinderConstants.PAREN_RIGHT))
                {
                    weaponBonusComputed -= weaponFocusMod;
                    weaponBonusComputed += weaponFocusMod * 2;
                    formula += " +" + ((weaponFocusMod * 2) - weaponFocusMod).ToString() + " Mythic Weapon Focus";
                }
            }           

            if (_sbCheckerBaseInput.IndvSB != null)
            {
                weaponBonusComputed += _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.NaturalAttack,
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
            string holdMeleeWeapon;

            int SizeDifference = 0;
            StatBlockInfo.SizeCategories MonSize = StatBlockInfo.GetSizeEnum(_size);
            StatBlockInfo.SizeCategories WeaponSize = weapon.WeaponSize;
            if (WeaponSize == StatBlockInfo.SizeCategories.Medium && MonSize != WeaponSize) WeaponSize = MonSize;
            if (MonSize != WeaponSize) SizeDifference = Math.Abs(StatBlockInfo.GetSizeDifference(MonSize, WeaponSize));

            holdMeleeWeapon = MW.Trim();
            int Pos = holdMeleeWeapon.IndexOf(PathfinderConstants.SPACE);
            if (Pos == 1)
            {
                holdMeleeWeapon = holdMeleeWeapon.Substring(Pos).Trim();
            }

            int BAB = Convert.ToInt32(Utility.GetNonParenValue(_sbCheckerBaseInput.MonsterSB.BaseAtk));

            if (holdMeleeWeapon.Contains("flurry of blows"))
            {
                holdMeleeWeapon = holdMeleeWeapon.Replace("flurry of blows", string.Empty).Trim();
                BAB = _racialHDValue + _sbCheckerBaseInput.CharacterClasses.GetNonMonkBABValue() + _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Monk") - 2;
            }


            _weaponChecker.ParseSingleWeapon(weapon, ref weaponBonus, ref weaponsDamage, ref holdMeleeWeapon, ref Bonuses);

            try
            {
                _weaponChecker.CheckMeleeWeaponDamage(weapon, weaponsDamage, TwoWeaponFighting, BiteAttack, weaponCount, weaponIndex,
                     _size, _sbCheckerBaseInput.AbilityScores, _sbCheckerBaseInput.MonsterSBSearch, _sbCheckerBaseInput.MessageXML, _sbCheckerBaseInput.CharacterClasses.FindClassLevel("fighter"), _acDefendingMod, MagicWeapon, GreaterMagicWeapon, _sbCheckerBaseInput.IndvSB);
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("CheckOneWeaponMeleeNonNatural - CheckMeleeWeaponDamage", weapon.name + "   " + ex.Message);
            }
            int AbilityBonus = _sbCheckerBaseInput.AbilityScores.StrMod;

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Weapon Finesse"))
            {
                if (IsWeaponFinesseCategory(weapon))
                {
                    AbilityBonus = _sbCheckerBaseInput.AbilityScores.DexMod;
                }
            }

            if (MW.Contains("dueling sword") && _sbCheckerBaseInput.MonsterSBSearch.HasFeat("Weapon Finesse") && _sbCheckerBaseInput.MonsterSBSearch.HasFeat("Exotic Weapon Proficiency (Aldori dueling sword)"))
            {
                AbilityBonus = _sbCheckerBaseInput.AbilityScores.DexMod;
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasDefensiveAbility("incorporeal"))
            {
                AbilityBonus = _sbCheckerBaseInput.AbilityScores.DexMod;
            }

            weaponBonusComputed = BAB + AbilityBonus + _sizeMod;
            formula += BAB.ToString() + " BAB +" + AbilityBonus.ToString() + " AbilityBonus +" + _sizeMod.ToString() + " _sizeMod";

            if (weapon.Broken)
            {
                weaponBonusComputed -= 2;
                formula += " -2 Broken";
            }
            if (BiteAttack && weapon.name == "bite")
            {
                if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Multiattack"))
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
                weaponBonusComputed += _sbCheckerBaseInput.MonsterSBSearch.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.NaturalAttack,
                                             OnGoingStatBlockModifier.StatBlockModifierSubTypes.None, ref formula);
            }

            if ((weapon.WeaponSpecialAbilities.WeaponSpecialAbilitiesValue & WeaponSpecialAbilitiesEnum.Furious) == WeaponSpecialAbilitiesEnum.Furious)
            {
                weaponBonusComputed += 2;
                formula += " +2 furious";
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

            if (SizeDifference > 0)
            {
                //assume small CreatureTypeFoundation has small weapon
                if (!(MonSize == StatBlockInfo.SizeCategories.Small && WeaponSize == StatBlockInfo.SizeCategories.Medium))
                {
                    weaponBonusComputed -= SizeDifference * 2;
                    formula += "-" + (SizeDifference * 2).ToString() + " weapon size difference";
                }
            }

            weaponBonusComputed += _weaponChecker.PoleArmTraingMods(weapon, _sbCheckerBaseInput.MonsterSBSearch, _sbCheckerBaseInput.CharacterClasses.FindClassLevel("fighter"), ref formula);

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

            if (_sbCheckerBaseInput.MonsterSBSearch.HasDefensiveAbility("weapon training"))
            {
                int fighterLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("fighter");
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

            if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("classic duelist") && (weapon.name.ToLower() == "rapier" || weapon.search_name.ToLower() == "short sword" || weapon.name.ToLower() == "cutlass"))
            {
                weaponBonusComputed++;
                formula += " +1 classic duelist";
            }

            string holdFocus = hold.Replace("scorpion", string.Empty).Trim();
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Weapon Focus (" + holdFocus + PathfinderConstants.PAREN_RIGHT))
            {
                weaponBonusComputed++;
                formula += " +1 Weapon Focus";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Greater Weapon Focus (" + holdFocus + PathfinderConstants.PAREN_RIGHT))
            {
                weaponBonusComputed++;
                formula += " +1 Greater Weapon Focus";
            }

            if (!BiteAttack)
            {
                if (TwoWeaponFighting && !_sbCheckerBaseInput.MonsterSBSearch.HasSQ("multiweapon mastery"))
                {
                    if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Two-Weapon Fighting"))
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

            if (_sbCheckerBaseInput.MonsterSBSearch.HasSpecialAttackGeneral("weapon training"))
            {
                weaponBonusComputed += _sbCheckerBaseInput.MonsterSBSearch.GetWeaponsTrainingModifier(hold2, ref formula);
            }

            if (weapon.Masterwork && weapon.EnhancementBonus == 0)
            {
                weaponBonusComputed++;
                formula += " +1 masterwork";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Shield Master") && weapon.name.Contains("shield"))
            {
                formula += " +" + weapon.EnhancementBonus.ToString() + " Shield Master";
                weaponBonusComputed += weapon.EnhancementBonus;
            }  

            if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("weapon master"))
            {
                int level = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Fighter");
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

            if (_sbCheckerBaseInput.MonsterSBSearch.HasTemplate("graveknight"))
            {
                weaponBonusComputed += 2;
                formula += " +2 Sacrilegious Aura";
            }

            bool ignore = false;
            if (_sbCheckerBaseInput.IndvSB != null)
            {
                WeaponCommon weaponCommon = new WeaponCommon(_sbCheckerBaseInput, _equipmentData, _naturalWeaponBusiness);
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

        private bool HasNonNaturalWeapon(List<string> weaponsList)
        {
            Weapon weapon = null;
            NaturalWeapon natural_weapon = null;

            foreach (string MW in weaponsList)
            {
                weapon = null;
                natural_weapon = null;
                bool MagicWeapon;
                bool GreaterMagicWeapon;

                WeaponCommon weaponCommon = new WeaponCommon(_sbCheckerBaseInput, _equipmentData, _naturalWeaponBusiness);
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

        private int NonNaturalWeaponCount(List<string> weaponsList)
        {
            int count = 0;
            Weapon weapon = null;
            NaturalWeapon natural_weapon = null;

            foreach (string MW in weaponsList)
            {
                weapon = null;
                natural_weapon = null;
                bool MagicWeapon;
                bool GreaterMagicWeapon;

                WeaponCommon weaponCommon = new WeaponCommon(_sbCheckerBaseInput, _equipmentData, _naturalWeaponBusiness);
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
