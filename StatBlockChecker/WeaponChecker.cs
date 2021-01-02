using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EquipmentBasic;
using CommonStatBlockInfo;
using ClassManager;
using OnGoing;
using Utilities;
using StatBlockCommon;
using CommonInterFacesDD;
using StatBlockCommon.Individual_SB;
using PathfinderDomains;

namespace StatBlockChecker
{
    public class WeaponChecker
    {
        private ClassMaster _characterClasses;
        private List<string> magicInEffect;
        private Dictionary<IEquipment, int> Weapons;
        private string RaceName;
        private bool DontUseRacialHD;
        private RaceBase.RaceType RaceBaseType;
        private bool HasRaceBase;
        private int RacialHDValue;

        public WeaponChecker(ClassMaster CharacterClasses, List<string> magicInEffect, Dictionary<IEquipment, int> Weapons, string RaceName,
               bool DontUseRacialHD, RaceBase.RaceType RaceBaseType, bool HasRaceBase, int RacialHDValue)
        {
            _characterClasses = CharacterClasses;
            this.magicInEffect = magicInEffect;
            this.Weapons = Weapons;
            this.RaceName = RaceName;
            this.DontUseRacialHD = DontUseRacialHD;
            this.RaceBaseType = RaceBaseType;
            this.HasRaceBase = HasRaceBase;
            this.RacialHDValue = RacialHDValue;
        }

        public void ParseSingleWeapon(Weapon weapon, ref string weaponBonus, ref string weaponsDamage, ref string holdWeapon, ref List<string> Bonuses)
        {
            int Pos = 0;
            holdWeapon = holdWeapon.Replace("hand of the apprentice", string.Empty).Trim();

            if (weapon.EnhancementBonus > 0)
            {
                Pos = holdWeapon.IndexOf(" ");
                holdWeapon = holdWeapon.Substring(Pos).Trim();
            }

            Pos = holdWeapon.IndexOf("(with ");
            if (Pos >= 0)
            {
                int Pos3 = holdWeapon.IndexOf(")");
                string temp2 = holdWeapon.Substring(Pos, Pos3 - Pos + 1);
                holdWeapon = holdWeapon.Replace(temp2, string.Empty);
            }
            int Pos2 = holdWeapon.IndexOf("(");
            if (Pos2 == 0) Pos2 = holdWeapon.IndexOf("(",1);

            if (Pos2 == -1)
            {
                //no damage, e.g. net
                FindWeaponBonus(ref weaponBonus, ref Bonuses, ref holdWeapon);
                return;
            }

            string temp = holdWeapon.Substring(0, Pos2);

            temp = temp.Replace("touch", string.Empty).Trim();
            Pos = temp.IndexOf(" with ");
            if (Pos >= 0)
            {
                Pos2 = temp.LastIndexOf(" +");
                string temp2 = temp.Substring(Pos, Pos2 - Pos);
                temp = temp.Replace(temp2, string.Empty);
            }
            FindWeaponBonus(ref weaponBonus, ref Bonuses, ref temp);
            
            weaponsDamage = holdWeapon.Substring(Pos2).Trim();
        }

        private static void FindWeaponBonus(ref string weaponBonus, ref List<string> Bonuses, ref string temp)
        {
            int Pos = temp.IndexOf("+");
            if (Pos == -1)
            {
                Pos = temp.IndexOf("-");
            }
            if (Pos == 0)
            {
                temp = temp.Substring(Pos + 1);
                Pos = temp.IndexOf("+");
                if (Pos == -1)
                {
                    Pos = temp.IndexOf("-");
                }
            }

            if (Pos == -1)
            {
                throw new Exception("FindWeaponBonus: No weapon bonus for " + temp);
            }

            try
            {
                weaponBonus = temp.Substring(Pos);
                weaponBonus = weaponBonus.Replace("+", string.Empty).Trim();
                Bonuses = weaponBonus.Split('/').ToList<string>();
            }
            catch (Exception ex)
            {
                Bonuses = new List<string>();
                throw new Exception("FindWeaponBonus: " + weaponBonus + " " + ex.Message);
            }
        }

