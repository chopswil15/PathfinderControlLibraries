using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatBlockCommon.Monster_SB;
using CommonStrings;
using Utilities;
using PathfinderGlobals;
using StatBlockCommon.Individual_SB;
using StatBlockFormating;
using StatBlockBusiness;

namespace StatBlockParsing
{
    public class IndividualStatBlock_Parse : MonsterStatBlock_Parse
    {
        private string CR = Environment.NewLine;
        private IndividualStatBlock IndvidSB = new IndividualStatBlock();
        private ISBCommonBaseInput _sbcommonBaseInput;
        private IMonsterStatBlockBusiness _monsterStatBlockBusiness;
        private IIndividualStatBlockBusiness _individualStatBlockBusiness;

        public new IndividualStatBlock Parse(string Indvidstr, string Source, bool isFrogGods, ref string ErrorMessage)
        {
            _sbcommonBaseInput = new SBCommonBaseInput();
            _monsterStatBlockBusiness = new MonsterStatBlockBusiness();
            _individualStatBlockBusiness = new IndividualStatBlockBusiness();
            base.MonSB = new MonsterStatBlock();
            _sbcommonBaseInput.MonsterSB = new MonsterStatBlock();
            _sbcommonBaseInput.IndvidSB = IndvidSB;
            SourceSuperScript = string.Empty;

            Indvidstr = Utility.CommonMonsterFindReplace(Indvidstr, CR);  
            Utility.RemoveSuperScriptsFromList(ItalicPhrases);

            if (Indvidstr.Contains("Spell-Like Abilities") && Indvidstr.Contains("Spells Prepared"))
            {
                if (Indvidstr.IndexOf("Spell-Like Abilities") > Indvidstr.IndexOf("Spells Prepared"))
                {
                    ErrorMessage = "Spell-Like Abilities before Spells Prepared";
                    return IndvidSB;
                }
            }

            base.MonSB = IndvidSB;           
            CR = Utility.FindCR(Indvidstr);
            int CRPos = Indvidstr.IndexOf(CR);
            base.UpdateCR(CR);

            Indvidstr = Utility.CommonMonsterFindReplace(Indvidstr, CR);

            int DefensePos = Indvidstr.IndexOf("Defense" + CR);
            if (DefensePos == -1)
            {
                Indvidstr = Indvidstr.Replace("Defenses" + CR, "Defense" + CR);
                DefensePos = Indvidstr.IndexOf("Defense" + CR);
            }

            if (DefensePos == -1 && isFrogGods)
            {
                int tempCRPos = Indvidstr.IndexOf("CR ");
                if (tempCRPos == -1)
                {
                    ErrorMessage = "No CR";
                    return IndvidSB;
                }
                DefensePos = Indvidstr.IndexOf("AC ", tempCRPos);
            }


            if (DefensePos == -1)
            {
                ErrorMessage = "Issue with Defense Header";
                return IndvidSB;
            }

            string temp = Indvidstr.Substring(0, DefensePos);
            Indvidstr = Indvidstr.Replace(temp, string.Empty).Trim();
            temp = ParseAgeVariantUnique(temp);
            temp = ParseAgeCategory(temp);

            //find Race line, if it exists
            CRPos = temp.IndexOf(CR); //name & CR
            string nameTemp = temp.Substring(0, CRPos);
            CRPos = temp.IndexOf(CR, CRPos + 1); //XP line
            string raceLine = temp.Substring(CRPos, temp.IndexOf(CR, CRPos + 1) - CRPos);
            CRPos = raceLine.IndexOf(PathfinderConstants.PAREN_LEFT);
            if (CRPos > 0)
            {
                raceLine = raceLine.Substring(0, CRPos);
            }
            string raceLineHold;
            //only non-racial races can have only Prestige classes (i.e. no bases classes), how to test?
            if (!CommonMethods.FindNumber(raceLine) || HasClass(raceLine) || HasPresteigeClass(raceLine))
            {
                raceLineHold = raceLine;
            }
            else
            {
                raceLineHold = nameTemp;
                raceLine = nameTemp;
            }

            ParseTemplatesApplied(ref temp, ref raceLine, raceLineHold);

            if (isFrogGods)
            {
                if (temp.Contains("; Perception")) temp = temp.Replace("; Perception", "; Senses Perception");
            }

            ParseBasic(temp, ref ErrorMessage);
            if (ErrorMessage.Length > 0) return IndvidSB;

            #region SimpleClassTemplate
            if (IndvidSB.Class.Length == 0)
            {
                List<string> simpleClassTemplateList = CommonMethods.GetSimpleClassTemplates();
                bool foundSimpleClassTemplate = false;
                string SimpleClassTemplate = string.Empty;
                var raceLineLower = raceLine.ToLower();

                foreach (string template in simpleClassTemplateList)
                {
                    var templateLower = template.ToLower();
                    if (raceLineLower.Contains(templateLower))
                    {
                        if (templateLower == "bard" && raceLineLower.Contains("bombardier")) continue;
                        if (templateLower == "ranger" && raceLineLower.Contains("stranger")) continue;
                        foundSimpleClassTemplate = true;
                        SimpleClassTemplate = template;
                        break;
                    }
                }

                if (foundSimpleClassTemplate)
                {
                    IndvidSB.TemplatesApplied += SimpleClassTemplate.ToLower() + "|";
                    string racelineHold = raceLine;
                    raceLine = raceLine.ToLower().Replace(SimpleClassTemplate.ToLower(), string.Empty);
                    temp = temp.Replace(racelineHold, raceLine);
                }
            }
            #endregion SimpleClassTemplate


            #region ParseDefense
            Indvidstr = Indvidstr.Replace("Ofense", "Offense");
            DefensePos = Indvidstr.IndexOf("Defense" + CR);
            if (DefensePos == -1 && isFrogGods) DefensePos = Indvidstr.IndexOf("AC ");

            int OffensePos = Indvidstr.IndexOf("Offense" + CR);
            if (OffensePos == -1 && isFrogGods)
            {
                OffensePos = Indvidstr.IndexOf("Speed ");
                if (OffensePos == -1)
                {
                    OffensePos = Indvidstr.IndexOf("Spd ");
                }
                if (OffensePos == -1)
                {
                    ErrorMessage = "Missing Speed value, should be before Melee block";
                    return IndvidSB;
                }
            }

            try
            {
                temp = Indvidstr.Substring(DefensePos, OffensePos - DefensePos);
            }
            catch (Exception ex)
            {
                ErrorMessage = "Issue with Finding Offense or Defense Headers --" + ex.Message;
                return IndvidSB;
            }
            Indvidstr = Indvidstr.Replace(temp, string.Empty).Trim();
            try
            {
                if (isFrogGods)
                {
                    if (temp.Contains("; Ref")) temp = temp.Replace("; Ref", ", Ref");
                    if (temp.Contains("; Will")) temp = temp.Replace("; Will", ", Will");
                }
                ParseDefense(temp);
            }
            catch (Exception ex)
            {
                ErrorMessage = "ParseDefense: " + ex.Message;
                return IndvidSB;
            }
            #endregion ParseDefense

            #region ParseOffense
            int TacticsPos = Indvidstr.IndexOf("Tactics" + CR);
            if (TacticsPos == -1) //no tactics section
            {
                TacticsPos = Indvidstr.IndexOf("Statistics" + CR);
                if (TacticsPos == -1 && isFrogGods)
                {
                    TacticsPos = Indvidstr.IndexOf("Str ");
                    if (TacticsPos == -1)
                    {
                        ErrorMessage = " Missing Str Heading";
                        return IndvidSB;
                    }
                }
                else if (TacticsPos == -1)
                {
                    ErrorMessage = " Missing Statistics Heading";
                    return IndvidSB;
                }                
            }

            OffensePos = Indvidstr.IndexOf("Offense" + CR);
            if (OffensePos == -1 && isFrogGods)
            {
                OffensePos = Indvidstr.IndexOf("Speed ");
                if (OffensePos == -1)
                {
                    OffensePos = Indvidstr.IndexOf("Spd ");
                }
            }

            temp = Indvidstr.Substring(OffensePos, TacticsPos - OffensePos);
            Indvidstr = Indvidstr.Replace(temp, string.Empty).Trim();
            ParseIndividualOffense(ref temp);

            if (isFrogGods)
            {
                if (temp.Contains(", Reach"))
                {
                    temp = temp.Replace(", Reach", "; Reach");
                }
            }
            ParseOffense(temp);
            #endregion ParseOffense

            #region ParseStatistics
            int BaseStatisticsPos = Indvidstr.IndexOf("Base Statistics");
            int BaseStatisticsOffset = 14;
            if (BaseStatisticsPos == -1)
            {
                BaseStatisticsPos = 0;
                BaseStatisticsOffset = 0;
            }
            int StatisticsPos = Indvidstr.IndexOf("Statistics" + CR, BaseStatisticsPos + BaseStatisticsOffset);
            if (StatisticsPos == -1 && isFrogGods)  StatisticsPos = Indvidstr.IndexOf("Str ");

            if (StatisticsPos == -1)
            {
                ErrorMessage =  Indvidstr.Contains(string.Empty) ? "Tactics Before Statistics Header" : "Issue with Statistics Header";
                return IndvidSB;
            }

            TacticsPos = Indvidstr.IndexOf("Tactics" + CR);
            if (TacticsPos >= 0)
            {
                temp = Indvidstr.Substring(TacticsPos, StatisticsPos - TacticsPos);
                Indvidstr = Indvidstr.Replace(temp, string.Empty).Trim();
                ParseTactics(temp);
            }

            int SpecialAbilitiesPos = Indvidstr.IndexOf("Special Abilities" + CR);
            if (SpecialAbilitiesPos == -1)
                SpecialAbilitiesPos = Indvidstr.Length;
            StatisticsPos = Indvidstr.IndexOf("Statistics" + CR);
            if (StatisticsPos == -1 && isFrogGods)
                StatisticsPos = Indvidstr.IndexOf("Str ");

            temp = Indvidstr.Substring(StatisticsPos, SpecialAbilitiesPos - StatisticsPos);
            Indvidstr = Indvidstr.Replace(temp, string.Empty).Trim();
            ParseIndividualStatistics(ref temp);
            ParseStatistics(temp, out ErrorMessage);
            if (ErrorMessage.Length > 0)  return IndvidSB;
            #endregion ParseStatistics

            SpecialAbilitiesPos = Indvidstr.IndexOf("Special Abilities" + CR);
            if (SpecialAbilitiesPos >= 0)  ParseSpecialAbilities(Indvidstr);

            if (MonSB.Race.Length == 0 && MonSB.Environment.Length == 0)
            {
                MonSB.Race = MonSB.name;
                int Pos = MonSB.Race.IndexOf("Subtier");
                if (Pos > 0) MonSB.Race = MonSB.Race.Substring(0, Pos);
                if (MonSB.Race.Contains("Animal Companion")) MonSB.Race = MonSB.Race.Replace("Animal Companion", string.Empty).Trim();
                if (MonSB.Race.Contains("animal companion")) MonSB.Race = MonSB.Race.Replace("animal companion", string.Empty).Trim();
                if (MonSB.TemplatesApplied.Length > 0)
                {
                    string templates = MonSB.TemplatesApplied.Replace("|", PathfinderConstants.SPACE).Trim().ProperCase();
                    MonSB.name = templates + PathfinderConstants.SPACE + MonSB.name;
                }
                else
                {
                    raceLine = MonSB.name;
                    raceLineHold = MonSB.name;
                    ParseTemplatesApplied(ref temp, ref raceLine, raceLineHold);
                    if (MonSB.TemplatesApplied.Length > 0)
                    {
                        List<string> templates = MonSB.TemplatesApplied.Split('|').ToList();
                        templates.RemoveAll(x => x == string.Empty);
                        foreach (var template in templates)
                        {
                            MonSB.Race = MonSB.Race.Replace(template, string.Empty);
                            MonSB.Race = MonSB.Race.Replace(template.ProperCase(), string.Empty);
                        }
                        MonSB.Race = MonSB.Race.Trim();
                    }
               }
            }


            #region CreateFullText
            IndividualStatBlock_Format IndivSB_Form = new IndividualStatBlock_Format();
            IndivSB_Form.ItalicPhrases = ItalicPhrases;
            IndivSB_Form.BoldPhrases = BoldPhrases;
            IndivSB_Form.BoldPhrasesSpecialAbilities = BoldPhrasesSpecialAbilities;
            IndivSB_Form.SourceSuperScript = SourceSuperScript;
            try
            {
                IndvidSB.FullText = IndivSB_Form.CreateFullText(IndvidSB);
            }
            catch (Exception ex)
            {
                ErrorMessage = "CreateFullText: " + ex.Message;
            }
            #endregion CreateFullText

            return IndvidSB;
        }

