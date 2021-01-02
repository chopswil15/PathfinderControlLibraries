using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using CommonStatBlockInfo;
using StatBlockCommon;
using ClassManager;
using FeatManager;
using PathfinderDomains;
using StatBlockCommon.Feat_SB;
using D_D_Common;

namespace StatBlockChecker
{
    public class FeatChecker
    {
        private StatBlockMessageWrapper _messageXML;
        private MonSBSearch _monSBSearch;
        private ClassMaster CharacterClasses;
        private bool HasEnvirmonment;
        private RaceBase Race_Base;
        private int IntAbilityScoreValue;
        private int HDValue;
        private List<FeatData> Feats;
        private int BAB;
        private int Int;
        private int Str;
        private int Cha;
        private int Con;
        private int Dex;
        private int Wis;
        private List<SkillCalculation> SkillCalculations;

        public FeatChecker(StatBlockMessageWrapper _messageXML, MonSBSearch _monSBSearch, ClassMaster CharacterClasses, bool HasEnvirmonment,
                   RaceBase Race_Base, int IntAbilityScoreValue, int HDValue, List<FeatData> Feats, int BAB, int Int, int Str, int Cha, int Con, int Dex, int Wis, List<SkillCalculation> SkillCalculations)
        {
            this._messageXML = _messageXML;
            this._monSBSearch = _monSBSearch;
            this.CharacterClasses = CharacterClasses;
            this.HasEnvirmonment = HasEnvirmonment;
            this.Race_Base = Race_Base;
            this.IntAbilityScoreValue = IntAbilityScoreValue;
            this.HDValue = HDValue;
            this.Feats = Feats;
            this.BAB = BAB;
            this.Int = Int;
            this.Str = Str;
            this.Dex = Dex;
            this.Con = Con;
            this.Cha = Cha;
            this.Wis = Wis;
            this.SkillCalculations = SkillCalculations;
        }
      

