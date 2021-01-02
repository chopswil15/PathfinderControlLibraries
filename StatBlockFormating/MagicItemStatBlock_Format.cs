using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStrings;
using Utilities;
using PathfinderGlobals;
using StatBlockCommon.MagicItem_SB;

namespace StatBlockFormating
{
    public class MagicItemStatBlock_Format : IMagicItemStatBlock_Format
    {
        protected readonly string CRLF = Environment.NewLine;

        private MagicItemStatBlock MI_SB;
        public List<string> ItalicPhrases { get; set; }
        public List<string> BoldPhrases { get; set; }

        public string CreateFullText(MagicItemStatBlock SB)
        {
            MI_SB = SB;
            StringBuilder stringBulider = new StringBuilder();

            stringBulider.Append(CommonMethods.Get_CSS_Ref());

            Foramt_Basics_Div(stringBulider);
            stringBulider.Append(PathfinderConstants.EDIV);

            if (MI_SB.AL.Length > 0)
            {
                Foramt_Statisitcs_Div(stringBulider);
            }

            if (MI_SB.Description.Length > 0)
            {
                Format_Description_Div(stringBulider);
            }


            if (MI_SB.Destruction.Length > 0)
            {
                Format_Destruction_Div(stringBulider);
            }

            if (MI_SB.MagicItems.Length == 0 && MI_SB.Requirements.Length > 0)
            {
                Format_Construction_Div(stringBulider);
            }
            else
            {
                if (MI_SB.MagicItems.Length > 0)
                {
                    Format_Creation_Div(stringBulider);
                }
            }

            stringBulider = stringBulider.Replace("<i>fly</i>ing", "flying");
            stringBulider = stringBulider.Replace("<i>web</i>bing", "webbing");
            stringBulider = stringBulider.Replace("da<i>mage</i>", "damage");
            stringBulider = stringBulider.Replace("<i>heal</i>ing", "healing");
            stringBulider = stringBulider.Replace("<i>jump</i>ing", "jumping");
            stringBulider = stringBulider.Replace("<i>bleed</i>ing", "bleeding");
            stringBulider = stringBulider.Replace("f<i>light</i>", "flight");
            stringBulider = stringBulider.Replace("<i>light</i>ning", "lightning");
            stringBulider = stringBulider.Replace("<i><i>resist</i> energy</i>", "<i>resist energy</i>");
            stringBulider = stringBulider.Replace("<i>see <i>invisibility</i></i>", "<i>see invisibility</i>");
            stringBulider = stringBulider.Replace("<i>greater <i>magic</i> weapon</i>", "<i>greater magic weapon</i>");
            stringBulider = stringBulider.Replace("S<i>ki</i>ll", "Skill");
            stringBulider = stringBulider.Replace("channel <i>resistance</i>", "channel resistance");
            stringBulider = stringBulider.Replace("meta<i>magic</i>", "metamagic");
            stringBulider = stringBulider.Replace("<i>light</i> crossbow", "light crossbow");
            stringBulider = stringBulider.Replace("butter<i>fly</i>", "butterfly");
            stringBulider = stringBulider.Replace("<i>sleep</i>ing", "sleeping");
            stringBulider = stringBulider.Replace("<i>teleport</i>ation", "teleportation");
            stringBulider = stringBulider.Replace("<i>web</i>s", "webs");
            stringBulider = stringBulider.Replace("<i>heal</i>s", "heals");
            stringBulider = stringBulider.Replace("<i>ki</i>ng", "king");
            stringBulider = stringBulider.Replace("s<i>ki</i>ll", "skill");
            stringBulider = stringBulider.Replace("<i>heal</i>th", "health");
            stringBulider = stringBulider.Replace("moon<i>light</i>", "moonlight");

            List<string> Supers = Utility.GetSuperScripts();
            foreach (string super in Supers)
            {
                stringBulider = stringBulider.Replace(super, "<sup>" + super + "</sup>");
            }

            stringBulider = stringBulider.Replace("CONSTR<sup>UC</sup>TION", "CONSTRUCTION");
            stringBulider = stringBulider.Replace("DESTR<sup>UC</sup>TION", "DESTRUCTION");

            return stringBulider.ToString();
        }