        private string ParseAgeVariantUnique(string data)
        {           
            if (data.Contains("Male or Female") || data.Contains("Male or female") || data.Contains("Female or male"))
            {
                IndvidSB.Gender = "Male or Female";
                data = data.Replace("Male or Female", string.Empty);
                data = data.Replace("Male or female", string.Empty);
                data = data.Replace("Female or male", string.Empty);
            }
            if (data.Contains("Male and Female") || data.Contains("Male and female") || data.Contains("Female and male"))
            {
                IndvidSB.Gender = "Male and Female";
                data = data.Replace("Male and Female", string.Empty);
                data = data.Replace("Male and female", string.Empty);
                data = data.Replace("Female and male", string.Empty);
            }
            if (data.Contains("Female") || data.Contains("female"))
            {
                IndvidSB.Gender = "Female";
                data = data.Replace("Female", string.Empty);
                data = data.Replace("female", string.Empty);
            }
            if (data.Contains("Male") || data.Contains("male "))
            {
                IndvidSB.Gender = "Male";
                data = data.Replace("Male", string.Empty);
                data = data.Replace("male ", string.Empty);
            }
            if (data.Contains("Unique") || data.Contains("unique"))
            {
                IndvidSB.UniqueMonster = true;
                data = data.Replace("Unique", string.Empty);
                data = data.Replace("unique", string.Empty);
            }

            if (data.Contains("Variant") || data.Contains("variant"))
            {
                IndvidSB.Variant = true;
                data = data.Replace("Variant", string.Empty);
                data = data.Replace("variant", string.Empty);
            }

            data = data.Replace("Middle-Aged", "middle age");
            data = data.Replace("middle-aged", "middle age");
            data = data.Replace("middle-age", "middle age");
            data = data.Replace("Middle Aged", "middle age");
            data = data.Replace("middle aged", "middle age");
            data = data.Replace("Middle-aged", "middle age");

            return data;
        }

