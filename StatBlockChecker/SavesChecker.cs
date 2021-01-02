using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using ClassManager;
using StatBlockCommon;
using Utilities;
using MagicItemAbilityWrapper;
using StatBlockCommon.Monster_SB;
using StatBlockCommon.Individual_SB;

namespace StatBlockChecker
{
    public class SavesChecker
    {
        private List<MagicItemAbilitiesWrapper> MagicItemAbilities;
        private ClassMaster CharacterClasses;
        private IndividualStatBlock_Combat _indvSB;
        private RaceBase Race_Base;
        private MonSBSearch _monSBSearch;
        private MonsterStatBlock MonSB;
        private AbilityScores.AbilityScores _abilityScores;
        private StatBlockMessageWrapper _messageXML;


        public SavesChecker(ClassMaster CharacterClasses, IndividualStatBlock_Combat _indvSB, RaceBase Race_Base, List<MagicItemAbilitiesWrapper> MagicItemAbilities,
                     MonSBSearch _monSBSearch, MonsterStatBlock MonSB, AbilityScores.AbilityScores _abilityScores, StatBlockMessageWrapper _messageXML)
        {
           this.MagicItemAbilities = MagicItemAbilities;
           this.CharacterClasses = CharacterClasses;
           this._indvSB = _indvSB;
           this.Race_Base = Race_Base;
           this._monSBSearch = _monSBSearch;
           this.MonSB = MonSB;
           this._messageXML = _messageXML;
           this._abilityScores = _abilityScores;
        }

        public void CheckFortValue()
        {
            string CheckName = "Fort";
            try
            {
                int ClassMod = CharacterClasses.GetFortSaveValue();
                int OnGoingMods = 0;
                string onGoingModFormula = string.Empty;
                if (_indvSB != null)
                {
                    OnGoingMods = _indvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow, OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,false ,ref onGoingModFormula);
                    OnGoingMods += _indvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow, OnGoingStatBlockModifier.StatBlockModifierSubTypes.SavingThrow_Fort, false ,ref onGoingModFormula);
                }
                int RaceMod = CharacterClasses.HasClass("animal companion") ? 0 : Race_Base.RaceFort();
                int AbilityMod = GetAbilityScoreMod(Race_Base.FortMod());
                int FortMod = ClassMod + RaceMod + AbilityMod + OnGoingMods;
                string formula = "+" + ClassMod.ToString() + " ClassMod " + " +" + RaceMod.ToString() + " RaceMod "
                      + " +" + AbilityMod.ToString() + " AbilityMod ";

                if (OnGoingMods != 0)
                {
                    formula += " +" + OnGoingMods.ToString() + " OnGoingMods(" + onGoingModFormula + ")";
                }
                if (_monSBSearch.HasFeat("Great Fortitude"))
                {
                    FortMod += 2;
                    formula += " +2 Great Fortitude";
                }
                if (_monSBSearch.HasTrait("Forlorn"))
                {
                    FortMod += 1;
                    formula += " +1 Forlorn";
                }
                if (MonSB.SubType.IndexOf("nightshade") >= 0)
                {
                    FortMod += 2;
                    formula += " +2 nightshade";
                }
                if (_monSBSearch.HasSQ("lore of true stamina"))
                {
                    FortMod += 2;
                    formula += " +2 lore of true stamina";
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

                    FortMod += bonus;
                    formula += " +" + bonus.ToString() + " spirit (champion)";
                }

                if (_monSBSearch.HasDomain("Protection"))
                {
                    int clericLevel = CharacterClasses.FindClassLevel("cleric");
                    if (clericLevel == 0) clericLevel = CharacterClasses.FindClassLevel("druid");
                    int tempMod = (clericLevel / 5) + 1;
                    FortMod += tempMod;
                    formula += " +" + tempMod.ToString() + " Protection domain";
                }

                if (Race_Base.Name() == "Svirfneblin")
                {
                    FortMod += 2;
                    formula += " +2 Svirfneblin";
                }

                string FamiliarString = _monSBSearch.FindFamiliarString(CharacterClasses.HasClass("witch"));

                if (FamiliarString == "rat")
                {
                    FortMod += 2;
                    formula += " +2 rat familiar";
                }