        public void CheckFeatCount(string Feats)
        {
            string CheckName = "Feat Count";

            string hold = Feats;
            Utility.ParenCommaFix(ref hold);
            List<string> MythicFeats = new List<string>();
            List<string> FeatsTemp = hold.Replace("*", string.Empty).Split(',').ToList<string>();
            FeatsTemp.RemoveAll(p => p == string.Empty);
            List<string> NewFeats = new List<string>();

            FeatsTemp = RemoveSuperScripts(FeatsTemp); //not mythic M
            FeatsTemp.RemoveAll(p => p == " ");

            int Count = FeatsTemp.Count - 1;
            for (int a = Count; a >= 0; a--)
            {
                string feat = FeatsTemp[a];
                if (feat.Contains("- ")) _messageXML.AddFail("Feat Spelling", "hyphen space in feat name");
                if (feat.Contains("("))
                {
                    int Pos = feat.IndexOf("(");
                    string temp3 = feat.Substring(Pos);
                    temp3 = temp3.Replace("(", string.Empty);
                    temp3 = temp3.Replace(")", string.Empty);
                    int count;
                    int.TryParse(temp3, out count);
                    if (count > 0)
                        NewFeats.Add(feat);
                    else
                    {
                        if(temp3.Contains("|"))
                        {
                            List<string> MultipleFeats = temp3.Split('|').ToList<string>();
                            
                            Pos = feat.IndexOf("(");
                            string baseFeat = feat.Substring(0, Pos).Trim();
                            FeatsTemp[a] = baseFeat + " (" + MultipleFeats[0].Trim() + ")";

                            for (int b = 1; b <= MultipleFeats.Count - 1; b++)
                            {
                                NewFeats.Add(baseFeat + " (" + MultipleFeats[b].Trim() + ")");
                            }
                        }
                    }
                }
                //if (feat.EndsWith("M") || feat.Contains("M "))
                if(feat.Contains("[M]"))
                {
                    string temp2 = feat.Replace("[M]", string.Empty);
                    if (temp2.EndsWith("M")) temp2 = temp2.Substring(0, temp2.Length - 1);
                    string tempOld = temp2;
                    if (temp2.Contains("("))
                    {
                        int Pos = temp2.IndexOf("(");
                        temp2 = temp2.Substring(0, Pos).Trim();
                    }
                    temp2 = temp2.Trim();
                    MythicFeats.Add(temp2.Trim());
                    IFeatStatBlock tempFeat = StatBlockService.GetMythicFeatByName(temp2);
                    if (tempFeat != null && tempFeat.prerequisite_feats == temp2)
                        FeatsTemp[a] = tempOld;
                    else
                        FeatsTemp[a] = string.Empty;

                }
            }
            FeatsTemp.Remove(string.Empty);
            FeatsTemp.AddRange(NewFeats);
            if (_monSBSearch.IsMythic)
            {
                int value = _monSBSearch.MythicValue;
                int MythicFeatCount = StatBlockInfo.GetMythicFeats(value);

                if (MythicFeatCount == MythicFeats.Count)
                    _messageXML.AddPass("Mythic Feat Count");
                else
                    _messageXML.AddFail("Mythic Feat Count", MythicFeatCount.ToString(), MythicFeats.Count.ToString(), "");
            }

            int FeatCount = CharacterClasses.FindClassFeatCount();
            string formula = FeatCount.ToString() + " classes";

            if ((FeatsTemp.Contains("AlertnessB") || FeatsTemp.Contains("Alertness")) && _monSBSearch.HasFamiliar())
            {
                FeatCount++;
                formula += " +1 Familiar bonus";
            }

            if (_monSBSearch.HasHex("cauldron"))
            {
                FeatCount++;
                formula += " +1 cauldron hex";
            }

            //if (Race_Base.RaceBaseType != RaceBase.RaceType.Race)
            //{
                int count2 = FeatsTemp.Count;
                for (int a = count2 - 1; a >= 0; a--)
                {
                    if (FeatsTemp[a].LastIndexOf("B") == FeatsTemp[a].Length - 1)
                    {
                        FeatsTemp.Remove(FeatsTemp[a]);
                    }
                    else if (FeatsTemp[a].Contains("("))
                    {
                        string tempFeat = FeatsTemp[a].Substring(0, FeatsTemp[a].IndexOf("(")).Trim();
                        if (tempFeat.LastIndexOf("B") == tempFeat.Length - 1)
                        {
                            FeatsTemp.Remove(FeatsTemp[a]);
                        }
                    }
                    else if (FeatsTemp[a].Contains("B "))
                    {
                        FeatsTemp.Remove(FeatsTemp[a]);
                    }
                }
            //}

            if (_monSBSearch.HasSQ("secrets")) //loremaster
            {
                if (_monSBSearch.HasSQ("secret health"))
                {
                    FeatCount++;
                    formula += " +1 secret health";
                }
                if (_monSBSearch.HasSQ("applicable knowledge"))
                {
                    FeatCount++;
                    formula += " +1 applicable knowledge";
                }
            }

            if (_monSBSearch.HasSQ("slayer talents"))
            {
                if (_monSBSearch.HasSQ("combat trick"))
                {
                    FeatCount++;
                    formula += " +1 combat trick";
                }
            }

            if (_monSBSearch.HasSQ("rogue talent") || _monSBSearch.HasSQ("ninja tricks"))
            {
                if (_monSBSearch.HasSQ("combat trick"))
                {
                    FeatCount++;
                    formula += " +1 combat trick";
                }
                if (_monSBSearch.HasSQ("finesse rogue"))
                {
                    FeatCount++; //weapon finesse
                    formula += " +1 finesse rogue";
                }
                if (_monSBSearch.HasSQ("weapon training"))
                {
                    FeatCount++;
                    formula += " +1 weapon training";
                }
                if (_monSBSearch.HasSQ("feat"))
                {
                    FeatCount++;
                    formula += " +1 rogue talent feat";
                }
                if (_monSBSearch.HasSQ("card sharp"))
                {
                    FeatCount++;
                    formula += " +1 card sharp";
                }
                if (_monSBSearch.HasSQ("grit"))
                {
                    FeatCount+= 2;
                    formula += " +2 grit";
                }
            }           

            if (_monSBSearch.HasCavalierOrder("order of the cockatrice"))
            {
                int cavalierLevels = CharacterClasses.FindClassLevel("cavalier");
                if (cavalierLevels >= 2)
                {
                    FeatCount++;
                    formula += " +1 order of the cockatrice";
                }
            }

            if (_monSBSearch.HasCavalierOrder("order of the whip"))
            {
                int cavalierLevels = CharacterClasses.FindClassLevel("cavalier");
                if (cavalierLevels >= 2)
                {
                    FeatCount++;
                    formula += " +1 order of the whip";
                }
            }

            if (_monSBSearch.HasSQ("revelations") && _monSBSearch.HasSQ("weapon mastery")) // oracle battle mystery
            {
                int OracleLevel = CharacterClasses.FindClassLevel("oracle");
                FeatCount++;
                formula += " +1 weapon focus";
                if (OracleLevel >= 8)
                {
                    FeatCount++;
                    formula += " +1 Improved Critical";
                }
                if (OracleLevel >= 12)
                {
                    FeatCount++;
                    formula += " +1 Greater Weapon Focus";
                }
            }

            if (_monSBSearch.HasSQ("revelations") && _monSBSearch.HasSQ("stone stability")) // oracle stone mystery
            {
                int OracleLevel = CharacterClasses.FindClassLevel("oracle");
                if (OracleLevel >= 5)
                {
                    FeatCount++;
                    formula += " +1  Improved Trip";
                }
                if (OracleLevel >= 10)
                {
                    FeatCount++;
                    formula += " +1 Greater Trip";
                }
            }

            if (_monSBSearch.HasSQ("revelations") && _monSBSearch.HasSQ("cinder dance")) // oracle flame mystery
            {
                int OracleLevel = CharacterClasses.FindClassLevel("oracle");
                if (OracleLevel >= 5)
                {
                    FeatCount++;
                    formula += " +1  Nimble Moves";
                }
                if (OracleLevel >= 10)
                {
                    FeatCount++;
                    formula += " +1 Acrobatic Steps";
                }
            }

            #region ClassArchetypes
            if (_monSBSearch.HasAnyClassArchetypes())
            {

                if (_monSBSearch.HasClassArchetype("unarmed fighter"))
                {
                    FeatCount++;
                    formula += " +1 Unarmed Style";
                }

                if (_monSBSearch.HasClassArchetype("pirate"))
                {
                    FeatCount++;
                    formula += " +1 Sea Legs";
                }

                if (_monSBSearch.HasClassArchetype("musketeer"))
                {
                    FeatCount += 2;
                    formula += " +2 Musketeer Instruction";
                }

                if (_monSBSearch.HasClassArchetype("cad"))
                {
                    int CadLevels = CharacterClasses.FindClassLevel("fighter");
                    if (CadLevels >= 3) FeatCount++; // Catch Off Guard
                    formula += " +1 Catch Off Guard";
                }

                if (_monSBSearch.HasClassArchetype("steel hound"))
                {
                    int investigatorLevels = CharacterClasses.FindClassLevel("investigator");
                    if (investigatorLevels >= 2) FeatCount += 2; // Amateur Gunslinger and Gunsmithing
                    formula += " +2 Packing Heat";
                }

                if (_monSBSearch.HasClassArchetype("constable"))
                {
                    FeatCount++; // Apprehend (Ex)
                    formula += " +1 Apprehend";
                }

                if (_monSBSearch.HasClassArchetype("staff magus"))
                {
                    FeatCount++; // Quarterstaff Master (Ex)
                    formula += " +1 Quarterstaff Master";
                }

                if (_monSBSearch.HasClassArchetype("musket master"))
                {
                    FeatCount++; // Rapid Reloader
                    formula += " +1 Rapid Reloader";
                }

                if (_monSBSearch.HasClassArchetype("weapon adept"))
                {
                    int monkLevels = CharacterClasses.FindClassLevel("monk");
                    if (monkLevels >= 1) FeatCount++;
                    formula += " +1 Perfect Strike";
                    if (monkLevels >= 2)
                    {
                        FeatCount++;
                        formula += " +1 Weapon Focus";
                    }
                }

            }
            #endregion ClassArchetypes

            if (_monSBSearch.HasSpellDomians())
            {
                int clericLevel = CharacterClasses.FindClassLevel("cleric");
                if (clericLevel == 0) clericLevel = CharacterClasses.FindClassLevel("druid");
                if (_monSBSearch.HasSpellDomain("Nobility") && clericLevel >= 8)
                {
                    FeatCount++;
                    formula += " +1 Nobility";
                }
                if (_monSBSearch.HasSpellDomain("Rune"))
                {
                    FeatCount++;
                    formula += " +1 Rune";
                }
                if (_monSBSearch.HasSpellDomain("Darkness"))
                {
                    FeatCount++;
                    formula += " +1 Darkness";
                }
                if (_monSBSearch.HasSpellDomain("Persistence"))
                {
                    FeatCount++;  //step up
                    formula += " +1 Persistence";
                }
                if (_monSBSearch.HasSpellDomain("Black Powder"))
                {
                    FeatCount++;  //Gunsmithing  inquisition
                    formula += " +1 Black Powder";
                }
            }

            if (_monSBSearch.HasTemplate("worm that walks"))
            {
                FeatCount++;  //diehard
                formula += " +1 worm that walks";
            }

            if (CharacterClasses.HasClass("arcanist"))
            {
                if (_monSBSearch.HasSpecialAttack("metamagic knowledge"))
                {
                    FeatCount++;  //Metamagic Knowledge
                    formula += " +1 metamagic knowledge";
                }
            }

            if (CharacterClasses.HasClass("slayer"))
            {
                if (_monSBSearch.HasSQ("ranger combat style"))
                {
                    int slayerLevel = CharacterClasses.FindClassLevel("slayer");
                    int tempMod = 0;

                    if (slayerLevel >= 2) tempMod++;
                    if (slayerLevel >= 6) tempMod++;
                    if (slayerLevel >= 10) tempMod++;
                    if (slayerLevel >= 14) tempMod++;
                    if (slayerLevel >= 18) tempMod++;

                    if (tempMod > 0)
                    {
                        FeatCount += tempMod;
                        formula += " +" + tempMod.ToString() + " ranger combat style";
                    }
                }

                if (_monSBSearch.HasSQ("weapon training"))
                {
                    FeatCount += 1;
                    formula += " +1 weapon training";
                }
            }

            if (CharacterClasses.HasClass("hellknight signifer"))
            {
                int hellknightSigniferLevel = CharacterClasses.FindClassLevel("hellknight signifer");
                if (hellknightSigniferLevel >= 2 && _monSBSearch.HasFeat("Arcane Armor Training"))
                {
                    FeatCount++;  //Arcane Armor Mastery
                    formula += " +1 Arcane Armor Expertise";
                }
            }

            if (_monSBSearch.HasSQ("investigator talent") && _monSBSearch.HasSQ("bonus feat")) // oracle battle mystery
            {
                FeatCount++;  //Investigator Talents
                formula += " +1 Investigator Talent";
            }


            int temp = 0;
            if (Race_Base != null)
            {
                if (Race_Base.RaceBaseType == RaceBase.RaceType.StatBlock && Race_Base.UseRacialHD)
                {
                    if (!HasEnvirmonment)
                    {
                        temp = Race_Base.BonusFeatCount();
                        formula += " +" + temp.ToString() + " Race";
                        FeatCount += temp;
                    }
                }

                //FeatCount += StatBlockInfo.GetHDFeats(CharacterClasses.FindTotalClassLevels());
                temp = StatBlockInfo.GetHDFeats(HDValue);
                formula += " +" + temp.ToString() + " Hit Dice";
                FeatCount += temp;
                if (Race_Base.RaceBaseType == RaceBase.RaceType.Race)
                {
                    temp = Race_Base.BonusFeatCount();
                    formula += " +" + temp.ToString() + " Race";
                    FeatCount += temp;
                }
            }

            int SB_count = FeatsTemp.Count();

            if (IntAbilityScoreValue == 0)
            {
                FeatCount = 0;
                formula = " Int=0";
            }

            if (SB_count == FeatCount)
            {
                _messageXML.AddPass(CheckName, formula);
            }
            else
            {
                _messageXML.AddFail(CheckName, FeatCount.ToString(), SB_count.ToString(), formula);
            }
        }