        private void Format_Creation_Div(StringBuilder creationDivText)
        {
            creationDivText.Append(PathfinderConstants.LINE);
            creationDivText.Append(PathfinderConstants.DIV + PathfinderConstants.H5 + PathfinderConstants.BOLD + "CREATION" + PathfinderConstants.EBOLD + PathfinderConstants.EH5 + PathfinderConstants.EDIV);
            creationDivText.Append(PathfinderConstants.LINE + PathfinderConstants.DIV);
            creationDivText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Magic Items " + PathfinderConstants.EBOLD + PathfinderConstants.ITACLIC + MI_SB.MagicItems + PathfinderConstants.EITACLIC + PathfinderConstants.EH5);
            creationDivText.Append(PathfinderConstants.EDIV);
        }

        private void Format_Construction_Div(StringBuilder constructionDivText)
        {
            constructionDivText.Append(PathfinderConstants.LINE);
            constructionDivText.Append(PathfinderConstants.DIV + PathfinderConstants.H5 + PathfinderConstants.BOLD + "CONSTRUCTION" + PathfinderConstants.EBOLD + PathfinderConstants.EH5 + PathfinderConstants.EDIV);
            constructionDivText.Append(PathfinderConstants.LINE + PathfinderConstants.DIV);
            string temp = MI_SB.Requirements;
            FormatRequirements(ref temp);
            constructionDivText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Requirements " + PathfinderConstants.EBOLD + temp + PathfinderConstants.SC_SP + PathfinderConstants.BOLD + "Cost " + PathfinderConstants.EBOLD + MI_SB.Cost + PathfinderConstants.EH5);
            constructionDivText.Append(PathfinderConstants.EDIV);
        }

        private void FormatRequirements(ref string Req)
        {
            string temp;
            List<string> Reqs = Req.Split(',').ToList();
            foreach (string tempReq in Reqs)
            {
                if (!tempReq.Contains("creator") && !tempReq.Contains("caster")
                    && !tempReq.Contains("crafter") && !tempReq.Contains("plus "))
                {
                    temp = tempReq.Trim();
                    char x = char.Parse(temp.Substring(0, 1));
                    int ascii = x;
                    if (ascii >= 92 && ascii <= 122)
                    {
                        Req = Req.Replace(temp, PathfinderConstants.ITACLIC + temp + PathfinderConstants.EITACLIC);
                    }
                }
            }
        }

        private void Format_Description_Div(StringBuilder descriptionDivText)
        {
            string tempDescription = MI_SB.Description;
            descriptionDivText.Append(PathfinderConstants.LINE);
            descriptionDivText.Append(PathfinderConstants.DIV + PathfinderConstants.H5 + PathfinderConstants.BOLD + "DESCRIPTION" + PathfinderConstants.EBOLD + PathfinderConstants.EH5 + PathfinderConstants.EDIV);
            descriptionDivText.Append(PathfinderConstants.LINE + PathfinderConstants.DIV);
            foreach (string phrase in ItalicPhrases)
            {
                if (tempDescription.Contains(phrase))
                {
                    tempDescription = tempDescription.Replace(phrase, PathfinderConstants.ITACLIC + phrase + PathfinderConstants.EITACLIC);
                }
            }
            foreach (string phrase in BoldPhrases)
            {
                if (tempDescription.Contains(phrase))
                {
                    tempDescription = tempDescription.Replace(phrase, PathfinderConstants.BOLD + phrase + PathfinderConstants.EBOLD);
                }
            }

            tempDescription = FormatList(tempDescription);
            tempDescription = PathfinderConstants.PARA + tempDescription;
            tempDescription = tempDescription.Replace(CRLF, "</p><p>");
            tempDescription = tempDescription + PathfinderConstants.EPARA;
            descriptionDivText.Append(PathfinderConstants.H4 + tempDescription + PathfinderConstants.EH4);
            descriptionDivText.Append(PathfinderConstants.EDIV);
        }

        private static string FormatList(string temp)
        {
            if (temp.Contains("•"))//list
            {
                int Pos = temp.IndexOf("•");
                temp = temp.Insert(Pos, PathfinderConstants.LIST);
                int LastPos = temp.LastIndexOf("•");

                temp = temp + PathfinderConstants.ELIST;
                temp = temp.Replace("•", PathfinderConstants.LIST_ITEM);
            }
            return temp;
        }