        public void ParseSingleNaturalWeapon(NaturalWeapon natural_weapon, ref string weaponBonus, ref string weaponsDamage, ref string holdWeapon, ref List<string> Bonuses)
        {
            int Pos2 = holdWeapon.IndexOf("(");
            if (Pos2 == -1)
            {
                throw new Exception("ParseSingleNaturalWeapon: damage missing parens-- " + holdWeapon);
            }

            string temp = holdWeapon.Substring(0, Pos2);
            int Pos = temp.IndexOf("+");
            if (Pos == -1)
            {
                Pos = temp.IndexOf("-");
            }
            weaponBonus = temp.Substring(Pos);
            weaponBonus = weaponBonus.Replace("touch", string.Empty);
            weaponBonus = weaponBonus.Replace("+", string.Empty).Trim();
            Bonuses = weaponBonus.Split('/').ToList<string>();
            weaponsDamage = holdWeapon.Substring(Pos2).Trim();
        }

        public void CheckRangedWeaponDamage(Weapon weapon, string weaponsDamage, string size, AbilityScores.AbilityScores _abilityScores,
                  MonSBSearch _monSBSearch, StatBlockMessageWrapper _messageXML, bool MagicWeapon, bool GreaterMagicWeapon, IndividualStatBlock_Combat _indvSB)
        {            
            string formula = string.Empty;                                

            StatBlockInfo.SizeCategories MonSize = StatBlockInfo.GetSizeEnum(size);

            if (_monSBSearch.HasSQ("undersized weapons"))
            {
                MonSize = StatBlockInfo.ReduceSize(MonSize);
            }

            StatBlockInfo.HDBlockInfo damageComputed = new StatBlockInfo.HDBlockInfo();
            if (weapon.name == "Sling" && _monSBSearch.HasGear("stones"))
            {
                damageComputed.ParseHDBlock(weapon.damage_small);
            }
            else if (MonSize == StatBlockInfo.SizeCategories.Medium)
            {
                damageComputed.ParseHDBlock(weapon.damage_medium);
            }
            else if (MonSize == StatBlockInfo.SizeCategories.Small)
            {
                damageComputed.ParseHDBlock(weapon.damage_small);
            }
            else
            {
                damageComputed.ParseHDBlock(StatBlockInfo.ChangeWeaponDamageSize(weapon.damage_medium, MonSize));
            }

            if(!weaponsDamage.Contains("entangle"))
            {
               ComputeRangeMod(weapon, _abilityScores, _monSBSearch,_messageXML,  ref formula, ref damageComputed);
            }

            if (_monSBSearch.HasSpecialAttackGeneral("weapon training"))
            {
                damageComputed.Modifier += _monSBSearch.GetWeaponsTrainingModifier(weapon.search_name, ref formula);              
            }

            if(_monSBSearch.HasClassArchetype("crossbowman"))
            {
                int fighterLevel = _characterClasses.FindClassLevel("fighter");
                if (fighterLevel >= 3)
                {
                    int dexBonus = _abilityScores.DexMod / 2;
                    if (dexBonus <= 0) dexBonus = 1;
                    damageComputed.Modifier += dexBonus;
                    formula += " +" + dexBonus.ToString() + " crossbowman deadshot";
                }

                if (fighterLevel >= 5)
                {
                    int tempBonus = 1;
                    if (fighterLevel >= 9) tempBonus++;
                    if (fighterLevel >= 13) tempBonus++;
                    if (fighterLevel >= 17) tempBonus++;
                    damageComputed.Modifier += tempBonus;
                    formula += " +" + tempBonus.ToString() + " crossbowman crossbow expert";
                }
            }

            if (_monSBSearch.HasClassArchetype("archer"))
            {
                int fighterLevel = _characterClasses.FindClassLevel("fighter");               

                if (fighterLevel >= 5)
                {
                    int tempBonus = 1;
                    if (fighterLevel >= 9) tempBonus++;
                    if (fighterLevel >= 13) tempBonus++;
                    if (fighterLevel >= 17) tempBonus++;
                    damageComputed.Modifier += tempBonus;
                    formula += " +" + tempBonus.ToString() + " Expert Archer";
                }
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

            if (_monSBSearch.HasFeat("Weapon Specialization (" + hold + ")"))
            {
                formula += " +2 Weapon Specialization";
                damageComputed.Modifier += 2;
            }

            if (_monSBSearch.HasFeat("Greater Weapon Specialization (" + hold + ")"))
            {
                formula += " +2 Greater Weapon Specialization";
                damageComputed.Modifier += 2;
            }

            if (weapon.WeaponSpecialMaterial == WeaponSpecialMaterials.AlchemicalSilver && (weapon.slashing || weapon.piercing))
            {
                damageComputed.Modifier--;
                formula += " -1 Alchemical Silver";
            }

            if (weapon.EnhancementBonus > 0)
            {
                damageComputed.Modifier += weapon.EnhancementBonus;
                formula += " +" + weapon.EnhancementBonus.ToString() + " Enhancement Bonus";
            }

            if (_abilityScores.StrMod != 0 && Utility.IsThrownWeapon(weapon.search_name.ToLower()))
            {                
                int MeleeModUsed = _abilityScores.StrMod;

                if (_monSBSearch.HasDefensiveAbility("incorporeal"))
                {
                    MeleeModUsed = _abilityScores.DexMod;
                }
                formula += " +" + MeleeModUsed.ToString() + " Str Bonus Used- Thrown";
                damageComputed.Modifier += Convert.ToInt32(MeleeModUsed);
            }

            if (weapon.name.Contains("bow") && !weapon.name.ToLower().Contains("composite") && !weapon.name.ToLower().Contains("cross") && _abilityScores.StrMod < 0)
            {
                damageComputed.Modifier += _abilityScores.StrMod;
            }

            bool ignoreEnhancement = false;
            WeaponCommon weaponCommon = new WeaponCommon(magicInEffect, Weapons, _indvSB, _messageXML, _monSBSearch, _characterClasses, RaceName, DontUseRacialHD, RaceBaseType, HasRaceBase, RacialHDValue);

            weaponCommon.GetOnGoingDamageMods(MagicWeapon, GreaterMagicWeapon, _indvSB, ref formula, ref damageComputed, ref ignoreEnhancement);

            weaponsDamage = weaponsDamage.Replace("(", string.Empty);
            weaponsDamage = weaponsDamage.Replace(")", string.Empty);
            weaponsDamage = weaponsDamage.Replace("nonlethal", string.Empty);
            int Pos = weaponsDamage.IndexOf("/");
            string weaponCrit = string.Empty;
            if (Pos >= 0)
            {
                weaponCrit = weaponsDamage.Substring(Pos + 1);
                weaponsDamage = weaponsDamage.Substring(0, Pos);
            }
            StatBlockInfo.HDBlockInfo damageSB = new StatBlockInfo.HDBlockInfo();
            damageSB.ParseHDBlock(weaponsDamage.Trim());

            if (weapon.name == "rock" && _monSBSearch.HasSpecialAttackGeneral("rock throwing"))
            {
                if (damageComputed.Modifier != (_abilityScores.StrMod * 1.5))
                {
                    _messageXML.AddFail("Ranged Attack Damage- Rock ", (_abilityScores.StrMod * 1.5).ToString(), damageComputed.Modifier.ToString());
                }
            }


            if (weapon.name == "bomb" && _characterClasses.HasClass("alchemist"))
            {
                damageComputed = new StatBlockInfo.HDBlockInfo();
                damageComputed.HDType = StatBlockInfo.HitDiceCategories.d6;
                int alchemistLevel = _characterClasses.FindClassLevel("alchemist");
                damageComputed.Multiplier = ((alchemistLevel - 1) / 2) + 1;
                damageComputed.Modifier = _abilityScores.IntMod;

                formula = "+" + _abilityScores.IntMod.ToString() + " Int mod";
            }

            if (damageSB.Equals(damageComputed))
            {
                _messageXML.AddPass("Ranged Attack Damage " + weapon.Weapon_FullName(),formula);
            }
            else
            {
                _messageXML.AddFail("Ranged Attack Damage " + weapon.Weapon_FullName(), damageComputed.ToString(), damageSB.ToString(),formula);
            }

            //string tempWeaponCrit = weapon.critical.Replace("/×2", string.Empty);
            //tempWeaponCrit = tempWeaponCrit.Replace((char)(8211), Char.Parse("-"));
            //if (tempWeaponCrit == weaponCrit)
            //{
            //    _messageXML.AddPass("Ranged Attack Critical- " + weapon.Weapon_FullName());
            //}
            //else
            //{
            //    _messageXML.AddFail("Ranged Attack Critical- " + weapon.Weapon_FullName(), weapon.critical, weaponCrit);
           
            //}
        }
       

        private static void ComputeRangeMod(Weapon weapon, AbilityScores.AbilityScores _abilityScores, MonSBSearch _monSBSearch, StatBlockMessageWrapper _messageXML,
                  ref string formula, ref StatBlockInfo.HDBlockInfo damageComputed)
        {
            int RangedModUsed = _abilityScores.StrMod;
            string rangeModUsedString = " Str";

            if (_monSBSearch.HasDefensiveAbility("incorporeal"))
            {
                RangedModUsed = 0; // _abilityScores.DexMod;
                rangeModUsedString = " Dex";
            }

            if (weapon.name.Contains("composite") && weapon.CompositeBonus == 0)
                _messageXML.AddFail("Composite Weapon", "Str Bonus Missing");

            if (!Utility.IsThrownWeapon(weapon.search_name.ToLower()))
            {
                if (!weapon.uses_ammunition || weapon.name == "Sling" || (weapon.name.Contains("composite") && weapon.CompositeBonus > 0))
                {
                    if (weapon.name.Contains("composite") && weapon.CompositeBonus > 0)
                    {
                        if (weapon.CompositeBonus >= RangedModUsed)
                        {
                            damageComputed.Modifier += Convert.ToInt32(RangedModUsed);
                            formula += " +" + RangedModUsed.ToString() + " composite";
                        }
                        else
                        {
                            damageComputed.Modifier += Convert.ToInt32(weapon.CompositeBonus);
                            formula += " +" + weapon.CompositeBonus.ToString() + " composite";
                        }
                    }
                    else
                    {
                        if (!weapon.name.ToLower().Contains("cross"))
                        {
                            damageComputed.Modifier += Convert.ToInt32(RangedModUsed);
                            formula += " +" + RangedModUsed.ToString() + rangeModUsedString;
                        }
                    }
                }
            }

            if (weapon.category == "Ranged Weapons" && !weapon.name.Contains("composite") && weapon.name.Contains("bow") && RangedModUsed < 0)
            {
                if (!weapon.name.ToLower().Contains("crossbow"))
                {
                    damageComputed.Modifier += Convert.ToInt32(RangedModUsed);
                    formula += " +" + RangedModUsed.ToString() + rangeModUsedString;
                }
            }
        }

        public void CheckMeleeWeaponDamage(Weapon weapon, string weaponsDamage, bool TwoWeaponFighting, bool BiteAttack, int weaponCount, int weaponIndex,
            string Size, AbilityScores.AbilityScores _abilityScores, MonSBSearch _monSBSearch, StatBlockMessageWrapper _messageXML, int FighterLevel, int ACDefendingMod, bool MagicWeapon, bool GreaterMagicWeapon, IndividualStatBlock_Combat _indvSB)
        {            
            string formula = string.Empty;
            bool SizeDifference = false;         
         

            StatBlockInfo.SizeCategories MonSize = StatBlockInfo.GetSizeEnum(Size);
            StatBlockInfo.SizeCategories WeaponSize = weapon.WeaponSize;
            if (MonSize != WeaponSize) SizeDifference = true;

            if (_monSBSearch.HasSQ("undersized weapons"))
                MonSize = StatBlockInfo.ReduceSize(MonSize);

            StatBlockInfo.HDBlockInfo damageComputed = new StatBlockInfo.HDBlockInfo();
            ShieldSpecialAbilitiesEnum shieldSA = weapon.ShieldSpecialAbilities.ShieldSpecialAbilityValues;

            bool ShieldBashBoost = false;
            if ((shieldSA & ShieldSpecialAbilitiesEnum.Bashing) == ShieldSpecialAbilitiesEnum.Bashing)
            {
                ShieldBashBoost = true;
                MonSize = StatBlockInfo.IncreaseSize(MonSize);
                MonSize = StatBlockInfo.IncreaseSize(MonSize);
            }

            bool HasNewWeaponDamge = false;
            Weapon weaponDamage = null;
            if (weapon.search_name.ToLower() == "halfling sling staff")
            {
               weaponDamage = Weapon.GetWeaponByName("club");
               HasNewWeaponDamge = true;
            }


            weaponsDamage = weaponsDamage.Replace("(", string.Empty);
            weaponsDamage = weaponsDamage.Replace(")", string.Empty);
            weaponsDamage = weaponsDamage.Replace("nonlethal", string.Empty);
            int Pos = weaponsDamage.IndexOf("/");
            string weaponCrit = string.Empty;
            if (Pos >= 0)
            {
                weaponCrit = weaponsDamage.Substring(Pos + 1);
                weaponsDamage = weaponsDamage.Substring(0, Pos);
            }
            StatBlockInfo.HDBlockInfo damageSB = new StatBlockInfo.HDBlockInfo();
            if (weaponsDamage.Contains("|"))
            {
                Pos = weaponsDamage.IndexOf("|");
                weaponsDamage = weaponsDamage.Substring(0, Pos);
            }
            damageSB.ParseHDBlock(weaponsDamage.Trim());

            if (weapon.@double)
            {
                //for double weapons assume the damage in the string is one of the ends
                if (weapon.damage_medium.Contains(damageSB.HDType.ToString()))
                    weapon.damage_medium = damageSB.Multiplier.ToString() + damageSB.HDType.ToString();

                if (weapon.damage_small.Contains(damageSB.HDType.ToString()))
                    weapon.damage_small = damageSB.Multiplier.ToString() + damageSB.HDType.ToString();
            }

            if (MonSize == StatBlockInfo.SizeCategories.Medium && !ShieldBashBoost && !SizeDifference)
            {
                if (HasNewWeaponDamge)
                    damageComputed.ParseHDBlock(weapon.damage_medium);
                else
                    damageComputed.ParseHDBlock(weapon.damage_medium);
            }
            else if (MonSize == StatBlockInfo.SizeCategories.Small && !ShieldBashBoost && !SizeDifference)
            {
                if (HasNewWeaponDamge)
                    damageComputed.ParseHDBlock(weapon.damage_small);
                else
                    damageComputed.ParseHDBlock(weapon.damage_small);
            }
            else if (!SizeDifference)
            {
                if (HasNewWeaponDamge)
                    damageComputed.ParseHDBlock(StatBlockInfo.ChangeWeaponDamageSize(weaponDamage.damage_medium, MonSize));
                else
                    damageComputed.ParseHDBlock(StatBlockInfo.ChangeWeaponDamageSize(weapon.damage_medium, MonSize));
            }
            else
            {
                //SizeDifference = true
                if (MonSize == StatBlockInfo.SizeCategories.Small)
                    damageComputed.ParseHDBlock(weapon.damage_small);
                else
                {
                    StatBlockInfo.SizeCategories tempSize = StatBlockInfo.SizeCategories.Medium;
                    if (WeaponSize == tempSize) tempSize = MonSize;
                    damageComputed.ParseHDBlock(StatBlockInfo.ChangeWeaponDamageSize(weapon.damage_medium, tempSize));
                }
            }

            double StrBonus;
            bool OneHandedAsTwo;
            string ModUsed = ComputeStrBonus(weapon, TwoWeaponFighting, BiteAttack, weaponCount, weaponIndex, _monSBSearch, _abilityScores, out StrBonus, out OneHandedAsTwo);

            formula += " +" + StrBonus.ToString() + " " + ModUsed + " Bonus Used";
            damageComputed.Modifier += Convert.ToInt32(StrBonus);          

            if (weapon.WeaponSpecialMaterial == WeaponSpecialMaterials.AlchemicalSilver && (weapon.slashing || weapon.piercing))
            {
                damageComputed.Modifier--;
                formula += " -1 Alchemical Silver";
            }

            if (_monSBSearch.HasSQ("hulking changeling"))
            {
                damageComputed.Modifier++;
                formula += " +1 hulking changeling";
            }

            string hold2 = null;
            if (weapon.NamedWeapon)
                hold2 = weapon.BaseWeaponName;
            else
                hold2 = weapon.search_name;

            if (_monSBSearch.HasSpecialAttackGeneral("weapon training"))
                damageComputed.Modifier += _monSBSearch.GetWeaponsTrainingModifier(hold2, ref formula);

            damageComputed.Modifier += PoleArmTraingMods(weapon, _monSBSearch, FighterLevel, ref formula);

            bool ignoreEnhancement = false;

            if (weapon.name.ToLower() == "unarmed strike")
                damageComputed.Modifier += _monSBSearch.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.NaturalDamage,
                                             OnGoingStatBlockModifier.StatBlockModifierSubTypes.None, ref formula);
            else
            {
                WeaponCommon weaponCommon = new WeaponCommon(magicInEffect, Weapons, _indvSB, _messageXML, _monSBSearch, _characterClasses, RaceName, DontUseRacialHD, RaceBaseType, HasRaceBase, RacialHDValue);
               
                weaponCommon.GetOnGoingDamageMods(MagicWeapon, GreaterMagicWeapon, _indvSB, ref formula, ref damageComputed, ref ignoreEnhancement);
            }

            if ((weapon.WeaponSpecialAbilities.WeaponSpecialAbilitiesValue & WeaponSpecialAbilitiesEnum.Furious) == WeaponSpecialAbilitiesEnum.Furious)
            {
                damageComputed.Modifier += 2;
                formula += " +1 furious";
            }          

            string hold = null;
            if (weapon.NamedWeapon)
                hold = weapon.BaseWeaponName.ToLower();
            else
                hold = weapon.search_name.ToLower();

            if (hold.Contains("aldori"))
                hold = hold.Replace("aldori", "Aldori");

            if (_characterClasses.HasClass("aldori swordlord"))
            {
                if (hold.Contains("dueling sword"))
                {
                    int tenpMod = _monSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Dexterity);
                    formula +=  " +" + tenpMod.ToString() + " Deft Strike";
                    damageComputed.Modifier += tenpMod;
                }
            }

