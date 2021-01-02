using System;
using System.Collections.Generic;
using System.Linq;

using StatBlockCommon;
using Skills;
using ClassManager;
using CommonStatBlockInfo;
using OnGoing;
using CommonInterFacesDD;
using MagicItemAbilityWrapper;
using D_D_Common;
using Utilities;
using EquipmentBasic;
using StatBlockCommon.Monster_SB;
using StatBlockCommon.Individual_SB;
using ClassFoundation;

namespace StatBlockChecker
{
    public class SkillsChecker
    {
        private List<SkillsInfo.SkillInfo> SkillsValues;
        private MonsterStatBlock MonSB;
        private ClassMaster CharacterClasses;
        public List<SkillCalculation> SkillCalculations = new List<SkillCalculation>();
        private Dictionary<string, SpellList> ClassSpells;
        private Dictionary<string, SpellList> SLA;
        private StatBlockInfo.SizeCategories SizeCat; 
        private CreatureTypeFoundation.CreatureTypeFoundation CreatureType;
        private int FavoredClassHP;
        private int FavoredClassLevels;
        private int StrMod;
        private int IntMod;
        private RaceBase Race_Base;
        private int TotalArmorCheckPenalty;
        private List<string> Gear;
        private IndividualStatBlock_Combat _indvSB;
        private int OnGoingMods = 0;
        private string OnGoingModsFormula = string.Empty;
        private string OnGoingModsCalculation = string.Empty;
        private List<string> CharacterClassSkills = new List<string>();
        private List<string> CreatureTypeSkills = new List<string>();
        private string RacialMods;
        private StatBlockMessageWrapper _messageXML;
        private int KnowledgeOne;
        private MonSBSearch _monSBSearch;
        private Dictionary<CommonInterFacesDD.IEquipment, int> ArmorList;
        private List<MagicItemAbilitiesWrapper> MagicItemAbilities;
        private SkillMods _skillMods;

        public SkillsChecker(MonsterStatBlock MonSB, ClassMaster CharacterClasses, Dictionary<string, SpellList> ClassSpells,
                             StatBlockInfo.SizeCategories SizeCat, MonSBSearch MonSBSearch, CreatureTypeFoundation.CreatureTypeFoundation CreatureType,
                             int FavoredClassHP, int FavoredClassLevels, int StrMod, int IntMod, int TotalArmorCheckPenalty, RaceBase Race_Base,
                              IndividualStatBlock_Combat _indvSB, ref StatBlockMessageWrapper messageXML, Dictionary<string, SpellList> SLA,
                              Dictionary<CommonInterFacesDD.IEquipment, int> Armor, List<MagicItemAbilitiesWrapper>  MagicItemAbilities)
        {
            this.MonSB = MonSB;
            this.CharacterClasses = CharacterClasses;
            this.ClassSpells = ClassSpells;
            this.SizeCat = SizeCat;
            this._monSBSearch = MonSBSearch;
            this.CreatureType = CreatureType;
            this.FavoredClassHP = FavoredClassHP;
            this.FavoredClassLevels = FavoredClassLevels;
            this.IntMod = IntMod;
            this.StrMod = StrMod;
            this.Race_Base = Race_Base;
            this.TotalArmorCheckPenalty = TotalArmorCheckPenalty;
            this._indvSB = _indvSB;
            this._messageXML = messageXML;
            this.SLA = SLA;
            this.ArmorList = Armor;
            this.MagicItemAbilities = MagicItemAbilities;

            Gear = MonSB.Gear.Split(',').ToList<string>();
            if (MonSB.OtherGear.Length > 0)
            {
               Gear.AddRange(MonSB.OtherGear.Split(',').ToList<string>());
            }

            for (int a = 0; a <= Gear.Count - 1; a++)
            {
                Gear[a] = Gear[a].Trim();
            }

            if (_indvSB != null)
            {
                OnGoingMods = _indvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.Skill,
                                    OnGoingStatBlockModifier.StatBlockModifierSubTypes.None,false, ref OnGoingModsFormula);
            }

            RacialMods = MonSB.RacialMods;
            if(Race_Base.RaceSB!= null && Race_Base.RaceSB.RacialMods.Length > 0)
            {
                RacialMods += Race_Base.RaceSB.RacialMods;
            }

            _skillMods = new SkillMods(MonSB, _monSBSearch, _messageXML, SizeCat, CharacterClasses, Race_Base, RacialMods, ArmorList, ClassSpells, SLA, _indvSB);
        }

        public List<SkillsInfo.SkillInfo> GetSkillsValues()
        {
            return SkillsValues;
        }

        #region Skills Check