        private List<string> RemoveSuperScripts(List<string> FeatsTemp)
        {
            List<string> superScripts = CommonInfo.GetSuperScripts();

            for (int a = 0; a <= FeatsTemp.Count - 1; a++)
            {
                string temp = FeatsTemp[a];
               
                FeatsTemp[a] = Utility.RemoveSuperScripts(temp);
                  
            }

            return FeatsTemp;
        }

        public void CheckFeatPreReqs()
        {
            CheckClassPreReqs(Feats);

            foreach (FeatData feat in Feats)
            {
                if (!HasMonkBonusFeats(feat.Name) && !HasRangerBonusFeats(feat.Name))
                {
                    ChechPreReqFeats(feat);
                    CheckFeatPreReqBAB(feat);
                    CheckFeatPreReqInt(feat);
                    CheckFeatPreReqCon(feat);
                    CheckFeatPreReqWis(feat);
                    CheckFeatPreReqCha(feat);
                    CheckFeatPreReqStr(feat);
                    CheckFeatPreReqDex(feat);
                    CheckFeatPreReqSkillRank(feat);
                }
            }
        }

        private void CheckFeatPreReqSkillRank(FeatData feat)
        {
            if (!feat.PreRequistSkillRanks.Any()) return;

            string methodName = "CheckFeatPreReqSkillRank";
            //handle bonus feats???

            foreach (string skillRank in feat.PreRequistSkillRanks)
            {
                int Pos = skillRank.LastIndexOf(" ");
                string tempRank = skillRank.Substring(Pos).Trim();
                string tempSkill = skillRank.Replace(tempRank, string.Empty).Trim();

                var skillCalc = SkillCalculations.Where(x => x.Name == tempSkill).FirstOrDefault();

                if (skillCalc != null)
                {
                    if (skillCalc.Rank >= Convert.ToInt32(tempRank))
                    {
                        _messageXML.AddPass(methodName + "   " + feat.Name + "--" + tempSkill);
                    }
                    else
                    {
                        _messageXML.AddFail(methodName, "For Feat " + feat.Name + "   " + tempSkill + " :" + tempRank, skillCalc.Name + ":" + skillCalc.Rank.ToString());
                    }
                }
                else
                {
                    _messageXML.AddFail(methodName, "Skill " + tempSkill + " missing for feat " + feat.Name + ", needs " + tempRank.ToString() + " rank(s)");
                }
            }
        }

