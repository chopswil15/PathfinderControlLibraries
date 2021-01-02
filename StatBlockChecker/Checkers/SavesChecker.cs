using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using Utilities;
using MagicItemAbilityWrapper;
using PathfinderGlobals;

namespace StatBlockChecker
{
    public class SavesChecker : ISavesChecker
    {
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        private IEquipmentData _equipmentData;


        public SavesChecker(ISBCheckerBaseInput sbCheckerBaseInput, IEquipmentData equipmentData)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _equipmentData = equipmentData;
        }

        public void CheckFortValue()
        {
            string CheckName = "Fort";
            try
            {
                int ClassMod = _sbCheckerBaseInput.CharacterClasses.GetFortSaveValue();
                int OnGoingMods = 0;
                string onGoingModFormula = string.Empty;
                if (_sbCheckerBaseInput.IndvSB != null)
                {
                    OnGoingMods = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow, OnGoingStatBlockModifier.StatBlockModifierSubTypes.None, false, ref onGoingModFormula);
                    OnGoingMods += _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow, OnGoingStatBlockModifier.StatBlockModifierSubTypes.SavingThrow_Fort, false, ref onGoingModFormula);
                }
                int RaceMod = _sbCheckerBaseInput.CharacterClasses.HasClass("animal companion") ? 0 : _sbCheckerBaseInput.Race_Base.RaceFort();
                int AbilityMod = GetAbilityScoreMod(_sbCheckerBaseInput.Race_Base.FortMod());
                int FortMod = ClassMod + RaceMod + AbilityMod + OnGoingMods;
                string formula = "+" + ClassMod.ToString() + " ClassMod " + " +" + RaceMod.ToString() + " RaceMod "
                      + " +" + AbilityMod.ToString() + " AbilityMod ";