            if (_monSBSearch.HasFeat("Weapon Specialization (" + hold + ")"))
            {
                formula += " +2 Weapon Specialization";
                damageComputed.Modifier += 2;
            }

            if (_monSBSearch.HasFeat("Greater Weapon Specialization (" + hold + ")"))
            {
                formula += " +2 Greater Weapon Specialization";
                damageComputed.Modifier += 2;
            }

            if (_monSBSearch.HasTemplate("graveknight"))
            {
                damageComputed.Modifier += 2;
                formula += " +2 Sacrilegious Aura";
            }

            if (_monSBSearch.HasFeat("Shield Master") && weapon.name.Contains("shield"))
            {
                formula += " +" + weapon.EnhancementBonus + " Shield Master";
                damageComputed.Modifier += weapon.EnhancementBonus;
            }

            //no enchantment bonus for shield bash
            if (weapon.EnhancementBonus > 0 && ACDefendingMod == 0 && !weapon.name.Contains("shield") && !ignoreEnhancement)
            {
                formula += " +" + weapon.EnhancementBonus.ToString() + " Enhancement Bonus";
                damageComputed.Modifier += weapon.EnhancementBonus;
            }

            if (weapon.Broken)
            {
                formula += " -2 Broken";
                damageComputed.Modifier -= 2;
            }

           

