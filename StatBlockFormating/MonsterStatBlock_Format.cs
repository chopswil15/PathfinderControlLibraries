using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStrings;
using PathfinderContext.Services;
using Utilities;
using CommonStatBlockInfo;
using PathfinderGlobals;
using StatBlockCommon.Monster_SB;

namespace StatBlockFormating
{
    public class MonsterStatBlock_Format : IMonsterStatBlock_Format
    {
        protected string CRLF = Environment.NewLine;
        public List<string> ItalicPhrases { get; set; }
        public List<string> BoldPhrases { get; set; }
        public List<string> BoldPhrasesSpecialAbilities { get; set; }
        public string SourceSuperScript { get; set; }

        public MonsterStatBlock MonSB { get; set; }

        public MonsterStatBlock_Format()
        {
            SourceSuperScript = string.Empty;
        }

        public string CreateFullText(MonsterStatBlock statBlock)
        {
            MonSB = statBlock;
            StringBuilder fullTextSB = new StringBuilder();

            // Get_CSS_Text(fullTextSB);            
            fullTextSB.Append(CommonMethods.Get_CSS_Ref());
            Format_MonsterIntro_Div(fullTextSB);

            Format_Basics_Div(fullTextSB);
            fullTextSB.Append(PathfinderConstants.EDIV);

            //defense
            Format_Defense_Div(fullTextSB);
            fullTextSB.Append(PathfinderConstants.EDIV);

            //Offense
            Format_Offense_Div(fullTextSB);
            fullTextSB.Append(PathfinderConstants.EDIV);

            if (MonSB.BeforeCombat.Length > 0 || MonSB.DuringCombat.Length > 0 || MonSB.Morale.Length > 0 || MonSB.BaseStatistics.Length > 0)
            {
                Format_Tactics_Div(fullTextSB);
            }

            //Statisitcs
            Format_Statistics_Div(fullTextSB);
            fullTextSB.Append(PathfinderConstants.EDIV);

            //Ecology
            Format_Ecology_Div(fullTextSB);
            if (MonSB.Note.Length > 0 && MonSB.SpecialAbilities.Length == 0)
            {
                fullTextSB.Append(PathfinderConstants.H5 + MonSB.Note + PathfinderConstants.EH5);
            }
            fullTextSB.Append(PathfinderConstants.EDIV);

            if (MonSB.SpecialAbilities.Length > 0)
            {
                Format_SA_Div(fullTextSB);
                if (MonSB.Note.Length > 0)
                {
                    fullTextSB.Append(PathfinderConstants.H5 + MonSB.Note + PathfinderConstants.EH5);
                }
                fullTextSB.Append(PathfinderConstants.EDIV);
            }

            fullTextSB.Append(PathfinderConstants.BREAK);
            //Description
            Format_Description_Div(fullTextSB);

            string temp = fullTextSB.ToString();
            string fix;
            foreach (string phrase in ItalicPhrases)
            {
                fix = phrase.Replace((char)8217, char.Parse("'"));
                if (temp.Contains(fix))
                {
                    if (!MonSB.Description_Visual.Contains(fix))
                    {
                        temp = temp.Replace(fix, PathfinderConstants.ITACLIC + fix + PathfinderConstants.EITACLIC);
                    }
                }
            }

            FindReplaceTextService findReplaceTextService = new FindReplaceTextService(PathfinderConstants.ConnectionString);
            findReplaceTextService.ExecuteFindReplaceOnText(ref temp);
            // Utility.FixItalics(ref temp);

            temp = temp.Replace("<b>Int</b>imidate", StatBlockInfo.SkillNames.INTIMIDATE);

            //fix false matches
            if (MonSB.Senses.Contains("low-light"))
            {
                temp = temp.Replace("low-<i>light</i>", "low-light");
            }


            if (MonSB.AC_Mods.Contains("shield") && temp.Contains("<i>shield</i>"))
            {
                int Pos = temp.IndexOf("<b>hp");
                fix = temp.Substring(0, Pos);
                fix = fix.Replace("<i>shield</i>", "shield");
                temp = temp.Replace(temp.Substring(0, Pos), fix);
            }

            if (MonSB.Speed.Contains("fly") && temp.Contains("<i>fly</i>"))
            {
                int Pos = temp.IndexOf("<b>Spd");
                Pos = temp.IndexOf("</h5>", Pos);
                fix = temp.Substring(0, Pos);
                fix = fix.Replace("<i>fly</i>", "fly");
                temp = temp.Replace(temp.Substring(0, Pos), fix);
            }

            //no itlaics in entire Defensive Abilities  line
            int Pos3 = temp.IndexOf(", <b>Will");
            int Pos2 = temp.IndexOf("Spd");
            fix = temp.Substring(Pos3, Pos2 - Pos3);
            string hold = fix;
            fix = fix.Replace("<i>", string.Empty);
            fix = fix.Replace("</i>", string.Empty);
            temp = temp.Replace(hold, fix);


            List<string> Supers = Utility.GetSuperScripts();
            foreach (string super in Supers)
            {
                temp = temp.Replace(super, "<sup>" + super + "</sup>");
            }
            temp = temp.Replace("U<sup>M</sup>", "<sup>UM</sup>");
            temp = temp.Replace("CONSTR<sup>UC</sup>TION", "CONSTRUCTION");
            temp = temp.Replace("<sup><sup>UC</sup>A</sup>", "<sup>UCA</sup>");
            temp = temp.Replace("<sup>HA</sup>BITAT", "HABITAT");

            return temp;
        }

        protected void Format_Basics_Div(StringBuilder formatBasicsDivSB)
        {
            string intHold = MonSB.Init;
            formatBasicsDivSB.Append("<div class=" + PathfinderConstants.QUOTE + "heading" + PathfinderConstants.QUOTE + ">");

            formatBasicsDivSB.Append("<p class=" + PathfinderConstants.QUOTE + "alignleft" + PathfinderConstants.QUOTE + ">" + MonSB.name);

            if (MonSB.AlternateNameForm.Length > 0)
            {
                formatBasicsDivSB.Append(" (" + MonSB.AlternateNameForm + PathfinderConstants.PAREN_RIGHT);
            }
            formatBasicsDivSB.Append("</p>" + "<p class=" + PathfinderConstants.QUOTE + "alignright" + PathfinderConstants.QUOTE + ">" + "CR " + MonSB.CR);
            if (MonSB.MR > 0)
            {
                formatBasicsDivSB.Append("/MR " + MonSB.MR.ToString());
            }
            formatBasicsDivSB.Append("</p>");
            formatBasicsDivSB.Append("<div style=" + PathfinderConstants.QUOTE + "clear: both;" + PathfinderConstants.QUOTE + "></div></div>");

            formatBasicsDivSB.Append(PathfinderConstants.DIV);
            formatBasicsDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "XP " + PathfinderConstants.EBOLD + MonSB.XP + PathfinderConstants.EH5);
            if (MonSB.Race.Length > 0)
            {
                formatBasicsDivSB.Append(PathfinderConstants.H5);
                if (MonSB.UniqueMonster)
                {
                    formatBasicsDivSB.Append("Unique ");
                }
                if (MonSB.Variant)
                {
                    formatBasicsDivSB.Append("Variant ");
                }
                string temp = MonSB.Class;
                int Pos = temp.IndexOf("cleric of");
                if (Pos >= 0)
                {
                    temp = temp.Substring(Pos + ("cleric of").Length).Trim();
                    temp = MonSB.Class.Replace(temp, temp.ProperCase());
                }
                if (!(MonSB.XP == "0" && MonSB.Race.Contains("ompanion")))
                {
                    string tempName = MonSB.Race;
                    if (MonSB.name.ToLower() == MonSB.Race.ToLower())
                    {
                        if (MonSB.TemplatesApplied.Length > 0)
                        {
                            List<string> templates = MonSB.TemplatesApplied.Split('|').ToList();
                            templates.Remove("");
                            foreach (string template in templates)
                            {
                                if (tempName.ToLower().IndexOf(template) >= 0)
                                {
                                    // tempName = tempName.ToLower().Replace(template, string.Empty).Trim();
                                }
                            }
                        }
                    }

                    if (MonSB.Gender.Length == 0)
                    {
                        formatBasicsDivSB.Append(tempName.ProperCase() + PathfinderConstants.SPACE + temp);
                    }
                    else
                    {
                        formatBasicsDivSB.Append(tempName + PathfinderConstants.SPACE + temp);
                    }
                }

                //if (MonSB.ClassArchetypes.Length > 0)
                //{
                //   // formatBasicsDivSB.Append(" (" + MonSB.ClassArchetypes + PathfinderConstants.PAREN_RIGHT);
                //}

                if (MonSB.MonsterSource.Length > 0)
                {
                    formatBasicsDivSB.Append(" (" + MonSB.MonsterSource + PathfinderConstants.PAREN_RIGHT);
                }

                formatBasicsDivSB.Append(PathfinderConstants.EH5);
            }
            else
            {
                if (MonSB.MonsterSource.Length > 0)
                {
                    formatBasicsDivSB.Append(PathfinderConstants.H5 + MonSB.MonsterSource + PathfinderConstants.EH5);
                }
            }
            formatBasicsDivSB.Append(PathfinderConstants.H5 + MonSB.Alignment + PathfinderConstants.SPACE + MonSB.Size + PathfinderConstants.SPACE + MonSB.Type + PathfinderConstants.SPACE + MonSB.SubType + PathfinderConstants.EH5);

            if (MonSB.Init.Contains("MA"))
            {
                // intHold = MonSB.Init.Replace("MA", "<sup>MA</sup>");
            }
            else if (MonSB.Init.Contains("M"))
            {
                intHold = MonSB.Init.Replace("M", "<sup>M</sup>");
            }
            formatBasicsDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Init " + PathfinderConstants.EBOLD + intHold + PathfinderConstants.SC_SP + PathfinderConstants.BOLD + "Senses " + PathfinderConstants.EBOLD + MonSB.Senses + PathfinderConstants.EH5);
            if (MonSB.Aura.Length > 0)
            {
                formatBasicsDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Aura " + PathfinderConstants.EBOLD + MonSB.Aura + PathfinderConstants.EH5);
            }
        }