                if (OnGoingMods != 0)
                {
                    formula += " +" + OnGoingMods.ToString() + " OnGoingMods(" + onGoingModFormula + PathfinderConstants.PAREN_RIGHT;
                }
                if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Great Fortitude"))
                {
                    FortMod += 2;
                    formula += " +2 Great Fortitude";
                }
                if (_sbCheckerBaseInput.MonsterSBSearch.HasTrait("Forlorn"))
                {
                    FortMod += 1;
                    formula += " +1 Forlorn";
                }
                if (_sbCheckerBaseInput.MonsterSB.SubType.Contains("nightshade"))
                {
                    FortMod += 2;
                    formula += " +2 nightshade";
                }
                if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("lore of true stamina"))
                {
                    FortMod += 2;
                    formula += " +2 lore of true stamina";
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

                    FortMod += bonus;
                    formula += " +" + bonus.ToString() + " spirit (champion)";
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasDomain("Protection"))
                {
                    int clericLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("cleric");
                    if (clericLevel == 0) clericLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("druid");
                    int tempMod = (clericLevel / 5) + 1;
                    FortMod += tempMod;
                    formula += " +" + tempMod.ToString() + " Protection domain";
                }

                if (_sbCheckerBaseInput.Race_Base.Name() == "Svirfneblin")
                {
                    FortMod += 2;
                    formula += " +2 Svirfneblin";
                }

                string FamiliarString = _sbCheckerBaseInput.MonsterSBSearch.FindFamiliarString(_sbCheckerBaseInput.CharacterClasses.HasClass("witch"));

                if (FamiliarString == "rat")
                {
                    FortMod += 2;
                    formula += " +2 rat familiar";
                }

                if (_sbCheckerBaseInput.CharacterClasses.HasClass("Paladin"))
                {
                    int level = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("paladin");
                    if (level >= 2) //Divine Grace
                    {
                        FortMod += _sbCheckerBaseInput.AbilityScores.ChaMod;
                        formula += " + " + _sbCheckerBaseInput.AbilityScores.ChaMod.ToString() + " Divine Grace";
                    }
                }

                if (_sbCheckerBaseInput.CharacterClasses.HasClass("Antipaladin"))
                {
                    int level = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Antipaladin");
                    if (level >= 2) //Unholy Resilience
                    {
                        FortMod += _sbCheckerBaseInput.AbilityScores.ChaMod;
                        formula += " + " + _sbCheckerBaseInput.AbilityScores.ChaMod.ToString() + " Unholy Resilience";
                    }
                }

                if (_sbCheckerBaseInput.CharacterClasses.HasClass("prophet of kalistrade"))
                {
                    int level = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("prophet of kalistrade");
                    //Auspicious Display --assumes the bling is present
                    int mod = 1;
                    if (level >= 4) mod++;
                    if (level >= 7) mod++;
                    if (level >= 10) mod++;
                    FortMod += mod;
                    formula += " + " + mod.ToString() + " Auspicious Display";
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasTemplate("graveknight"))
                {
                    FortMod += 2;
                    formula += " + 2 Sacrilegious Aura";
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasGear("heartstone"))
                {
                    FortMod += 2;
                    formula += " + 2 heartstone";
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("Sharper"))//Lucky Save
                {
                    int rogueLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("rogue");
                    int tempMod = 0;
                    if (rogueLevel >= 3) tempMod++;
                    if (rogueLevel >= 9) tempMod++;
                    if (rogueLevel >= 15) tempMod++;
                    if (tempMod > 0)
                    {
                        FortMod += tempMod;
                        formula += " + " + tempMod.ToString() + " Lucky Save";
                    }
                }


                FortMod += GetMagicItemSaveMods(ref formula);

                int FortSB = Convert.ToInt32(Utility.GetNonParenValue(_sbCheckerBaseInput.MonsterSB.Fort));

                if (FortMod == FortSB)
                {
                    _sbCheckerBaseInput.MessageXML.AddPass(CheckName, formula);
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, FortMod.ToString(), FortSB.ToString(), formula);
                }
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("CheckFortValue", ex.Message);
            }
        }

        public void CheckRefValue()
        {
            try
            {
                string CheckName = "Ref";
                int OnGoingMods = 0;
                string onGoingModFormula = string.Empty;
                OnGoingMods = GetRefOnGoingMods(ref onGoingModFormula);
                int ClassMod = _sbCheckerBaseInput.CharacterClasses.GetRefSaveValue();
                int RaceMod = _sbCheckerBaseInput.CharacterClasses.HasClass("animal companion") ? 0 : _sbCheckerBaseInput.Race_Base.RaceRef();
                int RefMod = ClassMod + RaceMod + _sbCheckerBaseInput.AbilityScores.DexMod + OnGoingMods;
                string calculation = ClassMod.ToString() + " + " + RaceMod.ToString() + " + " + _sbCheckerBaseInput.AbilityScores.DexMod.ToString() + " + " + OnGoingMods.ToString();
                //string formula = "ClassMod + RaceMod + DexMod + OnGoingMods";
                string formula = "+" + ClassMod.ToString() + " ClassMod " + " +" + RaceMod.ToString() + " RaceMod "
                     + " +" + _sbCheckerBaseInput.AbilityScores.DexMod.ToString() + " DexMod ";

                if (OnGoingMods != 0)
                {
                    formula += " +" + OnGoingMods.ToString() + " OnGoingMods(" + onGoingModFormula + PathfinderConstants.PAREN_RIGHT;
                }
                if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Lightning Reflexes"))
                {
                    RefMod += 2;
                    formula += " +2 Lightning Reflexes";
                }
                if (_sbCheckerBaseInput.MonsterSBSearch.HasTrait("Deft Dodger"))
                {
                    RefMod += 1;
                    formula += " +1 Deft Dodger";
                }
                if (_sbCheckerBaseInput.MonsterSB.SubType.Contains("nightshade"))
                {
                    RefMod += 2;
                    formula += " +2 nightshade";
                }
                if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("knowledge of avoidance"))
                {
                    RefMod += 2;
                    formula += " +2 knowledge of avoidance";
                }

                if (_sbCheckerBaseInput.Race_Base.Name() == "Svirfneblin")
                {
                    RefMod += 2;
                    formula += " +2 Svirfneblin";
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasDomain("Protection"))
                {
                    int clericLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("cleric");
                    if (clericLevel == 0) clericLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("druid");
                    int tempMod = (clericLevel / 5) + 1;
                    RefMod += tempMod;
                    formula += " +" + tempMod.ToString() + " Protection domain";
                }

                if (_sbCheckerBaseInput.CharacterClasses.HasClass("Paladin"))
                {
                    int level = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("paladin");
                    if (level >= 2) //Divine Grace
                    {
                        RefMod += _sbCheckerBaseInput.AbilityScores.ChaMod;
                        formula += " +" + _sbCheckerBaseInput.AbilityScores.ChaMod.ToString() + "Divine Grace";
                    }
                }

                if (_sbCheckerBaseInput.CharacterClasses.HasClass("prophet of kalistrade"))
                {
                    int level = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("prophet of kalistrade");
                    //Auspicious Display --assumes the bling is present
                    int mod = 1;
                    if (level >= 4) mod++;
                    if (level >= 7) mod++;
                    if (level >= 10) mod++;
                    RefMod += mod;
                    formula += " + " + mod.ToString() + " Auspicious Display";
                }

                if (_sbCheckerBaseInput.CharacterClasses.HasClass("Antipaladin"))
                {
                    int level = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Antipaladin");
                    if (level >= 2) //Unholy Resilience
                    {
                        RefMod += _sbCheckerBaseInput.AbilityScores.ChaMod;
                        formula += " +" + _sbCheckerBaseInput.AbilityScores.ChaMod.ToString() + " + Unholy Resilience";
                    }
                }

                if (_sbCheckerBaseInput.CharacterClasses.HasClass("Duelist"))
                {
                    int level = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Duelist");
                    if (level >= 4) //grace
                    {
                        RefMod += 2;
                        formula += " +2 Grace";
                    }
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasFamiliar())
                {
                    string familiar = _sbCheckerBaseInput.MonsterSBSearch.FindFamiliarString(false);
                    if (familiar.Contains("weasel"))
                    {
                        RefMod += 2;
                        formula += " +2 weasel";
                    }
                    if (familiar.Contains("fox"))
                    {
                        RefMod += 2;
                        formula += " +2 fox";
                    }
                }


                if (_sbCheckerBaseInput.MonsterSBSearch.HasTemplate("graveknight"))
                {
                    RefMod += 2;
                    formula += " +2 Sacrilegious Aura";
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasGear("heartstone"))
                {
                    RefMod += 2;
                    formula += " + 2 heartstone";
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("Sharper"))//Lucky Save
                {
                    int rogueLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("rogue");
                    int tempMod = 0;
                    if (rogueLevel >= 3) tempMod++;
                    if (rogueLevel >= 9) tempMod++;
                    if (rogueLevel >= 15) tempMod++;
                    if (tempMod > 0)
                    {
                        RefMod += tempMod;
                        formula += " + " + tempMod.ToString() + " Lucky Save";
                    }
                }

                RefMod += GetMagicItemSaveMods(ref formula);

                int RefSB = Convert.ToInt32(Utility.GetNonParenValue(_sbCheckerBaseInput.MonsterSB.Ref));

                if (RefMod == RefSB)
                {
                    _sbCheckerBaseInput.MessageXML.AddPass(CheckName, formula);
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, RefMod.ToString(), RefSB.ToString(), formula);
                }
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("CheckRefValue", ex.Message);
            }
        }

        public void CheckWillValue()
        {
            try
            {
                string CheckName = "Will";
                int OnGoingMods = 0;
                string onGoingModFormula = string.Empty;
                if (_sbCheckerBaseInput.IndvSB != null)
                {
                    OnGoingMods = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow, OnGoingStatBlockModifier.StatBlockModifierSubTypes.None, false, ref onGoingModFormula);
                    OnGoingMods += _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow, OnGoingStatBlockModifier.StatBlockModifierSubTypes.SavingThrow_Will, false, ref onGoingModFormula);
                }
                int ClassMod = _sbCheckerBaseInput.CharacterClasses.GetWillSaveValue();
                int RaceMod = _sbCheckerBaseInput.CharacterClasses.HasClass("animal companion") ? 0 : _sbCheckerBaseInput.Race_Base.RaceWill();
                int Mod = 0;
                string ModString = string.Empty;
                if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("madness"))
                {
                    Mod = _sbCheckerBaseInput.AbilityScores.ChaMod;
                    ModString = " ChaMod";
                }
                else
                {
                    Mod = _sbCheckerBaseInput.AbilityScores.WisMod;
                    ModString = " WisMod";
                }
                int WillMod = ClassMod + RaceMod + Mod + OnGoingMods;
                int WillSB = Convert.ToInt32(Utility.GetNonParenValue(_sbCheckerBaseInput.MonsterSB.Will));

                string formula = "+" + ClassMod.ToString() + " ClassMod " + " +" + RaceMod.ToString() + " RaceMod "
                   + " +" + Mod.ToString() + ModString;
                if (OnGoingMods != 0)
                {
                    formula += " +" + OnGoingMods.ToString() + " OnGoingMods(" + onGoingModFormula + PathfinderConstants.PAREN_RIGHT;
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Iron Will"))
                {
                    WillMod += 2;
                    formula += " +2 Iron Will";
                }
                if (_sbCheckerBaseInput.MonsterSBSearch.HasTrait("Indomitable Faith"))
                {
                    WillMod += 1;
                    formula += " +1 Indomitable Faith";
                }
                if (_sbCheckerBaseInput.MonsterSB.SubType.Contains("nightshade"))
                {
                    WillMod += 2;
                    formula += " +2 nightshade";
                }
                if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("inner strength"))
                {
                    WillMod += 2;
                    formula += " +2 inner strength";
                }

                if (_sbCheckerBaseInput.Race_Base.Name() == "Svirfneblin")
                {
                    WillMod += 2;
                    formula += " +2 Svirfneblin";
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasDomain("Protection"))
                {
                    int clericLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("cleric");
                    if (clericLevel == 0) clericLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("druid");
                    int tempMod = (clericLevel / 5) + 1;
                    WillMod += tempMod;
                    formula += " +" + tempMod.ToString() + " Protection domain";
                }

                if (_sbCheckerBaseInput.CharacterClasses.HasClass("Paladin"))
                {
                    int level = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("paladin");
                    if (level >= 2) //Divine Grace
                    {
                        WillMod += _sbCheckerBaseInput.AbilityScores.ChaMod;
                        formula += " +" + _sbCheckerBaseInput.AbilityScores.ChaMod.ToString() + " Divine Grace";
                    }
                }

                if (_sbCheckerBaseInput.CharacterClasses.HasClass("prophet of kalistrade"))
                {
                    int level = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("prophet of kalistrade");
                    //Auspicious Display --assumes the bling is present
                    int mod = 1;
                    if (level >= 4) mod++;
                    if (level >= 7) mod++;
                    if (level >= 10) mod++;
                    WillMod += mod;
                    formula += " + " + mod.ToString() + " Auspicious Display";
                }

                if (_sbCheckerBaseInput.CharacterClasses.HasClass("Antipaladin"))
                {
                    int level = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Antipaladin");
                    if (level >= 2) //Unholy Resilience
                    {
                        WillMod += _sbCheckerBaseInput.AbilityScores.ChaMod;
                        formula += " +" + _sbCheckerBaseInput.AbilityScores.ChaMod.ToString() + " Unholy Resilience";
                    }
                }


                if (_sbCheckerBaseInput.MonsterSBSearch.HasTemplate("graveknight"))
                {
                    WillMod += 2;
                    formula += " +2 Sacrilegious Aura";
                }

                WillMod += GetMagicItemSaveMods(ref formula);

                if (_sbCheckerBaseInput.CharacterClasses.HasClass("barbarian"))
                {
                    int barbarianLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("barbarian");

                    if (barbarianLevel <= 10)
                    {
                        WillMod += 2;
                        formula += " +2 rage";
                    }
                    if (barbarianLevel > 10 && barbarianLevel <= 19)
                    {
                        WillMod += 3;
                        formula += " +3 greater rage";
                    }
                    if (barbarianLevel >= 20)
                    {
                        WillMod += 4;
                        formula += " +4 mighty rage";
                    }
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasGear("heartstone"))
                {
                    WillMod += 2;
                    formula += " + 2 heartstone";
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("sczarni swindler"))
                {
                    int rogueLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("rogue");
                    int tempMod = 0;
                    if (rogueLevel >= 4) tempMod++;
                    if (rogueLevel >= 8) tempMod++;
                    if (rogueLevel >= 12) tempMod++;
                    if (rogueLevel >= 16) tempMod++;
                    if (rogueLevel >= 20) tempMod++;
                    if (tempMod > 0)
                    {
                        WillMod += tempMod;
                        formula += " +" + tempMod.ToString() + " No Fool";
                    }
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("Sharper"))//Lucky Save
                {
                    int rogueLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("rogue");
                    int tempMod = 0;
                    if (rogueLevel >= 3) tempMod++;
                    if (rogueLevel >= 9) tempMod++;
                    if (rogueLevel >= 15) tempMod++;
                    if (tempMod > 0)
                    {
                        WillMod += tempMod;
                        formula += " + " + tempMod.ToString() + " Lucky Save";
                    }
                }

                if (WillMod == WillSB)
                {
                    _sbCheckerBaseInput.MessageXML.AddPass(CheckName, formula);
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail(CheckName, WillMod.ToString(), WillSB.ToString(), formula);
                }
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("CheckWillValue", ex.Message);
            }
        }

        private int GetRefOnGoingMods(ref string formula)
        {
            int OnGoingMods = 0;
            if (_sbCheckerBaseInput.IndvSB != null)
            {
                OnGoingMods = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow, OnGoingStatBlockModifier.StatBlockModifierSubTypes.None, false, ref formula);
                OnGoingMods += _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow, OnGoingStatBlockModifier.StatBlockModifierSubTypes.SavingThrow_Ref, false, ref formula);
            }
            return OnGoingMods;
        }

        private int GetMagicItemSaveMods(ref string formula)
        {
            int mod = 0;
            foreach (MagicItemAbilitiesWrapper wrapper in _equipmentData.MagicItemAbilities)
            {
                if (wrapper != null)
                {
                    foreach (OnGoing.IOnGoing SBMods in wrapper.OnGoingStatBlockModifiers)
                    {
                        if (SBMods.OnGoingType == OnGoingType.StatBlock)
                        {
                            OnGoingStatBlockModifier Mod = (OnGoingStatBlockModifier)SBMods;
                            if (Mod.ModType == OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow)
                            {
                                formula += " +" + Mod.Modifier.ToString() + PathfinderConstants.SPACE + Mod.Name;
                                //  calcluation += " + " + Mod.Modifier.ToString();
                                mod += Mod.Modifier;
                            }
                        }
                    }
                }
            }

            return mod;
        }

        private int GetAbilityScoreMod(string Ability)
        {
            return _sbCheckerBaseInput.AbilityScores.GetAbilityModValue(AbilityScores.AbilityScores.GetAbilityNameEnum(Ability));
        }

    }
}
