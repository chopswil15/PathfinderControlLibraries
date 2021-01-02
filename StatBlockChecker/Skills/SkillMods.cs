using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using Skills;
using StatBlockCommon;

using EquipmentBasic;
using Utilities;
using CommonInterFacesDD;
using OnGoing;
using MagicItemAbilityWrapper;
using PathfinderGlobals;

namespace StatBlockChecker
{
    public class SkillMods : ISkillMods
    {
        private string _racialMods;
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        private ISpellsData _spellsData;
        private ISizeData _sizeData;
        private IEquipmentData _equipmentData;

        public SkillMods(ISBCheckerBaseInput sbCheckerBaseInput, ISpellsData spellsData, ISizeData sizeData,
                          IEquipmentData equipmentData, string RacialMods)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _spellsData = spellsData;
            _sizeData = sizeData;
            _equipmentData = equipmentData;
            _racialMods = RacialMods;
        }

        public int ComputeClimbMod(ref string formula)
        {
            int Mod = 0;

            if (_sbCheckerBaseInput.MonsterSB.Speed.Contains("climb") && !_sbCheckerBaseInput.MonsterSBSearch.HasGear("slippers of spider climbing"))
            {
                Mod = 8;
                formula += " +8 climb speed";
            }

            return Mod;
        }

        public int ComputeSwimMod(ref string formula)
        {
            int Mod = 0;

            if (_sbCheckerBaseInput.MonsterSB.Speed.Contains("swim"))
            {
                Mod = 8;
                formula += " +8 swim speed";
            }

            return Mod;
        }

        public int ComputeStealthSizeMod(ref string formula)
        {
            int Mod = 0;
            switch (_sizeData.SizeCat)
            {
                case StatBlockInfo.SizeCategories.Fine:
                    Mod = 16;
                    formula += " +16 Fine";
                    break;
                case StatBlockInfo.SizeCategories.Diminutive:
                    Mod = 12;
                    formula += " +12 Diminutive";
                    break;
                case StatBlockInfo.SizeCategories.Tiny:
                    Mod = 8;
                    formula += " +8 Tiny";
                    break;
                case StatBlockInfo.SizeCategories.Small:
                    Mod = 4;
                    formula += " +4 Small";
                    break;
                case StatBlockInfo.SizeCategories.Medium:
                    Mod = 0;
                    formula += " +0 Medium";
                    break;
                case StatBlockInfo.SizeCategories.Large:
                    Mod = -4;
                    formula += " -4 Large";
                    break;
                case StatBlockInfo.SizeCategories.Huge:
                    Mod = -8;
                    formula += " -8 Huge";
                    break;
                case StatBlockInfo.SizeCategories.Gargantuan:
                    Mod = -12;
                    formula += " -12 Gargantuan";
                    break;
                case StatBlockInfo.SizeCategories.Colossal:
                    Mod = -16;
                    formula += " -16 Colossal";
                    break;
            }

            return Mod;
        }

        public int ComputeFlySizeMod(ref string formula)
        {
            int Mod = 0;
            switch (_sizeData.SizeCat)
            {
                case StatBlockInfo.SizeCategories.Fine:
                    Mod = 8;
                    formula += " +8 Fine";
                    break;
                case StatBlockInfo.SizeCategories.Diminutive:
                    Mod = 6;
                    formula += " +6 Diminutive";
                    break;
                case StatBlockInfo.SizeCategories.Tiny:
                    Mod = 4;
                    formula += " +4 Tiny";
                    break;
                case StatBlockInfo.SizeCategories.Small:
                    Mod = 2;
                    formula += " +2 Small";
                    break;
                case StatBlockInfo.SizeCategories.Medium:
                    Mod = 0;
                    formula += " +0 Medium";
                    break;
                case StatBlockInfo.SizeCategories.Large:
                    Mod = -2;
                    formula += " -2 Large";
                    break;
                case StatBlockInfo.SizeCategories.Huge:
                    Mod = -4;
                    formula += " -4 Huge";
                    break;
                case StatBlockInfo.SizeCategories.Gargantuan:
                    Mod = -6;
                    formula += " -6 Gargantuan";
                    break;
                case StatBlockInfo.SizeCategories.Colossal:
                    Mod = -8;
                    formula += " -8 Colossal";
                    break;
            }

            return Mod;
        }

        public int ComputeFlyManeuverabilityMod(ref string formula)
        {
            string temp = _sbCheckerBaseInput.MonsterSB.Speed;
            int Pos = temp.IndexOf("fly");
            if (Pos == -1) return 0;

            try
            {
                Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT, Pos);
                int Pos2 = temp.IndexOf(PathfinderConstants.PAREN_RIGHT, Pos);
                if (Pos2 == -1) Pos2 = temp.Length - 1;
                temp = temp.Substring(Pos, Pos2 - Pos);
                temp = Utility.RemoveParentheses(temp);
                Pos = temp.IndexOf(",");
                if (Pos >= 0)
                {
                    temp = temp.Substring(0, Pos).Trim();
                }
                Pos = temp.IndexOf(";");
                if (Pos >= 0)
                {
                    temp = temp.Substring(0, Pos).Trim();
                }
            }
            catch
            {
                _sbCheckerBaseInput.MessageXML.AddFail("ComputeFlyManeuverabilityMod", "No Maneuverability");
            }
            int Mod = 0;

            switch (temp.ToLower())
            {
                case "clumsy":
                    Mod = -8;
                    formula += " -8 " + temp.ToLower();
                    break;
                case "poor":
                    Mod = -4;
                    formula += " -4 " + temp.ToLower();
                    break;
                case "average":
                    Mod = 0;
                    formula += " +0 " + temp.ToLower();
                    break;
                case "good":
                    Mod = 4;
                    formula += " +4 " + temp.ToLower();
                    break;
                case "perfect":
                    Mod = 8;
                    formula += " +8 " + temp.ToLower();
                    break;
                default:
                    Mod = -100;
                    break;
            }

