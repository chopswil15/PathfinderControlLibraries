using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStrings;
using PathfinderContext.Services;
using PathfinderGlobals;
using StatBlockCommon;
using StatBlockCommon.Spell_SB;
using Utilities;

namespace StatBlockFormating
{
    public class SpellStatBlock_Format : ISpellStatBlock_Format
    {
        protected readonly string CRLF = Environment.NewLine;

        public ISpellStatBlock SpellSB { get; set; }
        public List<string> ItalicPhrases { get; set; }
        public List<string> BoldPhrases { get; set; }

        public string CreateFullText(ISpellStatBlock SB)
        {
            SpellSB = SB;
            StringBuilder fullTextSB = new StringBuilder();

            fullTextSB.Append(CommonMethods.Get_CSS_Ref());

            Foramt_Basics_Div(fullTextSB);
            fullTextSB.Append(PathfinderConstants.EDIV);

            Format_Description_Div(fullTextSB);
            Format_HauntStatistics(fullTextSB);

            string Temp = fullTextSB.ToString();
            FindReplaceTextService findReplaceTextService = new FindReplaceTextService(PathfinderConstants.ConnectionString);
            findReplaceTextService.ExecuteFindReplaceOnText(ref Temp);
            // Utility.FixItalics(ref Temp);

            return Temp;
        }

        private void Format_HauntStatistics(StringBuilder Text)
        {
            if (SpellSB.haunt_statistics.Length == 0) return;

            Text.Append(PathfinderConstants.DIV + PathfinderConstants.H5 + PathfinderConstants.BOLD + "Haunt Statistics" + PathfinderConstants.EBOLD + PathfinderConstants.EH5 + PathfinderConstants.EDIV);
            string holdHS = SpellSB.haunt_statistics;
            holdHS = holdHS.Replace("Notice", PathfinderConstants.BOLD + "Notice" + PathfinderConstants.EBOLD);
            holdHS = holdHS.Replace("hp ", PathfinderConstants.BREAK + PathfinderConstants.BOLD + "hp " + PathfinderConstants.EBOLD);
            holdHS = holdHS.Replace("Trigger", PathfinderConstants.BOLD + "Trigger" + PathfinderConstants.EBOLD);
            holdHS = holdHS.Replace("Reset", PathfinderConstants.BOLD + "Reset" + PathfinderConstants.EBOLD);
            Text.Append(PathfinderConstants.DIV + PathfinderConstants.H5 + holdHS + PathfinderConstants.EH5 + PathfinderConstants.EDIV);
        }

        private void Format_MythicText_Div(StringBuilder Text)
        {
            string mythic_textHold = SpellSB.mythic_text;
            if (SpellSB.mythic_text.Contains("•"))//list
            {
                int Pos = mythic_textHold.IndexOf("•");
                mythic_textHold = mythic_textHold.Insert(Pos, PathfinderConstants.LIST);
                mythic_textHold = mythic_textHold + PathfinderConstants.ELIST;
                mythic_textHold = mythic_textHold.Replace("•", PathfinderConstants.LIST_ITEM);
            }
            Text.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Mythic: " + PathfinderConstants.EBOLD + mythic_textHold + PathfinderConstants.EH5);
        }

        public string ReCreateFullText(SpellStatBlock SB)
        {
            //find all italics
            SpellSB = SB;
            FindItalics();
            BoldPhrases = new List<string>();

            return CreateFullText(SB);
        }

        private void FindItalics()
        {
            string HoldText = SpellSB.full_text;
            string StartTag = "<i>";
            string EndTag = "</i>";
            string temp;
            int HoldPos = HoldText.IndexOf(StartTag);
            int Pos = HoldText.IndexOf(EndTag);
            List<string> tempItalics = new List<string>();

            while (HoldPos >= 0)
            {
                temp = HoldText.Substring(HoldPos, Pos - HoldPos + EndTag.Length);
                HoldText = HoldText.Replace(temp, string.Empty);
                temp = temp.Replace(StartTag, string.Empty);
                temp = temp.Replace(EndTag, string.Empty).Trim();
                if (tempItalics.Contains(temp) == false)
                {
                    tempItalics.Add(temp);
                }
                HoldPos = HoldText.IndexOf(StartTag);
                Pos = HoldText.IndexOf(EndTag);
            }

            ItalicPhrases = tempItalics;
        }