        private void CheckClassPreReqs(List<FeatData> Feats)
        {
            string methodName = "CheckClassPreReqs";

            foreach (ClassWrapper wrapper in CharacterClasses.Classes)
            {
                List<string> ClassPreReqs = CharacterClasses.GetPrestigePreReqFeats(wrapper.Name);
                FeatFoundation.FeatFoundation tempFeat = null;

                foreach (string oneFeat in ClassPreReqs)
                {
                    string holdFeat = oneFeat;
                    string SelectdItem = string.Empty;
                    List<string> splitFeats = oneFeat.Split(',').ToList<string>();

                    bool Found = false;
                    foreach (string oneSplitFeat in splitFeats)
                    {                        
                        if (oneSplitFeat.Contains(" ("))
                        {
                            int Pos = oneSplitFeat.IndexOf(" (");
                            holdFeat = oneSplitFeat.Substring(0, Pos).Trim();
                            SelectdItem = oneSplitFeat.Replace(holdFeat, string.Empty);
                            SelectdItem = Utility.RemoveParentheses(SelectdItem);
                        }
                        tempFeat = Feats.Find(p => p.Name == holdFeat && p.SelectedItem == SelectdItem);
                        if (tempFeat != null)
                        {
                            Found = true;
                            break;
                        }

                    }
                    if (!Found) _messageXML.AddFail(methodName, wrapper.Name + " is missing prereq feat " + oneFeat);
                }
            }   
        }