        public void CheckSkillMath()
        {
            int Ranks = ComputeMaxSkillRanks();
            int MaxRanks = Ranks;
            int SkillValue = 0;
            int ComputedRank = 0;
            int ComputedValue = 0;                            
            int ExtraSkills = 0;
            SkillCalculation SkillCalc = new SkillCalculation();
            Dictionary<string, int> VertPerformSkillValues = new Dictionary<string, int>();

            
            SkillsInfo.SkillInfo Info = new SkillsInfo.SkillInfo();
            SkillsValues = ParseSkills(SkillValue);
            if (Race_Base.UseRacialHD)
            {
                CreatureTypeSkills = CreatureType.ClassSkills();
            }
            List<string> classNames = CharacterClasses.GetClassNames();

            List<string> ClassArchetypesList = MonSB.ClassArchetypes.Split(',').ToList<string>();
            foreach (string name in classNames)
            {
                if (ClassArchetypesList.Any())
                {
                    bool found = false;
                    foreach (string archetypes in ClassArchetypesList)
                    {
                        if (CharacterClasses.IsClassArchetype(name, archetypes))
                        {
                            CharacterClassSkills.AddRange(CharacterClasses.GetClassArchetypeSkills(name, archetypes));
                            found = true;
                            break;
                        }                        
                    }
                    if (!found || !CharacterClassSkills.Any())
                        CharacterClassSkills.AddRange(CharacterClasses.GetClassSkills(name));
                }
                else
                    CharacterClassSkills.AddRange(CharacterClasses.GetClassSkills(name));
            }

            if (MonSB.Class.Contains("bard") || MonSB.Class.Contains("skald"))
                try
                {
                    VertPerformanceCheck(Ranks, ref ComputedRank, ref ComputedValue, ref ExtraSkills, VertPerformSkillValues, ref Info);
                }
                catch (Exception ex)
                {
                    _messageXML.AddFail("VertPerformanceCheck", ex.Message);
                }

            SkillCalculationValues skillCalculationValues = new SkillCalculationValues();
            if (_monSBSearch.HasTrait("Tongue of Many Towns")) skillCalculationValues.TongueOfManyTowns = 2;
            if (_monSBSearch.HasFeat("Scholar")) skillCalculationValues.ScholarFeatSkills = 2;
            if (_monSBSearch.HasFeat("Cosmopolitan")) skillCalculationValues.CosmopolitanFeatSkills = 2; 
            skillCalculationValues.HasMasterCraftsman = _monSBSearch.HasFeat("Master Craftsman");
            if (_monSBSearch.Race() == "gnome")
            {
                if (_monSBSearch.HasSQ("academician"))
                {
                    skillCalculationValues.GnomeAcademician = 2;
                }
                else
                {
                    skillCalculationValues.GnomeObsessive = 2;
                }
            }

            string rankText = string.Empty;

            for (int index = 0; index <= SkillsValues.Count - 1; index++)
            {
                Info = SkillsValues[index];

                if (Info.Skill == null)
                {
                    _messageXML.AddFail("Missing Skill Name", index.ToString(), string.Empty);
                    return;
                }
                try
                {
                    CheckOneSkillMath(ref Ranks, ref ComputedRank, ref ComputedValue, ref ExtraSkills, ref SkillCalc, VertPerformSkillValues, ref Info, ref rankText, index, skillCalculationValues);
                }
                catch (Exception ex)
                {
                    _messageXML.AddFail("CheckOneSkillMath","CheckOneSkillMath--" + Info.Skill.Name + "  " + ex.Message);
                }
            }

            _messageXML.AddInfo("Rank Math: " + rankText);
            _messageXML.AddInfo("Total Ranks Used " + (MaxRanks - Ranks).ToString());

            if (Ranks != 0)
            {
                if (Ranks > 0)
                {
                    _messageXML.AddFail("Skill Points Unused ", Ranks.ToString());
                }
                else
                {
                    _messageXML.AddFail("Skill Points Exceeded ", Ranks.ToString());
                }
            }
            else
            {
                _messageXML.AddPass("Skill Points Used: ");
            }

            CheckForMisingSkills();
            CheckClassSkillsPreReqs();
        }

        private void CheckOneSkillMath(ref int Ranks, ref int ComputedRank, ref int ComputedValue, ref int ExtraSkills,  
               ref SkillCalculation SkillCalc, Dictionary<string, int> VertPerformSkillValues, ref SkillsInfo.SkillInfo Info, 
               ref string rankText, int a, SkillCalculationValues skillCalculationValues)
        {
            if (VertPerformSkillValues.ContainsKey(Info.Skill.Name))
            {
                int vertValue = VertPerformSkillValues[Info.Skill.Name];
                ComputedRank = 0; ;
                ComputedValue = vertValue;
                ComputedRank = 0;
                Info.Value = vertValue;

                SkillCalc = new SkillCalculation(Info.Skill.Name, ComputedValue, 0, string.Empty,
                       0, vertValue, 0, ComputedRank, "Versatile Performance", false);
                SkillCalculations.Add(SkillCalc);
            }
            else if (Info.Skill != null)
            {
                ComputedRank = 0;
                SkillCalc = ComputeOneSkill(ref Info, Ranks, ref ComputedRank, ref ComputedValue, ref ExtraSkills,skillCalculationValues);
                if (ComputedRank > 0) Ranks -= (ComputedRank - skillCalculationValues.ExtraRanks);
                SkillCalculations.Add(SkillCalc);
                rankText += " +" + ComputedRank.ToString();
            }


            if (ComputedRank > MonSB.HDValue()) //can't have more ranks than HD
            {
                _messageXML.AddFail("Skill Over HD Max:" + Info.FullName(), ComputedRank.ToString(), MonSB.HDValue().ToString());
            }


            if (ComputedValue == Info.Value)
            {
                _messageXML.AddPass("Skill: " + Info.FullName() + " Ranks: " + ComputedRank.ToString());
            }
            else
            {
                _messageXML.AddFail("Skill:" + Info.FullName(), ComputedValue.ToString(), Info.Value.ToString());
            }

            Info.Rank = ComputedRank;
            SkillsValues[a] = Info;
        }