        private bool HasPresteigeClass(string raceLine)
        {
            raceLine = raceLine.ToLower();
            List<string> classes = CommonMethods.GetPrestigeClasses();

            foreach (string oneClass in classes)
            {
                if (raceLine.Contains(oneClass.ToLower())) return true;
            }

            return false;
        }

        private bool HasClass(string raceLine)
        {
            raceLine = raceLine.ToLower();
            List<string> classes = CommonMethods.GetClasses();

            foreach (string oneClass in classes)
            {
                if(raceLine.Contains(oneClass.ToLower())) return true;
            }

            classes = CommonMethods.GetNPCClasses();

            foreach (string oneClass in classes)
            {
                if (raceLine.Contains(oneClass.ToLower())) return true;
            }

            return false;
        }

        private void ParseTemplatesApplied(ref string temp, ref string raceLine, string raceLineHold)
        {
            TemplatesAppliedParser templatesAppliedParser = new TemplatesAppliedParser(_sbcommonBaseInput, _monsterStatBlockBusiness, _individualStatBlockBusiness);
            templatesAppliedParser.ParseTemplatesApplied(ref temp, ref raceLine, raceLineHold);          
        }   

        private string ParseAgeCategory(string temp)
        {
            int Pos = temp.IndexOf("CR ");

            if (temp.Contains(" dragon "))
            {
                List<string> DragonAges = CommonMethods.GetDragonAgeCategories();
                foreach (string age in DragonAges)
                {
                    if (temp.ToLower().IndexOf(age) >= Pos || temp.ToLower().IndexOf(age.ToLower()) >= Pos)
                    {
                        if (temp.ToLower().IndexOf(age) > Pos || temp.ToLower().IndexOf(age.ToLower()) > Pos)
                        {
                            IndvidSB.AgeCategory = age.ToLower();
                            break;
                        }
                    }
                }
            }
            else if (!temp.Contains("Elder thing") && !temp.Contains("Elder Witchlight"))
            {
                var tempLower = temp.ToLower();
                foreach (string age in CommonMethods.GetAgeCategories())
                {
                    var ageLower = age.ToLower();
                    if (temp.IndexOf(age) >= Pos && temp.Contains(age) || temp.IndexOf(ageLower) >= Pos && temp.Contains(ageLower))
                    {
                        if (age == "Old")
                        {
                            if (tempLower.Contains("kobold")) continue;
                            if (tempLower.Contains("cold")) continue;
                            if (tempLower.Contains("great old one")) continue;
                            if (temp.IndexOf(age) >= Pos)
                            {
                                temp = temp.ReplaceFirst(age, string.Empty);
                            }
                            else if (temp.IndexOf(PathfinderConstants.SPACE + ageLower) >= Pos)
                            {
                                temp = temp.ReplaceFirst(age.ToLower(), string.Empty);
                            }

                            IndvidSB.AgeCategory = ageLower;
                            return temp;
                        }
                        else
                        {
                            if (tempLower.Contains("elder mythos cultist")) continue;
                            if (!(ageLower == "elder" && temp.Contains("minotaur elder")))
                            {
                                temp = temp.Replace(age, string.Empty)
                                    .Replace(ageLower, string.Empty);

                                string ageHold = ageLower;
                                if (ageHold == "middle-aged" || ageHold == "middle aged")
                                {
                                    ageHold = "middle age";
                                }

                                IndvidSB.AgeCategory = ageHold;
                                if (IndvidSB.AgeCategory == "young") MonSB.TemplatesApplied += "young|";
                                return temp;
                            }
                        }
                    }
                }
            }
            return temp;
        }