        private void CheckFeatPreReqBAB(FeatFoundation.FeatFoundation feat)
        {
            if (feat.BAB < 0) return;
            if (feat.BonusFeat) return;

            if(feat.BAB > BAB)
            {
                _messageXML.AddFail("Feat Pre Req", feat.Name + " needs min BAB of " + feat.BAB.ToString() + ", but has BAB of " + BAB.ToString());
            }
        }

        private void CheckFeatPreReqStr(FeatFoundation.FeatFoundation feat)
        {
            if (feat.Str < 0) return;
            if (feat.BonusFeat) return;
            if(_monSBSearch.HasSubType("incorporeal")) return;

            if (feat.Str > Str)
            {
                _messageXML.AddFail("Feat Pre Req", feat.Name + " needs min Str of " + feat.Str.ToString() + ", but has Str of " + Str.ToString());
            }
        }

        private void CheckFeatPreReqDex(FeatFoundation.FeatFoundation feat)
        {
            if (feat.Dex < 0) return;
            if (feat.BonusFeat) return;

            if (feat.Dex > Dex)
            {
                _messageXML.AddFail("Feat Pre Req", feat.Name + " needs min Dex of " + feat.Dex.ToString() + ", but has Dex of " + Dex.ToString());
            }
        }

        private void CheckFeatPreReqInt(FeatFoundation.FeatFoundation feat)
        {
            if (feat.Int < 0) return;
            if (feat.BonusFeat) return;

            if (feat.Int > Int)
            {
                if (CharacterClasses.HasClass("brawler"))
                {
                    if (feat.Int > 13 || feat.Type != FeatFoundation.FeatTypes.Combat)
                    {
                        _messageXML.AddFail("Feat Pre Req", feat.Name + " needs min Int of " + feat.Int.ToString() + ", but has Int of " + Int.ToString());
                    }
                }
                else
                    _messageXML.AddFail("Feat Pre Req", feat.Name + " needs min Int of " + feat.Int.ToString() + ", but has Int of " + Int.ToString());
            }
        }