            if (damageSB.Equals(damageComputed))
            {
                _messageXML.AddPass("Melee Attack Damage-" + weapon.Weapon_FullName(), formula);
            }
            else
            {
                int temp5 = damageComputed.Modifier - 1;

                if (OneHandedAsTwo && damageSB.Modifier == temp5 && !_monSBSearch.HasShield()) // not all SB use two handed weapons; not error, their choice
                {
                    _messageXML.AddInfo(weapon.Weapon_FullName() + " could be used two-handed for extra damage");
                    _messageXML.AddPass("Melee Attack Damage-" + weapon.Weapon_FullName());
                }
                else
                {
                    _messageXML.AddFail("Melee Attack Damage-" + weapon.Weapon_FullName(), damageComputed.ToString(), damageSB.ToString(), formula);
                    if (OneHandedAsTwo) _messageXML.AddFail("Melee Attack Damage-", "Weapon could be used As Two-Handed?");
                }
            }
            string tempWeaponCrit = weapon.critical.Replace("/×2", string.Empty);
            tempWeaponCrit = tempWeaponCrit.Replace((char)(8211), Char.Parse("-"));
            //if (tempWeaponCrit == weaponCrit)
            //{
            //    _messageXML.AddPass("Melee Attack Critical- " + weapon.Weapon_FullName());
            //}
            //else
            //{
            //    _messageXML.AddFail("Melee Attack Critical- " + weapon.Weapon_FullName(), weapon.critical, weaponCrit);