        private void Format_Destruction_Div(StringBuilder destructionDivText)
        {
            string tempDestruction = MI_SB.Destruction;
            destructionDivText.Append(PathfinderConstants.LINE);
            destructionDivText.Append(PathfinderConstants.DIV + PathfinderConstants.H5 + PathfinderConstants.BOLD + "DESTRUCTION" + PathfinderConstants.EBOLD + PathfinderConstants.EH5 + PathfinderConstants.EDIV);
            destructionDivText.Append(PathfinderConstants.LINE + PathfinderConstants.DIV);
            foreach (string phrase in ItalicPhrases)
            {
                if (tempDestruction.Contains(phrase))
                {
                    tempDestruction = tempDestruction.Replace(phrase, PathfinderConstants.ITACLIC + phrase + PathfinderConstants.EITACLIC);
                }
            }
            foreach (string phrase in BoldPhrases)
            {
                if (tempDestruction.Contains(phrase))
                {
                    tempDestruction = tempDestruction.Replace(phrase, PathfinderConstants.BOLD + phrase + PathfinderConstants.EBOLD);
                }
            }

            if (tempDestruction.IndexOf("•") >= 0)//list
            {
                int Pos = tempDestruction.IndexOf("•");
                tempDestruction = tempDestruction.Insert(Pos, PathfinderConstants.LIST);
                int LastPos = tempDestruction.LastIndexOf("•");

                tempDestruction = tempDestruction + PathfinderConstants.ELIST;
                tempDestruction = tempDestruction.Replace("•", PathfinderConstants.LIST_ITEM);
            }
            tempDestruction = PathfinderConstants.PARA + tempDestruction;
            tempDestruction = tempDestruction.Replace(CRLF, "</p><p>");
            tempDestruction = tempDestruction + PathfinderConstants.EPARA;
            destructionDivText.Append(PathfinderConstants.H4 + tempDestruction + PathfinderConstants.EH4);
            destructionDivText.Append(PathfinderConstants.EDIV);
        }

        private void Foramt_Basics_Div(StringBuilder basicsDivText)
        {
            basicsDivText.Append("<div class=" + PathfinderConstants.QUOTE + "heading" + PathfinderConstants.QUOTE + ">");
            basicsDivText.Append("<p class=" + PathfinderConstants.QUOTE + "alignleft" + PathfinderConstants.QUOTE + ">" + MI_SB.name);
            if (MI_SB.MajorArtifactFlag)
            {
                basicsDivText.Append(" (Major Artifact)");
            }
            if (MI_SB.MinorArtifactFlag)
            {
                basicsDivText.Append(" (Minor Artifact)");
            }
            if (MI_SB.LegendaryWeapon)
            {
                basicsDivText.Append(" (Legendary Weapon)");
            }

            basicsDivText.Append(PathfinderConstants.EPARA);
            basicsDivText.Append("<div style=" + PathfinderConstants.QUOTE + "clear: both;" + PathfinderConstants.QUOTE + "></div></div>");

            basicsDivText.Append(PathfinderConstants.DIV);
            basicsDivText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Aura " + PathfinderConstants.EBOLD + MI_SB.Aura + PathfinderConstants.SC_SP + PathfinderConstants.BOLD + "CL " + PathfinderConstants.EBOLD + FormatCL(MI_SB.CL));
            if (MI_SB.Scaling.Length > 0)
            {
                basicsDivText.Append(PathfinderConstants.SC_SP + PathfinderConstants.BOLD + "Scaling " + PathfinderConstants.EBOLD + MI_SB.Scaling);
            }
            basicsDivText.Append(PathfinderConstants.EH5);
            basicsDivText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Slot " + PathfinderConstants.EBOLD + MI_SB.Slot);
            if (MI_SB.Price.Length > 0)
            {
                basicsDivText.Append(PathfinderConstants.SC_SP + PathfinderConstants.BOLD + "Price " + PathfinderConstants.EBOLD + MI_SB.Price);
            }
            basicsDivText.Append(PathfinderConstants.SC_SP + PathfinderConstants.BOLD + "Weight " + PathfinderConstants.EBOLD + MI_SB.Weight + PathfinderConstants.EH5);
        }

        private string FormatCL(string CL)
        {
            if (CL == "varies") return CL;
            string temp;
            switch (CL.Right(1))
            {
                case "1":
                    if (CL == "1")
                    {
                        temp = CL + "st";
                    }
                    else if (CL == "11")
                    {
                        temp = CL + "th";
                    }
                    else
                    {
                        temp = CL + "st";
                    }
                    break;
                case "2":
                    if (CL == "2")
                    {
                        temp = CL + "nd";
                    }
                    else
                    {
                        temp = CL + "th";
                    }
                    break;
                case "3":
                    if (CL == "3")
                    {
                        temp = CL + "rd";
                    }
                    else
                    {
                        temp = CL + "th";
                    }
                    break;
                default:
                    temp = CL + "th";
                    break;
            }
            return temp;
        }