        private void Format_MonsterIntro_Div(StringBuilder monsterIntroDivSB)
        {
            string temp;
            monsterIntroDivSB.Append(PathfinderConstants.DIV);

            if (MonSB.Group.Length > 0)
            {
                string temp2 = MonSB.name.Replace(MonSB.Group, string.Empty);
                temp = MonSB.Group + PathfinderConstants.COM_SP + temp2.Trim();
            }
            else
            {
                temp = MonSB.name;
            }

            monsterIntroDivSB.Append(PathfinderConstants.H2 + temp + PathfinderConstants.EH2);
            monsterIntroDivSB.Append(PathfinderConstants.H3 + PathfinderConstants.ITACLIC + MonSB.Description_Visual + PathfinderConstants.EITACLIC + PathfinderConstants.EH3);
            monsterIntroDivSB.Append(PathfinderConstants.BREAK);
            monsterIntroDivSB.Append(PathfinderConstants.EDIV);
        }

        private void Format_Description_Div(StringBuilder descriptionDivSB)
        {
            string temp, hold;
            descriptionDivSB.Append(PathfinderConstants.DIV);
            temp = MonSB.Description;

            hold = "•";
            if (temp.Contains(hold))//list
            {
                int Pos = temp.IndexOf(hold);
                temp = temp.Insert(Pos, PathfinderConstants.LIST);
                temp += PathfinderConstants.ELIST;
                temp = temp.Replace(hold, PathfinderConstants.LIST_ITEM);
            }

            hold = Environment.NewLine + "Ecology";
            int Pos2 = temp.IndexOf(hold);
            if (Pos2 == -1)
            {
                hold = "\nEcology";
                Pos2 = temp.IndexOf(hold);
                if (Pos2 == -1)
                {
                    hold = "Ecology";
                    Pos2 = temp.IndexOf(hold);
                }
            }
            if (Pos2 > 0)
            {
                temp = temp.Replace(hold, PathfinderConstants.BOLD + Environment.NewLine + "Ecology" + PathfinderConstants.EBOLD + Environment.NewLine);
            }

            hold = Environment.NewLine + MonSB.name + " Society";
            Pos2 = temp.IndexOf(hold);
            if (Pos2 == -1)
            {
                hold = "\n" + MonSB.name + " Society";
                Pos2 = temp.IndexOf(hold);
            }

            if (Pos2 > 0)
            {
                temp = temp.Replace(hold, PathfinderConstants.BOLD + Environment.NewLine + MonSB.name + " Society" + PathfinderConstants.EBOLD + Environment.NewLine);
            }

            hold = Environment.NewLine + "Variants";
            Pos2 = temp.IndexOf(hold);
            if (Pos2 == -1)
            {
                hold = "\nVariants";
                Pos2 = temp.IndexOf(hold);
                if (Pos2 == -1)
                {
                    hold = "Variants";
                    Pos2 = temp.IndexOf(hold);
                }
            }

            if (Pos2 > 0)
            {
                temp = temp.Replace(hold, PathfinderConstants.BOLD + Environment.NewLine + "Variants" + PathfinderConstants.EBOLD + Environment.NewLine);
            }

            hold = Environment.NewLine + "Variants";
            Pos2 = temp.IndexOf(hold);
            if (Pos2 == -1)
            {
                hold = "\nVariants";
                Pos2 = temp.IndexOf(hold);
            }

            if (Pos2 > 0)
            {
                temp = temp.Replace(hold, PathfinderConstants.BOLD + Environment.NewLine + "Variants" + PathfinderConstants.EBOLD + Environment.NewLine);
            }

            hold = Environment.NewLine + "Habitat & Society";
            Pos2 = temp.IndexOf(hold);
            if (Pos2 == -1)
            {
                hold = "\nHabitat & Society";
                Pos2 = temp.IndexOf(hold);
                if (Pos2 == -1)
                {
                    hold = "Habitat & Society";
                    Pos2 = temp.IndexOf(hold);
                }
                if (Pos2 == -1)
                {
                    hold = "Habitat &amp; Society";
                    Pos2 = temp.IndexOf(hold);
                }
            }

            if (Pos2 > 0)
            {
                temp = temp.Replace(hold, PathfinderConstants.BOLD + Environment.NewLine + "Habitat & Society" + PathfinderConstants.EBOLD + Environment.NewLine);
            }

            hold = Environment.NewLine + "Habitat and Society";
            Pos2 = temp.IndexOf(hold);
            if (Pos2 == -1)
            {
                hold = "\nHabitat and Society";
                Pos2 = temp.IndexOf(hold);
            }

            if (Pos2 > 0)
            {
                temp = temp.Replace(hold, PathfinderConstants.BOLD + Environment.NewLine + "Habitat & Society" + PathfinderConstants.EBOLD + Environment.NewLine);
            }


            if (temp.Contains(" Companions"))
            {
                FormatCompainions(ref temp);
            }

            if (temp.Contains(" Characters") || temp.Contains(" CHARACTERS"))
            {
                FormatCharacters(ref temp);
            }

            temp = temp.Replace("Constr<sup>uc</sup>tion", "Construction");
            if (temp.Contains("Construction") && !temp.Contains(". Construction"))
            {
                FormatConstruction(ref temp);
            }
            if (temp.Contains("LESHY"))
            {
                if (temp.Contains("Creating a") || temp.Contains("CREATING A"))
                {
                    FormatGrowing(ref temp);
                }
            }

            if (temp.Contains("Poison ("))
            {
                FormatPoison(ref temp);
            }

            string temp2;
            foreach (string bold in BoldPhrasesSpecialAbilities)
            {
                temp2 = bold.Replace((char)8217, Char.Parse("'"));
                temp = temp.Replace(temp2 + PathfinderConstants.SPACE, PathfinderConstants.EH5 + PathfinderConstants.H5 + PathfinderConstants.BOLD + temp2 + PathfinderConstants.SPACE + PathfinderConstants.EBOLD);
            }

            temp = PathfinderConstants.PARA + temp;
            temp = "<p>" + temp;
            temp = temp.Replace(CRLF + PathfinderConstants.BOLD, "</p><p>" + PathfinderConstants.BOLD);
            temp = temp.Replace(CRLF, "</p><p>");
            temp = temp + "</p>";
            descriptionDivSB.Append(PathfinderConstants.H4 + temp + PathfinderConstants.EH4);
            descriptionDivSB.Append(PathfinderConstants.EDIV);
        }

        private void FormatPoison(ref string poisonText)
        {
            try
            {
                int Pos = poisonText.IndexOf("Poison (");
                string temp = poisonText.Substring(Pos);

                Pos = temp.IndexOf(".");
                if (Pos == -1) Pos = temp.Length;
                temp = temp.Substring(0, Pos);
                string Hold = temp;

                temp = temp.Replace("save", PathfinderConstants.ITACLIC + "save" + PathfinderConstants.EITACLIC)
                    .Replace("frequency", PathfinderConstants.ITACLIC + "frequency" + PathfinderConstants.EITACLIC)
                    .Replace("effect", PathfinderConstants.ITACLIC + "effect" + PathfinderConstants.EITACLIC)
                    .Replace("cure", PathfinderConstants.ITACLIC + "cure" + PathfinderConstants.EITACLIC);
                poisonText = poisonText.Replace(Hold, temp);
            }
            catch (Exception ex)
            {
                throw new Exception("FormatPoison-" + ex.Message);
            }
        }

        private void FormatCharacters(ref string text)
        {
            int Pos = text.IndexOf(MonSB.name + " Characters");

            if (Pos == -1)
            {
                Pos = text.IndexOf(MonSB.name.ToUpper() + " CHARACTERS");
                if (Pos > 0)
                {
                    text = text.Replace(MonSB.name.ToUpper() + " CHARACTERS", MonSB.name + " Characters");
                }
            }

            text = text.Replace(MonSB.name + " Characters", PathfinderConstants.BREAK + PathfinderConstants.BOLD + MonSB.name + " Characters" + PathfinderConstants.EBOLD + PathfinderConstants.BREAK);
            for (int a = BoldPhrasesSpecialAbilities.Count - 1; a >= 0; a--)
            {
                if (text.Contains(BoldPhrasesSpecialAbilities[a]))
                {
                    text = text.Replace(BoldPhrasesSpecialAbilities[a], PathfinderConstants.BREAK + PathfinderConstants.BOLD + BoldPhrasesSpecialAbilities[a] + PathfinderConstants.EBOLD);
                    BoldPhrasesSpecialAbilities.Remove(BoldPhrasesSpecialAbilities[a]);
                }
            }
        }

        private void FormatCompainions(ref string compainionsText)
        {
            compainionsText = compainionsText.Replace(MonSB.name + " Companions", PathfinderConstants.BREAK + PathfinderConstants.BOLD + MonSB.name + " Companions" + PathfinderConstants.EBOLD + PathfinderConstants.BREAK);
            for (int a = BoldPhrasesSpecialAbilities.Count - 1; a >= 0; a--)
            {
                if (compainionsText.Contains(BoldPhrasesSpecialAbilities[a]))
                {
                    compainionsText = compainionsText.Replace(BoldPhrasesSpecialAbilities[a], PathfinderConstants.BOLD + BoldPhrasesSpecialAbilities[a] + PathfinderConstants.EBOLD);
                    BoldPhrasesSpecialAbilities.Remove(BoldPhrasesSpecialAbilities[a]);
                }
            }
        }