        private void ParseIndividualOffense(ref string Offense)
        {
            //work your way back
            string temp;

            int Pos = Offense.IndexOf("Base Statistics");
            if (Pos >= 0)
            {
                temp = Offense.Substring(Pos);
                Offense = Offense.Replace(temp, string.Empty).Trim();
                temp = CommonMethods.KeepCRsIndividual(temp, CR);
                temp = temp.Replace("Base Statistics", string.Empty);
                IndvidSB.BaseStatistics = temp.Trim();
            }           

            Pos = Offense.IndexOf("Opposition Schools");
            if(Pos ==-1)
            {
                Pos = Offense.IndexOf("Opposition School");
            }
            if (Pos >= 0)
            {
                temp = Offense.Substring(Pos);
                Pos = temp.IndexOf("* ");
                if (Pos >= 0)
                {
                    temp = temp.Substring(0, Pos);
                }
                if (temp.Contains(CR))
                {
                    Pos = temp.IndexOf(CR);
                    temp = temp.Substring(0, Pos);
                }
                Offense = Offense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(":", string.Empty);
                temp = temp.Replace(";", string.Empty);
                temp = temp.Replace("M Mythic spell", string.Empty);
                temp = temp.Replace("M mythic spells", string.Empty);
                temp = temp.Replace("M mythic spell", string.Empty);
                temp = temp.Replace("Opposition Schools", string.Empty);
                temp = temp.Replace("Opposition School", string.Empty);
                IndvidSB.ProhibitedSchools = temp.Trim();
            }

            Pos = Offense.IndexOf("Prohibited Schools");
            if (Pos >= 0)
            {
                temp = Offense.Substring(Pos);
                Pos = temp.IndexOf("* ");
                if (Pos >= 0)
                {
                    temp = temp.Substring(0, Pos);
                }
                Offense = Offense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(":", string.Empty);
                temp = temp.Replace("Prohibited Schools", string.Empty);
                IndvidSB.ProhibitedSchools = temp.Trim();
            }

            Pos = Offense.IndexOf("Thassilonian Specialization");
            if (Pos >= 0)
            {
                temp = Offense.Substring(Pos);
                Pos = temp.IndexOf("* ");
                if (Pos >= 0)
                {
                    temp = temp.Substring(0, Pos);
                }
                Offense = Offense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Thassilonian Specialization", string.Empty);
                temp = temp.Replace(";", string.Empty);
                IndvidSB.ThassilonianSpecialization = temp.Trim();
            }

            Pos = Offense.IndexOf("Focused School");
            if (Pos >= 0)
            {
                temp = Offense.Substring(Pos);
                Pos = temp.IndexOf("* ");
                if (Pos >= 0)
                {
                    temp = temp.Substring(0, Pos);
                }
                Offense = Offense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Focused School", string.Empty);
                temp = temp.Replace(";", string.Empty);
                IndvidSB.FocusedSchool = temp.Trim();
            }
        }