        private void CheckFeatPreReqCon(FeatFoundation.FeatFoundation feat)
        {
            if (feat.Con < 0) return;
            if (feat.BonusFeat) return;

            if (_monSBSearch.HasType("undead"))
            {
                if (feat.Con > Cha)
                {
                    _messageXML.AddFail("Feat Pre Req", feat.Name + " needs min Con of " + feat.Con.ToString() + ", but has Cha(undead) of " + Cha.ToString());
                }
                return;                
            }

            if (feat.Con > Con)
            {
                _messageXML.AddFail("Feat Pre Req", feat.Name + " needs min Con of " + feat.Con.ToString() + ", but has Con of " + Con.ToString());
            }
        }

        private void CheckFeatPreReqCha(FeatFoundation.FeatFoundation feat)
        {
            if (feat.Cha < 0) return;
            if (feat.BonusFeat) return;

            if (feat.Cha > Cha)
            {
                _messageXML.AddFail("Feat Pre Req", feat.Name + " needs min Cha of " + feat.Cha.ToString() + ", but has Cha of " + Cha.ToString());
            }
        }

        private void CheckFeatPreReqWis(FeatFoundation.FeatFoundation feat)
        {
            if (feat.Wis < 0) return;
            if (feat.BonusFeat) return;

            if (feat.Wis > Wis)
            {
                _messageXML.AddFail("Feat Pre Req", feat.Name + " needs min Wis of " + feat.Wis.ToString() + ", but has Wis of " + Wis.ToString());
            }
        }

        private void ChechPreReqFeats(FeatFoundation.FeatFoundation feat)
        {
            List<string> preReqs = null;
            if (!feat.BonusFeat)
            {
                preReqs = feat.PreRequistFeats;
                foreach (string preReq in preReqs)
                {
                    bool PreReqOrs = preReq.Contains("|");
                    List<string> preReqs2 = new List<string>();
                    bool Fail = false;
                    if (PreReqOrs)
                    {
                        preReqs2 = preReq.Split('|').ToList<string>();
                        foreach (string preReq2 in preReqs2)
                        {
                            if (CheckOnePreReqFeat(feat, preReq2, out Fail)) continue;
                            if (!Fail) break;
                        }
                        if (Fail) _messageXML.AddFail("Feat Pre Req", feat.Name + " is missing prereq feat " + preReq);
                    }
                    else
                    {
                        if (CheckOnePreReqFeat(feat, preReq, out Fail)) continue;
                        if (Fail) _messageXML.AddFail("Feat Pre Req", feat.Name + " is missing prereq feat " + preReq);
                    }
                }
            }
        }

        private bool CheckOnePreReqFeat(FeatFoundation.FeatFoundation feat, string preReq, out bool Fail)
        {
            Fail = false;
            string temp = preReq.Trim();
            string selectedItem = null;
            int Pos = temp.IndexOf("(");
            if (Pos >= 0)
            {
                selectedItem = temp.Substring(Pos);
                temp = temp.Replace(selectedItem, string.Empty).Trim();
                selectedItem = Utility.RemoveParentheses(selectedItem);
            }
            if (selectedItem == null) selectedItem = feat.SelectedItem;
            if (selectedItem == "see below") selectedItem = string.Empty;
            if (selectedItem.Contains("see page")) selectedItem = string.Empty;
            if (!Feats.Exists(y => y.Name.ToLower() == temp.ToLower() && y.SelectedItem == selectedItem))
            {
                if (temp == "Weapon Focus" && Feats.Exists(y => y.Name.ToLower() == temp.ToLower())) return true;
                if (CheckBonusFeats(feat.Name))
                {
                    Fail = true;
                }
            }

            return false;
        }


