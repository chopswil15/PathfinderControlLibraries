using PathfinderGlobals;
using StatBlockCommon.Affliction_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities;

namespace StatBlockFormating
{
    public class AfflictionStatBlock_Format : IAfflictionStatBlock_Format
    {       
        protected readonly string CRLF = Environment.NewLine;

        public AfflictionStatBlock AfflictionSB { get; set; }
        public List<string> ItalicPhrases { get; set; }
        public List<string> BoldPhrases { get; set; }


        public string CreateFullText(AfflictionStatBlock SB)
        {
            AfflictionSB = SB;
            StringBuilder fullTextSB = new StringBuilder();

            fullTextSB.Append(CommonMethods.Get_CSS_Ref());

            Foramt_Basics_Div(fullTextSB);


            fullTextSB.Append(PathfinderConstants.EDIV);
            string Temp = fullTextSB.ToString();
            foreach (string phrase in ItalicPhrases)
            {
                if (Temp.Contains(phrase)) Temp = Temp.Replace(phrase, PathfinderConstants.ITACLIC + phrase + PathfinderConstants.EITACLIC);
            }

            List<string> Supers = Utility.GetSuperScripts();
            foreach (string super in Supers)
            {
                Temp = Temp.Replace(super, "<sup>" + super + "</sup>");
            }

            return Temp;
        }

        private void Foramt_Basics_Div(StringBuilder oText)
        {
            oText.Append("<div class=" + PathfinderConstants.QUOTE + "heading" + PathfinderConstants.QUOTE + ">");

            oText.Append("<p class=" + PathfinderConstants.QUOTE + "alignleft" + PathfinderConstants.QUOTE + ">" + AfflictionSB.name + "</p>");
            oText.Append("<div style=" + PathfinderConstants.QUOTE + "clear: both;" + PathfinderConstants.QUOTE + "></div></div>");

            oText.Append(PathfinderConstants.DIV);

            if (AfflictionSB.type.Length > 0)
            {
                oText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Type " + PathfinderConstants.EBOLD + AfflictionSB.type);
                if (AfflictionSB.addiction.Length > 0)
                {
                    oText.Append(PathfinderConstants.SC_SP + PathfinderConstants.BOLD + "Addiction " + PathfinderConstants.EBOLD + AfflictionSB.addiction + PathfinderConstants.EH5);
                }
                else
                {
                    oText.Append(PathfinderConstants.SC_SP + PathfinderConstants.BOLD + "Save " + PathfinderConstants.EBOLD + AfflictionSB.save + PathfinderConstants.EH5);
                }
            }
            if (AfflictionSB.drug)
            {
                oText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Price " + PathfinderConstants.EBOLD + AfflictionSB.cost);
            }

            if (AfflictionSB.onset.Length > 0 && !AfflictionSB.drug)
            {
                oText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Onset " + PathfinderConstants.EBOLD + AfflictionSB.onset);
                oText.Append(PathfinderConstants.SC_SP + PathfinderConstants.BOLD + "Frequency " + PathfinderConstants.EBOLD + AfflictionSB.frequency + PathfinderConstants.EH5);
            }

            if (AfflictionSB.effect.Length > 0)
            {
                if (AfflictionSB.initial_effect.Length > 0)
                {
                    oText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Initial Effect " + PathfinderConstants.EBOLD + AfflictionSB.initial_effect + PathfinderConstants.SC_SP + PathfinderConstants.BOLD + "Secondary Effect " + PathfinderConstants.EBOLD + AfflictionSB.secondary_effect + PathfinderConstants.EH5 + PathfinderConstants.H5);
                }
                else
                {
                    oText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Effect " + PathfinderConstants.EBOLD + AfflictionSB.effect);
                }
                if (!AfflictionSB.drug)
                {
                    oText.Append(PathfinderConstants.SC_SP + PathfinderConstants.BOLD + "Cure " + PathfinderConstants.EBOLD + AfflictionSB.cure);
                    oText.Append(PathfinderConstants.EH5);
                }
            }
            else
            {
                oText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Cure " + PathfinderConstants.EBOLD + AfflictionSB.cure);
                oText.Append(PathfinderConstants.EH5);
            }

            if (AfflictionSB.cost.Length > 0 && !AfflictionSB.drug)
            {
                oText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Cost " + PathfinderConstants.EBOLD + AfflictionSB.cost + PathfinderConstants.EH5);
            }

            if (AfflictionSB.damage.Length > 0)
            {
                oText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Damage " + PathfinderConstants.EBOLD + AfflictionSB.damage + PathfinderConstants.EH5);
            }

            if (AfflictionSB.spell_effect.Length > 0)
            {
                oText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Spell Effect " + PathfinderConstants.EBOLD + AfflictionSB.spell_effect + PathfinderConstants.EH5);
            }

            if (AfflictionSB.special.Length > 0)
            {
                oText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Special " + PathfinderConstants.EBOLD + AfflictionSB.special + PathfinderConstants.EH5);
            }

            if (AfflictionSB.description.Length > 0)
            {
                oText.Append(PathfinderConstants.H5 + AfflictionSB.description + PathfinderConstants.EH5);
            }


        }
    }
}