                if (CharacterClasses.HasClass("Paladin"))
                {
                    int level = CharacterClasses.FindClassLevel("paladin");
                    if (level >= 2) //Divine Grace
                    {
                        FortMod += _abilityScores.ChaMod;
                        formula += " + " + _abilityScores.ChaMod.ToString() + " Divine Grace";
                    }
                }

                if (CharacterClasses.HasClass("Antipaladin"))
                {
                    int level = CharacterClasses.FindClassLevel("Antipaladin");
                    if (level >= 2) //Unholy Resilience
                    {
                        FortMod += _abilityScores.ChaMod;
                        formula += " + " + _abilityScores.ChaMod.ToString() + " Unholy Resilience";
                    }
                }

                if (CharacterClasses.HasClass("prophet of kalistrade"))
                {
                    int level = CharacterClasses.FindClassLevel("prophet of kalistrade");
                    //Auspicious Display --assumes the bling is present
                    int mod = 1;
                    if(level >= 4) mod++;
                    if (level >= 7) mod++;
                    if (level >= 10) mod++;
                    FortMod += mod;
                    formula += " + " + mod.ToString() + " Auspicious Display";
                }

                if (_monSBSearch.HasTemplate("graveknight"))
                {
                    FortMod += 2;
                    formula += " + 2 Sacrilegious Aura";
                }

                if (_monSBSearch.HasGear("heartstone"))
                {
                    FortMod += 2;
                    formula += " + 2 heartstone";
                }