        private SkillCalculation ComputeOneSkill(ref SkillsInfo.SkillInfo Info, int Ranks, ref int ComputedRank, 
                                   ref int ComputedValue, ref int ExtraSkills, 
                                    SkillCalculationValues skillCalculationValues)
        {
            int AbilityMod = 0;
            int ClassSkill = 0;
            int ArmorCheckPenalty = 0;
            int ExtraMod = 0;
            int diff = 0;
            int SkillMod = 0;
            int RaceMod = 0;
            int DomainMods = 0;
            int InquisitionMods = 0;
            SkillCalculation SkillCalc;
            string formula = string.Empty;
            bool ExtraSkillUsed = false;
            bool ExpertSkillUsed = false;
            bool CosmopolitanFeatSkillUsed = false;
            bool ScholarFeatSkillUsed = false;
            skillCalculationValues.ExtraRanks = 0; // reset everytime

            Skill skill = Info.Skill;

            ComputedRank = 0;

            ExtraMod = _skillMods.FindExtraSkillsMods(skill.Name, Info, ref formula, StrMod);

            ClassSkill = ComputeClassSkill(ref Info, skill);

            if (MonSB.Class.IndexOf("expert") >= 0 && skillCalculationValues.ExpertSkills < 10 && ClassSkill == 0)
            {
                ClassSkill = 1;
                skillCalculationValues.ExpertSkills++;
                ExtraSkillUsed = true;
            }

            ArmorCheckPenalty = GetSkillACP(skill);

            string AbilityUsed = _skillMods.GetSkillAbilityMod(ref AbilityMod, skill,MonSB.Size);

            SkillMod = _skillMods.ComputeSkillMod(Info.FullSkillName(), Info.Skill.Ability, Info.SubValue, ref formula, Info);
            string tempFormula;
            int magicItemMods = _skillMods.MagicItemMods(Info.FullSkillName(), Info.Skill.Ability, MagicItemAbilities, out tempFormula);

            RaceMod = _skillMods.ComputeRaceSkillMod(Info.FullSkillName(), ref formula, ref KnowledgeOne);
            DomainMods = _skillMods.ComputeDomainSkillMods(skill.Name, Info, ClassSkill, ref formula);
            InquisitionMods = _skillMods.ComputeInquisitionSkillMods(skill.Name, Info, ClassSkill, ref formula);
            _skillMods.ComputeBloodlineSkillMods(Info, ref ClassSkill, ref formula);
            _skillMods.ComputeOracleSkillMods(Info, ref ClassSkill);
            ComputedValue = AbilityMod + (ClassSkill * 3) + ExtraMod + SkillMod + RaceMod + DomainMods + ArmorCheckPenalty + OnGoingMods + magicItemMods + InquisitionMods;

            if (Info.Skill.SkillName == SkillData.SkillNames.KnowledgeReligion && GetRaceName() == "deep one")
            {                
                int raceHD = MonSB.HDValue();
                ComputedValue += raceHD;
                ComputedRank = raceHD;
                skillCalculationValues.ExtraRanks = raceHD;
                formula += "(ranks from deep one Devoted)";               
            }
            if (skillCalculationValues.TongueOfManyTowns > 0 && (Info.Skill.SkillName == SkillData.SkillNames.Diplomacy || Info.Skill.SkillName == SkillData.SkillNames.KnowledgeLocal || Info.Skill.SkillName == SkillData.SkillNames.Linguistics) && (ComputedValue < Info.Value))
            {
                skillCalculationValues.TongueOfManyTowns--;
                SkillMod++;
                ComputedValue++;
                formula += "+1 Tongue of Many Towns";
            }
            
            if (Info.Skill.SkillName == SkillData.SkillNames.Fly && !MonSB.Speed.Contains("fly"))
            {
                _messageXML.AddFail("ComputeOneSkill", "Fly skill with no fly speed");         
            }

            if (ComputedValue - 3 == Info.Value && ClassSkill > 0) // no ranks means no class skill bonus
            {
                ComputedValue -= 3;
                ComputedRank = 0;
                ClassSkill = 0;
                if (ExtraSkills > 0 && ExtraSkillUsed) ExtraSkills--;
                if (skillCalculationValues.ExpertSkills > 0 && ExpertSkillUsed) skillCalculationValues.ExpertSkills--;
            }
            else if (Ranks > 0)
            {
                if (skillCalculationValues.ScholarFeatSkills > 0 && skill.IsKnowledge && (ComputedValue + 2 < Info.Value))
                {
                    skillCalculationValues.ScholarFeatSkills--;
                    ComputedValue += 2;
                    formula += " +2 Scholar";
                    ScholarFeatSkillUsed = true;
                }
                if (skillCalculationValues.CosmopolitanFeatSkills > 0 && (ComputedValue + 3 < Info.Value)) //has to be more than 3 so you can add 1 rank too
                {
                    skillCalculationValues.CosmopolitanFeatSkills--;
                    ClassSkill = 1;
                    ComputedValue += 3;
                    formula += " +3 Cosmopolitan";
                    CosmopolitanFeatSkillUsed = true;
                }               
                if (ComputedValue < Info.Value)
                {
                    diff = Info.Value - ComputedValue;                   
                    ComputedValue += diff;
                    ComputedRank = diff;
                }
            }

            if (_monSBSearch.HasFeat("Master Craftsman") && skillCalculationValues.HasMasterCraftsman && ComputedRank - 2 >= 5)
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.Craft)
                {
                    ComputedRank -= 2;
                    formula += " +2 Master Craftsman";
                    skillCalculationValues.HasMasterCraftsman = false;
                    ExtraMod += 2;
                }
            }

            if (_skillMods.HasSkillFocus(Info.FullSkillName()) && ComputedRank >= 13)
            {
                ComputedRank -= 3;
                formula += "+3 SkillFocus-" + Info.FullSkillName() + " 10 ranks";
                ExtraMod += 3;
            }
            else if (_skillMods.HasSkillFocus(Info.FullSkillName()) && ComputedRank >= 10)
            {
                ComputedValue += 3;
                int hold = ComputedRank;
                ComputedRank -= (hold - 10);
                ExtraMod += 3;
            }            

            bool Over10 = false;
            if (ComputedRank >= 12)
            {
                int holdRanks = Ranks;
                int holdComputedRank = ComputedRank;
                int holdExtraMod = ExtraMod;
                string holdformula = formula;
                Over10 = _skillMods.Over10RanksBonus(Info, ref holdRanks, ref holdComputedRank, ref holdExtraMod, ref holdformula, skill);
               
                if (Over10) //no magicItemMods, so apply Over10
                {                    
                    ComputedRank = holdComputedRank;
                    ExtraMod = holdExtraMod;
                    formula = holdformula + " " + tempFormula;
                }
            }


            if (ComputedRank == 0 && ClassSkill > 0)
            {
                ClassSkill = 0;
                ComputedValue -= 3;
                if (CosmopolitanFeatSkillUsed) skillCalculationValues.CosmopolitanFeatSkills++;
                if (ScholarFeatSkillUsed) skillCalculationValues.ScholarFeatSkills++;  
                if (ComputedValue < Info.Value && Ranks > 0 && ExtraSkillUsed)
                {
                    diff = Info.Value - ComputedValue;
                    ComputedValue += diff;
                    ComputedRank += diff;
                    Ranks -= diff;
                }
                if (ComputedValue < Info.Value && Ranks > 0 && ExpertSkillUsed)
                {
                    diff = Info.Value - ComputedValue;
                    ComputedValue += diff;
                    ComputedRank += diff;
                    Ranks -= diff;
                }
            }
            if (skillCalculationValues.GnomeObsessive > 0 && (Info.Skill.SkillName == SkillData.SkillNames.Craft || Info.Skill.SkillName == SkillData.SkillNames.Profession) && (ComputedValue < Info.Value))
            {
                skillCalculationValues.GnomeObsessive = 0;
                ComputedValue += 2;
                formula += " +2 gnome Obsessive";
            }
            if (skillCalculationValues.GnomeObsessive > 0 && (Info.Skill.SkillName == SkillData.SkillNames.Craft || Info.Skill.SkillName == SkillData.SkillNames.Profession) && ComputedRank - 2 == MonSB.HDValue())
            {
                skillCalculationValues.GnomeObsessive = 0;
                ComputedRank -= 2;
                Ranks += 2;
                formula += " +2 gnome Obsessive";
            }
            if (skillCalculationValues.GnomeAcademician > 0 && Info.Skill.IsKnowledge && ComputedValue < Info.Value)
            {
                skillCalculationValues.GnomeAcademician = 0;
                ComputedValue += 2;
                formula += " +2 gnome Academician";
            }
            if (skillCalculationValues.GnomeAcademician > 0 && Info.Skill.IsKnowledge && ComputedRank - 2 == MonSB.HDValue())
            {
                skillCalculationValues.GnomeAcademician = 0;
                ComputedRank -= 2;
                Ranks += 2;
                formula += " +2 gnome Academician";
            }


            formula += OnGoingModsCalculation + " " + OnGoingModsFormula;
            if (magicItemMods != 0) formula += " " + tempFormula;

            if (ExtraSkills != -1 && ComputedRank > MonSB.HDValue() && ClassSkill == 0)
            {
                HandleExtraClassSkills(ref ComputedRank, ref ExtraSkills, ref ClassSkill, ref ExtraSkillUsed);
            }


            SkillCalc = new SkillCalculation(Info.FullSkillName(), ComputedValue, AbilityMod, AbilityUsed,
                            ClassSkill * 3, ExtraMod + SkillMod + RaceMod + DomainMods + OnGoingMods + magicItemMods + InquisitionMods, ArmorCheckPenalty, ComputedRank, formula, ExtraSkillUsed || CosmopolitanFeatSkillUsed);

            return SkillCalc;
        }

        private void HandleExtraClassSkills(ref int ComputedRank, ref int ExtraSkills, ref int ClassSkill, ref bool ExtraSkillUsed)
        {
            if (ExtraSkills > 0)
            {
                ExtraSkills--;
                if (ExtraSkills == 0) ExtraSkills = -1;
                ExtraSkillUsed = true;
                ClassSkill = 1;
                ComputedRank -= 3;
                return;
            }

            if (ExtraSkills == 0)
            {
                foreach (string skillString in CreatureTypeSkills)
                {
                    if (skillString.Contains("Extra"))
                    {
                        string temp = skillString.Replace("Extra", string.Empty).Trim();
                        ExtraSkills = Convert.ToInt32(temp);
                        ExtraSkillUsed = true;
                        ExtraSkills--;
                        ClassSkill = 1;
                        ComputedRank -= 3;
                        return;
                    }
                }
            }
        }

        private int ComputeClassSkill(ref SkillsInfo.SkillInfo Info, Skill skill)
        {
            if (IsClassSkill(skill, Info.SubValue))
            {
                return 1;
            }
            else if (IsCreatureTypeClassSkill(skill))
            {
                return 1;
            }

            return 0;
        }

        private void CheckClassSkillsPreReqs()
        {
            foreach (ClassWrapper wrapper in CharacterClasses.Classes)
            {
                List<PreReqSkill> ClassPreReqs = CharacterClasses.GetPrestigePreReqSkills(wrapper.Name);
                SkillsInfo.SkillInfo skill;

                foreach (PreReqSkill onePreReqSkill in ClassPreReqs)
                {
                    if (onePreReqSkill.SubType == null)
                    {
                        skill = SkillsValues.Find(y => y.Skill.SkillName == onePreReqSkill.SkillName);
                    }
                    else
                    {
                        skill = SkillsValues.Find(y => y.Skill.SkillName == onePreReqSkill.SkillName && y.SubValue == onePreReqSkill.SubType);
                    }

                    if (skill.Rank < onePreReqSkill.Value)
                    {
                        string temp = string.Empty;
                        if (onePreReqSkill.SubType != null && onePreReqSkill.SubType.Length > 0) temp = " (" + onePreReqSkill.SubType + ")";
                        _messageXML.AddFail("Skill Pre Req", wrapper.Name + " needs min of " + onePreReqSkill.Value
                            + " ranks of " + onePreReqSkill.SkillName.ToString() + temp + " has " + skill.Rank.ToString() + " ranks");
                    }
                }
            }

            if (CharacterClasses.ClassCount() > 0)
            {

                if (CharacterClasses.HasClass("loremaster"))
                {
                    int count = 0;
                    foreach (SkillsInfo.SkillInfo oneSkill in SkillsValues)
                    {
                        if (oneSkill.Skill.Name.Contains("Knowledge") && oneSkill.Rank >= 7)
                        {
                            count++;
                        }
                    }
                    if (count < 2) _messageXML.AddFail("Skill Pre Req", "Loremaster needs min of 7 ranks of 2 Knowledge skills but doesn't");
                }

                if (CharacterClasses.HasClass("low templar"))
                {
                    SkillsInfo.SkillInfo skill = SkillsValues.Find(y => y.Skill.SkillName == SkillData.SkillNames.KnowledgeNobility);
                    if (skill.Rank < 2)
                    {
                        skill = SkillsValues.Find(y => y.Skill.SkillName == SkillData.SkillNames.KnowledgePlanes);
                        if (skill.Rank < 2)
                        {
                            _messageXML.AddFail("Skill Pre Req", "Low Templar needs min 2 ranks of  Knowledge (nobility or planes) has " + skill.Rank.ToString() + " ranks");
                        }
                    }
                }
            }
        }

        private void VertPerformanceCheck(int Ranks, ref int ComputedRank, ref int ComputedValue, ref int ExtraSkills, 
                 Dictionary<string, int> VertPerformSkillValues, ref SkillsInfo.SkillInfo Info)
        {
            List<string> VertPerformValues = ComputeBardicVersatilePerformance();

            if (VertPerformValues.Any())
            {
                int VertPerformLoopCount = VertPerformValues.Count / 3;
                for (int a = 0; a < VertPerformValues.Count; a++)
                {
                    string tempPerformName = "Perform [" + VertPerformValues[a] + "]";
                    //  VertPerformValues.Remove(VertPerformValues[a]);

                    foreach (SkillsInfo.SkillInfo tempInfo in SkillsValues)
                    {
                        if (tempInfo.FullName() == tempPerformName)
                        {
                            Info = tempInfo;
                            SkillCalculationValues skillCalculationValues = new SkillCalculationValues();

                            ComputeOneSkill(ref Info, Ranks, ref ComputedRank, ref ComputedValue, ref ExtraSkills, skillCalculationValues);
                            if (ComputedValue > 0)
                            {
                                if (VertPerformValues.Count < 3)
                                {
                                    _messageXML.AddFail("VertPerformValues", "Count: " + VertPerformValues.Count.ToString() + " Performance Name:" + tempPerformName);
                                    return;
                                }

                                ComputeVersatilePerformance(ComputedValue, VertPerformSkillValues, VertPerformValues, a);
                            }
                        }
                    }
                }
            }
        }

        private void ComputeVersatilePerformance(int ComputedValue, Dictionary<string, int> VertPerformSkillValues, List<string> VertPerformValues, int index)
        {
            string firstVert = VertPerformValues[index + 1];
            string secondVert = VertPerformValues[index + 2];
            if (VertPerformSkillValues.ContainsKey(firstVert))
            {
                int temp = VertPerformSkillValues[firstVert];
                if (temp < ComputedValue)
                {
                    VertPerformSkillValues[firstVert] = ComputedValue;
                }
            }
            else
            {
                VertPerformSkillValues.Add(firstVert, ComputedValue);
            }
            if (VertPerformSkillValues.ContainsKey(secondVert))
            {
                int temp = VertPerformSkillValues[secondVert];
                if (temp < ComputedValue)
                {
                    VertPerformSkillValues[secondVert] = ComputedValue;
                }
            }
            else
            {
                VertPerformSkillValues.Add(secondVert, ComputedValue);
            }
        }

        private void CheckForMisingSkills()
        {
            if (MonSB.Speed.Contains("swim") && MonSB.Skills.IndexOf("Swim") == -1)
            {                
                int ComputedValue = 0;                
                SkillsInfo.SkillInfo Info = new SkillsInfo.SkillInfo("Swim +0");
                SkillCalculation SkillCalc = ComputeOneSkillWithFakes(ref Info, ref ComputedValue);
                _messageXML.AddFail("Missing Skill", "Swim, has swim speed. Could be " + ComputedValue.ToString() + " " + SkillCalc.ToString());
            }

            if (MonSB.Speed.Contains("fly") && !MonSB.Skills.Contains("Fly"))
            {                
                int ComputedValue = 0;                
                SkillsInfo.SkillInfo Info = new SkillsInfo.SkillInfo("Fly +0");
                SkillCalculation SkillCalc = ComputeOneSkillWithFakes(ref Info, ref ComputedValue);
                _messageXML.AddFail("Missing Skill", "Fly, has fly speed. Could be " + ComputedValue.ToString() + " " + SkillCalc.ToString());
            }
            if(!MonSB.Skills.Contains("Stealth"))   
            {
                int ComputedValue = 0;
                SkillsInfo.SkillInfo Info = new SkillsInfo.SkillInfo("Stealth +0");

                SkillCalculation SkillCalc = ComputeOneSkillWithFakes(ref Info,ref ComputedValue);
                if (ComputedValue != 0)
                {
                    _messageXML.AddFail("Missing Skill", "Stealth could be " + ComputedValue.ToString() + " " + SkillCalc.ToString());
                }
            }
        }

        private SkillCalculation ComputeOneSkillWithFakes(ref SkillsInfo.SkillInfo Info, ref int ComputedValue)
        {
            int Ranks = 60;
            int ComputedRank = 0;
            int ExtraSkills = 0;

            SkillCalculationValues skillCalculationValues = new SkillCalculationValues();

            return ComputeOneSkill(ref Info, Ranks, ref ComputedRank, ref ComputedValue,ref ExtraSkills, skillCalculationValues);
        }

        private int GetSkillACP(Skill skill)
        {
            int ArmorCheckPenalty = 0;
            if(TotalArmorCheckPenalty == 0) return 0;

            if (skill.ArmorCheckPenalty)
            {
                ArmorCheckPenalty = TotalArmorCheckPenalty;
                if (_monSBSearch.HasClassArchetype("acrobat") && _monSBSearch.HasLightArmor())
                {
                    if (skill.SkillName == SkillData.SkillNames.Acrobatics || skill.SkillName == SkillData.SkillNames.Climb || skill.SkillName == SkillData.SkillNames.Fly || skill.SkillName == SkillData.SkillNames.SleightofHand || skill.SkillName == SkillData.SkillNames.Stealth)
                    {
                        ArmorCheckPenalty = 0;
                    }
                }
                if (_monSBSearch.HasClassArchetype("roughrider"))
                 {
                     int roughriderLevel = CharacterClasses.FindClassLevel("fighter");
                     if (roughriderLevel >= 3 && skill.SkillName == SkillData.SkillNames.Ride)
                     {
                         ArmorCheckPenalty = 0;
                     }
                 }
            }
            return ArmorCheckPenalty;
        }           

        private List<string> ComputeBardicVersatilePerformance()
        {
            List<string> Values = new List<string>();
            string Perform = _monSBSearch.GetSQ("versatile performance");
            if (Perform.Length == 0) return Values;         
            
            Perform = Perform.Replace("versatile performance", string.Empty);
            Perform = Utility.RemoveParentheses(Perform);

            List<string> performs = Perform.Split('|').ToList<string>();
            

            foreach (string perform in performs)
            {
                string holdPerform = perform.Trim().ToLower();
                Values.Add(holdPerform);
                switch (holdPerform)
                {
                    case "act":
                        Values.Add("Bluff");
                        Values.Add("Disguise");
                        break;
                    case "comedy":
                        Values.Add("Bluff");
                        Values.Add("Intimidate");
                        break;
                    case "dance":
                        Values.Add("Acrobatics");
                        Values.Add("Fly");
                        break;
                    case "keyboard instruments":
                        Values.Add("Diplomacy");
                        Values.Add("Intimidate");
                        break;
                    case "oratory":
                        Values.Add("Diplomacy");
                        Values.Add("Sense Motive");
                        break;
                    case "percussion":
                        Values.Add("Handle Animal");
                        Values.Add("Intimidate");
                        break;
                    case "sing":
                        Values.Add("Bluff");
                        Values.Add("Sense Motive");
                        break;
                    case "string":
                    case "string instruments":
                        Values.Add("Bluff");
                        Values.Add("Diplomacy");
                        break;
                    case "wind":
                        Values.Add("Diplomacy");
                        Values.Add("Handle Animal");
                        break;
                }
            }
            return Values;
        }

        private List<SkillsInfo.SkillInfo> ParseSkills(int SkillValue)
        {
            SkillsInfo.SkillInfo tempSkillsValue = new SkillsInfo.SkillInfo();

            string hold = MonSB.Skills;
            hold = hold.Replace("*", string.Empty);
            Utility.ParenCommaFix(ref hold);
            List<string> Skills = hold.Split(',').ToList<string>();
            Skills.Remove(string.Empty);

            for (int a = Skills.Count - 1; a >= 0 ; a--)
            {
                if (Skills[a].Contains("|") && Skills[a].Contains("Knowledge"))
                {
                    string hold2 = Skills[a];
                    Skills.Remove(Skills[a]);
                    int Pos = hold2.IndexOf("+");
                    string plus = hold2.Substring(Pos).Trim();
                    string temp = hold2.Replace(plus, string.Empty);
                    temp = temp.Replace("Knowledge", string.Empty);
                    temp = Utility.RemoveParentheses(temp);
                    List<string> Knowledge = temp.Split('|').ToList<string>();
                    Knowledge.Remove(string.Empty);
                    foreach (string knowledge in Knowledge)
                    {
                        Skills.Add("Knowledge (" + knowledge.Trim() + ") " + plus);
                    }
                }

                if(Skills[a].Contains("Knowledge (all)"))
                {
                    string hold2 = Skills[a];
                    Skills.Remove(Skills[a]);
                    int Pos = hold2.IndexOf("+");
                    string plus = hold2.Substring(Pos).Trim();
                    string temp = hold2.Replace(plus, string.Empty);
                    temp = temp.Replace("Knowledge", string.Empty);
                    temp = Utility.RemoveParentheses(temp);
                    List<string> Knowledge = new List<string> {"Knowledge (arcana)","Knowledge (dungeoneering)", "Knowledge (engineering)", "Knowledge (geography)",
                          "Knowledge (history)","Knowledge (local)","Knowledge (nature)","Knowledge (nobility)","Knowledge (planes)","Knowledge (religion)"};
                    foreach (string knowledge in Knowledge)
                    {
                        Skills.Add(knowledge.Trim() + plus);
                    }
                }

                if (Skills[a].Contains("|") && Skills[a].Contains("Perform"))
                {
                    string hold2 = Skills[a];
                    Skills.Remove(Skills[a]);
                    int Pos = hold2.IndexOf("+");
                    string plus = hold2.Substring(Pos).Trim();
                    string temp = hold2.Replace(plus, string.Empty);
                    temp = temp.Replace("Perform", string.Empty);
                    temp = Utility.RemoveParentheses(temp);
                    List<string> Perform = temp.Split('|').ToList<string>();
                    Perform.Remove(string.Empty);
                    foreach (string perform in Perform)
                    {
                        Skills.Add("Perform (" + perform.Trim() + ") " + plus);
                    }
                }
            }

            List<SkillsInfo.SkillInfo> SkillsValues = new List<SkillsInfo.SkillInfo>();

            foreach (string skill in Skills)
            {
                try
                {
                    tempSkillsValue = new SkillsInfo.SkillInfo(skill.Trim());
                    if (tempSkillsValue.Skill != null)
                    {
                        SkillsValues.Add(tempSkillsValue);
                    }
                    else
                    {
                        _messageXML.AddFail("Missing Skill", skill.Trim());
                    }
                }
                catch (Exception ex)
                {
                    _messageXML.AddFail("ParseSkills", "Issue with " + skill + " -- " + ex.Message);
                }
            }

            return SkillsValues;
        }       

        private int ComputeMaxSkillRanks()
        {
            if (MonSB.GetAbilityScoreValue("Int") == 0) return 0;

            if (CharacterClasses.HasClass("animal companion"))
            {
                int animalCompanionMod = 0;
                int animalCompanionLevel = CharacterClasses.FindClassLevel("animal companion");

                switch (animalCompanionLevel)
                {
                    case 1:
                        animalCompanionMod = 2;
                        break;
                    case 2:
                    case 3:
                        animalCompanionMod = 3;
                        break;
                    case 4:
                        animalCompanionMod = 4;
                        break;
                    case 5:
                        animalCompanionMod = 5;
                        break;
                    case 6:
                    case 7:
                        animalCompanionMod = 6;
                        break;
                    case 8:
                        animalCompanionMod = 7;
                        break;
                    case 9:
                        animalCompanionMod = 8;
                        break;
                    case 10:
                    case 11:
                        animalCompanionMod = 9;
                        break;
                    case 12:
                        animalCompanionMod = 10;
                        break;
                    case 13:
                        animalCompanionMod = 11;
                        break;
                    case 14:
                    case 15:
                        animalCompanionMod = 12;
                        break;
                    case 16:
                        animalCompanionMod = 13;
                        break;
                    case 17:
                        animalCompanionMod = 14;
                        break;
                    case 18:
                    case 19:
                        animalCompanionMod = 15;
                        break;
                    case 20:
                        animalCompanionMod = 16;
                        break;
                }

                return animalCompanionMod;
            }

            int RaceMod = 0;
            int Level = 1;
            int Base = 1;
            int ClassSkills = 0;
            int RaceSkills = 0;
            int IntModHold = IntMod;

            if (MonSB.Class.Length > 0)
            {
                foreach (string name in CharacterClasses.GetClassNames())
                {
                    if (name == "Fighter" && _monSBSearch.HasClassArchetype("tactician"))
                    {
                        Base = 4;
                    }
                    else if (name == "Warpriest" && _monSBSearch.HasClassArchetype("cult leader"))
                    {
                        Base = 4;
                    }
                    else if (name == "Rogue" && _monSBSearch.HasClassArchetype("skulking slayer"))
                    {
                        Base = 6;
                    }
                    else
                    {
                       Base = CharacterClasses.GetSkillRanksPerLevel(name);
                    }
                    Level = CharacterClasses.FindClassLevel(name);

                    if (Base + IntModHold <= 0) //min 1 skill point per level
                    {
                        IntModHold = 0;
                        Base = 1;
                    }
                    ClassSkills += Level * (Base + IntModHold);
                }
            }
          
            int TotalLevels = CharacterClasses.FindTotalClassLevels();
            IntModHold = IntMod;

            if (UseHDForSkills())
            {
                string raceName = MonSB.IsBestiary ? MonSB.name.ToLower() : MonSB.Race.ToLower();

                if (raceName == "gearsman" || raceName == "gearsman robot")
                {
                    Base = 4;
                    Level = MonSB.HDValue() - TotalLevels;
                    int Bonus = MonSB.HDValue();
                    RaceSkills = (Level * (Base + IntModHold)) + Bonus;
                }
                else
                {
                    Base = CreatureType.SkillRanksPerLevel;

                    Level = MonSB.HDValue() - TotalLevels;

                    if (Base + IntModHold <= 0) //min 1 skill point per HD
                    {
                        IntModHold = 0;
                        Base = 1;
                    }
                    RaceSkills = Level * (Base + IntModHold);
                }
            }

            if (Race_Base != null)
            {
                if (Race_Base.Name() == "Human")
                {
                    RaceMod = 1;
                }
            }


            int Max = ClassSkills + RaceSkills + (RaceMod * TotalLevels) + (FavoredClassLevels - FavoredClassHP > 0 ? FavoredClassLevels - FavoredClassHP : 0); //ranks
            string info = "Skills Ranks: " + Max + " = " + ClassSkills.ToString() + " class skills ";
            if (RaceSkills > 0)
            {
                info += "+" + RaceSkills.ToString() + " race skills ";
            }
            if (RaceMod > 0)
            {
                info += "+" + TotalLevels.ToString() + " race mod ";
            }
            if (FavoredClassLevels - FavoredClassHP > 0)
            {
                info += "+" + (FavoredClassLevels - FavoredClassHP).ToString() + " Favored class";
            }

            _messageXML.AddInfo(info);

            return Max;
        }

        private bool UseHDForSkills()
        {
            if (Race_Base.UseRacialHD) return true;

            if (MonSB.Environment.Length > 0 && ((CreatureType.Name == "Humanoid" && CharacterClasses.FindTotalClassLevels() == 0) || CreatureType.Name != "Humanoid"))
            {
                return true;
            }

            if (CreatureType.Name != "Humanoid") return true;


            return false;
        }
        
        private bool IsCreatureTypeClassSkill(Skill skill)
        {
            //if (MonSB.Environment.Length == 0) return false;
            string SkillName = skill.Name;

            if (CreatureTypeSkills.Contains(SkillName))
            {
                return true;
            }

            string raceName = MonSB.IsBestiary ? MonSB.name.ToLower() : MonSB.Race.ToLower();

            switch (raceName)
            {
                case "bugbear":
                    if (skill.SkillName == SkillData.SkillNames.Stealth || skill.SkillName == SkillData.SkillNames.Perception) return true;
                    break;
                case "gearsman":
                case "gearsman robot":
                    if (skill.SkillName == SkillData.SkillNames.Profession || skill.SkillName == SkillData.SkillNames.Craft) return true; 
                    break;
                case "kasatha":
                     if (skill.SkillName == SkillData.SkillNames.Stealth || skill.SkillName == SkillData.SkillNames.Perception) return true;                  
                    break;
                case "kobold":
                    if (skill.SkillName == SkillData.SkillNames.Stealth || skill.SkillName == SkillData.SkillNames.Craft) return true;
                    break;
                case "spriggan":
                    switch(skill.SkillName)
                    {
                        case SkillData.SkillNames.Climb:
                        case SkillData.SkillNames.DisableDevice:
                        case SkillData.SkillNames.Perception:
                        case SkillData.SkillNames.SleightofHand:
                        case SkillData.SkillNames.Stealth:
                            return true;
                    }
                    break;
                case "wikkawak":
                    if (skill.SkillName == SkillData.SkillNames.Stealth || skill.SkillName == SkillData.SkillNames.Intimidate) return true;
                    break;
            }

            if (raceName.Contains("havoc dragon") && skill.SkillName == SkillData.SkillNames.Perform) return true;

            if (skill.IsKnowledge)
            {
                if (CreatureTypeSkills.Contains("Knowledge (1)") && KnowledgeOne == 0)
                {
                    KnowledgeOne = -1;
                    return true;
                }
                if (CreatureTypeSkills.Contains("Knowledge (all)"))
                {
                    return true;
                }
            }

            if (_monSBSearch.HasTemplate("lich") || _monSBSearch.HasTemplate("mummy lord"))
            {
                switch (skill.SkillName)
                {
                    case SkillData.SkillNames.Climb:
                    case SkillData.SkillNames.Disguise:
                    case SkillData.SkillNames.Intimidate:
                    case SkillData.SkillNames.Fly:
                    case SkillData.SkillNames.KnowledgeArcana:
                    case SkillData.SkillNames.KnowledgeReligion:
                    case SkillData.SkillNames.Perception:
                    case SkillData.SkillNames.SenseMotive:
                    case SkillData.SkillNames.Spellcraft:
                    case SkillData.SkillNames.Stealth:
                        return true;
                }                
            }            

            if (_monSBSearch.HasTemplate("skeletal champion"))
            {
                switch (skill.SkillName)
                {
                    case SkillData.SkillNames.Climb:
                    case SkillData.SkillNames.Disguise:
                    case SkillData.SkillNames.Fly:
                    case SkillData.SkillNames.Intimidate:
                    case SkillData.SkillNames.KnowledgeArcana:
                    case SkillData.SkillNames.KnowledgeReligion:
                    case SkillData.SkillNames.Perception:
                    case SkillData.SkillNames.SenseMotive:
                    case SkillData.SkillNames.Spellcraft:
                    case SkillData.SkillNames.Stealth:
                        return true;
                }

            }

            if (MonSB.Speed.Contains("fly") && skill.SkillName == SkillData.SkillNames.Fly && !_monSBSearch.HasGear("winged boots") && !_monSBSearch.HasGear("scroll of fly"))
            {
                return true;
            }

            if (skill.SkillName == SkillData.SkillNames.Fly && MonSB.Bloodline.Contains("elemental (air)"))
            {
                return true;
            }

            if (MonSB.SubType.Contains("giant"))
            {
                if (skill.SkillName == SkillData.SkillNames.Intimidate || skill.SkillName == SkillData.SkillNames.Perception)
                {
                    return true;
                }
            }

            if (MonSB.SubType.Contains("water") || MonSB.SubType.Contains("aquatic"))
            {
                if (skill.SkillName == SkillData.SkillNames.Swim)
                {
                    return true;
                }
            }            

            return false;
        }

        private bool IsClassSkill(Skill skill, string SubValue)
        {
            if (CharacterClassSkills.Contains(skill.Name))  return true;

            if(SubValue.Length > 0 && skill.Name == "Perform")
            {
                if (CharacterClassSkills.Contains(skill.Name + " (" + SubValue + ")")) return true;
            }

            if (_monSBSearch.HasTemplate("lich"))
            {
                List<SkillData.SkillNames> SkillNamesList = new List<SkillData.SkillNames> { SkillData.SkillNames.Climb, SkillData.SkillNames.Disguise, SkillData.SkillNames.Fly,
                      SkillData.SkillNames.Intimidate,SkillData.SkillNames.KnowledgeArcana,SkillData.SkillNames.KnowledgeReligion, SkillData.SkillNames.Perception,
                      SkillData.SkillNames.SenseMotive,SkillData.SkillNames.Spellcraft,SkillData.SkillNames.Stealth};

                if (SkillNamesList.Contains(skill.SkillName)) return true;
            }

            string RaceName = GetRaceName();

            switch (RaceName)
            {
                case "samsaran":
                    string sop = _monSBSearch.GetSQ("shards of the past");
                    if (sop.Contains(skill.Name)) return true;
                    break;
                case "wikkawak":
                    if (skill.SkillName == SkillData.SkillNames.Intimidate || skill.SkillName == SkillData.SkillNames.Perception) return true;
                    break;
                case "leng ghoul":
                    if (skill.IsKnowledge) return true;
                    break;
                case "hobkins":
                    if (skill.SkillName == SkillData.SkillNames.Intimidate) return true;
                    break;
                case "deep one":
                    if (skill.SkillName == SkillData.SkillNames.KnowledgeReligion) return true;
                    break;
            }

            if (_monSBSearch.HasSubType("robot"))
            {
                switch (skill.SkillName)
                {
                    case SkillData.SkillNames.Climb:
                    case SkillData.SkillNames.DisableDevice:
                    case SkillData.SkillNames.Fly:
                    case SkillData.SkillNames.Linguistics:
                    case SkillData.SkillNames.Perception:
                    case SkillData.SkillNames.SenseMotive:
                        return true;
                }

                if (skill.IsKnowledge) return true;
            }

            if (_monSBSearch.HasSubType("goblinoid") && skill.SkillName == SkillData.SkillNames.Stealth) return true;          

            if (skill.Name.Contains("Knowledge"))
            {
                if (CharacterClassSkills.Contains("Knowledge (1)"))  return true;
                if (CharacterClassSkills.Contains("Knowledge (all)"))  return true;
            }

            if (MonSB.TemplatesApplied.Contains("haunted one"))
            {
                if (skill.Name.Contains("Knowledge"))  return true;
            }

            if (_monSBSearch.HasSQ("order of the lion"))
            {
                if (skill.SkillName == SkillData.SkillNames.KnowledgeNobility) return true;
                if (skill.SkillName == SkillData.SkillNames.KnowledgeLocal) return true;
            }
            if (_monSBSearch.HasSQ("order of the dargon"))
            {
                if (skill.SkillName == SkillData.SkillNames.Perception) return true;
                if (skill.SkillName == SkillData.SkillNames.Survival) return true;
            }

            if (_monSBSearch.HasSQ("order of the cockatrice"))
            {
                if (skill.SkillName == SkillData.SkillNames.Appraise) return true;
                if (skill.SkillName == SkillData.SkillNames.Perform) return true;
            }
            
            if (_monSBSearch.HasSQ("order of the warrior"))
            {
                if (skill.SkillName == SkillData.SkillNames.KnowledgeHistory) return true;
                if (skill.SkillName == SkillData.SkillNames.KnowledgeNobility) return true;
            }

            if (_monSBSearch.HasSQ("ronin")) // kinda of a samurai order
            {
                if (skill.SkillName == SkillData.SkillNames.KnowledgeLocal) return true;
                if (skill.SkillName == SkillData.SkillNames.Survival) return true;
            }

            if (_monSBSearch.HasFeat("Derro Magister") && skill.SkillName == SkillData.SkillNames.Heal) return true;

            if (CharacterClasses.HasClass("bard"))
            {
                int bardLevel = CharacterClasses.FindClassLevel("Bard");
                if (bardLevel >= 16) return true; //Jack of All Trades
            }

            if (_monSBSearch.HasArchetype("ancient guardian"))
            {
                int druidLevel = CharacterClasses.FindClassLevel("Druid");
                if (druidLevel >= 2)
                {
                    if (skill.SkillName == SkillData.SkillNames.Diplomacy || skill.SkillName == SkillData.SkillNames.SenseMotive
                    || (skill.SkillName == SkillData.SkillNames.Perform && SubValue == "oratory"))
                    {
                        return true;
                    }
                }
            }

            if (_monSBSearch.HasArchetype("lore warden"))
            {
                if (skill.SkillName == SkillData.SkillNames.Craft|| skill.SkillName == SkillData.SkillNames.Linguistics || skill.SkillName == SkillData.SkillNames.Spellcraft) return true;
                if (skill.Name.Contains("Knowledge")) return true;
            }

            if (skill.SkillName == SkillData.SkillNames.Diplomacy && _monSBSearch.HasTrait("trustworthy") || _monSBSearch.HasTrait("Destined Diplomat"))    return true;
            if (skill.SkillName == SkillData.SkillNames.KnowledgeLocal && _monSBSearch.HasTrait("civilized")) return true;
            if (skill.SkillName == SkillData.SkillNames.UseMagicDevice && _monSBSearch.HasTrait("Dangerously Curious")) return true;

            return false;
        }

        private string GetRaceName()
        {
            return string.IsNullOrEmpty(_monSBSearch.Race()) ? _monSBSearch.Name.ToLower() : _monSBSearch.Race();
        }

        #endregion Skills Check     
    }


    public class SkillCalculationValues
    {
        public SkillCalculationValues()
        {
            ExtraRanks = 0;
            TongueOfManyTowns = 0;
            GnomeAcademician = 0;
            GnomeObsessive = 0;
            ExpertSkills = 0;
            ScholarFeatSkills = 0;
            CosmopolitanFeatSkills = 0;
        }

        public int ExtraRanks { get; set; }
        public int TongueOfManyTowns { get; set; }
        public int GnomeAcademician { get; set; } //GnomeAcademician replaces GnomeObsessive
        public int GnomeObsessive { get; set; }
        public int ExpertSkills { get; set; }
        public int ScholarFeatSkills { get; set; }
        public bool HasMasterCraftsman { get; set; }
        public int CosmopolitanFeatSkills { get; set; }
    }
}