            return Mod;
        }

        public int ComputeDomainSkillMods(string SkillName, SkillsInfo.SkillInfo Info, int ClassSkill, ref string formula)
        {
            int Mod = 0;

            if (_sbCheckerBaseInput.MonsterSB.SpellDomains.Length == 0) return Mod;

            if (_sbCheckerBaseInput.MonsterSB.SpellDomains.Contains("Knowledge") && Info.Skill.IsKnowledge && ClassSkill == 0)
            {
                Mod = 3;
                formula += " +3 Domain Knowledge";
            }

            if (_sbCheckerBaseInput.MonsterSB.SpellDomains.Contains("Trickery") && ClassSkill == 0)
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.Bluff || Info.Skill.SkillName == SkillData.SkillNames.Disguise || Info.Skill.SkillName == SkillData.SkillNames.Stealth)
                {
                    Mod = 3;
                    formula += " +3 Domain Trickery";
                }
            }

            return Mod;
        }

        public int ComputeInquisitionSkillMods(string SkillName, SkillsInfo.SkillInfo Info, int ClassSkill, ref string formula)
        {
            int Mod = 0;

            if (_sbCheckerBaseInput.MonsterSB.SpellDomains.Length == 0) return Mod;

            if (_sbCheckerBaseInput.MonsterSB.SpellDomains.Contains("Torture") && Info.Skill.SkillName == SkillData.SkillNames.Intimidate)
            {
                Mod = 2;
                formula += " +2 Inquisition Torture";
            }

            return Mod;
        }

        public void ComputeOracleSkillMods(SkillsInfo.SkillInfo Info, ref int ClassSkill)
        {
            if (ClassSkill == 1) return;
            if (_sbCheckerBaseInput.MonsterSB.Mystery.Length == 0) return;

            switch (_sbCheckerBaseInput.MonsterSBSearch.Mystery.ToLower())
            {
                case "ancestor":
                    switch (Info.Skill.SkillName)
                    {
                        case SkillData.SkillNames.Linguistics:
                        case SkillData.SkillNames.KnowledgeArcana:
                        case SkillData.SkillNames.KnowledgeDungeoneering:
                        case SkillData.SkillNames.KnowledgeEngineering:
                        case SkillData.SkillNames.KnowledgeGeography:
                        case SkillData.SkillNames.KnowledgeHistory:
                        case SkillData.SkillNames.KnowledgeLocal:
                        case SkillData.SkillNames.KnowledgeNature:
                        case SkillData.SkillNames.KnowledgeNobility:
                        case SkillData.SkillNames.KnowledgePlanes:
                        case SkillData.SkillNames.KnowledgeReligion:
                            ClassSkill = 1;
                            break;
                    }
                    return;
                case "apocalypse":
                    switch (Info.Skill.SkillName)
                    {
                        case SkillData.SkillNames.Bluff:
                        case SkillData.SkillNames.Disguise:
                        case SkillData.SkillNames.Survival:
                        case SkillData.SkillNames.Stealth:
                            ClassSkill = 1;
                            break;
                    }
                    return;
                case "ascetic":
                    switch (Info.Skill.SkillName)
                    {
                        case SkillData.SkillNames.Acrobatics:
                        case SkillData.SkillNames.Climb:
                        case SkillData.SkillNames.EscapeArtist:
                        case SkillData.SkillNames.Swim:
                            ClassSkill = 1;
                            break;
                    }
                    break;
                case "battle":
                    switch (Info.Skill.SkillName)
                    {
                        case SkillData.SkillNames.Intimidate:
                        case SkillData.SkillNames.KnowledgeEngineering:
                        case SkillData.SkillNames.Perception:
                        case SkillData.SkillNames.Ride:
                            ClassSkill = 1;
                            break;
                    }
                    return;
                case "bones":

                    switch (Info.Skill.SkillName)
                    {
                        case SkillData.SkillNames.Bluff:
                        case SkillData.SkillNames.Disguise:
                        case SkillData.SkillNames.Intimidate:
                        case SkillData.SkillNames.Stealth:
                            ClassSkill = 1;
                            break;
                    }
                    return;
                case "dark tapestry":

                    switch (Info.Skill.SkillName)
                    {
                        case SkillData.SkillNames.Disguise:
                        case SkillData.SkillNames.Intimidate:
                        case SkillData.SkillNames.KnowledgeArcana:
                        case SkillData.SkillNames.Stealth:
                            ClassSkill = 1;
                            break;
                    }
                    return;
                case "flame":
                    switch (Info.Skill.SkillName)
                    {
                        case SkillData.SkillNames.Acrobatics:
                        case SkillData.SkillNames.Climb:
                        case SkillData.SkillNames.Intimidate:
                        case SkillData.SkillNames.Perform:
                            ClassSkill = 1;
                            break;
                    }
                    return;
                case "heavens":
                    switch (Info.Skill.SkillName)
                    {
                        case SkillData.SkillNames.Fly:
                        case SkillData.SkillNames.KnowledgeArcana:
                        case SkillData.SkillNames.Perception:
                        case SkillData.SkillNames.Survival:
                            ClassSkill = 1;
                            break;
                    }
                    return;
                case "juju":
                    switch (Info.Skill.SkillName)
                    {
                        case SkillData.SkillNames.Bluff:
                        case SkillData.SkillNames.Intimidate:
                        case SkillData.SkillNames.KnowledgeNature:
                        case SkillData.SkillNames.Perform:
                        case SkillData.SkillNames.Survival:
                            if (Info.Skill.SkillName == SkillData.SkillNames.Perform && Info.SubValue != "oratory") return;
                            ClassSkill = 1;
                            break;
                    }
                    return;
                case "lore":
                    switch (Info.Skill.SkillName)
                    {
                        case SkillData.SkillNames.Appraise:
                        case SkillData.SkillNames.Spellcraft:
                        case SkillData.SkillNames.KnowledgeArcana:
                        case SkillData.SkillNames.KnowledgeDungeoneering:
                        case SkillData.SkillNames.KnowledgeEngineering:
                        case SkillData.SkillNames.KnowledgeGeography:
                        case SkillData.SkillNames.KnowledgeHistory:
                        case SkillData.SkillNames.KnowledgeLocal:
                        case SkillData.SkillNames.KnowledgeNature:
                        case SkillData.SkillNames.KnowledgeNobility:
                        case SkillData.SkillNames.KnowledgePlanes:
                        case SkillData.SkillNames.KnowledgeReligion:
                            ClassSkill = 1;
                            break;
                    }
                    return;
                case "metal":
                    switch (Info.Skill.SkillName)
                    {
                        case SkillData.SkillNames.Appraise:
                        case SkillData.SkillNames.Bluff:
                        case SkillData.SkillNames.DisableDevice:
                        case SkillData.SkillNames.Intimidate:
                            ClassSkill = 1;
                            break;
                    }
                    return;
                case "outer rifts":
                    switch (Info.Skill.SkillName)
                    {
                        case SkillData.SkillNames.Fly:
                        case SkillData.SkillNames.Intimidate:
                        case SkillData.SkillNames.KnowledgeArcana:
                        case SkillData.SkillNames.Survival:
                            ClassSkill = 1;
                            break;
                    }
                    break;
                case "time":
                    switch (Info.Skill.SkillName)
                    {
                        case SkillData.SkillNames.Fly:
                        case SkillData.SkillNames.KnowledgeArcana:
                        case SkillData.SkillNames.Perception:
                        case SkillData.SkillNames.UseMagicDevice:
                            ClassSkill = 1;
                            break;
                    }
                    return;
                case "stone":
                    switch (Info.Skill.SkillName)
                    {
                        case SkillData.SkillNames.Appraise:
                        case SkillData.SkillNames.Climb:
                        case SkillData.SkillNames.Intimidate:
                        case SkillData.SkillNames.Survival:
                            ClassSkill = 1;
                            break;
                    }
                    return;
                case "waves":
                    switch (Info.Skill.SkillName)
                    {
                        case SkillData.SkillNames.Acrobatics:
                        case SkillData.SkillNames.EscapeArtist:
                        case SkillData.SkillNames.KnowledgeNature:
                        case SkillData.SkillNames.Swim:
                            ClassSkill = 1;
                            break;
                    }
                    return;
                case "wind":
                    switch (Info.Skill.SkillName)
                    {
                        case SkillData.SkillNames.Acrobatics:
                        case SkillData.SkillNames.EscapeArtist:
                        case SkillData.SkillNames.Fly:
                        case SkillData.SkillNames.Stealth:
                            ClassSkill = 1;
                            break;
                    }
                    return;
                default:
                    _sbCheckerBaseInput.MessageXML.AddFail("ComputeOracleSkillMods", "Mystery " + _sbCheckerBaseInput.MonsterSB.Mystery + " not defined for skills");
                    break;
            }
        }

        public void ComputeBloodlineSkillMods(SkillsInfo.SkillInfo Info, ref int ClassSkill, ref string formula)
        {
            if (!_sbCheckerBaseInput.MonsterSBSearch.HasBloodline()) return;
            if (ClassSkill == 1) return;

            SkillData.SkillNames BloodlineClassSkill = _sbCheckerBaseInput.CharacterClasses.GetBloodlineClassSkill();
            string BloodlineName = _sbCheckerBaseInput.CharacterClasses.GetBloodlineName();
            if (BloodlineClassSkill == SkillData.SkillNames.KnowledgeAnyOne) _sbCheckerBaseInput.MessageXML.AddFail("ComputeBloodlineSkillMods", "Set Up KnowledgeAnyOne");

            bool found = false;

            if (_sbCheckerBaseInput.MonsterSBSearch.HasBloodline(BloodlineName))
            {
                found = true;
                if (Info.Skill.SkillName == BloodlineClassSkill)
                {
                    ClassSkill = 1;
                }
                return;
            }

            if (!found) _sbCheckerBaseInput.MessageXML.AddFail("ComputeBloodlineSkillMods", "Missing bloodline reprsetnation for " + _sbCheckerBaseInput.MonsterSB.Bloodline);
        }

        public int ComputeRaceSkillMod(string SkillName, ref string formula, ref int KnowledgeOne)
        {
            int Mod = 0;

            Dictionary<string, int> Mods = _sbCheckerBaseInput.Race_Base.RacialSkillMods();

            if (Mods.ContainsKey(SkillName) && !formula.Contains("racial mod"))
            {
                Mod = Mods[SkillName];
                formula += " +" + Mod.ToString() + " Race Mod";
            }
            if (Mod == 0 && Mods.ContainsKey(StatBlockInfo.SkillNames.KNOWLEDGE_ONE) && KnowledgeOne == 0 && SkillName.Contains("Knowledge"))
            {
                KnowledgeOne = -1;
                Mod = Mods[StatBlockInfo.SkillNames.KNOWLEDGE_ONE];
                formula += " +" + Mod.ToString() + " Race Mod";
            }

            return Mod;
        }

        public void GetFamilarMod(string SkillName, SkillsInfo.SkillInfo Info, ref int tempMod, ref string formula)
        {
            string FamiliarString = _sbCheckerBaseInput.MonsterSBSearch.FindFamiliarString(_sbCheckerBaseInput.CharacterClasses.HasClass("witch"));
            switch (FamiliarString)
            {
                case "bat":
                    if (Info.Skill.SkillName == SkillData.SkillNames.Fly)
                    {
                        tempMod += 3;
                        formula += " +3 bat familiar";
                    }
                    break;
                case "beheaded": //improved familiar
                    break;
                case "cacodaemon":  //improved familiar
                    break;
                case "cat":
                    if (Info.Skill.SkillName == SkillData.SkillNames.Stealth)
                    {
                        tempMod += 3;
                        formula += " +3 cat familiar";
                    }
                    break;
                case "centipede":
                    if (Info.Skill.SkillName == SkillData.SkillNames.Stealth)
                    {
                        tempMod += 3;
                        formula += " +3 centipede familiar";
                    }
                    break;
                case "compsognathus":
                    //+4 intit mod
                    break;
                case "crab":
                    //+2 bonus on CMB checks to start and maintain a grapple
                    break;
                case "fox":
                    //+2 reflex saves
                    break;
                case "goat":
                    if (Info.Skill.SkillName == SkillData.SkillNames.Survival)
                    {
                        tempMod += 3;
                        formula += " +3 goat familiar";
                    }
                    break;
                case "lizard":
                    if (Info.Skill.SkillName == SkillData.SkillNames.Climb)
                    {
                        tempMod += 3;
                        formula += " +3 lizard familiar";
                    }
                    break;
                case "monkey":
                    if (Info.Skill.SkillName == SkillData.SkillNames.Acrobatics)
                    {
                        tempMod += 3;
                        formula += " +3 monkey familiar";
                    }
                    break;
                case "owl":
                    break;
                case "parrot":
                    if (Info.Skill.SkillName == SkillData.SkillNames.Linguistics)
                    {
                        tempMod += 3;
                        formula += " +3 parrot familiar";
                    }
                    break;
                case "pig":
                    if (Info.Skill.SkillName == SkillData.SkillNames.Diplomacy)
                    {
                        tempMod += 3;
                        formula += " +3 pig familiar";
                    }
                    break;
                case "quasit": //improved familiar
                    break;
                case "rat":
                    //+2 fortitude, not skill
                    break;
                case "raven":
                    if (Info.Skill.SkillName == SkillData.SkillNames.Appraise)
                    {
                        tempMod += 3;
                        formula += " +3 raven familiar";
                    }
                    break;
                case "scorpion":
                    //+4 init, not skill
                    break;
                case "snake":
                    //not defined
                    break;
                case "spider":

                    break;
                case "toad": //+3 hp
                    break;
                case "viper":
                    if (Info.Skill.SkillName == SkillData.SkillNames.Bluff)
                    {
                        tempMod += 3;
                        formula += " +3 viper familiar";
                    }
                    break;
                case "weasel":
                    //relfex bonus
                    break;
                case "wren":
                    break;
                default:
                    if (!_sbCheckerBaseInput.MessageXML.MessageExists("No Familiar Info for " + FamiliarString))
                    {
                        _sbCheckerBaseInput.MessageXML.AddFail("Familiar", "No Familiar Info for " + FamiliarString);
                    }
                    break;
            }
        }

        public int FindExtraSkillsMods(string SkillName, SkillsInfo.SkillInfo Info, ref string formula, int StrMod)
        {
            int tempMod = 0;
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Acrobatic"))
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.Acrobatics || Info.Skill.SkillName == SkillData.SkillNames.Fly)
                {
                    tempMod += 2;
                    formula += " +2 Acrobatics";
                }
            }
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Shingle Runner"))
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.Acrobatics || Info.Skill.SkillName == SkillData.SkillNames.Climb)
                {
                    tempMod += 2;
                    formula += " +2 Shingle Runner";
                }
            }
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Alertness"))
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.Perception || Info.Skill.SkillName == SkillData.SkillNames.SenseMotive)
                {
                    tempMod += 2;
                    formula += " +2 Alertness";
                    if (_sbCheckerBaseInput.MonsterSBSearch.IsMythic && _sbCheckerBaseInput.MonsterSBSearch.HasMythicFeat("Alertness"))
                    {
                        tempMod += 2;
                        formula += " +2 Mythic Alertness";
                    }
                }
            }
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Animal Affinity"))
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.HandleAnimal || Info.Skill.SkillName == SkillData.SkillNames.Ride)
                {
                    tempMod += 2;
                    formula += " +2 Animal Affinity";
                }
            }
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Athletic"))
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.Climb || Info.Skill.SkillName == SkillData.SkillNames.Swim)
                {
                    tempMod += 2;
                    formula += " +2 Athletic";
                }
            }
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Deceitful"))
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.Bluff || Info.Skill.SkillName == SkillData.SkillNames.Disguise)
                {
                    tempMod += 2;
                    formula += " +2 Deceitful";
                }
            }
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Deft Hands"))
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.DisableDevice || Info.Skill.SkillName == SkillData.SkillNames.SleightofHand)
                {
                    tempMod += 2;
                    formula += " +2 Deft Hands";
                }
            }
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Persuasive"))
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.Diplomacy || Info.Skill.SkillName == SkillData.SkillNames.Intimidate)
                {
                    tempMod += 2;
                    formula += " +2 Persuasive";
                    if (_sbCheckerBaseInput.MonsterSBSearch.IsMythic && _sbCheckerBaseInput.MonsterSBSearch.HasMythicFeat("Persuasive"))
                    {
                        tempMod += 2;
                        formula += " +2 Mythic Persuasive";
                    }
                }
            }


            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Self-Sufficient"))
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.Heal || Info.Skill.SkillName == SkillData.SkillNames.Survival)
                {
                    tempMod += 2;
                    formula += " +2 Self-Sufficient";
                }
            }

            if (HasSkillFocus(Info.FullName()))
            {
                tempMod += 3; //min value if more than 10 ranks mod is more
                formula += " +3 SkillFocus";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Stealthy"))
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.EscapeArtist || Info.Skill.SkillName == SkillData.SkillNames.Stealth)
                {
                    tempMod += 2;
                    formula += " +2 Stealthy";
                }
            }
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Magical Aptitude"))
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.Spellcraft || Info.Skill.SkillName == SkillData.SkillNames.UseMagicDevice)
                {
                    tempMod += 2;
                    formula += " +2 Magical Aptitude";
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Sea Legs"))
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.Acrobatics || Info.Skill.SkillName == SkillData.SkillNames.Climb || Info.Skill.SkillName == SkillData.SkillNames.Swim)
                {
                    tempMod += 2;
                    formula += " +2 Sea Legs";
                }
            }

            //if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Run"))
            //{
            //    if (Info.Skill.SkillName == SkillData.SkillNames.Acrobatics)
            //    {
            //     //   tempMod += 4; jump olny
            //    }
            //}

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Jackal Heritage"))
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.Perception)
                {
                    tempMod += 2;
                    formula += " +2 Jackal Heritage";
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Derro Magister"))
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.Heal)
                {
                    tempMod += 4;
                    formula += " +4 Derro Magister";
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Intimidating Prowess"))
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.Intimidate)
                {
                    tempMod += StrMod;
                    formula += " +" + StrMod.ToString() + " Intimidating Prowess";
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Bag of Bones"))
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.EscapeArtist)
                {
                    tempMod += 5;
                    formula += " +5 Bag of Bones";
                }
            }

            if (Info.Skill.SkillName == SkillData.SkillNames.Stealth)
            {
                Armor armor = null;
                foreach (KeyValuePair<IEquipment, int> kvp in _equipmentData.Armor)
                {
                    if (kvp.Key is Armor)
                    {
                        armor = (Armor)kvp.Key;
                        ArmorSpecialAbilitiesEnum SA = armor.ArmorSpecialAbilities.ArmorSpecialAbilityValues;
                        if ((SA & ArmorSpecialAbilitiesEnum.Shadow) == ArmorSpecialAbilitiesEnum.Shadow)
                        {
                            tempMod += 5;
                            formula += " +5 shadow";
                        }
                    }
                }
            }

            if (Info.Skill.SkillName == SkillData.SkillNames.Bluff && _sbCheckerBaseInput.MonsterSBSearch.HasTrait("Canter"))
            {
                tempMod += 5;
                formula += " +5 Canter";
            }

            if ((Info.Skill.SkillName == SkillData.SkillNames.KnowledgeLocal || Info.Skill.SkillName == SkillData.SkillNames.KnowledgeNobility) && _sbCheckerBaseInput.MonsterSBSearch.HasTrait("Civilized"))
            {
                tempMod += 1;
                formula += " +1 Civilized";
            }

            if (Info.Skill.SkillName == SkillData.SkillNames.KnowledgeNobility && _sbCheckerBaseInput.MonsterSBSearch.HasTrait("child of the temple"))
            {
                tempMod += 1;
                formula += " +1 child of the temple";
            }

            if (Info.Skill.SkillName == SkillData.SkillNames.KnowledgeReligion && _sbCheckerBaseInput.MonsterSBSearch.HasTrait("child of the temple"))
            {
                tempMod += 1;
                formula += " +1 child of the temple";
            }

            if (Info.Skill.SkillName == SkillData.SkillNames.UseMagicDevice && _sbCheckerBaseInput.MonsterSBSearch.HasTrait("Dangerously Curious"))
            {
                tempMod += 1;
                formula += " +1 Dangerously Curious";
            }


            if (Info.Skill.SkillName == SkillData.SkillNames.Diplomacy && _sbCheckerBaseInput.MonsterSBSearch.HasTrait("trustworthy"))
            {
                tempMod += 1;
                formula += " +1 trustworthy";
            }

            if (Info.Skill.SkillName == SkillData.SkillNames.Climb && _sbCheckerBaseInput.MonsterSBSearch.HasGear("climber's kit"))
            {
                tempMod += 2;
                formula += " +2 climber's kit";
            }

            if (Info.Skill.SkillName == SkillData.SkillNames.Disguise && _sbCheckerBaseInput.MonsterSBSearch.HasGear("disguise kit"))
            {
                tempMod += 2;
                formula += " +2 disguise kit";
            }

            //has "charges" get bonus only when charge is used
            //if (Info.Skill.SkillName == SkillData.SkillNames.Heal && _sbCheckerBaseInput.MonsterSBSearch.HasGear("healer's kit"))
            //{
            //    tempMod += 2;
            //    formula += " +2 healer's kit";
            //}

            if (Info.Skill.SkillName == SkillData.SkillNames.Craft && (_sbCheckerBaseInput.MonsterSBSearch.HasGear("masterwork artisan's tools") || _sbCheckerBaseInput.MonsterSBSearch.HasGear("mwk artisan's tools")))
            {
                tempMod += 2;
                formula += " +2 masterwork artisan’s tools";
            }

            if (Info.Skill.SkillName == SkillData.SkillNames.DisableDevice && !_sbCheckerBaseInput.MonsterSBSearch.IsBestirarySB)
            {
                tempMod -= 2; //no kit
                if (_sbCheckerBaseInput.MonsterSBSearch.HasGear("thieves' tools") && !_sbCheckerBaseInput.MonsterSBSearch.HasGear("masterwork thieves' tools") && !_sbCheckerBaseInput.MonsterSBSearch.HasGear("mwk thieves' tools"))
                {
                    tempMod += 2;
                    formula += " +0 thieves' tools";
                }
                else if (_sbCheckerBaseInput.MonsterSBSearch.HasGear("masterwork thieves' tools") || _sbCheckerBaseInput.MonsterSBSearch.HasGear("mwk thieves' tools"))
                {
                    tempMod += 4;
                    formula += " +2 masterwork thieves' tools";
                }
                else
                    formula += " -2 no thieves' tools";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasHex("cauldron"))
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.Craft && Info.SubValue == "alchemy")
                {
                    tempMod += 4;
                    formula += " +4 cauldron hex";
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasSQ("enchanting smile"))
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.Bluff || Info.Skill.SkillName == SkillData.SkillNames.Diplomacy || Info.Skill.SkillName == SkillData.SkillNames.Intimidate)
                {
                    int mod = 2;
                    int wizardLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("wizard");
                    if (wizardLevel >= 5) mod++;
                    if (wizardLevel >= 10) mod++;
                    if (wizardLevel >= 15) mod++;
                    if (wizardLevel >= 20) mod++;
                    tempMod += mod;
                    formula += " +" + mod.ToString() + " enchanting smile";
                }
            }

            if (Info.Skill.SkillName == SkillData.SkillNames.Perform)
            {
                List<string> PerformValues = CommonMethods.GetPerformValues(Info.SubValue);
                foreach (string perform in PerformValues)
                {
                    if (_sbCheckerBaseInput.MonsterSBSearch.HasGear("masterwork " + perform) || _sbCheckerBaseInput.MonsterSBSearch.HasGear("mwk " + perform) || _sbCheckerBaseInput.MonsterSBSearch.HasGear("mwk musical instrument (" + perform))
                    {
                        tempMod += 2;
                        formula += " +2 masterwork " + perform;
                        break;
                    }
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFamiliar())
                GetFamilarMod(SkillName, Info, ref tempMod, ref formula);

            //if (_sbCheckerBaseInput.Race_Base.RaceBaseType != RaceBase.RaceType.Race)
            //{
            int racialMod = GetRacialModValue(SkillName, Info.SubValue);
            tempMod += racialMod;
            if (racialMod != 0)
                formula += " +" + racialMod.ToString() + " racial mod";
            //}

            return tempMod;
        }

        public bool HasSkillFocus(string SkillName)
        {
            if (!_sbCheckerBaseInput.MonsterSB.Feats.Contains("Skill Focus")) return false;

            if (SkillName.Contains(PathfinderConstants.PAREN_LEFT))
            {
                SkillName = SkillName.Replace(PathfinderConstants.PAREN_LEFT, "[");
                SkillName = SkillName.Replace(PathfinderConstants.PAREN_RIGHT, "]");
            }
            string hold = _sbCheckerBaseInput.MonsterSB.Feats;
            Utility.ParenCommaFix(ref hold);
            List<string> Skills = hold.Split(',').ToList();
            foreach (string skill in Skills)
            {
                if (skill.Contains("Skill Focus"))
                {
                    string temp = skill.Replace("Skill Focus", string.Empty).Trim();
                    temp = temp.Replace(PathfinderConstants.PAREN_LEFT, string.Empty);
                    temp = temp.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Trim().ToLower();
                    //if (SkillName.IndexOf("[") == -1)
                    //{
                    //    temp = temp.Replace("[", PathfinderConstants.PAREN_LEFT);
                    //    temp = temp.Replace("]", PathfinderConstants.PAREN_RIGHT);
                    //}
                    if (temp.Contains(SkillName.ToLower())) return true;
                }
            }

            return false;
        }

        public int GetRacialModValue(string RacialModName, string SubValue)
        {
            if (HasRacialMod(RacialModName))
            {
                string temp = _sbCheckerBaseInput.MonsterSB.RacialMods;
                int Pos = temp.IndexOf(";");
                if (Pos != -1)
                    temp = temp.Substring(0, Pos).Trim();

                HashSet<string> Mods = new HashSet<string>(temp.Split(','));

                List<string> Mods2 = null;

                if (_sbCheckerBaseInput.Race_Base.RaceSB != null)
                    Mods2 = (_sbCheckerBaseInput.Race_Base.RaceSB.RacialMods.Split(',').ToList<string>());
                else
                    Mods2 = _sbCheckerBaseInput.MonsterSB.RacialMods.Split(',').ToList();


                foreach (string one in Mods2)
                {
                    Mods.Add(one);
                }
                Mods.Remove(string.Empty);

                foreach (string mod in Mods)
                {
                    if (mod.Contains(RacialModName))
                    {
                        if (!(SubValue.Length >= 0 && mod.Contains(SubValue)))  continue;
                        string temp2 = mod.Replace(RacialModName, string.Empty).Trim();

                        if (temp2.IndexOf(PathfinderConstants.PAREN_LEFT) > 0) temp2 = temp2.Substring(0, temp2.IndexOf(PathfinderConstants.PAREN_LEFT)).Trim();
                        if (temp2.IndexOf(PathfinderConstants.SPACE) > 0) temp2 = "0";
                        return Convert.ToInt32(temp2);
                    }
                }
            }

            return 0;
        }

        public bool HasRacialMod(string RacialModName)
        {
            if (_racialMods.Length == 0) return false;

            if (_sbCheckerBaseInput.MonsterSB.RacialMods.Contains(RacialModName))
                return true;

            if (_sbCheckerBaseInput.Race_Base.RaceSB != null)
            {
                if (_sbCheckerBaseInput.Race_Base.RaceSB.RacialMods.Contains(RacialModName))
                    return true;
            }

            return false;
        }

        private bool HasSpell(string SpellName)
        {
            if (!_spellsData.ClassSpells.Any()) return false;

            foreach (KeyValuePair<string, SpellList> kvp in _spellsData.ClassSpells)
            {
                SpellList SL = kvp.Value;
                if (SL.SpellExists(SpellName))
                {
                    return true;
                }
            }

            return false;
        }

        private int GetSLACasterLevel(string SpellName)
        {
            int CL = -1;
            foreach (KeyValuePair<string, SpellList> kvp in _spellsData.SLA)
            {
                SpellList SL = kvp.Value;
                if (SL.SpellExists(SpellName))
                {
                    return SL.CasterLevel;
                }
            }

            return CL;
        }

        private int GetSpellCasterLevel(string SpellName)
        {
            int CL = -1;
            foreach (KeyValuePair<string, SpellList> kvp in _spellsData.ClassSpells)
            {
                SpellList SL = kvp.Value;
                if (SL.SpellExists(SpellName))
                {
                    return SL.CasterLevel;
                }
            }

            return CL;
        }

        public int ComputeSkillMod(string SkillName, StatBlockInfo.AbilityName Ability, string SubValue, ref string formula, SkillsInfo.SkillInfo Info)
        {
            int Mod = 0;
            int temp2 = 0;
            switch (Info.Skill.SkillName)
            {
                case SkillData.SkillNames.Fly:
                    Mod += ComputeFlySizeMod(ref formula) + ComputeFlyManeuverabilityMod(ref formula);
                    if (!_sbCheckerBaseInput.MonsterSB.Speed.Contains("fly"))
                    {
                        if (HasSpell("fly"))
                        {
                            temp2 = GetSpellCasterLevel("fly");
                            Mod += temp2 / 2;
                            formula += " + " + (temp2 / 2).ToString() + " fly spell(CL /2)";
                        }
                    }

                    if (_sbCheckerBaseInput.MonsterSBSearch.HasHex("flight"))
                    {
                        int WitchLevels = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Witch");
                        if (WitchLevels >= 5)
                        {
                            temp2 = WitchLevels;
                            Mod += temp2 / 2;
                            formula += " + " + (temp2 / 2).ToString() + " Hex fly spell(CL /2)";
                        }
                    }

                    if (_sbCheckerBaseInput.MonsterSB.SpellLikeAbilities.Contains("fly"))
                    {
                        temp2 = GetSLACasterLevel("fly");
                        Mod += temp2 / 2;
                        formula += " + " + (temp2 / 2).ToString() + " fly spell(CL /2)";
                    }
                    break;
                case SkillData.SkillNames.Stealth:
                    Mod += ComputeStealthSizeMod(ref formula);
                    break;
                case SkillData.SkillNames.Swim:
                    Mod += ComputeSwimMod(ref formula);
                    break;
                case SkillData.SkillNames.Climb:
                    Mod += ComputeClimbMod(ref formula);
                    break;
            }

            if (_sbCheckerBaseInput.IndvSB != null)
            {
                Mod += _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockSkillModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.Skill, SkillName, ref formula);
                if (Ability == StatBlockInfo.AbilityName.Charisma)
                {
                    Mod += _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockSkillAbilityModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.Skill, OnGoingStatBlockModifier.StatBlockModifierSubTypes.Skill_Ability_Cha, ref formula);
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.Race() == "samsaran")
            {
                string sop = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("shards of the past");
                if (sop.Contains(SkillName))
                {
                    Mod += 2;
                    formula += " +2 shards of the past- " + SkillName;
                }
            }

            foreach (string className in _sbCheckerBaseInput.CharacterClasses.GetClassNames())
            {
                switch (className.ToLower())
                {
                    case "barbarian":
                        if ((Info.Skill.SkillName == SkillData.SkillNames.Swim) && _sbCheckerBaseInput.MonsterSBSearch.HasRagePower("raging swimmer"))
                        {
                            int Mod2 = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("barbarian");
                            Mod += Mod2;
                            formula += " +" + Mod2.ToString() + " raging swimmer";
                        }
                        break;
                    case "bard":
                        if (_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("sea singer"))
                        {
                            if ((Info.Skill.SkillName == SkillData.SkillNames.KnowledgeGeography || Info.Skill.SkillName == SkillData.SkillNames.KnowledgeLocal ||
                                    Info.Skill.SkillName == SkillData.SkillNames.KnowledgeNature || Info.Skill.SkillName == SkillData.SkillNames.Linguistics))
                            {
                                int Mod2 = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Bard") / 2;
                                Mod += Mod2 == 0 ? 1 : Mod2;
                                formula += " + (" + _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Bard").ToString() + " /2 = " + Mod2.ToString() + ") World Traveler(Level /2)";
                            }
                        }
                        else if (_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("court bard"))
                        {
                            if ((Info.Skill.SkillName == SkillData.SkillNames.Diplomacy || Info.Skill.SkillName == SkillData.SkillNames.KnowledgeLocal ||
                                   Info.Skill.SkillName == SkillData.SkillNames.KnowledgeHistory || Info.Skill.SkillName == SkillData.SkillNames.KnowledgeNobility))
                            {
                                int Mod2 = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Bard") / 2;
                                Mod += Mod2 == 0 ? 1 : Mod2;
                                formula += " + (" + _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Bard").ToString() + " /2 = " + Mod2.ToString() + ") Heraldic Expertise(Level /2)";
                            }
                        }
                        else
                        {
                            if (Info.Skill.IsKnowledge)
                            {
                                int Mod2 = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Bard") / 2;
                                Mod += Mod2 == 0 ? 1 : Mod2;
                                formula += " + (" + _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Bard").ToString() + " /2 = " + Mod2.ToString() + ") Bardic Knowledge(Level /2)";
                            }
                        }
                        break;
                    case "loremaster":
                        if (Info.Skill.IsKnowledge)
                        {
                            int Mod2 = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Loremaster") / 2;
                            Mod += Mod2 == 0 ? 1 : Mod2;
                            formula += " + (" + _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Loremaster").ToString() + " /2 = " + Mod2.ToString() + ") Loremater Knowledge(Level /2)";
                        }
                        break;
                    case "master spy":
                        if ((Info.Skill.SkillName == SkillData.SkillNames.Bluff || Info.Skill.SkillName == SkillData.SkillNames.Disguise || Info.Skill.SkillName == SkillData.SkillNames.SenseMotive))
                        {
                            Mod += _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Master Spy");
                            formula += " + " + _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Master Spy").ToString() + " + Master Spy";
                        }
                        break;
                    case "inquisitor":
                        if (_sbCheckerBaseInput.CharacterClasses.HasClass("inquisitor") && (Info.Skill.SkillName == SkillData.SkillNames.Intimidate || Info.Skill.SkillName == SkillData.SkillNames.SenseMotive))
                        {
                            //Stern Gaze (Ex)
                            int Mod2 = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Inquisitor") / 2;
                            Mod += Mod2 == 0 ? 1 : Mod2;

                            formula += " + (" + _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Inquisitor").ToString() + "/2 = " + Mod.ToString() + " )  Inquisitor (Level /2)";
                        }
                        if (_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("infiltrator") && (Info.Skill.SkillName == SkillData.SkillNames.Bluff || Info.Skill.SkillName == SkillData.SkillNames.Diplomacy))
                        {
                            int tempMod = _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Wisdom);
                            Mod += tempMod;
                            formula += " + " + tempMod.ToString() + " Guileful Lore";
                        }
                        break;
                    case "mesmerist":
                        if (Info.Skill.SkillName == SkillData.SkillNames.Bluff) //perception only for find traps
                        {
                            int Mod2 = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("mesmerist") / 2;
                            Mod2 = Mod2 == 0 ? 1 : Mod2;
                            Mod += Mod2;
                            formula += " +" + Mod2.ToString() + " ( bluff (Level /2)=" + _sbCheckerBaseInput.CharacterClasses.FindClassLevel("mesmerist").ToString() + "/2)";
                        }
                        break;
                    case "ninja":
                        if (Info.Skill.SkillName == SkillData.SkillNames.Disguise)
                        {
                            int Mod2 = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("ninja") / 3;
                            Mod += Mod2;
                            formula += " +" + Mod2.ToString() + " ( Disguise (Level /3)=" + _sbCheckerBaseInput.CharacterClasses.FindClassLevel("ninja").ToString() + "/3)";
                        }
                        break;
                    case "pathfinder chronicler":
                        if (Info.Skill.IsKnowledge)
                        {
                            int Mod2 = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Pathfinder Chronicler") / 2;
                            Mod += Mod2 == 0 ? 1 : Mod2;
                            formula += " + (" + _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Pathfinder Chronicler").ToString() + " /2 = " + Mod2.ToString() + ")  Pathfinder Chronicler(Level /2)";
                        }
                        if ((Info.Skill.SkillName == SkillData.SkillNames.Linguistics || (Info.Skill.SkillName == SkillData.SkillNames.Profession && SubValue == "scribe")))
                        {
                            Mod += _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Pathfinder Chronicler");
                            formula += " +" + Mod.ToString() + " Pathfinder Chronicler";
                        }
                        break;
                    case "rogue":
                        if (Info.Skill.SkillName == SkillData.SkillNames.DisableDevice && !_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("acrobat")) //perception only for find traps
                        {
                            int Mod2 = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Rogue") / 2;
                            Mod2 = Mod2 == 0 ? 1 : Mod2;
                            Mod += Mod2;
                            formula += " +" + Mod2.ToString() + " ( trapfinding (Level /2)=" + _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Rogue").ToString() + "/2)";
                        }
                        if ((Info.Skill.SkillName == SkillData.SkillNames.Bluff || Info.Skill.SkillName == SkillData.SkillNames.Diplomacy) && _sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("rake")) //perception only for find traps
                        {
                            int Mod2 = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Rogue") / 3;
                            Mod2 = Mod2 == 0 ? 1 : Mod2;
                            Mod += Mod2;
                            formula += " +" + Mod2.ToString() + " ( Rake’s Smile (Level /3)=" + _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Rogue").ToString() + "/3)";
                        }
                        if ((Info.Skill.SkillName == SkillData.SkillNames.Bluff || Info.Skill.SkillName == SkillData.SkillNames.SleightofHand) && _sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("Sharper")) //
                        {
                            int Mod2 = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Rogue") / 2;
                            Mod2 = Mod2 == 0 ? 1 : Mod2;
                            Mod += Mod2;
                            formula += " +" + Mod2.ToString() + " ( Scam Artist (Level /2)=" + _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Rogue").ToString() + "/2)";
                        }
                        break;
                    case "skald":
                        if (Info.Skill.IsKnowledge)
                        {
                            int Mod2 = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Skald") / 2;
                            Mod += Mod2 == 0 ? 1 : Mod2;
                            formula += " + (" + _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Skald").ToString() + " /2 = " + Mod2.ToString() + ") Bardic Knowledge(Level /2)";
                        }
                        break;
                    case "shieldmarshal":
                        if ((Info.Skill.SkillName == SkillData.SkillNames.Perception || Info.Skill.SkillName == SkillData.SkillNames.SenseMotive))
                        {
                            int intMod = _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Intelligence);
                            Mod += intMod;
                            formula += " +" + intMod.ToString() + " Eye for Detail";
                        }
                        break;
                    case "sleepless detective":
                        if ((Info.Skill.SkillName == SkillData.SkillNames.Perception || Info.Skill.SkillName == SkillData.SkillNames.SenseMotive))
                        {
                            int intMod = _sbCheckerBaseInput.MonsterSBSearch.GetAbilityMod(AbilityScores.AbilityScores.AbilityName.Intelligence);
                            Mod += intMod;
                            formula += " +" + intMod.ToString() + " Canny Sleuth";
                        }
                        break;
                    case "druid":
                        if ((Info.Skill.SkillName == SkillData.SkillNames.KnowledgeNature || Info.Skill.SkillName == SkillData.SkillNames.Survival))
                        {
                            Mod += 2;
                            formula += " +2 Druid";
                        }
                        break;
                    case "hellknight signifer":
                        if ((Info.Skill.SkillName == SkillData.SkillNames.SenseMotive))
                        {
                            Mod += 2;
                            formula += " +2 Hellknight Signifer";
                        }
                        break;
                    case "umbral court agent":
                        if ((Info.Skill.SkillName == SkillData.SkillNames.Bluff || Info.Skill.SkillName == SkillData.SkillNames.Diplomacy || Info.Skill.SkillName == SkillData.SkillNames.KnowledgeNobility))
                        {
                            int modUCA = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("umbral court agent");
                            Mod += modUCA;
                            formula += " +" + modUCA.ToString() + " Umbral Courtier";
                        }
                        break;
                    case "warpriest":
                        if (_sbCheckerBaseInput.MonsterSBSearch.HasArchetype("cult leader"))
                        {
                            if (Info.Skill.SkillName == SkillData.SkillNames.Disguise || Info.Skill.SkillName == SkillData.SkillNames.Stealth)
                            {
                                Mod += 2;
                                formula += " +2 cultleader";
                            }
                        }
                        break;
                    case "storm kindler":
                        if (_sbCheckerBaseInput.CharacterClasses.HasClass("storm kindler") && (Info.Skill.SkillName == SkillData.SkillNames.Fly || Info.Skill.SkillName == SkillData.SkillNames.Swim))
                        {
                            //Oceanic Spirit (Su)
                            int modstormKindler = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("storm kindler");
                            Mod += modstormKindler;
                            formula += " +" + modstormKindler.ToString() + " storm kindler";
                        }
                        break;
                    case "medium":
                        if (Info.Skill.Ability == StatBlockInfo.AbilityName.Intelligence)
                        {
                            string spiritSB = _sbCheckerBaseInput.MonsterSBSearch.GetSQ("spirit bonus").Replace("spirit bonus", string.Empty).Trim();
                            int spiritSBValue = 0;
                            if (!string.IsNullOrEmpty(spiritSB))
                            {
                                spiritSB = spiritSB.Replace("*", string.Empty);
                                spiritSBValue = int.Parse(spiritSB);
                                formula += " +" + spiritSBValue.ToString() + " spirit bonus";
                                if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Spirit Focus"))
                                {
                                    spiritSBValue++;
                                    formula += " +1 Spirit Focus";
                                }
                                Mod += spiritSBValue;
                            }
                        }
                        break;
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasAnyClassArchetypes())
            {
                if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("urban ranger"))
                {
                    int rangerLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("ranger");
                    if (rangerLevel >= 3)
                    {
                        if (Info.Skill.SkillName == SkillData.SkillNames.Perception || Info.Skill.SkillName == SkillData.SkillNames.KnowledgeLocal || Info.Skill.SkillName == SkillData.SkillNames.Stealth || Info.Skill.SkillName == SkillData.SkillNames.Survival)
                        {
                            Mod += 2;
                            formula += " +2 Urban Ranger";
                        }
                    }
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("archer"))
                {
                    if (Info.Skill.SkillName == SkillData.SkillNames.Perception)
                    {
                        int fighterLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("fighter");
                        int tempMod = 0;
                        if (fighterLevel >= 2) tempMod++;
                        if (fighterLevel >= 6) tempMod++;
                        if (fighterLevel >= 10) tempMod++;
                        if (fighterLevel >= 14) tempMod++;
                        if (fighterLevel >= 18) tempMod++;
                        Mod += tempMod;
                        formula += " +" + tempMod.ToString() + " archer";
                    }
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("daredevil"))
                {
                    if (Info.Skill.SkillName == SkillData.SkillNames.Acrobatics || Info.Skill.SkillName == SkillData.SkillNames.Bluff || Info.Skill.SkillName == SkillData.SkillNames.Climb || Info.Skill.SkillName == SkillData.SkillNames.EscapeArtist)
                    {
                        int Mod2 = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Bard") / 2;
                        Mod += Mod2 == 0 ? 1 : Mod2;
                        formula += " + " + _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Bard").ToString() + "/2 + Bard";
                    }
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("smuggler"))
                {
                    if (Info.Skill.SkillName == SkillData.SkillNames.SleightofHand)
                    {
                        int Mod2 = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("rogue") / 2;
                        Mod += Mod2 == 0 ? 1 : Mod2;
                        formula += " + " + _sbCheckerBaseInput.CharacterClasses.FindClassLevel("rogue").ToString() + "/2 + rogue";
                    }
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("ancient guardian"))
                {
                    if (Info.Skill.SkillName == SkillData.SkillNames.Diplomacy || Info.Skill.SkillName == SkillData.SkillNames.SenseMotive
                        || (Info.Skill.SkillName == SkillData.SkillNames.Perform && SubValue == "oratory"))
                    {
                        int Mod2 = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("druid") / 2;
                        Mod += Mod2 == 0 ? 1 : Mod2;
                        formula += " + " + _sbCheckerBaseInput.CharacterClasses.FindClassLevel("druid").ToString() + "/2 + druid";
                    }
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("acrobat") && !_sbCheckerBaseInput.MonsterSBSearch.HasArmor())
                {
                    if (Info.Skill.SkillName == SkillData.SkillNames.Acrobatics || Info.Skill.SkillName == SkillData.SkillNames.Fly)
                    {
                        Mod += 2;
                        formula += " +2 acrobat";
                    }
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("swashbuckler"))
                {
                    if (Info.Skill.SkillName == SkillData.SkillNames.Acrobatics)
                    {
                        int rogueLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("Rogue");
                        int tempMod = 0;
                        if (rogueLevel >= 3) tempMod++;
                        if (rogueLevel >= 6) tempMod++;
                        if (rogueLevel >= 9) tempMod++;
                        if (rogueLevel >= 12) tempMod++;
                        if (rogueLevel >= 15) tempMod++;
                        if (rogueLevel >= 18) tempMod++;
                        Mod += tempMod;
                        formula += " +" + tempMod.ToString() + " swashbuckler";
                    }
                }

                if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("sea reaver"))
                {
                    if (Info.Skill.SkillName == SkillData.SkillNames.Acrobatics || Info.Skill.SkillName == SkillData.SkillNames.Climb
                          || (Info.Skill.SkillName == SkillData.SkillNames.Profession && SubValue == "sailor")
                          || Info.Skill.SkillName == SkillData.SkillNames.Survival
                         || Info.Skill.SkillName == SkillData.SkillNames.Swim)
                    {
                        int barLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("barbarian");
                        int tempMod = 0;
                        if (barLevel >= 3) tempMod++;
                        if (barLevel >= 6) tempMod++;
                        if (barLevel >= 9) tempMod++;
                        if (barLevel >= 12) tempMod++;
                        if (barLevel >= 15) tempMod++;
                        if (barLevel >= 18) tempMod++;
                        if (tempMod > 0)
                        {
                            Mod += tempMod;
                            formula += " +" + tempMod.ToString() + " sea reaver";
                        }
                    }
                }
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasClassArchetype("sczarni swindler"))//Poker Face (Ex)
            {
                if (Info.Skill.SkillName == SkillData.SkillNames.Bluff || (Info.Skill.SkillName == SkillData.SkillNames.Profession && SubValue == "gambler")
                         || Info.Skill.SkillName == SkillData.SkillNames.SenseMotive)
                {
                    int rogueLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("rogue");
                    int tempMod = 0;
                    if (rogueLevel >= 3) tempMod++;
                    if (rogueLevel >= 6) tempMod++;
                    if (rogueLevel >= 9) tempMod++;
                    if (rogueLevel >= 12) tempMod++;
                    if (rogueLevel >= 15) tempMod++;
                    if (rogueLevel >= 18) tempMod++;
                    if (tempMod > 0)
                    {
                        Mod += tempMod;
                        formula += " +" + tempMod.ToString() + " sczarni swindler";
                    }
                }
            }

            if (Info.Skill.IsKnowledge && _sbCheckerBaseInput.MonsterSBSearch.HasSQ("extension of all"))
            {
                int racialHD = _sbCheckerBaseInput.MonsterSB.RacialHD();
                int Mod2 = racialHD / 2;
                Mod += Mod2 == 0 ? 1 : Mod2;
                formula += " +" + Mod2.ToString() + " extension of all";
            }

            if (_sbCheckerBaseInput.MonsterSB.Bloodline.Contains("serpentine") && Info.Skill.SkillName == SkillData.SkillNames.EscapeArtist)
            {
                int SorcererLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("sorcerer");
                int temp = 0;
                formula += " +" + temp.ToString() + " serpentine bloodline";

                if (SorcererLevel >= 9) temp += 2;
                if (SorcererLevel >= 13) Mod += 1;
                if (SorcererLevel >= 17) Mod += 1;
                Mod += temp;
            }

            if (Info.Skill.SkillName == SkillData.SkillNames.Intimidate && _sbCheckerBaseInput.MonsterSBSearch.HasSQ("feral mutagen"))
            {

            }

            if (Info.Skill.SkillName == SkillData.SkillNames.Climb && _sbCheckerBaseInput.MonsterSBSearch.HasRagePower("raging climber"))
            {
                int barLevel = _sbCheckerBaseInput.CharacterClasses.FindClassLevel("barbarian");
                Mod += barLevel;
                formula += " +" + barLevel.ToString() + "raging climber";
            }

            return Mod;
        }

        public string GetSkillAbilityMod(ref int AbilityMod, Skill skill, string monsterSize)
        {
            StatBlockInfo.SizeCategories monSize = StatBlockInfo.GetSizeEnum(monsterSize);

            string AbilityUsed = skill.Ability.ToString().Substring(0, 3);
            int AbilityScore = 0;

            if ((skill.SkillName == SkillData.SkillNames.Climb || skill.SkillName == SkillData.SkillNames.Swim) &&
                (monSize <= StatBlockInfo.SizeCategories.Tiny))
            {
                AbilityScore = _sbCheckerBaseInput.MonsterSB.GetAbilityScoreValue(StatBlockInfo.AbilityName.Dexterity);
                AbilityUsed = StatBlockInfo.DEX;
                _sbCheckerBaseInput.MessageXML.AddInfo("Dex used for tiny or smaller in skill-" + skill.Name);
            }
            else if (_sbCheckerBaseInput.MonsterSB.SpellDomains.Contains("Heresy") && (skill.SkillName == SkillData.SkillNames.Bluff || skill.SkillName == SkillData.SkillNames.Intimidate))
            {
                AbilityScore = _sbCheckerBaseInput.MonsterSB.GetAbilityScoreValue(StatBlockInfo.AbilityName.Wisdom);
                AbilityUsed = StatBlockInfo.WIS;
            }
            else if (_sbCheckerBaseInput.MonsterSB.SpellDomains.Contains("Conversion") && (skill.SkillName == SkillData.SkillNames.Bluff || skill.SkillName == SkillData.SkillNames.Intimidate || skill.SkillName == SkillData.SkillNames.Diplomacy))
            {
                AbilityScore = _sbCheckerBaseInput.MonsterSB.GetAbilityScoreValue(StatBlockInfo.AbilityName.Wisdom);
                AbilityUsed = StatBlockInfo.WIS;
            }
            else
            {
                AbilityScore = _sbCheckerBaseInput.MonsterSB.GetAbilityScoreValue(skill.Ability);
            }

            OnGoingStatBlockModifier.StatBlockModifierSubTypes subType = Utility.GetOnGoingAbilitySubTypeFromString(AbilityUsed);
            AbilityScore += _sbCheckerBaseInput.IndvSB.GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes.Ability, subType);
            AbilityMod = StatBlockInfo.GetAbilityModifier(AbilityScore);

            return AbilityUsed;
        }

        public int MagicItemMods(string SkillName, StatBlockInfo.AbilityName abilityName, List<MagicItemAbilitiesWrapper> MagicItemAbilities, out string formula)
        {
            int Mod = 0;
            formula = string.Empty;
            foreach (MagicItemAbilitiesWrapper wrapper in MagicItemAbilities)
            {
                if (wrapper != null)
                {
                    foreach (OnGoing.IOnGoing SBMods in wrapper.OnGoingStatBlockModifiers)
                    {
                        if (SBMods.OnGoingType == OnGoingType.StatBlock)
                        {
                            OnGoingStatBlockModifier Mods = (OnGoingStatBlockModifier)SBMods;
                            if (Mods.ModType == OnGoingStatBlockModifier.StatBlockModifierTypes.Skill && Mods.SubType == OnGoingStatBlockModifier.StatBlockModifierSubTypes.Skill_Name && Mods.ConditionGroup == SkillName)
                            {
                                Mod += Mods.Modifier;
                                formula += Mods.Name;
                            }
                            if (Mods.ModType == OnGoingStatBlockModifier.StatBlockModifierTypes.Skill && Mods.SubType == OnGoingStatBlockModifier.StatBlockModifierSubTypes.None)
                            {
                                Mod += Mods.Modifier;
                                formula += Mods.Name;
                            }
                            if (abilityName == StatBlockInfo.AbilityName.Charisma)
                            {
                                if (Mods.ModType == OnGoingStatBlockModifier.StatBlockModifierTypes.Skill && Mods.SubType == OnGoingStatBlockModifier.StatBlockModifierSubTypes.Skill_Ability_Cha)
                                {
                                    Mod += Mods.Modifier;
                                    formula += Mods.Name;
                                }
                            }
                        }
                    }
                }
            }
            return Mod;
        }

        public bool Over10RanksBonus(SkillsInfo.SkillInfo Info, ref int Ranks, ref int ComputedRank, ref int ExtraMod, ref string formula, Skill skill)
        {
            bool Over10 = false;
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Magical Aptitude") && (skill.SkillName == SkillData.SkillNames.Spellcraft || skill.SkillName == SkillData.SkillNames.UseMagicDevice))
            {
                Over10 = true;
                ComputedRank -= 2;
                Ranks += 2;
                ExtraMod += 2;
                formula += " +2 Magical Aptitude 10 ranks";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Deceitful") && (skill.SkillName == SkillData.SkillNames.Bluff || skill.SkillName == SkillData.SkillNames.Disguise))
            {
                Over10 = true;
                ComputedRank -= 2;
                Ranks += 2;
                ExtraMod += 2;
                formula += " +2 Deceitful 10 ranks";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Stealthy") && (skill.SkillName == SkillData.SkillNames.EscapeArtist || skill.SkillName == SkillData.SkillNames.Stealth))
            {
                Over10 = true;
                ComputedRank -= 2;
                Ranks += 2;
                ExtraMod += 2;
                formula += " +2 Stealthy 10 ranks";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Alertness") && (skill.SkillName == SkillData.SkillNames.Perception || skill.SkillName == SkillData.SkillNames.SenseMotive))
            {
                Over10 = true;
                ComputedRank -= 2;
                Ranks += 2;
                ExtraMod += 2;
                formula += " +2 Alertness 10 ranks";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Athletic") && (skill.SkillName == SkillData.SkillNames.Climb || skill.SkillName == SkillData.SkillNames.Swim))
            {
                Over10 = true;
                ComputedRank -= 2;
                Ranks += 2;
                ExtraMod += 2;
                formula += " +2 Athletic 10 ranks";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Acrobatic") && (skill.SkillName == SkillData.SkillNames.Fly || skill.SkillName == SkillData.SkillNames.Acrobatics))
            {
                Over10 = true;
                ComputedRank -= 2;
                Ranks += 2;
                ExtraMod += 2;
                formula += " +2 Acrobatic 10 ranks";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Shingle Runner") && (skill.SkillName == SkillData.SkillNames.Acrobatics || skill.SkillName == SkillData.SkillNames.Acrobatics))
            {
                Over10 = true;
                ComputedRank -= 2;
                Ranks += 2;
                ExtraMod += 2;
                formula += " +2 Shingle Runner 10 ranks";
            }

            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Persuasive") && (skill.SkillName == SkillData.SkillNames.Diplomacy || skill.SkillName == SkillData.SkillNames.Intimidate))
            {
                Over10 = true;
                ComputedRank -= 2;
                Ranks += 2;
                ExtraMod += 2;
                formula += " +2 Persuasive 10 ranks";
            }
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Animal Affinity") && (skill.SkillName == SkillData.SkillNames.HandleAnimal || skill.SkillName == SkillData.SkillNames.Ride))
            {
                Over10 = true;
                ComputedRank -= 2;
                Ranks += 2;
                ExtraMod += 2;
                formula += " +2 Animal Affinity 10 ranks";
            }
            if (_sbCheckerBaseInput.MonsterSBSearch.HasFeat("Self-Sufficient") && (skill.SkillName == SkillData.SkillNames.Heal || skill.SkillName == SkillData.SkillNames.Survival))
            {
                Over10 = true;
                ComputedRank -= 2;
                Ranks += 2;
                ExtraMod += 2;
                formula += " +2 Self-Sufficient 10 ranks";
            }

            return Over10;
        }
    }
}