        private bool CheckBonusFeats(string FeatName)
        {
            return !HasMonkBonusFeats(FeatName) && !HasRangerBonusFeats(FeatName) && !HasCavalierBonusFeats(FeatName) && !HasAldoriSwordlordBonusFeats(FeatName)
                && !HasTemplateBonusFeats(FeatName) && !HasMysteryBonusFeats(FeatName);
        }

        private bool HasMysteryBonusFeats(string FeatName)
        {
            if (!_monSBSearch.HasSQ("revelations")) return false;

            if (_monSBSearch.HasSQ("stone stability"))
            {
                int OracleLevel = CharacterClasses.FindClassLevel("oracle");
                if (OracleLevel >= 5 && FeatName == "Improved Trip") return true;
                if (OracleLevel >= 10 && FeatName == "Greater Trip") return true;
            }

            return false;
        }

        private bool HasTemplateBonusFeats(string FeatName)
        {
            if (_monSBSearch.TemplateCount() == 0) return false;

            if (_monSBSearch.HasTemplate("worm that walks") && FeatName == "Diehard") return true;


            return false;
        }

        private bool HasAldoriSwordlordBonusFeats(string FeatName)
        {
            if (!CharacterClasses.HasClass("aldori swordlord")) return false;

            if (FeatName == "Aldori Dueling Mastery")
            {
                return true;
            }  

            return false;
        }

        private bool HasCavalierBonusFeats(string FeatName)
        {
            if (!CharacterClasses.HasClass("cavalier")) return false;

            int cavalierLevels = CharacterClasses.FindClassLevel("cavalier");
            if (cavalierLevels < 2) return false;

            if (_monSBSearch.HasCavalierOrder("order of the cockatrice"))
            {                
                if (cavalierLevels >= 2 && FeatName == "Dazzling Display")  return true;
            }
            if (_monSBSearch.HasCavalierOrder("order of the whip"))
            {
                if (cavalierLevels >= 2 && FeatName == "Whip Mastery") return true;
            }
            return false;
        }       

        private bool HasRangerBonusFeats(string FeatName)
        {
            if (!CharacterClasses.HasClass("ranger")) return false;
            

            int rangerLevel = CharacterClasses.FindClassLevel("ranger");
            if (rangerLevel < 2) return false;

            string CombatStyle = _monSBSearch.GetSpecialAttack("combat style").Replace("combat style", string.Empty);
            CombatStyle = Utility.RemoveParentheses(CombatStyle);

            if (string.IsNullOrEmpty(CombatStyle))
            {
                bool foundBonus = false;
                //Two-Weapon Combat
                List<string> BonusFeats = new List<string> { "Double Slice", "Improved Shield Bash", "Quick Draw", "Two-Weapon Fighting" };
                foreach (FeatFoundation.FeatFoundation feat in Feats)
                {
                    if (BonusFeats.Contains(feat.Name))
                    {
                        foundBonus = true;
                        break;
                    }
                }

                //if (foundBonus)
                //{
                //    if (rangerLevel >= 6) BonusFeats.AddRange(new List<string> { "Improved Two-Weapon Fighting", "Two-Weapon Defense" });
                //    if (rangerLevel >= 6) BonusFeats.AddRange(new List<string> { "Greater Two-Weapon Fighting", "Two-Weapon Rend" });
                //}


                //List<string> BonusFeats = new List<string> {"Catch Off-Guard", "Combat Reflexes", "Deflect Arrows", "Dodge", 
                //                  "Improved Grapple", "Scorpion Style", "Throw Anything"};

                CombatStyle = foundBonus ? "two-weapon combat" : "archery";
            }
            List<string> temp = RemoveSuperScripts(new List<string> { CombatStyle });
            List<string> CombatStyleFeats = GetRangerCombatStyleFeats(rangerLevel, temp[0]);

            return CombatStyleFeats.Contains(FeatName);
        }