        private void Foramt_Basics_Div(StringBuilder formatBasicsSB)
        {
            formatBasicsSB.Append("<div class=" + PathfinderConstants.QUOTE + "heading" + PathfinderConstants.QUOTE + ">");
            formatBasicsSB.Append("<p class=" + PathfinderConstants.QUOTE + "alignleft" + PathfinderConstants.QUOTE + ">" + SpellSB.name + "</p>");
            formatBasicsSB.Append("<div style=" + PathfinderConstants.QUOTE + "clear: both;" + PathfinderConstants.QUOTE + "></div></div>");

            formatBasicsSB.Append(PathfinderConstants.DIV);
            formatBasicsSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "School " + PathfinderConstants.EBOLD + SpellSB.school);
            if (SpellSB.subschool.Length > 0)
            {
                formatBasicsSB.Append(" (" + SpellSB.subschool + PathfinderConstants.PAREN_RIGHT);
            }
            if (SpellSB.descriptor.Length > 0)
            {
                List<string> superscripts = Utility.GetSuperScripts();
                string tempDescriptor = SpellSB.descriptor;
                foreach (string super in superscripts)
                {
                    if (tempDescriptor.Contains(super))
                    {
                        tempDescriptor = tempDescriptor.Replace(super, PathfinderConstants.SUPER + super + PathfinderConstants.ESUPER);
                    }
                }
                formatBasicsSB.Append(" [" + tempDescriptor + "]");
            }
            List<string> Supers = Utility.GetSuperScripts();
            string spellLeveltemp = SpellSB.spell_level;
            foreach (string super in Supers)
            {
                spellLeveltemp = spellLeveltemp.Replace(super, "<sup>" + super + "</sup>");
            }
            formatBasicsSB.Append(PathfinderConstants.SC_SP + PathfinderConstants.BOLD + "Level " + PathfinderConstants.EBOLD + spellLeveltemp);
            if (SpellSB.deity.Length > 0)
            {
                formatBasicsSB.Append(PathfinderConstants.SPACE + PathfinderConstants.PAREN_LEFT + SpellSB.deity + PathfinderConstants.PAREN_RIGHT);
            }
            formatBasicsSB.Append(PathfinderConstants.EH5);

            formatBasicsSB.Append(PathfinderConstants.EDIV);
            formatBasicsSB.Append(PathfinderConstants.LINE);
            formatBasicsSB.Append(PathfinderConstants.DIV + PathfinderConstants.H5 + PathfinderConstants.BOLD + "CASTING" + PathfinderConstants.EBOLD + PathfinderConstants.EH5 + PathfinderConstants.EDIV);
            formatBasicsSB.Append(PathfinderConstants.LINE + PathfinderConstants.DIV);

            formatBasicsSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Casting Time " + PathfinderConstants.EBOLD + SpellSB.casting_time + PathfinderConstants.EH5);
            formatBasicsSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Components " + PathfinderConstants.EBOLD + SpellSB.components + PathfinderConstants.EH5);

            formatBasicsSB.Append(PathfinderConstants.EDIV);
            formatBasicsSB.Append(PathfinderConstants.LINE);
            formatBasicsSB.Append(PathfinderConstants.DIV + PathfinderConstants.H5 + PathfinderConstants.BOLD + "EFFECT" + PathfinderConstants.EBOLD + PathfinderConstants.EH5 + PathfinderConstants.EDIV);
            formatBasicsSB.Append(PathfinderConstants.LINE + PathfinderConstants.DIV);
            if (SpellSB.range.Length > 0)
            {
                formatBasicsSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Range " + PathfinderConstants.EBOLD + SpellSB.range + PathfinderConstants.EH5);
            }

            if (SpellSB.area.Length > 0 && SpellSB.area == SpellSB.targets && SpellSB.area == SpellSB.effect)
            {
                formatBasicsSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Target, Effect, or Area " + PathfinderConstants.EBOLD + SpellSB.area);
            }
            else
            {
                if (SpellSB.area.Length > 0 && SpellSB.area == SpellSB.targets)
                {
                    formatBasicsSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Target or Area " + PathfinderConstants.EBOLD + SpellSB.area);
                }
                else
                {
                    if (SpellSB.area.Length > 0)
                    {
                        formatBasicsSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Area " + PathfinderConstants.EBOLD + SpellSB.area);
                        if (SpellSB.shapeable)
                        {
                            //   formatBasicsSB.Append(PathfinderConstants.SPACE + " (S)");
                        }
                        formatBasicsSB.Append(PathfinderConstants.EH5);
                    }

                    if (SpellSB.targets.Length > 0)
                    {
                        formatBasicsSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Targets " + PathfinderConstants.EBOLD + SpellSB.targets + PathfinderConstants.EH5);
                    }
                }

                if (SpellSB.effect.Length > 0)
                {
                    formatBasicsSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Effect " + PathfinderConstants.EBOLD + SpellSB.effect + PathfinderConstants.EH5);
                }
            }

            if (SpellSB.duration.Length > 0)
            {
                formatBasicsSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Duration " + PathfinderConstants.EBOLD + SpellSB.duration);
                if (SpellSB.dismissible)
                {
                    //   formatBasicsSB.Append(" (D)");
                }
                formatBasicsSB.Append(PathfinderConstants.EH5);
            }