            //}
        }

       
        public int PoleArmTraingMods(Weapon weapon, MonSBSearch _monSBSearch, int FighterLevel, ref string formula)
        {
            if (_monSBSearch.HasSpecialAttackGeneral("polearm training"))
            {
                if (IsPoleArm(weapon.name) || IsSpear(weapon.name))
                {
                    int PolearmTraining = 0;
                    if (FighterLevel >= 5) PolearmTraining += 1;
                    if (FighterLevel >= 9) PolearmTraining += 1;
                    if (FighterLevel >= 13) PolearmTraining += 1;
                    if (FighterLevel >= 17) PolearmTraining += 1;

                    formula += " +" + PolearmTraining.ToString() + " Polearm Training";
                    return PolearmTraining;
                }
            }

            return 0;
        }

        private static bool IsSpear(string weaponName)
        {
            List<string> spears = new List<string> { "amentum", "boar spear", "javelin", "harpoon", "lance", "longspear", "pilum", "shortspear", "sibat", "spear", "tiger fork", "trident" };
            return spears.Contains(weaponName.ToLower());
        }

        private static bool IsPoleArm(string weaponName)
        {
            List<string> polearms = new List<string> { "bardiche", "bec de corbin", "bill", "glaive", "glaive-guisarme", "guisarme", "halberd", "hooked lance", "lucerne hammer", "mancatcher", "monk's spade", "naginata", "nodachi", "ranseur", "rohomphaia", "tepoztopili", "tiger fork" };
            return polearms.Contains(weaponName.ToLower());
        }