        private void FormatGrowing(ref string growingText)
        {
            growingText = growingText.Replace("CREATING", "Creating")
                .Replace("RITUAL", "Ritual")
                .Replace(MonSB.name.ToUpper(), MonSB.name.ProperCase());
            int Pos5 = ("Creating A " + MonSB.name.ProperCase()).Length;
            int headerStart = growingText.IndexOf("Creating A " + MonSB.name.ProperCase());
            string header = growingText.Substring(headerStart, Pos5);
            growingText = growingText.Replace(header, 
                          PathfinderConstants.BREAK + PathfinderConstants.BOLD + header + PathfinderConstants.EBOLD + PathfinderConstants.BREAK)
                .Replace("Ritual", 
                         PathfinderConstants.BREAK + PathfinderConstants.BOLD + "Ritual" + PathfinderConstants.EBOLD);

            string begin = PathfinderConstants.BREAK + "<div class=" + PathfinderConstants.QUOTE + "heading" + PathfinderConstants.QUOTE + ">";
            begin += "<p class=" + PathfinderConstants.QUOTE + "alignleft" + PathfinderConstants.QUOTE + ">";
            //  int Pos = growingText.IndexOf(PathfinderConstants.BOLD + "Construction");
            int Pos2 = growingText.LastIndexOf("; Price");
            if (Pos2 == -1)
            {
                return;
            }
            int Pos = growingText.LastIndexOf("CL", Pos2);
            Pos = growingText.LastIndexOf(MonSB.name, Pos);
            if (Pos == -1)
            {
                Pos = growingText.IndexOf(PathfinderConstants.BOLD + "Creating");
                Pos = growingText.IndexOf(MonSB.name.ToUpper(), Pos);
                if (Pos > 0) growingText = growingText.Replace(MonSB.name.ToUpper(), MonSB.name.ProperCase());
            }
            growingText = growingText.Insert(Pos, begin);
            string end = "<div style=" + PathfinderConstants.QUOTE + "clear: both;" + PathfinderConstants.QUOTE + "></div>";
            //Pos = growingText.IndexOf(PathfinderConstants.BOLD + "Construction");
            //Pos = growingText.IndexOf(MonSB.Name,Pos);
            Pos2 = growingText.LastIndexOf("; Price");
            Pos = growingText.LastIndexOf("CL", Pos2);
            Pos = growingText.LastIndexOf(MonSB.name, Pos);
            growingText = growingText.Insert(Pos + MonSB.name.Length, end);

            growingText = growingText.Replace("CL", PathfinderConstants.BOLD + "CL" + PathfinderConstants.EBOLD)
                .Replace("Price", PathfinderConstants.BOLD + "Price" + PathfinderConstants.EBOLD);
            Pos = growingText.IndexOf("Price");
            Pos = growingText.IndexOf(PathfinderConstants.BOLD + "Ritual", Pos);
            growingText = growingText.Insert(Pos, PathfinderConstants.LINE);
            Pos = growingText.IndexOf("Price");
            string test = "Ritual" + PathfinderConstants.EBOLD;
            Pos = growingText.IndexOf(test, Pos);
            growingText = growingText.Insert(Pos + test.Length, PathfinderConstants.LINE);

            test = "Ritual" + PathfinderConstants.EBOLD;
            Pos = growingText.IndexOf(test);
            growingText = growingText.Insert(Pos + test.Length, PathfinderConstants.BREAK);

            growingText = growingText.Replace("Requirements", PathfinderConstants.BOLD + "Requirements" + PathfinderConstants.EBOLD)
                .Replace("Skill", PathfinderConstants.BOLD + "Skill" + PathfinderConstants.EBOLD)
                .Replace("Cost", PathfinderConstants.BOLD + "Cost" + PathfinderConstants.EBOLD);
        }

        private void FormatConstruction(ref string constructionTxt)
        {
            constructionTxt = constructionTxt.Replace("CONSTRUCTION", "Construction")
                .Replace(MonSB.name.ToUpper(), MonSB.name.ProperCase())
                .Replace("Construction", PathfinderConstants.BREAK + PathfinderConstants.BOLD + "Construction" + PathfinderConstants.EBOLD);

            string begin = PathfinderConstants.BREAK + "<div class=" + PathfinderConstants.QUOTE + "heading" + PathfinderConstants.QUOTE + ">";
            begin += "<p class=" + PathfinderConstants.QUOTE + "alignleft" + PathfinderConstants.QUOTE + ">";
            //  int Pos = constructionTxt.IndexOf(PathfinderConstants.BOLD + "Construction");
            int Pos2 = constructionTxt.LastIndexOf("; Price");
            if (Pos2 == -1)
            {
                return;
            }
            int Pos = constructionTxt.LastIndexOf("CL", Pos2);
            Pos = constructionTxt.LastIndexOf(MonSB.name, Pos);
            if (Pos == -1)
            {
                Pos = constructionTxt.IndexOf(PathfinderConstants.BOLD + "Construction");
                Pos = constructionTxt.IndexOf(MonSB.name.ToUpper(), Pos);
                if (Pos > 0) constructionTxt = constructionTxt.Replace(MonSB.name.ToUpper(), MonSB.name.ProperCase());
            }

            string nameToUse = MonSB.name;

            if (Pos == -1)
            {
                Pos = constructionTxt.LastIndexOf("CL", Pos2);
                Pos = constructionTxt.LastIndexOf(MonSB.Group.ToUpper(), Pos);
                if (Pos > 0) nameToUse = MonSB.Group;
            }

            try
            {
                constructionTxt = constructionTxt.Insert(Pos, begin);
            }
            catch (Exception ex)
            {
                throw new Exception("FormatConstruction(): Unable to find monster name-" + MonSB.name);
            }
            string end = "<div style=" + PathfinderConstants.QUOTE + "clear: both;" + PathfinderConstants.QUOTE + "></div>";
            //Pos = constructionTxt.IndexOf(PathfinderConstants.BOLD + "Construction");
            //Pos = constructionTxt.IndexOf(MonSB.Name,Pos);
            Pos2 = constructionTxt.LastIndexOf("; Price");
            Pos = constructionTxt.LastIndexOf("CL", Pos2);
            Pos = constructionTxt.LastIndexOf(nameToUse, Pos);
            constructionTxt = constructionTxt.Insert(Pos + nameToUse.Length, end);

            constructionTxt = constructionTxt.Replace("CL", PathfinderConstants.BOLD + "CL" + PathfinderConstants.EBOLD)
                .Replace("Price", PathfinderConstants.BOLD + "Price" + PathfinderConstants.EBOLD);
            Pos = constructionTxt.IndexOf("Price");
            Pos = constructionTxt.IndexOf(PathfinderConstants.BOLD + "Construction", Pos);
            constructionTxt = constructionTxt.Insert(Pos, PathfinderConstants.LINE);
            Pos = constructionTxt.IndexOf("Price");
            string test = "Construction" + PathfinderConstants.EBOLD;
            Pos = constructionTxt.IndexOf(test, Pos);
            constructionTxt = constructionTxt.Insert(Pos + test.Length, PathfinderConstants.LINE);

            test = "Construction" + PathfinderConstants.EBOLD;
            Pos = constructionTxt.IndexOf(test);
            constructionTxt = constructionTxt.Insert(Pos + test.Length, PathfinderConstants.BREAK);

            constructionTxt = constructionTxt.Replace("Requirements", PathfinderConstants.BOLD + "Requirements" + PathfinderConstants.EBOLD)
                .Replace("Skill", PathfinderConstants.BOLD + "Skill" + PathfinderConstants.EBOLD)
                .Replace("Cost", PathfinderConstants.BOLD + "Cost" + PathfinderConstants.EBOLD);
        }

        protected void Format_SA_Div(StringBuilder specialAbilitiesDivSB)
        {
            specialAbilitiesDivSB.Append(PathfinderConstants.LINE + PathfinderConstants.DIV + PathfinderConstants.H5 + PathfinderConstants.BOLD + "SPECIAL ABILITIES" + PathfinderConstants.EBOLD + PathfinderConstants.EH5 + PathfinderConstants.EDIV);
            specialAbilitiesDivSB.Append(PathfinderConstants.LINE + PathfinderConstants.DIV);
            string temp = MonSB.SpecialAbilities;
            FormatSpecialAbilities(ref temp);
            specialAbilitiesDivSB.Append(temp);
        }

        private void Format_Ecology_Div(StringBuilder ecologyDivSB)
        {
            ecologyDivSB.Append(PathfinderConstants.LINE);
            ecologyDivSB.Append(PathfinderConstants.DIV + PathfinderConstants.H5 + PathfinderConstants.BOLD + "ECOLOGY" + PathfinderConstants.EBOLD + PathfinderConstants.EH5 + PathfinderConstants.EDIV);
            ecologyDivSB.Append(PathfinderConstants.LINE + PathfinderConstants.DIV);

            ecologyDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Environment " + PathfinderConstants.EBOLD + MonSB.Environment + PathfinderConstants.EH5);
            ecologyDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Organization " + PathfinderConstants.EBOLD + MonSB.Organization + PathfinderConstants.EH5);
            ecologyDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Treasure " + PathfinderConstants.EBOLD + MonSB.Treasure + PathfinderConstants.EH5);
        }