                if (_monSBSearch.HasArchetype("Sharper"))//Lucky Save
                {
                    int rogueLevel = CharacterClasses.FindClassLevel("rogue");
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

                int FortSB = Convert.ToInt32(Utility.GetNonParenValue(MonSB.Fort));

                if (FortMod == FortSB)
                {
                    _messageXML.AddPass(CheckName, formula);
                }
                else
                {
                    _messageXML.AddFail(CheckName, FortMod.ToString(), FortSB.ToString(), formula);
                }
            }
            catch (Exception ex)
            {
                _messageXML.AddFail("CheckFortValue", ex.Message);
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
                int ClassMod = CharacterClasses.GetRefSaveValue();
                int RaceMod = CharacterClasses.HasClass("animal companion") ? 0 : Race_Base.RaceRef();
                int RefMod = ClassMod + RaceMod + _abilityScores.DexMod + OnGoingMods;
                string calculation = ClassMod.ToString() + " + " + RaceMod.ToString() + " + " + _abilityScores.DexMod.ToString() + " + " + OnGoingMods.ToString();
                //string formula = "ClassMod + RaceMod + DexMod + OnGoingMods";
                string formula = "+" + ClassMod.ToString() + " ClassMod " + " +" + RaceMod.ToString() + " RaceMod "
                     + " +" + _abilityScores.DexMod.ToString() + " DexMod ";

                if (OnGoingMods != 0)
                {
                    formula += " +" + OnGoingMods.ToString() + " OnGoingMods(" + onGoingModFormula + ")";
                }
                if (_monSBSearch.HasFeat("Lightning Reflexes"))
                {
                    RefMod += 2;
                    formula += " +2 Lightning Reflexes";
                }
                if (_monSBSearch.HasTrait("Deft Dodger"))
                {
                    RefMod += 1;
                    formula += " +1 Deft Dodger";
                }
                if (MonSB.SubType.IndexOf("nightshade") >= 0)
                {
                    RefMod += 2;
                    formula += " +2 nightshade";
                }
                if (_monSBSearch.HasSQ("knowledge of avoidance"))
                {
                    RefMod += 2;
                    formula += " +2 knowledge of avoidance";
                }

                if (Race_Base.Name() == "Svirfneblin")
                {
                    RefMod += 2;
                    formula += " +2 Svirfneblin";
                }

                if (_monSBSearch.HasDomain("Protection"))
                {
                    int clericLevel = CharacterClasses.FindClassLevel("cleric");
                    if (clericLevel == 0) clericLevel = CharacterClasses.FindClassLevel("druid");
                    int tempMod = (clericLevel / 5) + 1;
                    RefMod += tempMod;
                    formula += " +" + tempMod.ToString() + " Protection domain";
                }

                if (CharacterClasses.HasClass("Paladin"))
                {
                    int level = CharacterClasses.FindClassLevel("paladin");
                    if (level >= 2) //Divine Grace
                    {
                        RefMod += _abilityScores.ChaMod;
                        formula += " +" + _abilityScores.ChaMod.ToString() + "Divine Grace";
                    }
                }

                if (CharacterClasses.HasClass("prophet of kalistrade"))
                {
                    int level = CharacterClasses.FindClassLevel("prophet of kalistrade");
                    //Auspicious Display --assumes the bling is present
                    int mod = 1;
                    if (level >= 4) mod++;
                    if (level >= 7) mod++;
                    if (level >= 10) mod++;
                    RefMod += mod;
                    formula += " + " + mod.ToString() + " Auspicious Display";
                }

                if (CharacterClasses.HasClass("Antipaladin"))
                {
                    int level = CharacterClasses.FindClassLevel("Antipaladin");
                    if (level >= 2) //Unholy Resilience
                    {
                        RefMod += _abilityScores.ChaMod;
                        formula += " +" + _abilityScores.ChaMod.ToString() + " + Unholy Resilience";
                    }
                }

                if (CharacterClasses.HasClass("Duelist"))
                {
                    int level = CharacterClasses.FindClassLevel("Duelist");
                    if (level >= 4) //grace
                    {
                        RefMod += 2;
                        formula += " +2 Grace";
                    }
                }

                if (_monSBSearch.HasFamiliar())
                {
                    string familiar = _monSBSearch.FindFamiliarString(false);
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


                if (_monSBSearch.HasTemplate("graveknight"))
                {
                    RefMod += 2;
                    formula += " +2 Sacrilegious Aura";
                }

                if (_monSBSearch.HasGear("heartstone"))
                {
                    RefMod += 2;
                    formula += " + 2 heartstone";
                }

                if (_monSBSearch.HasArchetype("Sharper"))//Lucky Save
                {
                    int rogueLevel = CharacterClasses.FindClassLevel("rogue");
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

                int RefSB = Convert.ToInt32(Utility.GetNonParenValue(MonSB.Ref));

                if (RefMod == RefSB)
                {
                    _messageXML.AddPass(CheckName, formula);
                }
                else
                {
                    _messageXML.AddFail(CheckName, RefMod.ToString(), RefSB.ToString(), formula);
                }
            }
            catch (Exception ex)
            {
                _messageXML.AddFail("CheckRefValue", ex.Message);
            }
        }

        public void CheckWillValue()
        {
            try
            {
                string CheckName = "Will";
                int OnGoingMods = 0;
                string onGoingModFormula = string.Empty;
                if (_indvSB != null)
                {
                    OnGoingMods = _indvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow, OnGoingStatBlockModifier.StatBlockModifierSubTypes.None, false,ref onGoingModFormula);
                    OnGoingMods += _indvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow, OnGoingStatBlockModifier.StatBlockModifierSubTypes.SavingThrow_Will, false ,ref onGoingModFormula);
                }
                int ClassMod = CharacterClasses.GetWillSaveValue();
                int RaceMod = CharacterClasses.HasClass("animal companion") ? 0 : Race_Base.RaceWill();
                int Mod = 0;
                string ModString = string.Empty;
                if (_monSBSearch.HasSQ("madness"))
                {
                    Mod = _abilityScores.ChaMod;
                    ModString = " ChaMod";
                }
                else
                {
                    Mod = _abilityScores.WisMod;
                    ModString = " WisMod";
                }
                int WillMod = ClassMod + RaceMod + Mod + OnGoingMods;
                int WillSB = Convert.ToInt32(Utility.GetNonParenValue(MonSB.Will));

                string formula = "+" + ClassMod.ToString() + " ClassMod " + " +" + RaceMod.ToString() + " RaceMod "
                   + " +" + Mod.ToString() + ModString;
                if (OnGoingMods != 0)
                {
                    formula += " +" + OnGoingMods.ToString() + " OnGoingMods(" + onGoingModFormula + ")";
                }

                if (_monSBSearch.HasFeat("Iron Will"))
                {
                    WillMod += 2;
                    formula += " +2 Iron Will";
                }
                if (_monSBSearch.HasTrait("Indomitable Faith"))
                {
                    WillMod += 1;
                    formula += " +1 Indomitable Faith";
                }
                if (MonSB.SubType.IndexOf("nightshade") >= 0)
                {
                    WillMod += 2;
                    formula += " +2 nightshade";
                }
                if (_monSBSearch.HasSQ("inner strength"))
                {
                    WillMod += 2;
                    formula += " +2 inner strength";
                }

                if (Race_Base.Name() == "Svirfneblin")
                {
                    WillMod += 2;
                    formula += " +2 Svirfneblin";
                }

                if (_monSBSearch.HasDomain("Protection"))
                {
                    int clericLevel = CharacterClasses.FindClassLevel("cleric");
                    if (clericLevel == 0) clericLevel = CharacterClasses.FindClassLevel("druid");
                    int tempMod = (clericLevel / 5) + 1;
                    WillMod += tempMod;
                    formula += " +" + tempMod.ToString() + " Protection domain";
                }

                if (CharacterClasses.HasClass("Paladin"))
                {
                    int level = CharacterClasses.FindClassLevel("paladin");
                    if (level >= 2) //Divine Grace
                    {
                        WillMod += _abilityScores.ChaMod;
                        formula += " +" + _abilityScores.ChaMod.ToString() + " Divine Grace";
                    }
                }

                if (CharacterClasses.HasClass("prophet of kalistrade"))
                {
                    int level = CharacterClasses.FindClassLevel("prophet of kalistrade");
                    //Auspicious Display --assumes the bling is present
                    int mod = 1;
                    if (level >= 4) mod++;
                    if (level >= 7) mod++;
                    if (level >= 10) mod++;
                    WillMod += mod;
                    formula += " + " + mod.ToString() + " Auspicious Display";
                }

                if (CharacterClasses.HasClass("Antipaladin"))
                {
                    int level = CharacterClasses.FindClassLevel("Antipaladin");
                    if (level >= 2) //Unholy Resilience
                    {
                        WillMod += _abilityScores.ChaMod;
                        formula += " +" + _abilityScores.ChaMod.ToString() + " Unholy Resilience";
                    }
                }


                if (_monSBSearch.HasTemplate("graveknight"))
                {
                    WillMod += 2;
                    formula += " +2 Sacrilegious Aura";
                }

                WillMod += GetMagicItemSaveMods(ref formula);

                if (CharacterClasses.HasClass("barbarian"))
                {                    
                    int barbarianLevel = CharacterClasses.FindClassLevel("barbarian");

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

                if (_monSBSearch.HasGear("heartstone"))
                {
                    WillMod += 2;
                    formula += " + 2 heartstone";
                }

                if (_monSBSearch.HasClassArchetype("sczarni swindler"))
                {
                    int rogueLevel = CharacterClasses.FindClassLevel("rogue");
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

                if (_monSBSearch.HasArchetype("Sharper"))//Lucky Save
                {
                    int rogueLevel = CharacterClasses.FindClassLevel("rogue");
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
                    _messageXML.AddPass(CheckName, formula);
                }
                else
                {
                    _messageXML.AddFail(CheckName, WillMod.ToString(), WillSB.ToString(), formula);
                }
            }
            catch (Exception ex)
            {
                _messageXML.AddFail("CheckWillValue", ex.Message);
            }
        }

        private int GetRefOnGoingMods(ref string formula)
        {
            int OnGoingMods = 0;
            if (_indvSB != null)
            {
                OnGoingMods = _indvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow, OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,false, ref formula);
                OnGoingMods += _indvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.SavingThrow, OnGoingStatBlockModifier.StatBlockModifierSubTypes.SavingThrow_Ref, false, ref formula);
            }
            return OnGoingMods;
        }        

        private int GetMagicItemSaveMods(ref string formula)
        {
            int mod = 0;
            foreach (MagicItemAbilitiesWrapper wrapper in MagicItemAbilities)
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
                                formula += " +" + Mod.Modifier.ToString() + " " + Mod.Name;
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
            return _abilityScores.GetAbilityModValue(AbilityScores.AbilityScores.GetAbilityNameEnum(Ability));
        }

    }
}