        private static string ComputeStrBonus(Weapon weapon, bool TwoWeaponFighting, bool BiteAttack, int weaponCount, int weaponIndex,
                   MonSBSearch _monSBSearch, AbilityScores.AbilityScores _abilityScores, out double StrBonus, out bool OneHandedAsTwo)
        {
            int MeleeModUsed = _abilityScores.StrMod;
            string BonuesMod  = "Str";

            if (_monSBSearch.HasDefensiveAbility("incorporeal"))
            {
                MeleeModUsed = _abilityScores.DexMod;
                BonuesMod = "Dex";
            }

            StrBonus = MeleeModUsed;

            if (TwoWeaponFighting && weaponIndex > 1)
            {
                if (StrBonus > 0)
                {
                    StrBonus /= 2;
                    StrBonus = Math.Floor(StrBonus);
                }
            }
            if (_monSBSearch.HasFeat("Double Slice") && TwoWeaponFighting && weaponIndex > 1)
            {
                StrBonus = MeleeModUsed;
            }

            if (BiteAttack && weapon.name == "bite")
            {
                StrBonus /= 2;
                StrBonus = Math.Floor(StrBonus);
            }

            if (weapon.category.Contains("Two-Handed Melee") && !TwoWeaponFighting && !_monSBSearch.HasShield())
            {
                if (MeleeModUsed > 0)
                {
                    StrBonus = 1.5 * StrBonus;
                    StrBonus = Math.Floor(StrBonus);
                }
            }

            OneHandedAsTwo = false;
            if (CanUseTwoHandedStrBonus(weapon, weaponCount, _monSBSearch, StrBonus))
            {
                StrBonus = 1.5 * StrBonus;
                StrBonus = Math.Floor(StrBonus);
                OneHandedAsTwo = true;
            }

            if (weapon.search_name.ToLower() == "aldori dueling sword" && !TwoWeaponFighting)
            {
                StrBonus = 1.5 * StrBonus;
                StrBonus = Math.Floor(StrBonus);
            }

            return BonuesMod;
        }

        private static bool CanUseTwoHandedStrBonus(Weapon weapon, int weaponCount, MonSBSearch _monSBSearch, double StrBonus)
        {
            if (StrBonus > 0 && weaponCount == 1 && weapon.category == "One-Handed Melee Weapon" && !_monSBSearch.HasShield() && (CanUseAsTwoHandedWeapon(weapon.name) && CanUseAsTwoHandedWeapon(weapon.BaseWeaponName))) return true;
            if (weapon.search_name.ToLower() == "halfling sling staff") return true;
            return false;
        }

        private static bool CanUseAsTwoHandedWeapon(string weaponName)
        {
            List<string> nonTwoHandedWeapons = new List<string> { "rapier","whip","scorpion whip" };

            return !nonTwoHandedWeapons.Contains(weaponName.ToLower());
        }
    }
}