        protected void Format_Offense_Div(StringBuilder offenseDivSB)
        {
            string temp = string.Empty;
            offenseDivSB.Append(PathfinderConstants.LINE);
            offenseDivSB.Append(PathfinderConstants.DIV + PathfinderConstants.H5 + PathfinderConstants.BOLD + "OFFENSE" + PathfinderConstants.EBOLD + PathfinderConstants.EH5 + PathfinderConstants.EDIV);
            offenseDivSB.Append(PathfinderConstants.LINE + PathfinderConstants.DIV);

            offenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Spd " + PathfinderConstants.EBOLD + MonSB.Speed);
            if (MonSB.Speed_Mod.Length > 0)
            {
                offenseDivSB.Append(PathfinderConstants.SC_SP + MonSB.Speed_Mod);
            }
            offenseDivSB.Append(PathfinderConstants.EH5);
            if (MonSB.Melee.Length > 0)
            {
                temp = MonSB.Melee;
                FormatCombat(ref temp);
                temp = temp.Replace("†", "&#8224;");
                offenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Melee " + PathfinderConstants.EBOLD + temp + PathfinderConstants.EH5);
            }

            if (MonSB.Ranged.Length > 0)
            {
                temp = MonSB.Ranged;
                FormatCombat(ref temp);
                offenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Ranged " + PathfinderConstants.EBOLD + temp + PathfinderConstants.EH5);
            }
            if (MonSB.Space.Length > 0)
            {
                offenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Space " + PathfinderConstants.EBOLD + MonSB.Space + PathfinderConstants.SC_SP + PathfinderConstants.BOLD + "Reach " + PathfinderConstants.EBOLD + MonSB.Reach + PathfinderConstants.EH5);
            }

            if (MonSB.SpecialAttacks.Length > 0)
            {
                offenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Special Attacks " + PathfinderConstants.EBOLD + MonSB.SpecialAttacks + PathfinderConstants.EH5);
            }
            if (MonSB.PsychicMagic.Length > 0)
            {
                temp = PathfinderConstants.H5 + MonSB.PsychicMagic;
                FormatSpells(ref temp);
                FormatSpellsItalics(ref temp);
                offenseDivSB.Append(temp + PathfinderConstants.EH5);
            }
            if (MonSB.Implements.Length > 0)
            {
                temp = PathfinderConstants.H5 + MonSB.Implements;
                FormatImplements(ref temp);
                // FormatSpellsItalics(ref temp);
                offenseDivSB.Append(temp + PathfinderConstants.EH5);
            }
            if (MonSB.KineticistWildTalents.Length > 0)
            {
                temp = PathfinderConstants.H5 + MonSB.KineticistWildTalents;
                FormatKineticistWildTalents(ref temp);
                //  FormatSpellsItalics(ref temp);
                offenseDivSB.Append(temp + PathfinderConstants.EH5);
            }

            if (MonSB.SpellLikeAbilities.Length > 0)
            {
                temp = PathfinderConstants.H5 + MonSB.SpellLikeAbilities;
                FormatSpells(ref temp);
                FormatSpellsItalics(ref temp);
                offenseDivSB.Append(temp + PathfinderConstants.EH5);
            }

            if (MonSB.SpellsKnown.Length > 0)
            {
                temp = PathfinderConstants.H5 + MonSB.SpellsKnown;
                FormatSpells(ref temp);
                FormatSpellsItalics(ref temp);
                offenseDivSB.Append(temp + PathfinderConstants.EH5);
                bool foundDomain = false;
                if (MonSB.SpellDomains.Length > 0 && MonSB.SpellsPrepared.Length == 0)
                {
                    offenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "D" + PathfinderConstants.EBOLD + " domain spell; " + PathfinderConstants.BOLD + "Domains " + PathfinderConstants.EBOLD + MonSB.SpellDomains);
                    foundDomain = true;
                }

                if (temp.Contains("<sup>M"))
                {
                    if (foundDomain) offenseDivSB.Append(PathfinderConstants.SC_SP);
                    if (!foundDomain) offenseDivSB.Append(PathfinderConstants.H5);
                    offenseDivSB.Append(PathfinderConstants.BOLD + "M" + PathfinderConstants.EBOLD + " Mythic spell");
                    if (!foundDomain) offenseDivSB.Append(PathfinderConstants.EH5);
                }
                else if (foundDomain) offenseDivSB.Append(PathfinderConstants.EH5);
            }

            if (MonSB.Bloodline.Length > 0)
            {
                offenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Bloodline " + PathfinderConstants.EBOLD + MonSB.Bloodline + PathfinderConstants.EH5);
            }