        private void Foramt_Statisitcs_Div(StringBuilder statisitcsDivText)
        {
            statisitcsDivText.Append(PathfinderConstants.LINE);
            statisitcsDivText.Append(PathfinderConstants.DIV + PathfinderConstants.H5 + PathfinderConstants.BOLD + "STATISTICS" + PathfinderConstants.EBOLD + PathfinderConstants.EH5 + PathfinderConstants.EDIV);
            statisitcsDivText.Append(PathfinderConstants.LINE + PathfinderConstants.DIV);

            statisitcsDivText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "AL " + PathfinderConstants.EBOLD + MI_SB.AL + PathfinderConstants.SC_SP + PathfinderConstants.BOLD + "Ego " + PathfinderConstants.EBOLD + MI_SB.Ego + PathfinderConstants.EH5);
            statisitcsDivText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Int " + PathfinderConstants.EBOLD + MI_SB.Int + PathfinderConstants.COM_SP +
                            PathfinderConstants.BOLD + "Wis " + PathfinderConstants.EBOLD + MI_SB.Wis + PathfinderConstants.COM_SP + PathfinderConstants.BOLD + "Cha " + PathfinderConstants.EBOLD + MI_SB.Cha + PathfinderConstants.EH5);
            if (MI_SB.Senses.Length > 0)
            {
                statisitcsDivText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Senses " + PathfinderConstants.EBOLD + MI_SB.Senses + PathfinderConstants.EH5);
            }
            if (MI_SB.Communication.Length > 0)
            {
                statisitcsDivText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Communication " + PathfinderConstants.EBOLD + MI_SB.Communication + PathfinderConstants.EH5);
            }
            if (MI_SB.Languages.Length > 0)
            {
                statisitcsDivText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Languages " + PathfinderConstants.EBOLD + MI_SB.Languages + PathfinderConstants.EH5);
            }

            if (MI_SB.Powers.Length > 0)
            {
                statisitcsDivText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Powers " + PathfinderConstants.EBOLD + MI_SB.Powers + PathfinderConstants.EH5);
            }
            if (MI_SB.SpecialPurpose.Length > 0)
            {

                statisitcsDivText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Special Purpose " + PathfinderConstants.EBOLD + FormatSpecialPurpose(MI_SB.SpecialPurpose));
                if (MI_SB.DedicatedPowers.Length > 0)
                {
                    statisitcsDivText.Append(PathfinderConstants.SC_SP + PathfinderConstants.BOLD + "Dedicated Powers " + PathfinderConstants.EBOLD + MI_SB.DedicatedPowers);
                }
                statisitcsDivText.Append(PathfinderConstants.EH5);
            }