            if (SpellSB.saving_throw.Length > 0)
            {
                formatBasicsSB.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Saving Throw " + PathfinderConstants.EBOLD + SpellSB.saving_throw + PathfinderConstants.SC_SP +
                                PathfinderConstants.BOLD + "Spell Resistance " + PathfinderConstants.EBOLD + SpellSB.spell_resistence + PathfinderConstants.EH5);
            }
        }

        private void Format_Description_Div(StringBuilder formatDescriptionDivSB)
        {
            string temp = SpellSB.description;
            formatDescriptionDivSB.Append(PathfinderConstants.LINE);
            formatDescriptionDivSB.Append(PathfinderConstants.DIV + PathfinderConstants.H5 + PathfinderConstants.BOLD + "DESCRIPTION" + PathfinderConstants.EBOLD + PathfinderConstants.EH5 + PathfinderConstants.EDIV);
            formatDescriptionDivSB.Append(PathfinderConstants.LINE + PathfinderConstants.DIV);

            foreach (string phrase in ItalicPhrases)
            {
                if (temp.Contains(phrase))
                {
                    temp = temp.Replace(phrase, PathfinderConstants.ITACLIC + phrase + PathfinderConstants.EITACLIC);
                }
            }
            foreach (string phrase in BoldPhrases)
            {
                if (temp.Contains(phrase))
                {
                    temp = temp.Replace(phrase, PathfinderConstants.BOLD + phrase + PathfinderConstants.EBOLD);
                }
            }
            if (temp.Contains("•"))//list
            {
                int Pos = temp.IndexOf("•");
                temp = temp.Insert(Pos, PathfinderConstants.LIST);
                temp = temp + PathfinderConstants.ELIST;
                temp = temp.Replace("•", PathfinderConstants.LIST_ITEM);
            }
            temp = "<p>" + temp;
            temp = temp.Replace(CRLF, "</p><p>");
            temp = temp + "</p>";
            SpellSB.description_formated = temp;


            formatDescriptionDivSB.Append(PathfinderConstants.H4 + temp + PathfinderConstants.EH4);


            if (SpellSB.mythic_text.Length > 0)
            {
                Format_MythicText_Div(formatDescriptionDivSB);
            }

            if (SpellSB.augmented.Length > 0)
            {
                string tempAug = SpellSB.augmented;
                int Pos = tempAug.IndexOf(":");
                string tempHold = tempAug.Substring(0, Pos);
                tempAug = tempAug.Replace(tempHold, PathfinderConstants.BOLD + tempHold + PathfinderConstants.EBOLD);
                formatDescriptionDivSB.Append(PathfinderConstants.H5 + tempAug + PathfinderConstants.EH5);
            }

            formatDescriptionDivSB.Append(PathfinderConstants.EDIV);
        }

        public void ReplaceTables(ISpellStatBlock SB_old, ISpellStatBlock SB_new)
        {
            int TableCount = SB_old.description_formated.PhraseCount("<table>");

            if (TableCount == 0) return;

            int PosStart = SB_old.description_formated.IndexOf("<table>");
            int PosEnd = SB_old.description_formated.IndexOf("</table>");
            List<string> TableHold = new List<string>();


            for (int a = 0; a < TableCount; a++)
            {
                TableHold.Add(SB_old.description_formated.Substring(PosStart, PosEnd - PosStart + "</table>".Length));
                PosStart = SB_old.description_formated.IndexOf("<table>", PosEnd);
                if (PosStart >= 0)
                {
                    PosEnd = SB_old.description_formated.IndexOf("</table>", PosStart);
                }
            }

            string TableStart = string.Empty;
            string TableEnd = string.Empty;
            string TableText = string.Empty;
            string OldFormatted = SB_new.description_formated;

            foreach (string Table in TableHold)
            {
                TableStart = FindTableTextStart(Table);
                TableEnd = FindTableTextEnd(Table);
                PosStart = SB_new.description_formated.IndexOf(TableStart);
                PosEnd = SB_new.description_formated.IndexOf(TableEnd);
                if (PosStart >= 0 && PosEnd >= 0)
                {
                    TableText = SB_new.description_formated.Substring(PosStart, PosEnd - PosStart + TableEnd.Length);
                    SB_new.description_formated = SB_new.description_formated.Replace(TableText, Table);
                    SB_new.full_text = SB_new.full_text.Replace(OldFormatted, SB_new.description_formated);
                }
            }
        }

        private string FindTableTextEnd(string Table)
        {
            string Tag = "<td";
            string EndTag = "</td>";
            int Pos = Table.LastIndexOf(Tag);

            int Pos2 = Table.IndexOf(">", Pos);
            Pos = Table.LastIndexOf(EndTag);
            return Table.Substring(Pos2 + 1, Pos - Pos2 - 1);
        }

        private string FindTableTextStart(string Table)
        {
            string Tag = "<caption";
            string EndTag = "</caption>";
            int Pos = Table.IndexOf(Tag);

            if (Pos == -1)
            {
                Tag = "<th";
                EndTag = "</th";
                Pos = Table.IndexOf(Tag);
            }

            int Pos2 = Table.IndexOf(">", Pos);
            Pos = Table.IndexOf(EndTag);
            return Table.Substring(Pos2 + 1, Pos - Pos2 - 1);

        }
    }
}
