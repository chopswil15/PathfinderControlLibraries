using CommonInterFacesDD;
using CommonStatBlockInfo;
using EquipmentBasic;
using MagicItemAbilityWrapper;
using OnGoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockChecker.Parsers
{
    public class ACModParser : IACModParser
    {
        private AbilityScores.AbilityScores _abilityScores;
        private int _acDefendingMod;
        private int _sizeMod;
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        private IArmorClassData _armorClassData;
        private IEquipmentData _equipmentData;

        public ACModParser(ISBCheckerBaseInput sbCheckerBaseInput, IEquipmentData equipmentData,
               IArmorClassData armorClassData, ref int acDefendingMod, int sizeMod)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _equipmentData = equipmentData;
            _abilityScores = _sbCheckerBaseInput.AbilityScores;
            _acDefendingMod = acDefendingMod;
            _sizeMod = sizeMod;
            _armorClassData = armorClassData;
        }


        public void ParseACMods()
        {
            Armor armor;
            Weapon weapon;
            double Bonus;
            StatBlockInfo.ACMods acMods_Computed = new StatBlockInfo.ACMods();

            foreach (KeyValuePair<IEquipment, int> kvp in _equipmentData.Armor)
            {
                if (kvp.Key is Armor)
                {
                    armor = (Armor)kvp.Key;
                    if (armor.category == "shield")
                    {
                        if (armor.bonus.HasValue)
                        {
                            Bonus = Convert.ToInt32(armor.bonus);
                            if (armor.Broken)
                            {
                                Bonus = Math.Floor(Bonus / 2);
                            }
                            acMods_Computed.Shield += Convert.ToInt32(Bonus) + armor.EnhancementBonus;
                        }
                    }
                    if (armor.category.Contains("armor"))
                    {
                        if (armor.bonus.HasValue)
                        {
                            Bonus = Convert.ToInt32(armor.bonus);
                            if (armor.Broken)
                            {
                                Bonus = Math.Floor(Bonus / 2);
                            }
                            acMods_Computed.Armor += Convert.ToInt32(Bonus) + armor.EnhancementBonus;
                        }
                    }
                }
            }
            foreach (KeyValuePair<IEquipment, int> kvp in _equipmentData.Weapons)
            {
                weapon = (Weapon)kvp.Key;
                if ((weapon.WeaponSpecialAbilities.WeaponSpecialAbilitiesValue & WeaponSpecialAbilitiesEnum.Defending) == WeaponSpecialAbilitiesEnum.Defending)
                {
                    if (_sbCheckerBaseInput.MonsterSB.AC_Mods.Contains("defending"))
                    {
                        _acDefendingMod += weapon.EnhancementBonus;
                        acMods_Computed.Defending += weapon.EnhancementBonus;
                    }
                }
            }


            if (_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("free hand fighter")) //Elusive
            {
                int FighterLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("fighter");
                if (FighterLevel >= 3) acMods_Computed.Dodge++;
                if (FighterLevel >= 7) acMods_Computed.Dodge++;
                if (FighterLevel >= 11) acMods_Computed.Dodge++;
                if (FighterLevel >= 15) acMods_Computed.Dodge++;
                if (FighterLevel >= 19) acMods_Computed.Dodge++;
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("monk"))
            {
                acMods_Computed.Wis += _abilityScores.WisMod;
                int MonkLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("monk");
                if (_sbCheckerBaseInput.MonsterSBSearch.HasGear("monk's robe")) MonkLevel += 5;
                acMods_Computed.Monk += MonkLevel / 4;
            }
            else if (_sbCheckerBaseInput.MonsterSBSearch.HasGear("monk's robe"))
            {
                acMods_Computed.Monk += 1; //level 5 monk, no wis mod
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("psychic") && _sbCheckerBaseInput.MonsterSB.PsychicDiscipline == "self-perfection")
            {
                acMods_Computed.Wis += _abilityScores.WisMod;
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("sorcerer"))
            {
                if (_sbCheckerBaseInput.MonsterSBSearch.HasBloodline("serpentine"))
                {
                    int SorcererLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("sorcerer");
                    if (SorcererLevel >= 9) acMods_Computed.Natural++;
                    if (SorcererLevel >= 13) acMods_Computed.Natural++;
                    if (SorcererLevel >= 17) acMods_Computed.Natural++;
                }
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("animal companion"))
            {
                int animalCompanionLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("animal companion");
                int animalCompanionMod = StatBlockInfo.AnimalCompanionNaturalArmorBonus(animalCompanionLevel);

                acMods_Computed.Natural += animalCompanionMod;
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasBloodline("aquatic"))
            {
                int bloodlineLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("sorcerer");
                if (bloodlineLevel >= 9)
                {
                    acMods_Computed.Natural++;
                }
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("dragon disciple"))
            {
                int dragonDiscipleLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("dragon disciple");
                if (dragonDiscipleLevel >= 1) acMods_Computed.Natural++;
                if (dragonDiscipleLevel >= 3) acMods_Computed.Natural++;
                if (dragonDiscipleLevel >= 7) acMods_Computed.Natural++;
            }

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("bloatmage"))
            {
                int BloatMageLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("bloatmage");
                if (BloatMageLevel >= 3) acMods_Computed.Natural++;
                if (BloatMageLevel >= 7) acMods_Computed.Natural++;
            }

            acMods_Computed.Dex = _armorClassData.MaxDexMod < _abilityScores.DexMod ? _armorClassData.MaxDexMod : _abilityScores.DexMod;

            if (_sbCheckerBaseInput.MonsterSB.DefensiveAbilities.Contains("incorporeal"))
            {
                int incorporealMod = _abilityScores.ChaMod <= 0 ? 1 : _abilityScores.ChaMod;
                acMods_Computed.Deflection += incorporealMod;
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Snapping Turtle Style")) acMods_Computed.Shield++;
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Dodge"))
            {
                acMods_Computed.Dodge++;
                if (_sbCheckerBaseInput.MonsterSBSearch.HasMythicFeat("Dodge")) acMods_Computed.Dodge++;
            }
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Shield Focus")) acMods_Computed.Shield++;
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Greater Shield Focus")) acMods_Computed.Shield++;
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Two-Weapon Defense")) acMods_Computed.Shield++;
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Improved Natural Armor")) acMods_Computed.Natural++;

            //if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Combat Expertise"))
            //{
            //    acMods_Computed.Dodge++;
            //    acMods_Computed.Dodge += int.Parse(_sbCheckerBaseInput.MonsterSB.BaseAtk) / 4;
            //}


            if (_sbCheckerBaseInput.MonsterSB.Class.Contains("barbarian"))
            {
                acMods_Computed.Rage = -2;
                //ranged AC mod only
                //if (_sbCheckerBaseInput.MonsterSBSearch.HasSpecialAttackGeneral("rolling dodge"))
                //{
                //    int BarbarianLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("barbarian");
                //    if (BarbarianLevel >= 6) acMods_Computed.Dodge++;
                //    if (BarbarianLevel >= 12) acMods_Computed.Dodge++;
                //    if (BarbarianLevel >= 18) acMods_Computed.Dodge++;
                //}
            }

            if (_sbCheckerBaseInput.MonsterSB.Class.Contains("bloodrager")) acMods_Computed.BloodRage = -2;

            if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("savage barbarian"))
            {
                int BarbarianLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("barbarian");
                //Naked Courage (Ex)
                if (BarbarianLevel >= 3) acMods_Computed.Natural++;
                if (BarbarianLevel >= 9) acMods_Computed.Natural++;
                if (BarbarianLevel >= 15) acMods_Computed.Natural++;

                //Natural Toughness (Ex)
                if (BarbarianLevel >= 7) acMods_Computed.Natural++;
                if (BarbarianLevel >= 10) acMods_Computed.Natural++;
                if (BarbarianLevel >= 13) acMods_Computed.Natural++;
                if (BarbarianLevel >= 16) acMods_Computed.Natural++;
                if (BarbarianLevel >= 19) acMods_Computed.Natural++;
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("mutagen"))
            {
                acMods_Computed.Natural += 2;
                if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("greater mutagen"))
                {
                    acMods_Computed.Natural += 2;
                    if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("grand mutagen")) acMods_Computed.Natural += 2;
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("otherworldly insight")) acMods_Computed.Insight += 10;

            if (_sbCheckerBaseInput.MonsterSBSearch.HasMystery("bones"))
            {
                int oracleLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("oracle");
                acMods_Computed.Armor += 4;
                if (oracleLevel >= 7) acMods_Computed.Armor += 2;
                if (oracleLevel >= 11) acMods_Computed.Armor += 2;
                if (oracleLevel >= 15) acMods_Computed.Armor += 2;
                if (oracleLevel >= 19) acMods_Computed.Armor += 2;
            }

            if (_sbCheckerBaseInput.IndvSB != null)
            {
                acMods_Computed.Shield += _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.AC, OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Shield);
                acMods_Computed.Armor += _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.AC, OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Armor);
                acMods_Computed.Deflection += _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValueStackable(OnGoingStatBlockModifier.StatBlockModifierTypes.AC, OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Deflection, true);
                acMods_Computed.Dodge += _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.AC, OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Dodge);
                acMods_Computed.Natural += _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.AC, OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Natural);
                acMods_Computed.Enhancement += _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.AC, OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Enhancement);
                acMods_Computed.Insight += _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.AC, OnGoingStatBlockModifier.StatBlockModifierSubTypes.AC_Insight);
            }

            //if (_sbCheckerBaseInput.MonsterSB.DefensiveAbilities.Contains("uncanny dodge"))
            //{
            //  //  acMods_Computed.Dodge++;
            //}

            if (_sbCheckerBaseInput.MonsterSBSearch.Race() == "kasatha") acMods_Computed.Dodge += 2;

            try
            {
                acMods_Computed.Natural += _sbCheckerBaseInput.Race_Base.RaceNaturalArmor();
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("Race_Base.RaceNaturalArmor", ex.Message);
            }
            acMods_Computed.Size = _sizeMod;

            foreach (MagicItemAbilitiesWrapper wrapper in _equipmentData.MagicItemAbilities)
            {
                if (wrapper != null)
                {
                    foreach (OnGoing.IOnGoing SBMods in wrapper.OnGoingStatBlockModifiers)
                    {
                        if (SBMods.OnGoingType == OnGoingType.StatBlock)
                        {
                            OnGoingStatBlockModifier Mod = (OnGoingStatBlockModifier)SBMods;
                            if (Mod.ModType == OnGoingStatBlockModifier.StatBlockModifierTypes.AC)
                            {
                                acMods_Computed.AddModEffect(Mod);
                            }
                        }
                    }
                }
            }


            _armorClassData.ACMods_Computed = acMods_Computed;
        }
    }
}
