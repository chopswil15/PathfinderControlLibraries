using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathfinderGlobals;
using StatBlockCommon.Feat_SB;
using Utilities;

namespace StatBlockFormating
{
    public class FeatStatBlock_Format : IFeatStatBlock_Format
    {

        protected readonly string CRLF = Environment.NewLine;

        public FeatStatBlock FeatSB { get; set; }
        public List<string> ItalicPhrases { get; set; }
        public List<string> BoldPhrases { get; set; }


        public string CreateFullText(FeatStatBlock SB)
        {
            FeatSB = SB;
            StringBuilder oText = new StringBuilder();

            oText.Append(CommonMethods.Get_CSS_Ref());

            Foramt_Basics_Div(oText);
            oText.Append(PathfinderConstants.EDIV);
            string Temp = oText.ToString();
            foreach (string phrase in ItalicPhrases)
            {
                if (Temp.IndexOf(phrase) >= 0)
                {
                    Temp = Temp.Replace(phrase, PathfinderConstants.ITACLIC + phrase + PathfinderConstants.EITACLIC);
                }
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
            string temp = FeatSB.name;
            //if (FeatSB.type != "General")
            //{
            List<string> temp3 = new List<string> { FeatSB.type };
            if (FeatSB.critical) temp3.Add("Critical");
            if (FeatSB.teamwork) temp3.Add("Teamwork");
            if (FeatSB.style) temp3.Add("Style");
            if (FeatSB.peformance) temp3.Add("Performance");
            if (FeatSB.grit) temp3.Add("Grit");
            if (FeatSB.panache) temp3.Add("Panache");
            if (FeatSB.betrayal) temp3.Add("Betrayal");
            if (FeatSB.targeting) temp3.Add("Targeting");
            if (FeatSB.esoteric) temp3.Add("Esoteric");
            if (FeatSB.stare) temp3.Add("Stare");
            if (FeatSB.weapon_mastery) temp3.Add("Weapon Mastery");
            if (FeatSB.item_mastery) temp3.Add("Item Mastery");
            if (FeatSB.armor_mastery) temp3.Add("Armor Mastery");
            if (FeatSB.shield_mastery) temp3.Add("Shield Mastery");
            if (FeatSB.blood_hex) temp3.Add("Blood Hex");
            if (FeatSB.trick) temp3.Add("Trick");
            if (FeatSB.money) temp3.Add("Money");

            temp3.RemoveAll(x => x == string.Empty);
            temp3.Remove("General");
            if (temp3.Any())
            {
                temp += " (" + string.Join(", ", temp3.ToArray()) + PathfinderConstants.PAREN_RIGHT;
            }
            //}
            oText.Append("<p class=" + PathfinderConstants.QUOTE + "alignleft" + PathfinderConstants.QUOTE + ">" + temp + "</p>");
            oText.Append("<div style=" + PathfinderConstants.QUOTE + "clear: both;" + PathfinderConstants.QUOTE + "></div></div>");

            oText.Append(PathfinderConstants.DIV);
            oText.Append(PathfinderConstants.H5 + FeatSB.description);

            if (FeatSB.prerequisites.Length > 0)
            {
                oText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Prerequisites: " + PathfinderConstants.EBOLD + FeatSB.prerequisites + PathfinderConstants.EH5);
            }

            if (FeatSB.benefit.Length > 0)
            {
                oText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Benefit: " + PathfinderConstants.EBOLD + FeatSB.benefit + PathfinderConstants.EH5);
            }

            if (FeatSB.normal.Length > 0)
            {
                oText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Normal: " + PathfinderConstants.EBOLD + FeatSB.normal + PathfinderConstants.EH5);
            }

            if (FeatSB.goal.Length > 0)
            {
                oText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Goal: " + PathfinderConstants.EBOLD + FeatSB.goal + PathfinderConstants.EH5);
            }

            if (FeatSB.completion_benefit.Length > 0)
            {
                oText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Completion Benefit: " + PathfinderConstants.EBOLD + FeatSB.completion_benefit + PathfinderConstants.EH5);
            }

            if (FeatSB.special.Length > 0)
            {
                oText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Special: " + PathfinderConstants.EBOLD + FeatSB.special + PathfinderConstants.EH5);
            }

            if (FeatSB.note.Length > 0)
            {
                oText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Note: " + PathfinderConstants.EBOLD + FeatSB.note + PathfinderConstants.EH5);
            }

            if (FeatSB.suggested_traits.Length > 0)
            {
                oText.Append(PathfinderConstants.H5 + PathfinderConstants.BOLD + "Suggested Traits: " + PathfinderConstants.EBOLD + FeatSB.suggested_traits + PathfinderConstants.EH5);
            }
        }
    }
}