            if (MonSB.SpellsPrepared.Length > 0)
            {
                temp = PathfinderConstants.H5 + MonSB.SpellsPrepared;
                FormatSpells(ref temp);
                FormatSpellsItalics(ref temp);
                offenseDivSB.Append(temp + PathfinderConstants.EH5);
                bool foundSpellsPrepared = false;
                if (MonSB.SpellDomains.Length > 0)
                {
                    offenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "D" + PathfinderConstants.EBOLD + " domain spell; " + PathfinderConstants.BOLD + "Domains " + PathfinderConstants.EBOLD + MonSB.SpellDomains);
                    foundSpellsPrepared = true;
                }
                if (temp.Contains("<sup>M") && MonSB.ProhibitedSchools.Length == 0)
                {
                    if (foundSpellsPrepared) offenseDivSB.Append(PathfinderConstants.SC_SP);
                    if (!foundSpellsPrepared) offenseDivSB.Append(PathfinderConstants.H5);
                    offenseDivSB.Append(PathfinderConstants.BOLD + "M" + PathfinderConstants.EBOLD + " Mythic spell");
                    if (!foundSpellsPrepared) offenseDivSB.Append(PathfinderConstants.EH5);
                }
                else if (foundSpellsPrepared) offenseDivSB.Append(PathfinderConstants.EH5);
            }

            if (MonSB.ExtractsPrepared.Length > 0)
            {
                temp = PathfinderConstants.H5 + MonSB.ExtractsPrepared;
                FormatSpells(ref temp);
                FormatSpellsItalics(ref temp);
                offenseDivSB.Append(temp + PathfinderConstants.EH5);
            }


            if (MonSB.AdditionalExtractsKnown.Length > 0)
            {
                offenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Additional Extracts Known " + PathfinderConstants.EBOLD + MonSB.AdditionalExtractsKnown + PathfinderConstants.EH5);
            }

            if (MonSB.PsychicDiscipline.Length > 0)
            {
                offenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Psychic Discipline " + PathfinderConstants.EBOLD + MonSB.PsychicDiscipline + PathfinderConstants.EH5);
            }

            if (MonSB.Mystery.Length > 0)
            {
                offenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Mystery " + PathfinderConstants.EBOLD + MonSB.Mystery + PathfinderConstants.EH5);
            }

            if (MonSB.Spirit.Length > 0)
            {
                string hold = MonSB.Spirit;
                if (MonSB.Spirit.Contains("Wandering Spirit"))
                {
                    hold = MonSB.Spirit.Replace("Wandering Spirit", PathfinderConstants.BOLD + "Wandering Spirit" + PathfinderConstants.EBOLD);
                }
                offenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "S" + PathfinderConstants.EBOLD + " spirit magic spell; " + PathfinderConstants.BOLD + "Spirit " + PathfinderConstants.EBOLD + hold);
            }

            if (MonSB.Patron.Length > 0)
            {
                offenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Patron " + PathfinderConstants.EBOLD + MonSB.Patron + PathfinderConstants.EH5);
            }


            if (MonSB.OffenseNote.Length > 0)
            {
                offenseDivSB.Append(PathfinderConstants.H5 + MonSB.OffenseNote + PathfinderConstants.EH5);
            }

            if (MonSB.ProhibitedSchools.Length > 0 && MonSB.Environment.Length > 0)
            {
                offenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Prohibited Schools " + PathfinderConstants.EBOLD + MonSB.ProhibitedSchools + PathfinderConstants.EH5);
            }

            //if (MonSB.Bloodline.Length > 0)
            //{
            //    offenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Bloodline " + PathfinderConstants.EBOLD + MonSB.Bloodline + PathfinderConstants.EH5);              
            //}
        }

        private void FormatImplements(ref string ImplementsText)
        {
            ImplementsText = ImplementsText.Replace("Implements", PathfinderConstants.BOLD + "Implements" + PathfinderConstants.EBOLD)
                .Replace("Implement Schools", PathfinderConstants.BOLD + "Implement Schools" + PathfinderConstants.EBOLD);
            var schools = new List<string>() { "Abjuration", "Conjuration", "Divination", "Enchantment", "Evocation", "Illusion", "Necromancy", "Transmutation" };
            foreach (string school in schools)
            {
                ImplementsText = ImplementsText.Replace(school, 
                    PathfinderConstants.BREAK + PathfinderConstants.BOLD + school + PathfinderConstants.EBOLD);
            }

        }

        private void FormatKineticistWildTalents(ref string kineticistWildTalentsText)
        {
            kineticistWildTalentsText = kineticistWildTalentsText.Replace("Kineticist Wild Talents Known", PathfinderConstants.BOLD + "Kineticist Wild Talents Known" + PathfinderConstants.EBOLD)
                .Replace("Infusions", PathfinderConstants.BREAK + "Infusions")
                .Replace("Kinetic blasts", PathfinderConstants.BREAK + "Kinetic blasts")
                .Replace("Kinetic Blasts", PathfinderConstants.BREAK + "Kinetic Blasts")
                .Replace("Utility", PathfinderConstants.BREAK + "Utility");
        }

        private void Format_Tactics_Div(StringBuilder tacticsDivSB)
        {
            tacticsDivSB.Append(PathfinderConstants.LINE);
            tacticsDivSB.Append(PathfinderConstants.DIV + PathfinderConstants.H5 + PathfinderConstants.BOLD + "TACTICS" + PathfinderConstants.EBOLD + PathfinderConstants.EH5 + PathfinderConstants.EDIV);
            tacticsDivSB.Append(PathfinderConstants.LINE + PathfinderConstants.DIV);

            if (MonSB.BeforeCombat.Length > 0)
            {
                tacticsDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Before Combat " + PathfinderConstants.EBOLD + MonSB.BeforeCombat + PathfinderConstants.EH5);
            }

            if (MonSB.DuringCombat.Length > 0)
            {
                tacticsDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "During Combat " + PathfinderConstants.EBOLD + MonSB.DuringCombat + PathfinderConstants.EH5);
            }

            if (MonSB.Morale.Length > 0)
            {
                tacticsDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Morale " + PathfinderConstants.EBOLD + MonSB.Morale + PathfinderConstants.EH5);
            }

            if (MonSB.BaseStatistics.Length > 0)
            {
                string temp = MonSB.BaseStatistics;
                foreach (string phrase in BoldPhrases)
                {
                    if (temp.Contains(phrase)) temp = temp.Replace(phrase, PathfinderConstants.BOLD + phrase + PathfinderConstants.EBOLD);
                }
                temp = temp.Replace("da<i>mage</i>", "damage")
                    .Replace("<i>heal</i>ing", "healing")
                    .Replace("<i>jump</i>ing", "jumping")
                    .Replace("f<i>light</i>", "flight")
                    .Replace("<i><i>resist</i> energy</i> ", "<i>resist energy</i> ");
                tacticsDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Base Statistics " + PathfinderConstants.EBOLD + temp + PathfinderConstants.EH5);
            }

            tacticsDivSB.Append(PathfinderConstants.EDIV);
        }

        protected void Format_Statistics_Div(StringBuilder statisticsDivSB)
        {
            string temp, temp2;
            statisticsDivSB.Append(PathfinderConstants.LINE);
            statisticsDivSB.Append(PathfinderConstants.DIV + PathfinderConstants.H5 + PathfinderConstants.BOLD + "STATISTICS" + PathfinderConstants.EBOLD + PathfinderConstants.EH5 + PathfinderConstants.EDIV);
            statisticsDivSB.Append(PathfinderConstants.LINE + PathfinderConstants.DIV);
            //Statistics
            temp = MonSB.AbilityScores;
            FormatAbilities(ref temp);
            statisticsDivSB.Append(PathfinderConstants.H5 + temp + PathfinderConstants.EH5);
            statisticsDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Base Atk " + PathfinderConstants.EBOLD + MonSB.BaseAtk + PathfinderConstants.SC_SP + PathfinderConstants.BOLD + "CMB " + PathfinderConstants.EBOLD + MonSB.CMB + PathfinderConstants.SC_SP + PathfinderConstants.BOLD + "CMD " + PathfinderConstants.EBOLD + MonSB.CMD + PathfinderConstants.EH5);
            if (MonSB.Feats.Length > 0)
            {
                temp = MonSB.Feats;
                FormatFeats(ref temp);
                statisticsDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Feats " + PathfinderConstants.EBOLD + temp + PathfinderConstants.EH5);
                List<string> FeatsTemp = MonSB.Feats.Replace("*", string.Empty).Split(',').ToList();
                FeatsTemp.RemoveAll(p => p == string.Empty);
                int Count = FeatsTemp.Count - 1;
                for (int a = Count; a >= 0; a--)
                {
                    string feat = FeatsTemp[a].Trim();
                    feat = Utility.RemoveSuperScripts(feat);

                    if (feat.EndsWith("M") && !feat.EndsWith("UM"))
                    {
                        feat = feat.Substring(0, feat.Length - 1) + "[M]";
                    }
                    else if (feat.Contains("M ") && !feat.Contains("UM "))
                    {
                        feat = feat.Replace("M ", "[M] ");
                    }
                    FeatsTemp[a] = feat;
                }               
                MonSB.Feats = string.Join(", ", FeatsTemp.ToArray());
            }
            if (MonSB.Skills.Length > 0)
            {
                statisticsDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Skills " + PathfinderConstants.EBOLD + MonSB.Skills);
                if (MonSB.RacialMods.Length > 0)
                {
                    statisticsDivSB.Append("; " + PathfinderConstants.BOLD + "Racial Modifiers " + PathfinderConstants.EBOLD + MonSB.RacialMods);
                }
                statisticsDivSB.Append(PathfinderConstants.EH5);
            }

            if (MonSB.Traits.Length > 0)
            {
                statisticsDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Traits " + PathfinderConstants.EBOLD + MonSB.Traits + PathfinderConstants.EH5);
            }

            if (MonSB.Languages.Length > 0)
            {
                statisticsDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Languages " + PathfinderConstants.EBOLD + MonSB.Languages + PathfinderConstants.EH5);
            }

            if (MonSB.SQ.Length > 0)
            {
                statisticsDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "SQ " + PathfinderConstants.EBOLD + MonSB.SQ + PathfinderConstants.EH5);
            }

            if (MonSB.Gear.Length > 0 && MonSB.OtherGear.Length > 0)
            {
                temp = MonSB.Gear;
                FormatPossessions(ref temp);
                temp2 = MonSB.OtherGear;
                FormatPossessions(ref temp2);
                statisticsDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Combat Gear " + PathfinderConstants.EBOLD + temp + "; " + PathfinderConstants.BOLD + "Other Gear " + PathfinderConstants.EBOLD + temp2 + PathfinderConstants.EH5);
            }

            if (MonSB.Gear.Length == 0 && MonSB.OtherGear.Length > 0)
            {
                temp2 = MonSB.OtherGear;
                FormatPossessions(ref temp2);
                statisticsDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Gear " + PathfinderConstants.EBOLD + temp2 + PathfinderConstants.EH5);
            }
            if (MonSB.Gear.Length > 0 && MonSB.OtherGear.Length == 0)
            {
                temp = MonSB.Gear;
                FormatPossessions(ref temp);
                statisticsDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Gear " + PathfinderConstants.EBOLD + temp + PathfinderConstants.EH5);
            }
        }

        protected void Format_Defense_Div(StringBuilder defenseDivSB)
        {
            bool bContinue = false;

            defenseDivSB.Append(PathfinderConstants.LINE);
            defenseDivSB.Append(PathfinderConstants.DIV + PathfinderConstants.H5 + PathfinderConstants.BOLD + "DEFENSE" + PathfinderConstants.EBOLD + PathfinderConstants.EH5 + PathfinderConstants.EDIV);
            defenseDivSB.Append(PathfinderConstants.LINE + PathfinderConstants.DIV);

            if (MonSB.AC.Contains(CRLF)) //2nd AC block
            {
                int Pos = MonSB.AC.IndexOf(CRLF);
                string[] tempACMods = MonSB.AC_Mods.Split(new string[] { " or" }, StringSplitOptions.RemoveEmptyEntries);
                string tempAC = MonSB.AC.Replace(CRLF, CRLF + PathfinderConstants.BOLD + "AC " + PathfinderConstants.EBOLD);
                tempAC = tempAC.Replace(CRLF, tempACMods[0] + " or " + CRLF);
                tempAC = tempAC + PathfinderConstants.SPACE + tempACMods[1];
                tempAC = tempAC.Replace(CRLF, PathfinderConstants.BREAK);
                defenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "AC " + PathfinderConstants.EBOLD + tempAC + PathfinderConstants.EH5);
            }
            else
            {
                defenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "AC " + PathfinderConstants.EBOLD + MonSB.AC + PathfinderConstants.SPACE + MonSB.AC_Mods.Replace("|", ";") + PathfinderConstants.EH5);
            }
            defenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "hp " + PathfinderConstants.EBOLD + MonSB.HP + PathfinderConstants.SPACE + MonSB.HD);
            if (MonSB.HP_Mods.Length > 0)
            {
                defenseDivSB.Append(PathfinderConstants.SC_SP + MonSB.HP_Mods);
            }
            defenseDivSB.Append(PathfinderConstants.EH5);
            defenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Fort " + PathfinderConstants.EBOLD + MonSB.Fort + PathfinderConstants.COM_SP + PathfinderConstants.BOLD
                                   + "Ref " + PathfinderConstants.EBOLD + MonSB.Ref + PathfinderConstants.COM_SP + PathfinderConstants.BOLD
                                   + "Will " + PathfinderConstants.EBOLD + MonSB.Will);
            if (MonSB.Save_Mods.Length > 0)
            {
                defenseDivSB.Append(PathfinderConstants.SC_SP + MonSB.Save_Mods);
            }
            defenseDivSB.Append(PathfinderConstants.EH5);

            if (MonSB.DefensiveAbilities.Length > 0)
            {
                bContinue = true;
                defenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Defensive Abilities " + PathfinderConstants.EBOLD + MonSB.DefensiveAbilities);
            }

            if (MonSB.DR.Length > 0)
            {
                if (bContinue)
                {
                    defenseDivSB.Append(PathfinderConstants.SC_SP);
                }
                else
                {
                    defenseDivSB.Append(PathfinderConstants.H5);
                }
                bContinue = true;
                defenseDivSB.Append(PathfinderConstants.BOLD + "DR " + PathfinderConstants.EBOLD + MonSB.DR);
            }

            if (MonSB.Immune.Length > 0)
            {
                if (bContinue)
                {
                    defenseDivSB.Append(PathfinderConstants.SC_SP);
                }
                else
                {
                    defenseDivSB.Append(PathfinderConstants.H5);
                }
                bContinue = true;
                defenseDivSB.Append(PathfinderConstants.BOLD + "Immune " + PathfinderConstants.EBOLD + MonSB.Immune);
            }

            if (MonSB.Vulnerability.Length > 0)
            {
                if (bContinue)
                {
                    defenseDivSB.Append(PathfinderConstants.SC_SP);
                }
                else
                {
                    defenseDivSB.Append(PathfinderConstants.H5);
                }
                bContinue = true;
                defenseDivSB.Append(PathfinderConstants.BOLD + "Vulnerability " + PathfinderConstants.EBOLD + MonSB.Vulnerability);
            }

            if (MonSB.Resist.Length > 0)
            {
                if (bContinue)
                {
                    defenseDivSB.Append(PathfinderConstants.SC_SP);
                }
                else
                {
                    defenseDivSB.Append(PathfinderConstants.H5);
                }
                bContinue = true;
                defenseDivSB.Append(PathfinderConstants.BOLD + "Resist " + PathfinderConstants.EBOLD + MonSB.Resist);
            }

            if (MonSB.SR.Length > 0)
            {
                if (bContinue)
                {
                    defenseDivSB.Append(PathfinderConstants.SC_SP);
                }
                else
                {
                    defenseDivSB.Append(PathfinderConstants.H5);
                }
                bContinue = true;
                defenseDivSB.Append(PathfinderConstants.BOLD + "SR " + PathfinderConstants.EBOLD + MonSB.SR);
            }

            if (bContinue) defenseDivSB.Append(PathfinderConstants.EH5);

            if (MonSB.Weaknesses.Length > 0)
            {
                defenseDivSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Weaknesses " + PathfinderConstants.EBOLD + MonSB.Weaknesses + PathfinderConstants.EH5);
            }
        }

        protected void FormatSpecialAbilitiesNew(ref string specialAbilitiesText)
        {
            if (specialAbilitiesText.Contains("Poison (")) FormatPoison(ref specialAbilitiesText);

            string firstSpecialAbility = BoldPhrasesSpecialAbilities[0];
            firstSpecialAbility = firstSpecialAbility.Replace((char)8217, char.Parse("'"))
                .Replace("Special Abilities", string.Empty)
                .Replace("Special Abilities".ToUpper(), string.Empty).Trim();
            BoldPhrasesSpecialAbilities.Remove(firstSpecialAbility);
            if (firstSpecialAbility.Length > 0)
            {
                specialAbilitiesText = specialAbilitiesText.Replace(firstSpecialAbility,
                    PathfinderConstants.H5 + PathfinderConstants.BOLD + firstSpecialAbility + PathfinderConstants.EBOLD);
            }
            else
            {
                BoldPhrasesSpecialAbilities.Remove("Special Abilities");
            }

            string temp;
            foreach (string bold in BoldPhrasesSpecialAbilities)
            {
                temp = bold.Replace((char)8217, char.Parse("'"))
                    .Replace("SPECIAL ABILITIES", string.Empty).Trim();
                if (temp.Length > 0)
                {
                    specialAbilitiesText = specialAbilitiesText.Replace(temp, 
                        PathfinderConstants.EH5 + PathfinderConstants.H5 + PathfinderConstants.BOLD + temp + PathfinderConstants.EBOLD);
                }
            }

            temp = "•";
            if (specialAbilitiesText.Contains(temp))//list
            {
                int Pos = specialAbilitiesText.IndexOf(temp);
                specialAbilitiesText = specialAbilitiesText.Insert(Pos, PathfinderConstants.LIST);
                specialAbilitiesText +=  PathfinderConstants.ELIST;
                specialAbilitiesText = specialAbilitiesText.Replace(temp, PathfinderConstants.LIST_ITEM);
            }

            specialAbilitiesText += PathfinderConstants.EH5;
            specialAbilitiesText = specialAbilitiesText.Replace("<h6> </h6>", string.Empty)
                .Replace("<h6></h6>", string.Empty)
                .Replace("<h5><b></h5>", string.Empty);
        }

        protected void FormatSpecialAbilities(ref string specialAbilitiesText)
        {
            if (BoldPhrasesSpecialAbilities != null && BoldPhrasesSpecialAbilities.Any())
            {
                FormatSpecialAbilitiesNew(ref specialAbilitiesText);
            }
            else
            {
                FormatSpecialAbilitiesOld formatSpecialAbilitiesOld = new FormatSpecialAbilitiesOld();
                specialAbilitiesText = formatSpecialAbilitiesOld.FormatSpecialAbilities(specialAbilitiesText, CRLF);
            }
        }     

     

        private void FormatAbilities(ref string abilitiesText)
        {
            abilitiesText = abilitiesText.Replace("Str ", PathfinderConstants.BOLD + "Str " + PathfinderConstants.EBOLD)
                .Replace("Int ", PathfinderConstants.BOLD + "Int " + PathfinderConstants.EBOLD)
                .Replace("Wis ", PathfinderConstants.BOLD + "Wis " + PathfinderConstants.EBOLD)
                .Replace("Dex ", PathfinderConstants.BOLD + "Dex " + PathfinderConstants.EBOLD)
                .Replace("Cha ", PathfinderConstants.BOLD + "Cha " + PathfinderConstants.EBOLD)
                .Replace("Con ", PathfinderConstants.BOLD + "Con " + PathfinderConstants.EBOLD);
        }

        private void FormatFeats(ref string featsText)
        {
            featsText = featsText.Replace("B,", "<sup>B</sup>,").Replace("B ", "<sup>B </sup>").Replace("M,", "<sup>M</sup>,")
                .Replace("M ", "<sup>M</sup> ").Replace("[B]", "<sup>B</sup>");
            // featsText = featsText.Replace("B (", "<sup>B</sup> (");
            if (featsText.Right(1) == "B") featsText = featsText.Substring(0, featsText.Length - 1) + "<sup>B</sup>";

            if (featsText.Right(1) == "M" && featsText.Right(2) != "UM") featsText = featsText.Substring(0, featsText.Length - 1) + "<sup>M</sup>";

            // featsText = featsText.Replace("U<sup>M</sup>,", "UM,");
            featsText = featsText.Replace("U<sup>M</sup>", "UM");
        }

        private void FormatSpells(ref string spellsText)
        {
            string temp;
            int posStart = spellsText.IndexOf("(CL ");
            if (posStart == -1)
            {
                spellsText = spellsText.Replace("(CL\n", "(CL ");
                posStart = spellsText.IndexOf("(CL ");
            }
            if (posStart == -1) return; // no CL found;
            int PosFind = spellsText.IndexOf(PathfinderConstants.PAREN_RIGHT, posStart) + 1;

            string tempHold = spellsText.Substring(PosFind);
            string tempHold2 = tempHold;

            int CRPos = tempHold2.IndexOf(CRLF);
            if (CRPos == -1)
            {
                CRLF = "\n";
            }

            tempHold2 = tempHold2.Replace("\n\r\n", CRLF);
            tempHold2 = tempHold2.Replace("D,", "<sup>D</sup>,").Replace("D*", "<sup>D</sup>*")
                .Replace("D (", "<sup>D</sup> (").Replace("D(", "<sup>D</sup> (").Replace("D" + CRLF, "<sup>D</sup>" + CRLF)
                .Replace("D " + CRLF, "<sup>D</sup>" + CRLF);
            if (tempHold2.EndsWith("D")) tempHold2 = tempHold2.Substring(0, tempHold2.Length - 1) + "<sup>D</sup>";

            tempHold2 = tempHold2.Replace("S,", "<sup>S</sup>,").Replace("S" + CRLF, "<sup>S</sup>" + CRLF)
                .Replace("S " + CRLF, "<sup>S</sup>" + CRLF);

            tempHold2 = tempHold2.Replace("B,", "<sup>B</sup>,").Replace("B*", "<sup>B</sup>*").Replace("B (", "<sup>B</sup> (")
                .Replace("B(", "<sup>B</sup> (").Replace("B" + CRLF, "<sup>B</sup>" + CRLF).Replace("B\n", "<sup>B</sup>" + CRLF);

            tempHold2 = tempHold2.Replace("1st,", "<sup>1st</sup>,").Replace("2nd,", "<sup>2nd</sup>,").Replace("3rd,", "<sup>3rd</sup>,")
                .Replace("1st (DC", "<sup>1st</sup> (DC").Replace("2nd (DC", "<sup>2nd</sup> (DC").Replace("3rd (DC", "<sup>3rd</sup> (DC");


            tempHold2 = tempHold2.Replace("M,", "<sup>M</sup>,").Replace("M ", "<sup>M</sup> ");
            if (tempHold2.EndsWith("M")) tempHold2 = tempHold2.Substring(0, tempHold2.Length - 1) + "<sup>M</sup>";

            tempHold2 = tempHold2.Replace("U<sup>M</sup>,", "UM,").Replace("U<sup>M</sup> ", "UM ").Replace("U<sup>M</sup> (", "UM (")
                .Replace("U<sup>M </sup>(", "UM (");

            for (int a = 0; a <= 9; a++)
            {
                tempHold2 = tempHold2.Replace(a.ToString() + "th,", "<sup>" + a.ToString() + "th</sup>,");
                tempHold2 = tempHold2.Replace(a.ToString() + "th (DC", "<sup>" + a.ToString() + "th</sup> (DC");
            }
            spellsText = spellsText.Replace(tempHold, tempHold2);

            int PosHold = spellsText.IndexOf(PathfinderConstants.PAREN_LEFT);
            string Hold = spellsText.Substring(0, PosHold - 1);
            int Pos = Hold.IndexOf(">");
            if (Pos >= 0)
            {
                Hold = Hold.Substring(Pos + 1);
                Hold = Hold.Trim();
                if (Hold.Contains("Spell-Like Abilities")) Hold = "Spell-Like Abilities";
            }

            int count = (spellsText.Length - spellsText.Replace(Hold, string.Empty).Length) / Hold.Length;
            if (count > 1)
            {
                int Pos2 = spellsText.IndexOf(Hold); // 1st
                Pos2 = spellsText.IndexOf(Hold, Pos2 + 1);  //next
                while (Pos2 >= 0)
                {
                    tempHold = spellsText.Substring(0, Pos2);
                    temp = Utility.FindLastNonCapital(tempHold);
                    Pos = spellsText.LastIndexOf(temp, Pos2);
                    spellsText = spellsText.Insert(Pos + temp.Length, PathfinderConstants.EH5 + PathfinderConstants.H5);
                    Pos2 = spellsText.IndexOf(Hold, Pos2 + 1 + (PathfinderConstants.EH5 + PathfinderConstants.H5).Length);  //next
                }
            }

            int forceBreakCount = 0;

            if (count == 1 || !Hold.Contains("Spell-Like Abilities"))
            {
                Pos = spellsText.IndexOf("Spell-Like Abilities");
                if (count == 1 && Pos > 4)
                {
                    Hold = spellsText.Substring(0, Pos - 1) + " Spell-Like Abilities";
                    Hold = Hold.Replace(PathfinderConstants.H5, string.Empty).Trim();
                }
                temp = PathfinderConstants.BOLD + Hold + PathfinderConstants.EBOLD;
                spellsText = spellsText.Replace(Hold, temp);
            }
            else
            {
                int Pos2 = spellsText.IndexOf(Hold); // 1st               
                while (Pos2 >= 0 || forceBreakCount >= 5)
                {
                    forceBreakCount++;
                    temp = PathfinderConstants.EH5 + PathfinderConstants.H5;
                    Pos = spellsText.LastIndexOf(temp, Pos2);
                    if (Pos >= 0)
                    {
                        tempHold = spellsText.Substring(Pos + temp.Length, Pos2 - Pos - temp.Length);
                        tempHold = tempHold.Trim() + PathfinderConstants.SPACE + Hold;
                        Pos2 = spellsText.IndexOf(tempHold.Trim(), Pos);
                    }
                    else
                        tempHold = Hold;

                    temp = PathfinderConstants.BOLD + tempHold + PathfinderConstants.EBOLD;
                    spellsText = spellsText.Substring(0, Pos2) + temp + spellsText.Substring(Pos2 + tempHold.Length);
                    Pos2 = spellsText.IndexOf(Hold, Pos2 + 15 + tempHold.Length);  //next
                }
            }

            if (!Hold.Contains("Spell-Like Abilities")) FormatNonSpellLikeAbilities(ref spellsText);


            temp = spellsText;
            temp = temp.Replace("At-will", "At will");
            temp = temp.Replace("--", "-");
            temp = temp.Replace("-", "—");
            temp = temp.ReplaceFirst("At will—", PathfinderConstants.BREAK + "At will—");
            temp = temp.ReplaceFirst("At will -", PathfinderConstants.BREAK + "At will—");
            temp = temp.ReplaceFirst("Constant—", PathfinderConstants.BREAK + "Constant—");
            temp = temp.ReplaceFirst("9th—", PathfinderConstants.BREAK + "9th—");
            temp = temp.ReplaceFirst("8th—", PathfinderConstants.BREAK + "8th—");
            temp = temp.ReplaceFirst("7th—", PathfinderConstants.BREAK + "7th—");
            temp = temp.ReplaceFirst("6th—", PathfinderConstants.BREAK + "6th—");
            temp = temp.ReplaceFirst("5th—", PathfinderConstants.BREAK + "5th—");
            temp = temp.ReplaceFirst("4th—", PathfinderConstants.BREAK + "4th—");
            temp = temp.ReplaceFirst("3rd—", PathfinderConstants.BREAK + "3rd—");
            temp = temp.ReplaceFirst("2nd—", PathfinderConstants.BREAK + "2nd—");
            temp = temp.ReplaceFirst("1st—", PathfinderConstants.BREAK + "1st—");
            temp = temp.ReplaceFirst("0—", PathfinderConstants.BREAK + "0—");
            temp = temp.ReplaceFirst("0 (at", PathfinderConstants.BREAK + "0 (at");
            temp = temp.Replace("—", "&mdash;");
            temp = temp.Replace("+/&mdash;", "+/-");
            temp = temp.Replace("concentration &mdash;", "concentration -");
            temp = temp.Replace("Spell&mdash;Like", "Spell-Like");
            temp = temp.Replace(CRLF, PathfinderConstants.BREAK);
            temp = temp.Replace(PathfinderConstants.ASCII_10, PathfinderConstants.BREAK);
            temp = temp.Replace(PathfinderConstants.BREAK + PathfinderConstants.BREAK, PathfinderConstants.BREAK);
            temp = temp.Replace(PathfinderConstants.H5 + PathfinderConstants.BREAK, PathfinderConstants.H5);

            spellsText = temp + PathfinderConstants.EH5;
        }

        private void FormatNonSpellLikeAbilities(ref string spellLikeAbilitiesText)
        {
            int posStart = spellLikeAbilitiesText.IndexOf("(CL ");
            int posFind = spellLikeAbilitiesText.IndexOf("(CL ", posStart + 5);
            int closeParen, closeParenComma, pos;
            string hold, temp;
            var cr_To_Use = (spellLikeAbilitiesText.Contains(CRLF)) ? CRLF : "\n";

            while (posFind >= 0)
            {
                pos = spellLikeAbilitiesText.LastIndexOf(cr_To_Use, posFind);
                temp = spellLikeAbilitiesText.Substring(pos, posFind - pos);
                temp = temp.Replace(cr_To_Use, string.Empty).Trim();
                hold = temp;
                string temp2 = Utility.FindLastNonCapital(hold);
                pos = hold.IndexOf(temp2);
                temp = hold.Substring(pos + temp2.Length);
                if (!temp.Contains(","))
                {
                    temp = PathfinderConstants.EH5 + PathfinderConstants.H5 + PathfinderConstants.BOLD + temp + PathfinderConstants.EBOLD;
                    spellLikeAbilitiesText = spellLikeAbilitiesText.ReplaceFirst(hold, temp, posStart);
                }
                posStart = spellLikeAbilitiesText.IndexOf(temp);
                posStart = spellLikeAbilitiesText.IndexOf("(CL ", posStart);
                posFind = spellLikeAbilitiesText.IndexOf("(CL ", posStart + 5);
                if (posFind == -1) break;
                closeParen = spellLikeAbilitiesText.IndexOf(PathfinderConstants.PAREN_RIGHT, posFind);
                closeParenComma = spellLikeAbilitiesText.IndexOf("),", posFind);
                if (closeParen > 0 && closeParen == closeParenComma) break;
            }
        }

        private void FormatSpellsItalics(ref string spellText)
        {
            if (ItalicPhrases != null) return;

            List<string> MetaMagicList = Utility.GetMetaMagicPowers();
            string temp = spellText;
            string temp2, holdLevel;

            int charcount = 1;
            int Pos = temp.IndexOf(PathfinderConstants.PAREN_RIGHT + PathfinderConstants.BREAK);
            temp = temp.Remove(0, Pos + 6);

            string[] temp3 = new string[1];
            temp3[0] = PathfinderConstants.BREAK;
            List<string> Levels = temp.Split(temp3, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> Spells;

            foreach (string level in Levels)
            {
                holdLevel = level;
                Pos = holdLevel.IndexOf("</h5>");
                if (Pos > 0) holdLevel = holdLevel.Substring(0, Pos).Trim();

                Pos = holdLevel.IndexOf("—");
                if (Pos == -1)
                {
                    Pos = holdLevel.IndexOf("-");
                    if (Pos == -1)
                    {
                        Pos = holdLevel.IndexOf("&mdash;");
                        charcount = 7;
                        if (Pos == -1)
                        {
                            Pos = holdLevel.IndexOf("&#8211;");
                            charcount = 7;
                        }
                    }
                }
                if (Pos >= 0)
                {
                    temp2 = holdLevel.Remove(0, Pos + charcount).Trim();
                    if (!temp2.Contains("CL "))
                    {
                        Spells = temp2.Split(',').ToList();
                        foreach (string spell in Spells)
                        {
                            temp2 = spell;
                            Pos = temp2.IndexOf(PathfinderConstants.PAREN_LEFT);
                            if (Pos >= 0) temp2 = temp2.Remove(Pos).Trim();
                            Pos = temp2.IndexOf(PathfinderConstants.PAREN_RIGHT);
                            if (Pos >= 0) temp2 = string.Empty;
                            if (temp2.Length > 0)
                            {
                                Pos = temp2.IndexOf("<sup>");
                                if (Pos >= 0) temp2 = temp2.Substring(0, Pos).Trim();

                                Pos = temp2.IndexOf("</h5>");
                                if (Pos >= 0) temp2 = temp2.Substring(0, Pos).Trim();

                                foreach (string Meta in MetaMagicList)
                                {
                                    if (temp2.Contains(Meta)) temp2 = temp2.ReplaceFirst(Meta, string.Empty);
                                }
                                spellText = spellText.Replace(temp2, PathfinderConstants.ITACLIC + temp2 + PathfinderConstants.EITACLIC);
                            }
                        }
                    }
                }
            }
        }

        private void FormatCombat(ref string combatText)
        {
            int pos, pos2, pos3;
            string temp;

            //  sText = sText.Replace(" and", " and" + PathfinderConstants.BREAK);
            combatText = combatText.Replace(" or ", " or " + PathfinderConstants.BREAK);
            combatText = combatText.Replace(" or" + CRLF, " or " + PathfinderConstants.BREAK);
            combatText = combatText.Replace(" or" + PathfinderConstants.ASCII_10, " or" + PathfinderConstants.ASCII_10 + PathfinderConstants.BREAK);
            pos = combatText.IndexOf(PathfinderConstants.BREAK + PathfinderConstants.ASCII_10 + "+");

            if (pos == -1)
            {
                if (combatText.Substring(0, 1) == "+")
                {
                    pos2 = combatText.IndexOf("+", 2);
                    if (pos2 > -1)
                    {
                        temp = combatText.Substring(0, pos2 - 1);
                        combatText = combatText.Replace(temp, PathfinderConstants.ITACLIC + temp + PathfinderConstants.EITACLIC);
                    }
                }
            }
            else
            {
                while (pos >= 0)
                {
                    pos2 = combatText.IndexOf("+", pos + 7);
                    temp = combatText.Substring(pos + 6, pos2 - 1 - pos - 6);
                    combatText = combatText.Replace(temp, PathfinderConstants.ITACLIC + temp + PathfinderConstants.EITACLIC);
                    pos = combatText.IndexOf(PathfinderConstants.BREAK + PathfinderConstants.ASCII_10 + "+", pos2 + 7);
                }
            }
            pos = combatText.IndexOf(PathfinderConstants.BREAK + "+");
            if (pos == -1)
            {
                if (combatText.Substring(0, 1) == "+")
                {
                    pos2 = combatText.IndexOf("+", 2);
                    if (pos2 > -1)
                    {
                        temp = combatText.Substring(0, pos2 - 1);
                        combatText = combatText.Replace(temp, PathfinderConstants.ITACLIC + temp + PathfinderConstants.EITACLIC);
                    }
                }
            }
            else
            {
                while (pos >= 0)
                {
                    pos2 = combatText.IndexOf("+", pos + 6);
                    temp = combatText.Substring(pos + 5, pos2 - 1 - pos - 5);
                    combatText = combatText.Replace(temp, PathfinderConstants.ITACLIC + temp + PathfinderConstants.EITACLIC);
                    pos = combatText.IndexOf(PathfinderConstants.BREAK + "+", pos + 6);
                }
            }
            pos = combatText.IndexOf(PathfinderConstants.BREAK + PathfinderConstants.ASCII_10 + "* +");
            if (pos == -1)
            {
                if (combatText.Substring(0, 3) == "* +")
                {
                    pos2 = combatText.IndexOf("+", 4);
                    temp = combatText.Substring(0, pos2 - 1);
                    combatText = combatText.Replace(temp, PathfinderConstants.ITACLIC + temp + PathfinderConstants.EITACLIC);
                }
            }

            while (pos >= 0)
            {
                pos2 = combatText.IndexOf("+", pos + 7);
                temp = combatText.Substring(pos + 6, pos2 - 1 - pos - 6);
                combatText = combatText.Replace(temp, PathfinderConstants.ITACLIC + temp + PathfinderConstants.EITACLIC);
                pos = combatText.IndexOf(PathfinderConstants.BREAK + PathfinderConstants.ASCII_10 + "+", pos + 7);
            }

            // no PathfinderConstants.BREAK between ()
            pos = combatText.IndexOf(PathfinderConstants.PAREN_LEFT);
            while (pos >= 0)
            {
                pos2 = combatText.IndexOf(PathfinderConstants.BREAK, pos);
                pos3 = combatText.IndexOf(PathfinderConstants.PAREN_RIGHT, pos);
                if (pos < pos2 && pos2 < pos3)
                {
                    combatText = combatText.ReplaceFirst(PathfinderConstants.BREAK, string.Empty, pos2);
                }
                pos = combatText.IndexOf(PathfinderConstants.PAREN_LEFT, pos + 1);
            }
        }

        private void FormatPossessions(ref string possessionsText)
        {
            if (ItalicPhrases != null && ItalicPhrases.Any()) return;

            string Hold;
            bool MagicItemTest;
            int Pos, Pos2;
            List<string> MagicItems = CommonMethods.GetMagicItemNouns();

            string tempPossessions = possessionsText;
            tempPossessions = tempPossessions.Replace("combat gear plus ", string.Empty);
            string[] temp = new string[1];
            temp[0] = ", ";

            List<string> Items = tempPossessions.Split(temp, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (string Item in Items)
            {
                MagicItemTest = false;

                foreach (string MItem in MagicItems)
                {
                    if (Item.Contains(MItem))
                        MagicItemTest = true;
                }
                if (Item.Contains("+") && !MagicItemTest && !Item.Contains("(+"))
                    possessionsText = possessionsText.Replace(Item, PathfinderConstants.ITACLIC + Item + PathfinderConstants.EITACLIC);


                if (Item.Contains("+") && !MagicItemTest && Item.Contains("(+"))
                {
                    Pos = Item.IndexOf("+");
                    if (Pos >= 0)
                    {
                        tempPossessions = Item;
                        tempPossessions = tempPossessions.Trim();
                        possessionsText = possessionsText.Replace(tempPossessions, PathfinderConstants.ITACLIC + tempPossessions + PathfinderConstants.EITACLIC);
                    }
                }

                if (Item.Contains("+") && !MagicItemTest && Item.Contains("(+"))
                {
                    Pos2 = Item.IndexOf("(+");
                    Pos = Item.IndexOf("+");
                    tempPossessions = Item.Substring(0, Pos2 - 1);
                    tempPossessions = tempPossessions.Trim();
                    if (!Item.Contains(" Str)"))
                        possessionsText = possessionsText.Replace(tempPossessions, PathfinderConstants.ITACLIC + tempPossessions + PathfinderConstants.EITACLIC);
                }

                string scroll = "scroll of ";
                string scrolls = "scrolls of ";

                if (MagicItemTest)
                {
                    if (Item.Contains(scroll) || Item.Contains(scrolls))
                    {
                        if (Item.Contains(scroll))
                            tempPossessions = Item.Replace(scroll, string.Empty);

                        if (Item.Contains(scrolls))
                        {
                            Pos = Item.IndexOf(scrolls);
                            tempPossessions = Item.Substring(Pos + 10);
                            tempPossessions = Item.Replace(tempPossessions, string.Empty);
                        }

                        possessionsText = possessionsText.Replace(tempPossessions, PathfinderConstants.ITACLIC + tempPossessions + PathfinderConstants.EITACLIC);
                    }
                    else
                    {
                        possessionsText = possessionsText.Replace(Item, PathfinderConstants.ITACLIC + Item + PathfinderConstants.EITACLIC);
                    }
                    tempPossessions = PathfinderConstants.ITACLIC + Item + PathfinderConstants.EITACLIC;
                    if (tempPossessions.Contains(PathfinderConstants.PAREN_LEFT))
                    {
                        Hold = tempPossessions;
                        tempPossessions = tempPossessions.Replace(PathfinderConstants.PAREN_LEFT, PathfinderConstants.EITACLIC + PathfinderConstants.PAREN_LEFT);
                        tempPossessions = tempPossessions.Replace(PathfinderConstants.PAREN_RIGHT + PathfinderConstants.EITACLIC, PathfinderConstants.PAREN_RIGHT);
                        possessionsText = possessionsText.Replace(Hold, tempPossessions);
                    }
                    if (Item.Contains("(as "))
                    {
                        Pos = Item.IndexOf("(as ");
                        tempPossessions = Item.Replace(Item.Substring(0, Pos + 2), string.Empty);
                        tempPossessions = tempPossessions.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                        tempPossessions = tempPossessions.Trim();
                        possessionsText = possessionsText.Replace(tempPossessions, PathfinderConstants.ITACLIC + tempPossessions + PathfinderConstants.EITACLIC);
                    }
                }
            }
        }

        public void ReplaceTables(MonsterStatBlock SB_old, MonsterStatBlock SB_new)
        {
            int TableCount = SB_old.FullText.PhraseCount("<table>");
            if (TableCount == 0) return;

            int PosStart = SB_old.FullText.IndexOf("<table>");
            int PosEnd = SB_old.FullText.IndexOf("</table>");
            List<string> TableHold = new List<string>();


            for (int a = 0; a < TableCount; a++)
            {
                TableHold.Add(SB_old.FullText.Substring(PosStart, PosEnd - PosStart + "</table>".Length));
                PosStart = SB_old.FullText.IndexOf("<table>", PosEnd);
                if (PosStart >= 0)
                    PosEnd = SB_old.FullText.IndexOf("</table>", PosStart);
            }

            string TableStart, TableEnd, TableText;

            foreach (string Table in TableHold)
            {
                TableStart = FindTableTextStart(Table);
                TableEnd = FindTableTextEnd(Table);
                PosStart = SB_new.FullText.IndexOf(TableStart);
                PosEnd = SB_new.FullText.IndexOf(TableEnd);
                if (PosStart >= 0 && PosEnd >= 0)
                {
                    TableText = SB_new.FullText.Substring(PosStart, PosEnd - PosStart + TableEnd.Length);
                    SB_new.FullText = SB_new.FullText.Replace(TableText, Table);
                }
            }
        }

        private string FindTableTextEnd(string table)
        {
            string Tag = "<td";
            string EndTag = "</td>";
            int Pos = table.LastIndexOf(Tag);

            int Pos2 = table.IndexOf(">", Pos);
            Pos = table.LastIndexOf(EndTag);
            return table.Substring(Pos2 + 1, Pos - Pos2 - 1);
        }

        private string FindTableTextStart(string table)
        {
            string Tag = "<caption";
            string EndTag = "</caption>";
            int Pos = table.IndexOf(Tag);

            if (Pos == -1)
            {
                Tag = "<th";
                EndTag = "</th";
                Pos = table.IndexOf(Tag);
            }

            int Pos2 = table.IndexOf(">", Pos);
            Pos = table.IndexOf(EndTag);
            string firstCell = table.Substring(Pos2 + 1, Pos - Pos2 - 1);
            Pos = table.IndexOf(Tag, Pos2);
            Pos2 = table.IndexOf(">", Pos);
            Pos = table.IndexOf(EndTag, Pos2);
            string secondCell = table.Substring(Pos2 + 1, Pos - Pos2 - 1);
            return firstCell + PathfinderConstants.SPACE + secondCell;

        }

        public void Get_CSS_Text(StringBuilder oText)
        {
            oText.Append("<style type=" + PathfinderConstants.QUOTE + "text/css" + PathfinderConstants.QUOTE + ">" + CRLF);

            oText.Append("div {" + CRLF);
            oText.Append("padding-top: 0px;" + CRLF);
            oText.Append("padding-bottom: 0px;" + CRLF);
            oText.Append("padding-right: 0px;" + CRLF);
            oText.Append("padding-left: 30px;" + CRLF);
            oText.Append("}" + CRLF);

            oText.Append("div.heaading {" + CRLF);
            oText.Append("padding-top: 0px;" + CRLF);
            oText.Append("padding-bottom: 0px;" + CRLF);
            oText.Append("padding-right: 0px;" + CRLF);
            oText.Append("padding-left: 0px;" + CRLF);
            oText.Append("}" + CRLF);

            oText.Append("h2 {" + CRLF);
            oText.Append("margin-bottom: 0;" + CRLF);
            oText.Append("margin-top: 0;" + CRLF);
            oText.Append("text-indent: -20px;" + CRLF);
            oText.Append("font-size: 22;" + CRLF);
            oText.Append("font-family: arial;" + CRLF);
            oText.Append("}" + CRLF);

            oText.Append("h3 {" + CRLF);
            oText.Append("margin-bottom: 0;" + CRLF);
            oText.Append("margin-top: 0;" + CRLF);
            oText.Append("margin-left: -20px;" + CRLF);
            oText.Append("text-indent: 0px;" + CRLF);
            oText.Append("font-size: 16;" + CRLF);
            oText.Append("font-weight: normal;" + CRLF);
            oText.Append("font-family: arial;" + CRLF);
            oText.Append("}" + CRLF);

            oText.Append("h4 {" + CRLF);
            oText.Append("margin-bottom: 0px;" + CRLF);
            oText.Append("margin-top: 0px;" + CRLF);
            oText.Append("margin-left: -20px;" + CRLF);
            oText.Append("text-indent: 0px;" + CRLF);
            oText.Append("text-align: justify;" + CRLF);
            oText.Append("font-size: 16;" + CRLF);
            oText.Append("font-weight: normal;" + CRLF);
            oText.Append("font-family: arial;" + CRLF);
            oText.Append("}" + CRLF);

            oText.Append("p {" + CRLF);
            oText.Append("margin-top: 0px;" + CRLF);
            oText.Append("margin-right: 0px;" + CRLF);
            oText.Append("margin-bottom: 1px;" + CRLF);
            oText.Append("margin-left: 0px;" + CRLF);
            oText.Append("padding-top: 0px;" + CRLF);
            oText.Append("padding-right: 0px;" + CRLF);
            oText.Append("padding-bottom: 1px;" + CRLF);
            oText.Append("padding-left: 0px;" + CRLF);
            oText.Append("}" + CRLF);

            oText.Append("h5 {" + CRLF);
            oText.Append("margin-bottom: 0;" + CRLF);
            oText.Append("margin-top: 0;" + CRLF);
            oText.Append("text-indent: -20px;" + CRLF);
            oText.Append("font-size: 16;" + CRLF);
            oText.Append("font-weight: normal;" + CRLF);
            oText.Append("font-family: arial;" + CRLF);
            oText.Append("}" + CRLF);

            oText.Append("h6 {" + CRLF);
            oText.Append("margin-bottom: 0;" + CRLF);
            oText.Append("margin-top: 0;" + CRLF);
            oText.Append("margin-left: 30px;" + CRLF);
            oText.Append("text-indent: -20px;" + CRLF);
            oText.Append("font-size: 16;" + CRLF);
            oText.Append("font-weight: normal;" + CRLF);
            oText.Append("font-family: arial;" + CRLF);
            oText.Append("}" + CRLF);

            oText.Append("p.alignleft {" + CRLF);
            oText.Append("margin-left: 0px;" + CRLF);
            oText.Append("text-indent: 10px;" + CRLF);
            oText.Append("font-size: 22;" + CRLF);
            oText.Append("font-weight: bold;" + CRLF);
            oText.Append("font-family: arial;" + CRLF);
            oText.Append("float: left;" + CRLF);
            oText.Append("text-align:left;" + CRLF);
            oText.Append("}" + CRLF);

            oText.Append("p.alignright {" + CRLF);
            oText.Append("font-size: 22;" + CRLF);
            oText.Append("font-weight: bold;" + CRLF);
            oText.Append("font-family: arial;" + CRLF);
            oText.Append("float: right;" + CRLF);
            oText.Append("text-align:right;" + CRLF);
            oText.Append("}" + CRLF);

            oText.Append("</style>" + CRLF);
        }
    }
}