        private List<string> GetRangerCombatStyleFeats(int RangerLevel, string CombatStyle)
        {
            List<string> CombatStyleFeats = null;

            switch (CombatStyle)
            {
                case "archery":
                    CombatStyleFeats = new List<string> { "Far Shot", "Focused Shot", "Point Blank Shot", "Precise Shot", "Rapid Shot" };
                    if (RangerLevel >= 6) CombatStyleFeats.AddRange(new List<string> { "Crossbow Mastery", "Improved Precise Shot", "Parting Shot", "Point Blank Master", "Manyshot" });
                    if (RangerLevel >= 10) CombatStyleFeats.AddRange(new List<string> { "Pinpoint Targeting", "Shot on the Run" });
                    break;
                case "two-weapon combat":
                case "two-weapon":
                    CombatStyleFeats = new List<string> { "Double Slice", "Improved Shield Bash", "Quick Draw", "Two-Weapon Fighting" };
                    if (RangerLevel >= 6) CombatStyleFeats.AddRange(new List<string> { "Improved Two-Weapon Fighting", "Two-Weapon Defense" });
                    if (RangerLevel >= 10) CombatStyleFeats.AddRange(new List<string> { "Greater Two-Weapon Fighting", "Two-Weapon Rend" });
                    break;
                case "crossbow":
                    CombatStyleFeats = new List<string> { "Deadly Aim", "Focused Shot", "Precise Shot", "Rapid Reload" };
                    if (RangerLevel >= 6) CombatStyleFeats.AddRange(new List<string> { "Crossbow Mastery", "Improved Precise Shot" });
                    if (RangerLevel >= 10) CombatStyleFeats.AddRange(new List<string> { "Pinpoint Targeting", "Shot on the Run" });
                    break;
                case "mounted combat":
                    CombatStyleFeats = new List<string> { "Mounted Combat", "Mounted Archery", "Ride-By Attack", "Trick Riding" };
                    if (RangerLevel >= 6) CombatStyleFeats.AddRange(new List<string> { "Mounted Shield", "Spirited Charge" });
                    if (RangerLevel >= 10) CombatStyleFeats.AddRange(new List<string> { "Mounted Skirmisher", "Unseat" });
                    break;
                case "natural weapon":
                    CombatStyleFeats = new List<string> { "Aspect of the Beast", "Improved Natural Attack", "Rending Claws", "Weapon Focus" };
                    if (RangerLevel >= 6) CombatStyleFeats.AddRange(new List<string> { "Eldritch Claws", "Vital Strike" });
                    if (RangerLevel >= 10) CombatStyleFeats.AddRange(new List<string> { "Multiattack", "Improved Vital Strike" });
                    break;
                case "two-handed weapon":
                    CombatStyleFeats = new List<string> { "Cleave", "Power Attack", "Pushing Assault", "Shield of Swings" };
                    if (RangerLevel >= 6) CombatStyleFeats.AddRange(new List<string> { "Furious Focus", "Great Cleave" });
                    if (RangerLevel >= 10) CombatStyleFeats.AddRange(new List<string> { "Dreadful Carnage", "Improved Sunder" });
                    break;
                case "weapon and shield":
                    CombatStyleFeats = new List<string> { "Improved Shield Bash", "Shield Focus", "Shield Slam", "Two-Weapon Fighting" };
                    if (RangerLevel >= 6) CombatStyleFeats.AddRange(new List<string> { "Saving Shield", "Shield Master" });
                    if (RangerLevel >= 10) CombatStyleFeats.AddRange(new List<string> { "Bashing Finish", "Greater Shield Focus" });
                    break;
                default:
                    CombatStyleFeats = new List<string>();
                    break;
            }

            return CombatStyleFeats;
        }

        private bool HasMonkBonusFeats(string FeatName)
        {
            if (!CharacterClasses.HasClass("monk")) return false;
            int monkLevel = CharacterClasses.FindClassLevel("monk");
            List<string> BonusFeats = new List<string> {"Catch Off-Guard", "Combat Reflexes", "Deflect Arrows", "Dodge", 
                              "Improved Grapple", "Scorpion Style", "Throw Anything", "Stunning Fist"};

            if (monkLevel >= 6)
            {
                BonusFeats.AddRange(new List<string> { "Gorgon's Fist", "Improved Bull Rush", "Improved Disarm", "Improved Feint", "Improved Trip", "Mobility" });
            }
            if (monkLevel >= 10)
            {
                BonusFeats.AddRange(new List<string> { "Improved Critical", "Medusa's Wrath", "Snatch Arrows", "Spring Attack" });
            }

            return BonusFeats.Contains(FeatName);
        }    
    }
}
