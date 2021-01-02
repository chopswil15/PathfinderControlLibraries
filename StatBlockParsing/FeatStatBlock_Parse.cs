using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStrings;
using PathfinderGlobals;
using StatBlockCommon.Feat_SB;
using Utilities;

namespace StatBlockParsing
{
    public class FeatStatBlock_Parse
    {
        private string CR = Environment.NewLine;
        private FeatStatBlock _oneFeat;
        public List<string> ItalicPhrases { get; set; }
        public List<string> BoldPhrases { get; set; }

        public FeatStatBlock Parse(string Featstr, ref string ErrorMessage)
        {
            _oneFeat = new FeatStatBlock();

            Featstr = Featstr.Replace(" f ", " f")
                .Replace("half ling", "halfling")
                .Replace("ef lect ", "eflect ")
                .Replace("ef lexe ", "eflexe ")
                .Replace("ef lexes ", "eflexes ")
                .Replace("ef lexes,", "eflexes,")
                .Replace("×", "x")
                .Replace("°", "&#176;");
            Featstr = Featstr.Replace("Benef it:", "Benefit:");
            Featstr = Featstr.Replace("COMBAT", "Combat");
            Featstr = Featstr.Replace("STORY", "Story");
            Featstr = Featstr.Replace("TEAMWORK", "Teamwork");
            Featstr = Featstr.Replace((char)(8217), char.Parse("'"));
            Featstr = Featstr.Replace((char)(8212), char.Parse("-"));
            Featstr = Featstr.Replace((char)(8211), char.Parse("-"));
            Featstr = Featstr.Replace((char)(150), char.Parse("-"));
            Featstr = Featstr.Replace((char)(151), char.Parse("-"));
            Featstr = Featstr.Replace("“", ((char)34).ToString());
            Featstr = Featstr.Replace("”", ((char)34).ToString());
            Featstr = Featstr.Replace("•", "&#8226;");
            Featstr = Featstr.Replace("†", string.Empty);
            Featstr = Featstr.Replace("f lict", "flict");
            Featstr = Featstr.Replace("fl ict", "flict");

            Featstr = Featstr.Trim();
            int CRPos = Featstr.IndexOf(CR);
            if (CRPos == -1)
            {
                CR = "\n";
                CRPos = Featstr.IndexOf(CR);
            }

            if (CRPos == -1)
            {
                ErrorMessage = "Can't determine End Of Line";
                return _oneFeat;
            }

            _oneFeat.type = "General";

            string temp2 = Featstr.Substring(0, CRPos);
            Featstr = Featstr.Replace(temp2 + CR, string.Empty).Trim();
            int Pos2 = temp2.IndexOf(PathfinderConstants.PAREN_LEFT);
            if (Pos2 == -1) Pos2 = temp2.IndexOf("[");
            if (Pos2 >= 0)
            {
                string temp = temp2.Substring(Pos2);
                temp2 = temp2.Replace(temp, string.Empty);
                temp = Utility.RemoveParentheses(temp);
                temp = temp.Replace("[", string.Empty)
                    .Replace("]", string.Empty).Trim();
                if (temp.Contains("Critical"))
                {
                    temp = temp.Replace("Critical", string.Empty)
                        .Replace(",", string.Empty).Trim();
                    _oneFeat.critical = true;
                }
                if (temp.Contains("Teamwork"))
                {
                    temp = temp.Replace("Teamwork", string.Empty)
                        .Replace(",", string.Empty).Trim();
                    _oneFeat.teamwork = true;
                }
                if (temp.Contains("Style"))
                {
                    temp = temp.Replace("Style", string.Empty)
                        .Replace(",", string.Empty).Trim();
                    _oneFeat.style = true;
                }
                if (temp.Contains("Performance"))
                {
                    temp = temp.Replace("Performance", string.Empty)
                        .Replace(",", string.Empty).Trim();
                    _oneFeat.peformance = true;
                }
                if (temp.Contains("Grit"))
                {
                    temp = temp.Replace("Grit", string.Empty)
                        .Replace(",", string.Empty).Trim();
                    _oneFeat.grit = true;
                }
                if (temp.Contains("Panache"))
                {
                    temp = temp.Replace("Panache", string.Empty)
                        .Replace(",", string.Empty).Trim();
                    _oneFeat.panache = true;
                }
                if (temp.Contains("Betrayal"))
                {
                    temp = temp.Replace("Betrayal", string.Empty)
                        .Replace(",", string.Empty).Trim();
                    _oneFeat.betrayal = true;
                }
                if (temp.Contains("Targeting"))
                {
                    temp = temp.Replace("Targeting", string.Empty)
                        .Replace(",", string.Empty).Trim();
                    _oneFeat.targeting = true;
                }
                if (temp.Contains("Esoteric"))
                {
                    temp = temp.Replace("Esoteric", string.Empty)
                        .Replace(",", string.Empty).Trim();
                    _oneFeat.esoteric = true;
                }
                if (temp.Contains("Stare"))
                {
                    temp = temp.Replace("Stare", string.Empty)
                        .Replace(",", string.Empty).Trim();
                    _oneFeat.stare = true;
                }
                if (temp.Contains("Weapon Mastery"))
                {
                    temp = temp.Replace("Weapon Mastery", string.Empty)
                        .Replace(",", string.Empty).Trim();
                    _oneFeat.weapon_mastery = true;
                }
                if (temp.Contains("Item Mastery"))
                {
                    temp = temp.Replace("Item Mastery", string.Empty)
                        .Replace(",", string.Empty).Trim();
                    _oneFeat.item_mastery = true;
                }
                if (temp.Contains("Armor Mastery"))
                {
                    temp = temp.Replace("Armor Mastery", string.Empty)
                        .Replace(",", string.Empty).Trim();
                    _oneFeat.armor_mastery = true;
                }
                if (temp.Contains("Shield Mastery"))
                {
                    temp = temp.Replace("Shield Mastery", string.Empty)
                        .Replace(",", string.Empty).Trim();
                    _oneFeat.shield_mastery = true;
                }
                if (temp.Contains("Blood Hex"))
                {
                    temp = temp.Replace("Blood Hex", string.Empty)
                        .Replace(",", string.Empty).Trim();
                    _oneFeat.blood_hex = true;
                }
                if (temp.Contains("Trick"))
                {
                    temp = temp.Replace("Trick", string.Empty)
                        .Replace(",", string.Empty).Trim();
                    _oneFeat.trick = true;
                }
                if (temp.Contains("Money"))
                {
                    temp = temp.Replace("Money", string.Empty)
                        .Replace(",", string.Empty).Trim();
                    _oneFeat.money = true;
                }

                _oneFeat.mythic = temp.Contains("Mythic");
                if (temp.Length > 0) _oneFeat.type = temp.ProperCase().Trim();
            }

            _oneFeat.name = temp2.ProperCase().Trim();

            //work your way back
            int Pos = Featstr.IndexOf("Note:");

            if (Pos >= 0)
            {
                temp2 = Featstr.Substring(Pos);
                Featstr = Featstr.Replace(temp2, string.Empty).Trim();
                temp2 = temp2.Replace("Note:", string.Empty).Trim();
                //remove the unwanted CRs
                temp2 = temp2.Replace(CR, PathfinderConstants.SPACE);
                _oneFeat.note = temp2;
            }

            Pos = Featstr.IndexOf("Suggested Traits:");
            if (Pos >= 0)
            {
                temp2 = Featstr.Substring(Pos);
                Featstr = Featstr.Replace(temp2, string.Empty).Trim();
                temp2 = temp2.Replace("Suggested Traits:", string.Empty).Trim();
                //remove the unwanted CRs
                temp2 = temp2.Replace(CR, PathfinderConstants.SPACE);
                _oneFeat.suggested_traits = temp2;
            }

            Pos = Featstr.IndexOf("Special:");
            if (Pos >= 0)
            {
                temp2 = Featstr.Substring(Pos);
                Featstr = Featstr.Replace(temp2, string.Empty).Trim();
                temp2 = temp2.Replace("Special:", string.Empty).Trim();
                //remove the unwanted CRs
                temp2 = temp2.Replace(CR, PathfinderConstants.SPACE);
                _oneFeat.special = temp2;
                if (_oneFeat.special.Contains("multiple times")) _oneFeat.multiples = true;
            }



            Pos = Featstr.IndexOf("Completion Benefit:");
            if (Pos >= 0)
            {
                temp2 = Featstr.Substring(Pos);
                Featstr = Featstr.Replace(temp2, string.Empty).Trim();
                temp2 = temp2.Replace("Completion Benefit:", string.Empty).Trim();
                //remove the unwanted CRs
                temp2 = temp2.Replace(CR, PathfinderConstants.SPACE);
                _oneFeat.completion_benefit = temp2;
            }

            Pos = Featstr.IndexOf("Goal:");
            if (Pos >= 0)
            {
                temp2 = Featstr.Substring(Pos);
                Featstr = Featstr.Replace(temp2, string.Empty).Trim();
                temp2 = temp2.Replace("Goal:", string.Empty).Trim();
                //remove the unwanted CRs
                temp2 = temp2.Replace(CR, PathfinderConstants.SPACE);
                _oneFeat.goal = temp2;
            }


            Pos = Featstr.IndexOf("Normal:");
            if (Pos >= 0)
            {
                temp2 = Featstr.Substring(Pos);
                Featstr = Featstr.Replace(temp2, string.Empty).Trim();
                temp2 = temp2.Replace("Normal:", string.Empty).Trim();
                //remove the unwanted CRs
                temp2 = temp2.Replace(CR, PathfinderConstants.SPACE);
                _oneFeat.normal = temp2;
            }

            Pos = Featstr.IndexOf("Benefit:");
            if (Pos >= 0)
            {
                temp2 = Featstr.Substring(Pos);
                Featstr = Featstr.Replace(temp2, string.Empty).Trim();
                temp2 = temp2.Replace("Benefit:", string.Empty).Trim();
                //remove the unwanted CRs
                temp2 = temp2.Replace(CR, PathfinderConstants.SPACE);
                _oneFeat.benefit = temp2;
            }

            Pos = Featstr.IndexOf("Benefits:");
            if (Pos >= 0)
            {
                temp2 = Featstr.Substring(Pos);
                Featstr = Featstr.Replace(temp2, string.Empty).Trim();
                temp2 = temp2.Replace("Benefits:", string.Empty).Trim();
                //remove the unwanted CRs
                temp2 = temp2.Replace(CR, PathfinderConstants.SPACE);
                _oneFeat.benefit = temp2;
            }

            Pos = Featstr.IndexOf("Prerequisites:");
            if (Pos >= 0)
            {
                temp2 = Featstr.Substring(Pos);
                Featstr = Featstr.Replace(temp2, string.Empty).Trim();
                temp2 = temp2.Replace("Prerequisites:", string.Empty).Trim();
                //remove the unwanted CRs
                temp2 = temp2.Replace(CR, PathfinderConstants.SPACE);
                _oneFeat.prerequisites = temp2;
            }

            Pos = Featstr.IndexOf("Prerequisite:");
            if (Pos >= 0)
            {
                temp2 = Featstr.Substring(Pos);
                Featstr = Featstr.Replace(temp2, string.Empty).Trim();
                temp2 = temp2.Replace("Prerequisite:", string.Empty).Trim();
                //remove the unwanted CRs
                temp2 = temp2.Replace(CR, PathfinderConstants.SPACE);
                _oneFeat.prerequisites = temp2;
            }

            Pos = Featstr.IndexOf("Requirement:");
            if (Pos >= 0)
            {
                temp2 = Featstr.Substring(Pos);
                Featstr = Featstr.Replace(temp2, string.Empty).Trim();
                temp2 = temp2.Replace("Requirement:", string.Empty).Trim();
                //remove the unwanted CRs
                temp2 = temp2.Replace(CR, PathfinderConstants.SPACE);
                _oneFeat.prerequisites = temp2;
            }


            //left over is description
            Featstr = Featstr.Replace(CR, PathfinderConstants.SPACE);
            _oneFeat.description = Featstr;

            StatBlockFormating.FeatStatBlock_Format FeatSB_Form = new StatBlockFormating.FeatStatBlock_Format();
            FeatSB_Form.ItalicPhrases = ItalicPhrases;
            FeatSB_Form.BoldPhrases = BoldPhrases;
            _oneFeat.full_text = FeatSB_Form.CreateFullText(_oneFeat);
            _oneFeat.full_text = _oneFeat.full_text.Trim();

            return _oneFeat;
        }
    }
}
