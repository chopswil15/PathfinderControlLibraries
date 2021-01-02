using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;
using System.Text.RegularExpressions;
using PathfinderContext.Services;
using CommonStatBlockInfo;
using PathfinderGlobals;
using StatBlockCommon.Individual_SB;

namespace StatBlockFormating
{
    public class IndividualStatBlock_Format : MonsterStatBlock_Format, IIndividualStatBlock_Format
    {
        private IndividualStatBlock IndivSB;
        //public List<string> ItalicPhrases { get; set; }
        //public List<string> BoldPhrases { get; set; }
        //public List<string> BoldPhrasesSpecialAbilities { get; set; }


        private bool TemplateSecond()
        {
            List<string> lowerTemplates = new List<string> { "graveknight", "ghost", "were", "vampire", "lich", "skeleton", "skeletal champion", "ogrekin", "worm that walks", "zombie" };
            foreach (string lower in lowerTemplates)
            {
                if (IndivSB.TemplatesApplied.ToLower().Contains(lower)) return true;
            }
            return false;
        }

        public string CreateFullText(IndividualStatBlock SB)
        {
            IndivSB = SB;
            base.MonSB = IndivSB;
            base.ItalicPhrases = ItalicPhrases;
            base.BoldPhrases = BoldPhrases;
            StringBuilder fullTextSB = new StringBuilder();

            fullTextSB.Append(CommonMethods.Get_CSS_Ref());

            string hold = IndivSB.Race;

            if (IndivSB.TemplatesApplied.Length > 0)
            {
                string templates = IndivSB.TemplatesApplied.Replace("young|", string.Empty).Trim();
                templates = templates.Replace("|", PathfinderConstants.SPACE).Replace(",", PathfinderConstants.SPACE)
                    .Replace("@", PathfinderConstants.SPACE).Trim();
                if (TemplateSecond())
                {
                    IndivSB.Race = IndivSB.Race + PathfinderConstants.SPACE + templates;
                }
                else
                {
                    IndivSB.Race = templates + PathfinderConstants.SPACE + IndivSB.Race;
                }
            }


            if (IndivSB.Gender.Length > 0)
            {
                IndivSB.Race = IndivSB.Gender + PathfinderConstants.SPACE + IndivSB.Race;
            }

            if (IndivSB.AgeCategory != "Adult" && !IndivSB.Race.Contains(" dragon"))
            {
                IndivSB.Race = IndivSB.AgeCategory + PathfinderConstants.SPACE + IndivSB.Race;
            }


            if (IndivSB.Race.Length > 0 && IndivSB.XP != "0" && IndivSB.Gender.Length == 0)
            {
                IndivSB.Race = char.ToUpper(IndivSB.Race[0]) + IndivSB.Race.Substring(1);
            }

            Format_Basics_Div(fullTextSB);
            hold = hold.Replace("  ", PathfinderConstants.SPACE);
            IndivSB.Race = hold;
            fullTextSB.Append(PathfinderConstants.EDIV);

            //defense
            Format_Defense_Div(fullTextSB);
            fullTextSB.Append(PathfinderConstants.EDIV);

            //Offense
            Format_Offense_Div(fullTextSB);
            Format_IndividualOffense_Div(fullTextSB);
            fullTextSB.Append(PathfinderConstants.EDIV);

            //Tactics
            if (IndivSB.BeforeCombat.Length > 0 || IndivSB.DuringCombat.Length > 0
                || IndivSB.Morale.Length > 0 || IndivSB.BaseStatistics.Length > 0)
            {
                Format_Tactics_Div(fullTextSB);
            }

            //Statisitcs
            Format_Statistics_Div(fullTextSB);
            Format_IndividualStatistics_Div(fullTextSB);
            fullTextSB.Append(PathfinderConstants.EDIV);

            if (IndivSB.SpecialAbilities.Length > 0)
            {
                base.BoldPhrasesSpecialAbilities = BoldPhrasesSpecialAbilities;
                Format_SA_Div(fullTextSB);
                if (MonSB.Note.Length > 0)
                {
                    fullTextSB.Append(PathfinderConstants.H5 + MonSB.Note + PathfinderConstants.EH5);
                }
                fullTextSB.Append(PathfinderConstants.EDIV);
            }

            string temp = fullTextSB.ToString();
            string fix = string.Empty;
            string holdPhrase = string.Empty;
            List<string> MetaMagicList = Utility.GetMetaMagicPowers();

            try
            {
                foreach (string phrase in ItalicPhrases.OrderBy(x => x.Length).Reverse())
                {
                    holdPhrase = phrase;
                    if (phrase.Contains("minor image"))
                    {
                        int Pos = 1;
                        Pos++;
                    }
                    fix = phrase.Replace((char)8217, char.Parse("'"));
                    if (temp.Contains(fix))
                    {
                        foreach (string Meta in MetaMagicList)
                        {
                            if (fix.Contains(Meta) && fix.IndexOf(Meta) == 0)
                            {
                                if (!fix.Contains("silent image") && !fix.Contains("corruption "))
                                {
                                    fix = fix.Replace(Meta, string.Empty).Trim();
                                    break;
                                }
                            }
                        }
                        temp = temp.Replace(fix, PathfinderConstants.ITACLIC + fix + PathfinderConstants.EITACLIC);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("issue with " + holdPhrase + " on MetaMagicList");
            }

            FindReplaceTextService findReplaceTextService = new FindReplaceTextService(PathfinderConstants.ConnectionString);
            findReplaceTextService.ExecuteFindReplaceOnText(ref temp);

            //  Utility.FixItalics(ref temp);

            temp = temp.Replace("<b>Int</b>imidate", StatBlockInfo.SkillNames.INTIMIDATE);
            temp = temp.Replace("<i>mad</i>ness", "madness");

            //fix false matches
            if (MonSB.Speed.Contains("fly"))
            {
                int Pos = temp.IndexOf("<b>Spd");
                Pos = temp.IndexOf("</h5>", Pos);
                fix = temp.Substring(0, Pos);
                fix = fix.Replace("<i>fly</i>", "fly");
                temp = temp.Replace(temp.Substring(0, Pos), fix);
            }

            if (MonSB.AC.Contains("touch"))
            {
                int Pos = temp.IndexOf("<b>AC");
                Pos = temp.IndexOf("</h5>", Pos);
                fix = temp.Substring(0, Pos);
                fix = fix.Replace("<i>touch</i>", "touch");
                temp = temp.Replace(temp.Substring(0, Pos), fix);
            }

            if (MonSB.SpecialAttacks.Contains("rage"))
            {
                int Pos = temp.IndexOf("<b>Special Attacks");
                Pos = temp.IndexOf("</h5>", Pos);
                fix = temp.Substring(0, Pos);
                fix = fix.Replace("<i>rage</i>", "rage");
                temp = temp.Replace(temp.Substring(0, Pos), fix);
            }

            if (MonSB.SpecialAttacks.Contains("suggestion"))
            {
                int Pos = temp.IndexOf("<b>Special Attacks");
                Pos = temp.IndexOf("</h5>", Pos);
                fix = temp.Substring(0, Pos);
                fix = fix.Replace("<i>suggestion</i>", "suggestion");
                temp = temp.Replace(temp.Substring(0, Pos), fix);
            }

            if (MonSB.SQ.Contains("ice shape"))
            {
                int Pos = temp.IndexOf("<b>SQ");
                Pos = temp.IndexOf("</h5>", Pos);
                fix = temp.Substring(0, Pos);
                fix = fix.Replace("<i>ice</i> shape", "ice shape");
                temp = temp.Replace(temp.Substring(0, Pos), fix);
            }

            if (MonSB.SQ.Contains("icewalking"))
            {
                int Pos = temp.IndexOf("<b>SQ");
                Pos = temp.IndexOf("</h5>", Pos);
                fix = temp.Substring(0, Pos);
                fix = fix.Replace("<i>ice</i>walking", "icewalking");
                temp = temp.Replace(temp.Substring(0, Pos), fix);
            }

            if (MonSB.AC_Mods.Contains("shield") && temp.Contains("<i>shield</i>"))
            {
                int Pos = temp.IndexOf("<b>hp");
                fix = temp.Substring(0, Pos);
                fix = fix.Replace("<i>shield</i>", "shield");
                temp = temp.Replace(temp.Substring(0, Pos), fix);
            }


            List<string> Supers = Utility.GetSuperScripts();
            foreach (string super in Supers)
            {
                temp = temp.Replace(super, "<sup>" + super + "</sup>");
            }
            temp = temp.Replace("<sup><sup>UC</sup>A</sup>", "<sup>UCA</sup>");



            return temp;//fullTextSB.ToString();        
        }

        private void Format_IndividualOffense_Div(StringBuilder individualOffenseDivSB)
        {
            if (IndivSB.FocusedSchool.Length > 0)
            {
                individualOffenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Focused School " + PathfinderConstants.EBOLD + IndivSB.FocusedSchool);
                if (IndivSB.ProhibitedSchools.Length > 0)
                {
                    individualOffenseDivSB.Append("; ");
                }
            }

            if (IndivSB.ThassilonianSpecialization.Length > 0)
            {
                individualOffenseDivSB.Append(PathfinderConstants.H5);
                individualOffenseDivSB.Append(PathfinderConstants.BOLD + "Thassilonian Specialization " + PathfinderConstants.EBOLD + IndivSB.ThassilonianSpecialization + PathfinderConstants.SC_SP);
            }

            if (IndivSB.ProhibitedSchools.Length > 0 && IndivSB.Environment.Length == 0)
            {
                if (IndivSB.FocusedSchool.Length == 0 && IndivSB.ThassilonianSpecialization.Length == 0)
                {
                    individualOffenseDivSB.Append(PathfinderConstants.H5);
                }

                bool found = false;
                List<string> temp = CommonMethods.GetWizardSpecializations();
                foreach (string specilization in temp)
                {
                    if (MonSB.Class.Contains(specilization.ToLower()))
                    {
                        found = true;
                        break;
                    }
                }

                string holdText = "Prohibited Schools ";
                if (found) holdText = "Opposition Schools ";
                individualOffenseDivSB.Append(PathfinderConstants.BOLD + holdText + PathfinderConstants.EBOLD + IndivSB.ProhibitedSchools);
                Regex regex = new Regex(@"[a-z]M");
                Match match = regex.Match(IndivSB.SpellsPrepared);
                if (match.Success)
                {
                    individualOffenseDivSB.Append(PathfinderConstants.SC_SP + PathfinderConstants.BOLD + "M" + PathfinderConstants.EBOLD + " Mythic spell");
                }
                individualOffenseDivSB.Append(PathfinderConstants.EH5);
            }

            //if (IndivSB.BaseStatistics.Length > 0)
            //{

            //    if (IndivSB.BeforeCombat.Length == 0 && IndivSB.Morale.Length == 0 &&
            //        IndivSB.DuringCombat.Length == 0)//no tatctics section
            //    {
            //        string temp = IndivSB.BaseStatistics;
            //        foreach (string phrase in BoldPhrases)
            //        {
            //            if (temp.IndexOf(phrase) >= 0)
            //            {
            //                temp = temp.Replace(phrase, PathfinderConstants.BOLD + phrase + PathfinderConstants.EBOLD);
            //            }
            //        }
            //        individualOffenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Base Statistics " + PathfinderConstants.EBOLD + temp + PathfinderConstants.EH5); 
            //    }
            //}           
        }

        private void Format_IndividualStatistics_Div(StringBuilder individualStatisticsDivSB)
        {
            if (IndivSB.Note.Length > 0 && IndivSB.SpecialAbilities.Length == 0)
            {
                individualStatisticsDivSB.Append(PathfinderConstants.H5 + IndivSB.Note + PathfinderConstants.EH5);
            }
        }

        private void Format_Tactics_Div(StringBuilder tacticsDivSB)
        {
            tacticsDivSB.Append(PathfinderConstants.LINE);
            tacticsDivSB.Append(PathfinderConstants.DIV + PathfinderConstants.H5 + PathfinderConstants.BOLD + "TACTICS" + PathfinderConstants.EBOLD + PathfinderConstants.EH5 + PathfinderConstants.EDIV);
            tacticsDivSB.Append(PathfinderConstants.LINE + PathfinderConstants.DIV);

            if (IndivSB.BeforeCombat.Length > 0)
            {
                tacticsDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Before Combat " + PathfinderConstants.EBOLD + IndivSB.BeforeCombat + PathfinderConstants.EH5);
            }

            if (IndivSB.DuringCombat.Length > 0)
            {
                tacticsDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "During Combat " + PathfinderConstants.EBOLD + IndivSB.DuringCombat + PathfinderConstants.EH5);
            }

            if (IndivSB.Morale.Length > 0)
            {
                tacticsDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Morale " + PathfinderConstants.EBOLD + IndivSB.Morale + PathfinderConstants.EH5);
            }

            if (IndivSB.BaseStatistics.Length > 0)
            {
                string temp = IndivSB.BaseStatistics;
                foreach (string phrase in BoldPhrases)
                {
                    if (temp.Contains(phrase)) temp = temp.Replace(phrase, PathfinderConstants.BOLD + phrase + PathfinderConstants.EBOLD);
                }
                temp = temp.Replace("da<i>mage</i>", "damage");
                temp = temp.Replace("<i>heal</i>ing", "healing");
                temp = temp.Replace("<i>jump</i>ing", "jumping");
                temp = temp.Replace("f<i>light</i>", "flight");
                temp = temp.Replace("<i><i>resist</i> energy</i> ", "<i>resist energy</i> ");
                temp = temp.Replace("<b>Dex</b>terity", "Dexterity");
                temp = temp.Replace("<b>Wis</b>dom", " Wisdom");
                tacticsDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Base Statistics " + PathfinderConstants.EBOLD + temp + PathfinderConstants.EH5);
            }

            tacticsDivSB.Append(PathfinderConstants.EDIV);
        }
    }
}