        private void ParseIndividualStatistics(ref string Statistics)
        {
            //this is for IndividualStatBlock only fields
            //do this 1st before ParseStatistics() so it doesn't interfear with base class method
            IndividualStatisticsParser individualStatisticsParser = new IndividualStatisticsParser(_sbcommonBaseInput);
            SourceSuperScript = individualStatisticsParser.ParseIndividualStatistics(ref Statistics, CR);
        }       

        private void ParseTactics(string Tactics)
        {
            Tactics = Tactics.Replace("Tactics", string.Empty).Trim();
            //work your way back
            string temp;
            int Pos = Tactics.IndexOf("Base Statistics");
            if (Pos >= 0)
            {
                temp = Tactics.Substring(Pos);
                Tactics = Tactics.Replace(temp, string.Empty).Trim();
                temp = CommonMethods.KeepCRsIndividual(temp, CR);
                temp = temp.Replace("Base Statistics", string.Empty);
                IndvidSB.BaseStatistics = temp.Trim();
            }

            Pos = Tactics.IndexOf("see Morale ");
            if (Tactics.Length > 0)
            {
                Pos = Tactics.IndexOf("Morale ", Pos + 10);
            }

            if (Pos >= 0)
            {
                if (Pos == 0) //only Morale
                {
                    temp = Tactics;
                }
                else
                {
                    temp = Tactics.Substring(0, Pos);
                    Pos = temp.LastIndexOf(CR);
                    temp = Tactics.Substring(Pos);
                }
                
                Tactics = Tactics.Replace(temp, string.Empty).Trim();
                temp = CommonMethods.KeepCRsIndividual(temp, CR);
                temp = temp.Replace("Morale", string.Empty);
                IndvidSB.Morale = temp.Trim();
            }


            Pos = Tactics.IndexOf("During Combat ");
            if (Pos >= 0)
            {
                if (Pos == 0)  //only During Combat left
                {
                    temp = Tactics;
                }
                else
                {
                    temp = Tactics.Substring(0, Pos);
                    Pos = temp.LastIndexOf(CR);
                 //   if (Pos == -1) Pos = Tactics.Length;
                    temp = Tactics.Substring(Pos);
                }               
                Tactics = Tactics.Replace(temp, string.Empty).Trim();
                temp = CommonMethods.KeepCRsIndividual(temp, CR);
                temp = temp.Replace("During Combat", string.Empty);
                IndvidSB.DuringCombat = temp.Trim();
            }

            Pos = Tactics.IndexOf("Before Combat ");
            if (Pos >= 0)
            {
                temp = Tactics;
                Tactics = Tactics.Replace(temp, string.Empty).Trim();

                temp = CommonMethods.KeepCRsIndividual(temp, CR);
                temp = temp.Replace("Before Combat", string.Empty);
                IndvidSB.BeforeCombat = temp.Trim();
            }
        }
    }
}