            statisitcsDivText.Append(PathfinderConstants.EDIV);
        }

        private string FormatSpecialPurpose(string SP)
        {
            string temp = SP;
            temp = FormatList(temp);

            return temp;
        }

        private void FormatSpells(ref string spellText)
        {
            string temp = string.Empty;
            int PosStart = spellText.IndexOf("(CL ");
            if (PosStart == -1)
            {
                spellText = spellText.Replace("(CL\n", "(CL ");
                PosStart = spellText.IndexOf("(CL ");
            }
            if (PosStart == -1) return; // no CL found;
            int PosFind = spellText.IndexOf(PathfinderConstants.PAREN_RIGHT, PosStart);

            string tempHold = spellText.Substring(PosFind);
            string tempHold2 = tempHold;


            tempHold2 = tempHold2.Replace("\n\r\n", CRLF);
            tempHold2 = tempHold2.Replace("D,", "<sup>D</sup>,");
            tempHold2 = tempHold2.Replace("D*", "<sup>D</sup>*");
            tempHold2 = tempHold2.Replace("D (", "<sup>D</sup> (");
            tempHold2 = tempHold2.Replace("D" + CRLF, "<sup>D</sup>" + CRLF);
            tempHold2 = tempHold2.Replace("D\n", "<sup>D</sup>" + CRLF);
            tempHold2 = tempHold2.Replace("1st,", "<sup>1st</sup>,");
            tempHold2 = tempHold2.Replace("2nd,", "<sup>2nd</sup>,");
            tempHold2 = tempHold2.Replace("3rd,", "<sup>3rd</sup>,");
            tempHold2 = tempHold2.Replace("1st (DC", "<sup>1st</sup> (DC");
            tempHold2 = tempHold2.Replace("2nd (DC", "<sup>2nd</sup> (DC");
            tempHold2 = tempHold2.Replace("3rd (DC", "<sup>3rd</sup> (DC");
            for (int a = 0; a <= 9; a++)
            {
                tempHold2 = tempHold2.Replace(a.ToString() + "th,", "<sup>" + a.ToString() + "th</sup>,");
                tempHold2 = tempHold2.Replace(a.ToString() + "th (DC", "<sup>" + a.ToString() + "th</sup> (DC");
            }
            spellText = spellText.Replace(tempHold, tempHold2);

            int PosHold = spellText.IndexOf(PathfinderConstants.PAREN_LEFT);
            string Hold = spellText.Substring(0, PosHold - 1);
            int Pos = Hold.IndexOf(">");
            if (Pos >= 0)
            {
                Hold = Hold.Substring(Pos + 1);
                Hold = Hold.Trim();
            }
            int Count = (spellText.Length - spellText.Replace(Hold, string.Empty).Length) / Hold.Length;
            if (Count > 1)
            {
                int Pos2 = spellText.IndexOf(Hold); // 1st
                Pos2 = spellText.IndexOf(Hold, Pos2 + 1);  //next
                while (Pos2 >= 0)
                {
                    tempHold = spellText.Substring(0, Pos2);
                    temp = Utility.FindLastNonCapital(tempHold);
                    Pos = spellText.LastIndexOf(temp, Pos2);
                    spellText = spellText.Insert(Pos + temp.Length, PathfinderConstants.BREAK);
                    Pos2 = spellText.IndexOf(Hold, Pos2 + 1 + PathfinderConstants.BREAK.Length);  //next
                }
            }

            temp = PathfinderConstants.BOLD + Hold + PathfinderConstants.EBOLD;

            spellText = spellText.Replace(Hold, temp);
            PosStart = spellText.IndexOf("(CL ");

            PosFind = spellText.IndexOf("(CL ", PosStart + 5);
            while (PosFind >= 0)
            {
                temp = spellText.Substring(0, PosFind - 1);
                temp = Utility.FindLastNonCapital(temp);
                if (temp.Length > 0 && temp != "I")
                {
                    Pos = spellText.IndexOf(temp + PathfinderConstants.SPACE, PosStart);
                    if (Pos == -1)
                    {
                        Pos = spellText.IndexOf(temp, PosStart);
                    }
                    temp = spellText.Substring(Pos, PosFind - Pos);
                    if (temp.IndexOf(CRLF) > 0)
                    {
                        Pos = temp.IndexOf(CRLF);
                    }
                    else
                    {
                        Pos = temp.IndexOf(PathfinderConstants.SPACE);
                    }
                    temp = temp.Remove(0, Pos);
                    if (temp.Substring(0, 1) == PathfinderConstants.ASCII_10) temp = temp.Replace(PathfinderConstants.ASCII_10, string.Empty);
                    Hold = temp.Trim();
                    temp = PathfinderConstants.EH5 + PathfinderConstants.H5 + PathfinderConstants.BOLD + temp + PathfinderConstants.EBOLD;
                    if (Hold.Length > 0) spellText = spellText.ReplaceFirst(Hold, temp, PosStart);
                }
                else
                {
                    break;
                }
                if (PosFind + temp.Length > spellText.Length)
                {
                    break;
                }

                PosFind = spellText.IndexOf("(CL ", PosFind + temp.Length);
            }

            PosHold = spellText.IndexOf(":", PosHold);
            temp = spellText;
            temp = temp.Replace("--", "-");
            temp = temp.Replace("-", "—");
            temp = temp.ReplaceFirst("At will—", PathfinderConstants.BREAK + "At will—");
            temp = temp.ReplaceFirst("At will -", PathfinderConstants.BREAK + "At will—");
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
            temp = temp.Replace("—", "&mdash;");
            temp = temp.Replace("+/&mdash;", "+/-");
            temp = temp.Replace("Spell&mdash;Like", "Spell-Like");
            temp = temp.Replace(CRLF, PathfinderConstants.BREAK);
            temp = temp.Replace(PathfinderConstants.ASCII_10, PathfinderConstants.BREAK);
            temp = temp.Replace(PathfinderConstants.BREAK + PathfinderConstants.BREAK, PathfinderConstants.BREAK);

            spellText = temp + PathfinderConstants.EH5;
        }
    }
}
