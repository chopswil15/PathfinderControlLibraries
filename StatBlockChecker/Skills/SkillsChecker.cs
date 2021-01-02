using System;
using System.Collections.Generic;
using System.Linq;

using Skills;
using ClassManager;
using OnGoing;
using Utilities;
using ClassFoundational;
using StatBlockChecker.Parsers;
using StatBlockChecker.Skills;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace StatBlockChecker
{
    public class SkillsChecker : ISkillsChecker
    {
        private List<SkillsInfo.SkillInfo> _skillsValuesList;        
        private List<string> _gearList;
        private int _onGoingMods;
        private string _onGoingModsFormula;
        private string _onGoingModsCalculation;
        private List<string> _characterClassSkillsList;
        private List<string> _creatureTypeSkillsList;
        private int _knowledgeOneValue;

        public List<SkillCalculation> SkillCalculationsList { get; private set; }

        private SkillMods _skillMods;
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        private IEquipmentData _equipmentData;
        private ISpellsData _spellsData;
        private IFavoredClassData _favoredClassData;
        private IArmorClassData _armorClassData;
        private ISizeData _sizeData;

        public SkillsChecker(ISBCheckerBaseInput sbCheckerBaseInput, IEquipmentData equipmentData,
                             ISpellsData spellsData, IFavoredClassData favoredClassData,
                             IArmorClassData armorClassData, ISizeData sizeData)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _equipmentData = equipmentData;
            _spellsData = spellsData;
            _favoredClassData = favoredClassData;
            _armorClassData = armorClassData;
            _sizeData = sizeData;

            _onGoingModsFormula = string.Empty;
            _onGoingModsCalculation = string.Empty;
            SkillCalculationsList = new List<SkillCalculation>();
            _characterClassSkillsList = new List<string>();
            _creatureTypeSkillsList = new List<string>();

            _gearList = _sbCheckerBaseInput.MonsterSB.Gear.Split(',').ToList();
            if (_sbCheckerBaseInput.MonsterSB.OtherGear.Length > 0)
            {
                _gearList.AddRange(_sbCheckerBaseInput.MonsterSB.OtherGear.Split(',').ToList());
            }

            for (int a = 0; a <= _gearList.Count - 1; a++)
            {
                _gearList[a] = _gearList[a].Trim();
            }

            if (_sbCheckerBaseInput.IndvSB != null)
            {
                _onGoingMods = _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.Skill,
                                    OnGoingStatBlockModifier.StatBlockModifierSubTypes.None, false, ref _onGoingModsFormula);
            }

            string racialMods = _sbCheckerBaseInput.MonsterSB.RacialMods;
            if (_sbCheckerBaseInput.Race_Base.RaceSB != null && _sbCheckerBaseInput.Race_Base.RaceSB.RacialMods.Length > 0)
            {
                racialMods += _sbCheckerBaseInput.Race_Base.RaceSB.RacialMods;
            }

            _skillMods = new SkillMods(_sbCheckerBaseInput, _spellsData, _sizeData, _equipmentData, racialMods);
        }

        public List<SkillsInfo.SkillInfo> GetSkillsValues()
        {
            return _skillsValuesList;
        }

        #region Skills Check

        public void CheckSkillMath()
        {        

            int Ranks = ComputeMaxSkillRanks();
            int MaxRanks = Ranks;
            int ComputedRank = 0;
            int ComputedValue = 0;
            int ExtraSkills = 0;
            SkillCalculation SkillCalc = new SkillCalculation();
            Dictionary<string, int> VertPerformSkillValues = new Dictionary<string, int>();


            SkillsInfo.SkillInfo Info = new SkillsInfo.SkillInfo();
            _skillsValuesList = ParseSkills();
            if (_sbCheckerBaseInput.Race_Base.UseRacialHD)
            {
                _creatureTypeSkillsList = _sbCheckerBaseInput.CreatureType.ClassSkills();
            }
            List<string> classNames = _sbCheckerBaseInput.CharacterClasses.GetClassNames();

            List<string> ClassArchetypesList = _sbCheckerBaseInput.MonsterSB.ClassArchetypes.Split(',').ToList();
            foreach (string name in classNames)
            {
                if (ClassArchetypesList.Any())
                {
                    bool found = false;
                    foreach (string archetypes in ClassArchetypesList)
                    {
                        if (_sbCheckerBaseInput.CharacterClasses.IsClassArchetype(name, archetypes))
                        {
                            _characterClassSkillsList.AddRange(_sbCheckerBaseInput.CharacterClasses.GetClassArchetypeSkills(name, archetypes));
                            found = true;
                            break;
                        }
                    }
                    if (!found || !_characterClassSkillsList.Any())
                        _characterClassSkillsList.AddRange(_sbCheckerBaseInput.CharacterClasses.GetClassSkills(name));
                }
                else
                    _characterClassSkillsList.AddRange(_sbCheckerBaseInput.CharacterClasses.GetClassSkills(name));
            }

            if (_sbCheckerBaseInput.MonsterSB.Class.Contains("bard") || _sbCheckerBaseInput.MonsterSB.Class.Contains("skald"))
                try
                {
                    VertPerformanceCheck(Ranks, ref ComputedRank, ref ComputedValue, ref ExtraSkills, VertPerformSkillValues, ref Info);
                }
                catch (Exception ex)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("VertPerformanceCheck", ex.Message);
                }

            SkillCalculationValues skillCalculationValues = new SkillCalculationValues();
            if (_sbCheckerBaseInput.MonsterSBSearch.HasTrait("Tongue of Many Towns")) skillCalculationValues.TongueOfManyTowns = 2;
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Scholar")) skillCalculationValues.ScholarFeatSkills = 2;
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Cosmopolitan")) skillCalculationValues.CosmopolitanFeatSkills = 2;
            skillCalculationValues.HasMasterCraftsman = _sbCheckerBaseInput.MonsterSBSearch.HasFeat("Master Craftsman");
            if (_sbCheckerBaseInput.MonsterSBSearch.Race() == "gnome")
            {
                if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("academician"))
                {
                    skillCalculationValues.GnomeAcademician = 2;
                }
                else
                {
                    skillCalculationValues.GnomeObsessive = 2;
                }
            }

            string rankText = string.Empty;

            for (int index = 0; index <= _skillsValuesList.Count - 1; index++)
            {
                Info = _skillsValuesList[index];

                if (Info.Skill == null)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("Missing Skill Name", index.ToString(), string.Empty);
                    return;
                }
                try
                {
                    CheckOneSkillMath(ref Ranks, ref ComputedRank, ref ComputedValue, ref ExtraSkills, 
                        ref SkillCalc, VertPerformSkillValues, ref Info, ref rankText, index, 
                        skillCalculationValues);
                }
                catch (Exception ex)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("CheckOneSkillMath", "CheckOneSkillMath--" + Info.Skill.Name + "  " + ex.Message);
                }
            }

            _sbCheckerBaseInput.MessageXML.AddInfo("Rank Math: " + rankText);
            _sbCheckerBaseInput.MessageXML.AddInfo("Total Ranks Used " + (MaxRanks - Ranks).ToString());

            if (Ranks != 0)
            {
                if (Ranks > 0)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("Skill Points Unused ", Ranks.ToString());
                }
                else
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("Skill Points Exceeded ", Ranks.ToString());
                }
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddPass("Skill Points Used: ");
            }

            CheckForMisingSkills();
            CheckClassSkillsPreReqs();
        }

        private void CheckOneSkillMath(ref int Ranks, ref int ComputedRank, ref int ComputedValue, ref int ExtraSkills,
               ref SkillCalculation SkillCalc, Dictionary<string, int> VertPerformSkillValues, ref SkillsInfo.SkillInfo Info,
               ref string rankText, int skillValueIndex, SkillCalculationValues skillCalculationValues)
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
                SkillCalculationsList.Add(SkillCalc);
            }
            else if (Info.Skill != null)
            {
                ComputedRank = 0;
                SkillCalc = ComputeOneSkill(ref Info, Ranks, ref ComputedRank, ref ComputedValue, ref ExtraSkills, skillCalculationValues);
                if (ComputedRank > 0) Ranks -= (ComputedRank - skillCalculationValues.ExtraRanks);
                SkillCalculationsList.Add(SkillCalc);
                rankText += " +" + ComputedRank.ToString();
            }


            if (ComputedRank > _sbCheckerBaseInput.MonsterSB.HDValue()) //can't have more ranks than HD
            {
                _sbCheckerBaseInput.MessageXML.AddFail("Skill Over HD Max:" + Info.FullName(), ComputedRank.ToString(), _sbCheckerBaseInput.MonsterSB.HDValue().ToString());
            }


            if (ComputedValue == Info.Value)
            {
                _sbCheckerBaseInput.MessageXML.AddPass("Skill: " + Info.FullName() + " Ranks: " + ComputedRank.ToString());
            }
            else
            {
                _sbCheckerBaseInput.MessageXML.AddFail("Skill:" + Info.FullName(), ComputedValue.ToString(), Info.Value.ToString());
            }

            Info.Rank = ComputedRank;
            _skillsValuesList[skillValueIndex] = Info;
        }


        private SkillCalculation ComputeOneSkill(ref SkillsInfo.SkillInfo Info, int Ranks, ref int ComputedRank,
                                   ref int ComputedValue, ref int ExtraSkills, SkillCalculationValues skillCalculationValues)
        {
            int diff;
            SkillCalculation skillCalc;
                        
            bool expertSkillUsed = false;
            bool cosmopolitanFeatSkillUsed = false;
            bool scholarFeatSkillUsed = false;
            skillCalculationValues.ExtraRanks = 0; // reset everytime
            Skill skill = Info.Skill;
            ComputedRank = 0;

            ComputOneSkillData computOneSkillData = new ComputOneSkillData();           
            ComputedValue = SetBaseComputedValue(ref Info, skillCalculationValues, skill, computOneSkillData);

            if (Info.Skill.SkillName == SkillData.SkillNames.KnowledgeReligion && GetRaceName() == "deep one")
            {
                int raceHD = _sbCheckerBaseInput.MonsterSB.HDValue();
                ComputedValue += raceHD;
                ComputedRank = raceHD;
                skillCalculationValues.ExtraRanks = raceHD;
                computOneSkillData.Formula += "(ranks from deep one Devoted)";
            }
            if (skillCalculationValues.TongueOfManyTowns > 0 
                && (Info.Skill.SkillName == SkillData.SkillNames.Diplomacy 
                   || Info.Skill.SkillName == SkillData.SkillNames.KnowledgeLocal 
                   || Info.Skill.SkillName == SkillData.SkillNames.Linguistics) 
               && (ComputedValue < Info.Value))
            {
                skillCalculationValues.TongueOfManyTowns--;
                computOneSkillData.SkillMod++;
                ComputedValue++;
                computOneSkillData.Formula += "+1 Tongue of Many Towns";
            }

            if (Info.Skill.SkillName == SkillData.SkillNames.Fly && !_sbCheckerBaseInput.MonsterSB.Speed.Contains("fly"))
            {
                _sbCheckerBaseInput.MessageXML.AddFail("ComputeOneSkill", "Fly skill with no fly speed");
            }

            if (ComputedValue - 3 == Info.Value && computOneSkillData.ClassSkill > 0) // no ranks means no class skill bonus
            {
                ComputedValue -= 3;
                ComputedRank = 0;
                computOneSkillData.ClassSkill = 0;
                if (ExtraSkills > 0 && computOneSkillData.ExtraSkillUsed) ExtraSkills--;
                if (skillCalculationValues.ExpertSkills > 0 && expertSkillUsed) skillCalculationValues.ExpertSkills--;
            }
            else if (Ranks > 0)
            {
                if (skillCalculationValues.ScholarFeatSkills > 0 && skill.IsKnowledge && (ComputedValue + 2 < Info.Value))
                {
                    skillCalculationValues.ScholarFeatSkills--;
                    ComputedValue += 2;
                    computOneSkillData.Formula += " +2 Scholar";
                    scholarFeatSkillUsed = true;
                }
                if (skillCalculationValues.CosmopolitanFeatSkills > 0 && (ComputedValue + 3 < Info.Value)) //has to be more than 3 so you can add 1 rank too
                {
                    skillCalculationValues.CosmopolitanFeatSkills--;
                    computOneSkillData.ClassSkill = 1;
                    ComputedValue += 3;
                    computOneSkillData.Formula += " +3 Cosmopolitan";
                    cosmopolitanFeatSkillUsed = true;
                }
                if (ComputedValue < Info.Value)
                {
                    diff = Info.Value - ComputedValue;
                    ComputedValue += diff;
                    ComputedRank = diff;
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Master Craftsman") && skillCalculationValues.HasMasterCraftsman && ComputedRank - 2 >= 5)
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.Craft)
                {
                    ComputedRank -= 2;
                    computOneSkillData.Formula += " +2 Master Craftsman";
                    skillCalculationValues.HasMasterCraftsman = false;
                    computOneSkillData.ExtraMod += 2;
                }
            }

            if (_skillMods.HasSkillFocus(Info.FullSkillName()) && ComputedRank >= 13)
            {
                ComputedRank -= 3;
                computOneSkillData.Formula += "+3 SkillFocus-" + Info.FullSkillName() + " 10 ranks";
                computOneSkillData.ExtraMod += 3;
            }
            else if (_skillMods.HasSkillFocus(Info.FullSkillName()) && ComputedRank >= 10)
            {
                ComputedValue += 3;
                int hold = ComputedRank;
                ComputedRank -= (hold - 10);
                computOneSkillData.ExtraMod += 3;
            }

            bool over10;
            if (ComputedRank >= 12)
            {
                int holdRanks = Ranks;
                int holdComputedRank = ComputedRank;
                int holdExtraMod = computOneSkillData.ExtraMod;
                string holdformula = computOneSkillData.Formula;
                over10 = _skillMods.Over10RanksBonus(Info, ref holdRanks, ref holdComputedRank, ref holdExtraMod, ref holdformula, skill);

                if (over10) //no computOneSkillData.MagicItemMods, so apply over10
                {
                    ComputedRank = holdComputedRank;
                    computOneSkillData.ExtraMod = holdExtraMod;
                    computOneSkillData.Formula = holdformula + PathfinderConstants.SPACE + computOneSkillData.TempFormula;
                }
            }


            if (ComputedRank == 0 && computOneSkillData.ClassSkill > 0)
            {
                computOneSkillData.ClassSkill = 0;
                ComputedValue -= 3;
                if (cosmopolitanFeatSkillUsed) skillCalculationValues.CosmopolitanFeatSkills++;
                if (scholarFeatSkillUsed) skillCalculationValues.ScholarFeatSkills++;
                if (ComputedValue < Info.Value && Ranks > 0 && computOneSkillData.ExtraSkillUsed)
                {
                    diff = Info.Value - ComputedValue;
                    ComputedValue += diff;
                    ComputedRank += diff;
                    Ranks -= diff;
                }
                if (ComputedValue < Info.Value && Ranks > 0 && expertSkillUsed)
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
                computOneSkillData.Formula += " +2 gnome Obsessive";
            }
            if (skillCalculationValues.GnomeObsessive > 0 
                && (Info.Skill.SkillName == SkillData.SkillNames.Craft || Info.Skill.SkillName == SkillData.SkillNames.Profession) 
                && ComputedRank - 2 == _sbCheckerBaseInput.MonsterSB.HDValue())
            {
                skillCalculationValues.GnomeObsessive = 0;
                ComputedRank -= 2;
                Ranks += 2;
                computOneSkillData.Formula += " +2 gnome Obsessive";
            }
            if (skillCalculationValues.GnomeAcademician > 0 && Info.Skill.IsKnowledge && ComputedValue < Info.Value)
            {
                skillCalculationValues.GnomeAcademician = 0;
                ComputedValue += 2;
                computOneSkillData.Formula += " +2 gnome Academician";
            }
            if (skillCalculationValues.GnomeAcademician > 0 
                && Info.Skill.IsKnowledge 
                && ComputedRank - 2 == _sbCheckerBaseInput.MonsterSB.HDValue())
            {
                skillCalculationValues.GnomeAcademician = 0;
                ComputedRank -= 2;
                Ranks += 2;
                computOneSkillData.Formula += " +2 gnome Academician";
            }


            computOneSkillData.Formula += _onGoingModsCalculation + PathfinderConstants.SPACE + _onGoingModsFormula;
            if (computOneSkillData.MagicItemMods != 0) computOneSkillData.Formula += PathfinderConstants.SPACE + computOneSkillData.TempFormula;

            if (ExtraSkills != -1 && ComputedRank > _sbCheckerBaseInput.MonsterSB.HDValue() && computOneSkillData.ClassSkill == 0)
            {
                int classSkill = computOneSkillData.ClassSkill;
                bool extraSkillUsed = computOneSkillData.ExtraSkillUsed;
                HandleExtraClassSkills(ref ComputedRank, ref ExtraSkills, ref classSkill, ref extraSkillUsed);
                computOneSkillData.ClassSkill = classSkill;
                computOneSkillData.ExtraSkillUsed = expertSkillUsed;
            }

            int extraMod = computOneSkillData.ExtraMod + computOneSkillData.SkillMod + computOneSkillData.RaceMod + 
                computOneSkillData.DomainMods + _onGoingMods + computOneSkillData.MagicItemMods + computOneSkillData.InquisitionMods;

            skillCalc = new SkillCalculation(Info.FullSkillName(), ComputedValue, computOneSkillData.AbilityMod, computOneSkillData.AbilityUsed,
                            computOneSkillData.ClassSkill * 3, extraMod, computOneSkillData.ArmorCheckPenalty, ComputedRank, 
                            computOneSkillData.Formula, computOneSkillData.ExtraSkillUsed || cosmopolitanFeatSkillUsed);

            return skillCalc;
        }

        private int SetBaseComputedValue(ref SkillsInfo.SkillInfo Info, SkillCalculationValues skillCalculationValues,
                                         Skill skill, ComputOneSkillData computOneSkillData)
        {
            int ComputedValue;
            string formula = string.Empty;
            computOneSkillData.ExtraMod = _skillMods.FindExtraSkillsMods(skill.Name, Info, ref formula, _sbCheckerBaseInput.AbilityScores.StrMod);
            int classSkill = ComputeClassSkill(ref Info, skill);
            computOneSkillData.ArmorCheckPenalty = GetSkillACP(skill);
            if (_sbCheckerBaseInput.MonsterSB.Class.Contains("expert") && skillCalculationValues.ExpertSkills < 10 && computOneSkillData.ClassSkill == 0)
            {
                computOneSkillData.ClassSkill = 1;
                skillCalculationValues.ExpertSkills++;
                computOneSkillData.ExtraSkillUsed = true;
            }

            int abilityMod = 0;
            computOneSkillData.AbilityUsed = _skillMods.GetSkillAbilityMod(ref abilityMod, skill, _sbCheckerBaseInput.MonsterSB.Size);
            computOneSkillData.AbilityMod = abilityMod;
            computOneSkillData.SkillMod = _skillMods.ComputeSkillMod(Info.FullSkillName(), Info.Skill.Ability, Info.SubValue, ref formula, Info);
            string tempFormula;
            computOneSkillData.MagicItemMods = _skillMods.MagicItemMods(Info.FullSkillName(), Info.Skill.Ability, _equipmentData.MagicItemAbilities, out tempFormula);
            computOneSkillData.TempFormula = tempFormula;
            computOneSkillData.RaceMod = _skillMods.ComputeRaceSkillMod(Info.FullSkillName(), ref formula, ref _knowledgeOneValue);
            computOneSkillData.DomainMods = _skillMods.ComputeDomainSkillMods(skill.Name, Info, computOneSkillData.ClassSkill, ref formula);
            computOneSkillData.InquisitionMods = _skillMods.ComputeInquisitionSkillMods(skill.Name, Info, computOneSkillData.ClassSkill, ref formula);
            
            _skillMods.ComputeBloodlineSkillMods(Info, ref classSkill, ref formula);
            _skillMods.ComputeOracleSkillMods(Info, ref classSkill);
            ComputedValue = abilityMod + (classSkill * 3) + computOneSkillData.ExtraMod + computOneSkillData.SkillMod + 
                computOneSkillData.RaceMod + computOneSkillData.DomainMods + computOneSkillData.ArmorCheckPenalty + _onGoingMods +
                computOneSkillData.MagicItemMods + computOneSkillData.InquisitionMods;
            computOneSkillData.Formula = formula;
            computOneSkillData.ClassSkill = classSkill;

           return ComputedValue;
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
                foreach (string skillString in _creatureTypeSkillsList)
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
            foreach (ClassWrapper wrapper in _sbCheckerBaseInput.CharacterClasses.Classes)
            {
                List<PreReqSkill> classPreReqs = _sbCheckerBaseInput.CharacterClasses.GetPrestigePreReqSkills(wrapper.Name);
                SkillsInfo.SkillInfo skill;

                foreach (PreReqSkill onePreReqSkill in classPreReqs)
                {
                    if (onePreReqSkill.SubType == null)
                    {
                        skill = _skillsValuesList.Find(y => y.Skill.SkillName == onePreReqSkill.SkillName);
                    }
                    else
                    {
                        skill = _skillsValuesList.Find(y => y.Skill.SkillName == onePreReqSkill.SkillName 
                                                              && y.SubValue == onePreReqSkill.SubType);
                    }

                    if (skill.Rank < onePreReqSkill.Value)
                    {
                        string temp = string.Empty;
                        if (onePreReqSkill.SubType != null && onePreReqSkill.SubType.Length > 0) temp = " (" + onePreReqSkill.SubType + PathfinderConstants.PAREN_RIGHT;
                        _sbCheckerBaseInput.MessageXML.AddFail("Skill Pre Req", wrapper.Name + " needs min of " + onePreReqSkill.Value
                                   + " ranks of " + onePreReqSkill.SkillName.ToString() + temp + " has " + skill.Rank.ToString() + " ranks");
                    }
                }
            }

            if (_sbCheckerBaseInput.CharacterClasses.ClassCount() > 0)
            {
                if (_sbCheckerBaseInput.CharacterClasses.HasClass("loremaster"))
                {
                    int count = 0;
                    foreach (SkillsInfo.SkillInfo oneSkill in _skillsValuesList)
                    {
                        if (oneSkill.Skill.Name.Contains("Knowledge") && oneSkill.Rank >= 7)
                        {
                            count++;
                        }
                    }
                    if (count < 2) _sbCheckerBaseInput.MessageXML.AddFail("Skill Pre Req", "Loremaster needs min of 7 ranks of 2 Knowledge skills but doesn't");
                }

                if (_sbCheckerBaseInput.CharacterClasses.HasClass("low templar"))
                {
                    SkillsInfo.SkillInfo skill = _skillsValuesList.Find(y => y.Skill.SkillName == SkillData.SkillNames.KnowledgeNobility);
                    if (skill.Rank < 2)
                    {
                        skill = _skillsValuesList.Find(y => y.Skill.SkillName == SkillData.SkillNames.KnowledgePlanes);
                        if (skill.Rank < 2)
                        {
                            _sbCheckerBaseInput.MessageXML.AddFail("Skill Pre Req", "Low Templar needs min 2 ranks of  Knowledge (nobility or planes) has " + skill.Rank.ToString() + " ranks");
                        }
                    }
                }
            }
        }

        private void VertPerformanceCheck(int ranks, ref int computedRank, ref int ComputedValue, ref int extraSkills,
                 Dictionary<string, int> vertPerformSkillValues, ref SkillsInfo.SkillInfo info)
        {
            List<string> vertPerformValues = ComputeBardicVersatilePerformance();
            if (!vertPerformValues.Any()) return;
               
            for (int a = 0; a < vertPerformValues.Count; a++)
            {
                string tempPerformName = "Perform [" + vertPerformValues[a] + "]";
                //  vertPerformValues.Remove(vertPerformValues[a]);

                foreach (SkillsInfo.SkillInfo tempInfo in _skillsValuesList)
                {
                    if (tempInfo.FullName() == tempPerformName)
                    {
                        info = tempInfo;
                        SkillCalculationValues skillCalculationValues = new SkillCalculationValues();

                        ComputeOneSkill(ref info, ranks, ref computedRank, ref ComputedValue, ref extraSkills, skillCalculationValues);
                        if (ComputedValue > 0)
                        {
                            if (vertPerformValues.Count < 3)
                            {
                                _sbCheckerBaseInput.MessageXML.AddFail("vertPerformValues", "Count: " + vertPerformValues.Count.ToString() + " Performance Name:" + tempPerformName);
                                return;
                            }

                            ComputeVersatilePerformance(ComputedValue, vertPerformSkillValues, vertPerformValues, a);
                        }
                    }
                }
            }            
        }

        private void ComputeVersatilePerformance(int computedValue, Dictionary<string, int> vertPerformSkillValues, 
                List<string> vertPerformValues, int index)
        {
            string firstVert = vertPerformValues[index + 1];
            string secondVert = vertPerformValues[index + 2];
            if (vertPerformSkillValues.ContainsKey(firstVert))
            {
                int temp = vertPerformSkillValues[firstVert];
                if (temp < computedValue)
                {
                    vertPerformSkillValues[firstVert] = computedValue;
                }
            }
            else
            {
                vertPerformSkillValues.Add(firstVert, computedValue);
            }
            if (vertPerformSkillValues.ContainsKey(secondVert))
            {
                int temp = vertPerformSkillValues[secondVert];
                if (temp < computedValue)
                {
                    vertPerformSkillValues[secondVert] = computedValue;
                }
            }
            else
            {
                vertPerformSkillValues.Add(secondVert, computedValue);
            }
        }

        private void CheckForMisingSkills()
        {
            if (_sbCheckerBaseInput.MonsterSB.Speed.Contains("swim") && !_sbCheckerBaseInput.MonsterSB.Skills.Contains(StatBlockInfo.SkillNames.SWIM))
            {
                int ComputedValue = 0;
                SkillsInfo.SkillInfo Info = new SkillsInfo.SkillInfo("Swim +0");
                SkillCalculation SkillCalc = ComputeOneSkillWithFakes(ref Info, ref ComputedValue);
                _sbCheckerBaseInput.MessageXML.AddFail("Missing Skill", "Swim, has swim speed. Could be " + ComputedValue.ToString() + PathfinderConstants.SPACE + SkillCalc.ToString());
            }

            if (_sbCheckerBaseInput.MonsterSB.Speed.Contains("fly") && !_sbCheckerBaseInput.MonsterSB.Skills.Contains(StatBlockInfo.SkillNames.FLY))
            {
                int ComputedValue = 0;
                SkillsInfo.SkillInfo Info = new SkillsInfo.SkillInfo("Fly +0");
                SkillCalculation SkillCalc = ComputeOneSkillWithFakes(ref Info, ref ComputedValue);
                _sbCheckerBaseInput.MessageXML.AddFail("Missing Skill", "Fly, has fly speed. Could be " + ComputedValue.ToString() + PathfinderConstants.SPACE + SkillCalc.ToString());
            }
            if (!_sbCheckerBaseInput.MonsterSB.Skills.Contains(StatBlockInfo.SkillNames.STEALTH))
            {
                int ComputedValue = 0;
                SkillsInfo.SkillInfo Info = new SkillsInfo.SkillInfo("Stealth +0");

                SkillCalculation SkillCalc = ComputeOneSkillWithFakes(ref Info, ref ComputedValue);
                if (ComputedValue != 0)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("Missing Skill", "Stealth could be " + ComputedValue.ToString() + PathfinderConstants.SPACE + SkillCalc.ToString());
                }
            }
        }

        private SkillCalculation ComputeOneSkillWithFakes(ref SkillsInfo.SkillInfo Info, ref int ComputedValue)
        {
            int Ranks = 60;
            int ComputedRank = 0;
            int ExtraSkills = 0;

            SkillCalculationValues skillCalculationValues = new SkillCalculationValues();

            return ComputeOneSkill(ref Info, Ranks, ref ComputedRank, ref ComputedValue, ref ExtraSkills, skillCalculationValues);
        }

        private int GetSkillACP(Skill skill)
        {
            if (_armorClassData.TotalArmorCheckPenalty == 0) return 0;
            int armorCheckPenalty = 0;            

            if (skill.ArmorCheckPenalty)
            {
                armorCheckPenalty = _armorClassData.TotalArmorCheckPenalty;
                if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("acrobat") && _sbCheckerBaseInput.MonsterSBSearch.HasLightArmor())
                {
                    if (skill.SkillName == SkillData.SkillNames.Acrobatics || skill.SkillName == SkillData.SkillNames.Climb || skill.SkillName == SkillData.SkillNames.Fly || skill.SkillName == SkillData.SkillNames.SleightofHand || skill.SkillName == SkillData.SkillNames.Stealth)
                    {
                        armorCheckPenalty = 0;
                    }
                }
                if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("roughrider"))
                {
                    int roughriderLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("fighter");
                    if (roughriderLevel >= 3 && skill.SkillName == SkillData.SkillNames.Ride)
                    {
                        armorCheckPenalty = 0;
                    }
                }
            }
            return armorCheckPenalty;
        }

        private List<string> ComputeBardicVersatilePerformance()
        {
            List<string> values = new List<string>();
            string perform = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("versatile performance");
            if (perform.Length == 0) return values;

            perform = perform.Replace("versatile performance", string.Empty);
            perform = Utility.RemoveParentheses(perform);

            List<string> performances = perform.Split('|').ToList();


            foreach (string onePerformance in performances)
            {
                string holdPerform = onePerformance.Trim().ToLower();
                values.Add(holdPerform);
                switch (holdPerform)
                {
                    case "act":
                        values.Add(StatBlockInfo.SkillNames.BLUFF);
                        values.Add(StatBlockInfo.SkillNames.DISGUISE);
                        break;
                    case "comedy":
                        values.Add(StatBlockInfo.SkillNames.BLUFF);
                        values.Add(StatBlockInfo.SkillNames.INTIMIDATE);
                        break;
                    case "dance":
                        values.Add(StatBlockInfo.SkillNames.ACROBATICS);
                        values.Add(StatBlockInfo.SkillNames.FLY);
                        break;
                    case "keyboard instruments":
                        values.Add(StatBlockInfo.SkillNames.DIPLOMACY);
                        values.Add(StatBlockInfo.SkillNames.INTIMIDATE);
                        break;
                    case "oratory":
                        values.Add(StatBlockInfo.SkillNames.DIPLOMACY);
                        values.Add(StatBlockInfo.SkillNames.SENSE_MOTIVE);
                        break;
                    case "percussion":
                        values.Add(StatBlockInfo.SkillNames.HANDLE_ANIMAL);
                        values.Add(StatBlockInfo.SkillNames.INTIMIDATE);
                        break;
                    case "sing":
                        values.Add(StatBlockInfo.SkillNames.BLUFF);
                        values.Add(StatBlockInfo.SkillNames.SENSE_MOTIVE);
                        break;
                    case "string":
                    case "string instruments":
                        values.Add(StatBlockInfo.SkillNames.BLUFF);
                        values.Add(StatBlockInfo.SkillNames.DIPLOMACY);
                        break;
                    case "wind":
                        values.Add(StatBlockInfo.SkillNames.DIPLOMACY);
                        values.Add(StatBlockInfo.SkillNames.HANDLE_ANIMAL);
                        break;
                }
            }
            return values;
        }

        private List<SkillsInfo.SkillInfo> ParseSkills()
        {
            SkillsParser skillsParser = new SkillsParser(_sbCheckerBaseInput);
            List<SkillsInfo.SkillInfo>  result = skillsParser.ParseSkills(_sbCheckerBaseInput.MonsterSB.Skills);
            return result;            
        }

        private int ComputeMaxSkillRanks()
        {
            MaxSkillRanksComputer maxSkillRanksComputer = new MaxSkillRanksComputer(_sbCheckerBaseInput, _favoredClassData);
            return maxSkillRanksComputer.ComputeMaxSkillRanks();
        }        

        private bool IsCreatureTypeClassSkill(Skill skill)
        {
            //if (_sbCheckerBaseInput.MonsterSB.Environment.Length == 0) return false;
            string SkillName = skill.Name;

            if (_creatureTypeSkillsList.Contains(SkillName))
            {
                return true;
            }

            string raceName = _sbCheckerBaseInput.MonsterSB.IsBestiary ? _sbCheckerBaseInput.MonsterSB.name.ToLower() : _sbCheckerBaseInput.MonsterSB.Race.ToLower();

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
                    switch (skill.SkillName)
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
                if (_creatureTypeSkillsList.Contains(StatBlockInfo.SkillNames.KNOWLEDGE_ONE) && _knowledgeOneValue == 0)
                {
                    _knowledgeOneValue = -1;
                    return true;
                }
                if (_creatureTypeSkillsList.Contains(StatBlockInfo.SkillNames.KNOWLEDGE_ALL))
                {
                    return true;
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasTemplate("lich") || _sbCheckerBaseInput.MonsterSBSearch.HasTemplate("mummy lord"))
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

            if (_sbCheckerBaseInput.MonsterSBSearch.HasTemplate("skeletal champion"))
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

            if (_sbCheckerBaseInput.MonsterSB.Speed.Contains("fly") && skill.SkillName == SkillData.SkillNames.Fly && !_sbCheckerBaseInput.MonsterSBSearch.HasGear("winged boots") && !_sbCheckerBaseInput.MonsterSBSearch.HasGear("scroll of fly"))
            {
                return true;
            }

            if (skill.SkillName == SkillData.SkillNames.Fly && _sbCheckerBaseInput.MonsterSB.Bloodline.Contains("elemental (air)"))
            {
                return true;
            }

            if (_sbCheckerBaseInput.MonsterSB.SubType.Contains("giant"))
            {
                if (skill.SkillName == SkillData.SkillNames.Intimidate || skill.SkillName == SkillData.SkillNames.Perception)
                {
                    return true;
                }
            }

            if (_sbCheckerBaseInput.MonsterSB.SubType.Contains("water") || _sbCheckerBaseInput.MonsterSB.SubType.Contains("aquatic"))
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
            if (_characterClassSkillsList.Contains(skill.Name)) return true;

            if (SubValue.Length > 0 && skill.Name == StatBlockInfo.SkillNames.PERFORM)
            {
                if (_characterClassSkillsList.Contains(skill.Name + " (" + SubValue + PathfinderConstants.PAREN_RIGHT)) return true;
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasTemplate("lich"))
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
                    string sop = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("shards of the past");
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

            if (_sbCheckerBaseInput.MonsterSBSearch.HasSubType("robot"))
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

            if (_sbCheckerBaseInput.MonsterSBSearch.HasSubType("goblinoid") && skill.SkillName == SkillData.SkillNames.Stealth) return true;

            if (skill.Name.Contains("Knowledge"))
            {
                if (_characterClassSkillsList.Contains(StatBlockInfo.SkillNames.KNOWLEDGE_ONE)) return true;
                if (_characterClassSkillsList.Contains(StatBlockInfo.SkillNames.KNOWLEDGE_ALL)) return true;
            }

            if (_sbCheckerBaseInput.MonsterSB.TemplatesApplied.Contains("haunted one"))
            {
                if (skill.Name.Contains("Knowledge")) return true;
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("order of the lion"))
            {
                if (skill.SkillName == SkillData.SkillNames.KnowledgeNobility) return true;
                if (skill.SkillName == SkillData.SkillNames.KnowledgeLocal) return true;
            }
            if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("order of the dargon"))
            {
                if (skill.SkillName == SkillData.SkillNames.Perception) return true;
                if (skill.SkillName == SkillData.SkillNames.Survival) return true;
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("order of the cockatrice"))
            {
                if (skill.SkillName == SkillData.SkillNames.Appraise) return true;
                if (skill.SkillName == SkillData.SkillNames.Perform) return true;
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("order of the warrior"))
            {
                if (skill.SkillName == SkillData.SkillNames.KnowledgeHistory) return true;
                if (skill.SkillName == SkillData.SkillNames.KnowledgeNobility) return true;
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("ronin")) // kinda of a samurai order
            {
                if (skill.SkillName == SkillData.SkillNames.KnowledgeLocal) return true;
                if (skill.SkillName == SkillData.SkillNames.Survival) return true;
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Derro Magister") && skill.SkillName == SkillData.SkillNames.Heal) return true;

            if (_sbCheckerBaseInput.CharacterClasses.HasClass("bard"))
            {
                int bardLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Bard");
                if (bardLevel >= 16) return true; //Jack of All Trades
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("ancient guardian"))
            {
                int druidLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Druid");
                if (druidLevel >= 2)
                {
                    if (skill.SkillName == SkillData.SkillNames.Diplomacy || skill.SkillName == SkillData.SkillNames.SenseMotive
                    || (skill.SkillName == SkillData.SkillNames.Perform && SubValue == "oratory"))
                    {
                        return true;
                    }
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("lore warden"))
            {
                if (skill.SkillName == SkillData.SkillNames.Craft || skill.SkillName == SkillData.SkillNames.Linguistics || skill.SkillName == SkillData.SkillNames.Spellcraft) return true;
                if (skill.Name.Contains("Knowledge")) return true;
            }

            if (skill.SkillName == SkillData.SkillNames.Diplomacy && _sbCheckerBaseInput.MonsterSBSearch.HasTrait("trustworthy") || _sbCheckerBaseInput.MonsterSBSearch.HasTrait("Destined Diplomat")) return true;
            if (skill.SkillName == SkillData.SkillNames.KnowledgeLocal && _sbCheckerBaseInput.MonsterSBSearch.HasTrait("civilized")) return true;
            if (skill.SkillName == SkillData.SkillNames.UseMagicDevice && _sbCheckerBaseInput.MonsterSBSearch.HasTrait("Dangerously Curious")) return true;

            return false;
        }

        private string GetRaceName()
        {
            return string.IsNullOrEmpty(_sbCheckerBaseInput.MonsterSBSearch.Race()) ? _sbCheckerBaseInput.MonsterSBSearch.Name.ToLower() : _sbCheckerBaseInput.MonsterSBSearch.Race();
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

    public class ComputOneSkillData
    {
        public bool ExtraSkillUsed { get; set; }
        public int ExtraMod { get; set; }
        public int ClassSkill { get; set; }
        public int ArmorCheckPenalty { get; set; }
        public int AbilityMod { get; set; }
        public int SkillMod { get; set; }
        public int MagicItemMods { get; set; }
        public int RaceMod { get; set; }
        public int DomainMods { get; set; }
        public int InquisitionMods { get; set; }
        public string Formula { get; set; }
        public string AbilityUsed { get; set; }
        public string TempFormula { get; set; }
    }
}
