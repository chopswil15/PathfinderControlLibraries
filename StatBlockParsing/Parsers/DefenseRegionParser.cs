using CommonStrings;
using System;

using Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathfinderGlobals;

namespace StatBlockParsing
{
    public class DefenseRegionParser : IDefenseRegionParser
    {
        private ISBCommonBaseInput _sbcommonBaseInput;

        public DefenseRegionParser(ISBCommonBaseInput sbcommonBaseInput)
        {
            _sbcommonBaseInput = sbcommonBaseInput;
        }

        public void ParseDefense(string defense, string CR)
        {
            defense = defense.Replace("Defense", string.Empty).Trim()
                .Replace("&#8211;", "-");

            int Pos = defense.IndexOf("flat-footed");
            if (Pos == -1)
            {
                throw new Exception("Missing flat-footed AC");
            }
            Pos = defense.IndexOf(PathfinderConstants.PAREN_LEFT, Pos);
            int Pos2 = defense.IndexOf(CR);

            if (Pos2 < Pos) Pos = Pos2;
            if (Pos == -1) Pos = defense.IndexOf(";");
            if (Pos == -1) Pos = defense.IndexOf(CR);
            string temp = defense.Substring(0, Pos);

            defense = defense.Replace(temp, string.Empty).Trim();
            if (defense.Contains(" or " + CR + "AC")) //2nd AC block
            {
                Pos2 = defense.IndexOf(CR);
                Pos = defense.IndexOf("flat-footed", Pos2);
                Pos = defense.IndexOf(PathfinderConstants.PAREN_LEFT, Pos);

                string temp5 = defense.Substring(0, Pos);
                Pos = temp5.IndexOf("AC");
                temp5 = temp5.Substring(Pos);

                defense = defense.Replace(temp5, string.Empty).Trim();
                temp = temp + CR + temp5;
            }
            temp = temp.Replace(";", string.Empty)
                .Replace("AC", string.Empty);
            _sbcommonBaseInput.MonsterSB.AC = temp.Trim();

            int CRPos = defense.IndexOf(PathfinderConstants.PAREN_RIGHT);
            if (_sbcommonBaseInput.MonsterSB.AC.Contains(CR)) //2nd AC block
            {
                CRPos = defense.IndexOf(PathfinderConstants.PAREN_RIGHT, CRPos + 1);
            }
            Pos2 = defense.IndexOf("hp");
            if (Pos2 > CRPos)
            {
                temp = defense.Substring(0, CRPos + 1);
                defense = defense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(";", "|")
                    .Replace(CR, PathfinderConstants.SPACE);
                _sbcommonBaseInput.MonsterSB.AC_Mods = temp.Trim();

                CRPos = defense.IndexOf(PathfinderConstants.PAREN_RIGHT);
                Pos2 = defense.IndexOf("hp");
                if (Pos2 > CRPos && CRPos > -1)
                {
                    temp = defense.Substring(0, CRPos + 1);
                    defense = defense.Replace(temp, string.Empty).Trim();
                    temp = temp.Replace(";", "|")
                        .Replace(CR, PathfinderConstants.SPACE);
                    _sbcommonBaseInput.MonsterSB.AC_Mods += temp.Trim();
                }
            }

            CRPos = defense.IndexOf(";");
            Pos2 = defense.IndexOf("hp");
            if (Pos2 > CRPos && CRPos != -1)
            {
                temp = defense.Substring(0, Pos2);
                defense = defense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(";", string.Empty);
                temp = temp.Replace(CR, PathfinderConstants.SPACE);
                temp = PathfinderConstants.PAREN_LEFT + temp.Trim() + PathfinderConstants.PAREN_RIGHT;
                _sbcommonBaseInput.MonsterSB.AC_Mods += PathfinderConstants.SPACE + temp;
            }

            Pos = defense.IndexOf(PathfinderConstants.PAREN_RIGHT);
            if (defense.Contains("(currently"))
            {
                int Pos4 = defense.IndexOf(PathfinderConstants.PAREN_RIGHT, Pos + 2);
                int Pos3 = defense.IndexOf(";");
                if (Pos3 < Pos4) Pos = Pos3;
            }
            if (Pos == -1) throw new Exception("No HD Parens");
            temp = defense.Substring(0, Pos + 1);
            defense = defense.Replace(temp, string.Empty).Trim();
            Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
            if (temp.Contains("(currently"))
            {
                Pos = temp.IndexOf("(cu");
            }
            if (Pos == -1)
            {
                Pos = temp.IndexOf(";");
            }
            string temp2 = temp.Substring(0, Pos);
            if (temp.Length > 0) temp = temp.Replace(temp2, string.Empty);
            temp2 = temp2.Replace("hp", string.Empty).Trim();
            _sbcommonBaseInput.MonsterSB.HP = temp2.Replace(" each", string.Empty);
            _sbcommonBaseInput.MonsterSB.HD = temp.Trim();


            Pos = defense.IndexOf("Fort");
            if (Pos > 0)
            {
                temp = defense.Substring(0, Pos);
                defense = defense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(";", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.HP_Mods = temp;
            }


            //work your way back
            Pos = defense.IndexOf("Weaknesses");
            if (Pos >= 0)
            {
                temp = defense.Substring(Pos);
                defense = defense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Weaknesses", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Weaknesses = temp;
            }
            Pos = defense.IndexOf("Weakness");
            if (Pos >= 0)
            {
                temp = defense.Substring(Pos);
                defense = defense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Weakness", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Weaknesses = temp;
            }

            Pos = defense.IndexOf("SR");
            if (Pos >= 0)
            {
                temp = defense.Substring(Pos);
                defense = defense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("SR", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.SR = temp;
            }

            Pos = defense.IndexOf("Resist");
            if (Pos >= 0)
            {
                temp = defense.Substring(Pos);
                defense = defense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Resist", string.Empty).Trim()
                    .Replace(";", string.Empty);
                _sbcommonBaseInput.MonsterSB.Resist = temp;
            }

            Pos = defense.IndexOf("Vulnerability ");
            if (Pos >= 0)
            {
                temp = defense.Substring(Pos);
                defense = defense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(";", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace("Vulnerability", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Vulnerability = temp;
            }

            defense = defense.Replace("Immune\n", "Immune ");
            defense = defense.Replace("Immune" + CR, "Immune ");
            Pos = defense.IndexOf("Immune ");
            if (Pos >= 0)
            {
                temp = defense.Substring(Pos);
                defense = defense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(";", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace("Immune", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Immune = temp;
            }

            defense = defense.Replace("; DR" + CR, "; DR ");
            Pos = defense.IndexOf("DR ");
            if (Pos >= 0)
            {
                temp = defense.Substring(Pos);
                defense = defense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(";", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace("DR", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.DR = temp;
            }

            Pos = defense.IndexOf("Defensive Abilities");
            if (Pos >= 0)
            {
                temp = defense.Substring(Pos);
                defense = defense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(";", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace("Defensive Abilities", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.DefensiveAbilities = temp;
            }
            Pos = defense.IndexOf("Defensive Ability ");
            if (Pos >= 0)
            {
                temp = defense.Substring(Pos);
                defense = defense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(";", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace("Defensive Ability", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.DefensiveAbilities = temp;
            }


            try
            {
                _sbcommonBaseInput.MonsterSB.Saves = defense.Replace(CR, PathfinderConstants.SPACE);
                defense = defense.Replace("Fort", string.Empty);
                Pos = defense.IndexOf(",");
                temp = defense.Substring(0, Pos);
                defense = defense.Remove(0, Pos + 1).Trim();
                _sbcommonBaseInput.MonsterSB.Fort = temp.Trim();

                defense = defense.ReplaceFirst("Ref", string.Empty).Trim();
                Pos = defense.IndexOf(",");
                temp = defense.Substring(0, Pos);
                defense = defense.Remove(0, Pos + 1).Trim();
                _sbcommonBaseInput.MonsterSB.Ref = temp.Trim();

                defense = defense.Replace("Will", string.Empty).Trim();
            }
            catch
            {
                throw new Exception("Incorrect Save formatting");
            }

            Pos = defense.IndexOf("; ");

            if (Pos >= 0)
            {
                temp = defense.Substring(Pos);
                defense = defense.Replace(temp, string.Empty);
                temp = temp.Replace("; ", string.Empty).Replace(CR, PathfinderConstants.SPACE).Trim();
                _sbcommonBaseInput.MonsterSB.Save_Mods = temp;
            }

            defense = defense.Replace(";", string.Empty);
            _sbcommonBaseInput.MonsterSB.Will = defense;
        }
    }
}
