using PathfinderGlobals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace StatBlockParsing
{
    public class StatisticsRegionParser : IStatisticsRegionParser
    {
        private ISBCommonBaseInput _sbcommonBaseInput;

        public StatisticsRegionParser(ISBCommonBaseInput sbcommonBaseInput)
        {
            _sbcommonBaseInput = sbcommonBaseInput;
        }

        public string ParseStatistics(string Statistics, string CR, out string ErrorMessage)
        {
            ErrorMessage = string.Empty;
            Statistics = Statistics.Replace("Statistics", string.Empty).Trim()
                .Replace("&#8211;", "-");
            string sourceSuperScript = string.Empty;

            string temp;
            int Pos = Statistics.IndexOf(" See ");
            if (Pos >= 0)
            {
                string temp2 = FindSuperScriptString(Statistics, Pos, CR);
                Statistics = Statistics.Replace(temp2, string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Note = temp2.Trim();
                int CRPos = _sbcommonBaseInput.MonsterSB.Note.IndexOf(PathfinderConstants.SPACE);
                sourceSuperScript = _sbcommonBaseInput.MonsterSB.Note.Substring(0, CRPos);
            }

            //work your way back
            Pos = Statistics.IndexOf("Combat Gear");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Combat Gear", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE);
                _sbcommonBaseInput.MonsterSB.Gear = temp.Trim();
            }

            Pos = Statistics.IndexOf("Gear");       
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Gear", string.Empty)
                    .Replace(";", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE);
                _sbcommonBaseInput.MonsterSB.OtherGear = temp.Trim();
            }

            Pos = Statistics.IndexOf("SQ");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("SQ", string.Empty);
                temp = temp.Replace(CR, PathfinderConstants.SPACE);
                _sbcommonBaseInput.MonsterSB.SQ = temp.Trim();
            }

            Pos = Statistics.IndexOf("Special Qualities");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Special Qualities", string.Empty);
                _sbcommonBaseInput.MonsterSB.SQ = temp.Trim();
            }


            Pos = Statistics.IndexOf("Languages");
            if (Pos == -1) Pos = Statistics.IndexOf("Language");

            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Languages", string.Empty)
                    .Replace("Language", string.Empty);
                _sbcommonBaseInput.MonsterSB.Languages = temp.Trim();
            }

            Pos = Statistics.IndexOf("Traits");
            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Traits", string.Empty);
                _sbcommonBaseInput.MonsterSB.Traits = temp.Trim();
            }

            Statistics = Statistics.Replace("Racial" + CR + "Modifiers", "Racial Modifiers")
                .Replace("Racial" + CR + "Modifier", "Racial Modifiers")
                .Replace("Racial Bonus", "Racial Modifiers")
                .Replace("Racial Modifier ", "Racial Modifiers ");

            Pos = Statistics.IndexOf("Armor Check");

            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Armor Check", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE);
            }

            Pos = Statistics.IndexOf("Racial Modifiers");
            if (Pos == -1)
            {
                Statistics = Statistics.Replace("Racial Modifier", "Racial Modifiers");
                Pos = Statistics.IndexOf("Racial Modifiers");
            }

            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Racial Modifiers", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE);
                _sbcommonBaseInput.MonsterSB.RacialMods = temp.Trim();
            }

            Pos = Statistics.IndexOf("Skills");

            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Skills", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace("  ", PathfinderConstants.SPACE)//double space to space
                    .Replace(";", string.Empty);
                _sbcommonBaseInput.MonsterSB.Skills = temp.Trim();
            }

            Pos = Statistics.IndexOf("Feats");

            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Feats", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace(";", string.Empty);
                _sbcommonBaseInput.MonsterSB.Feats = temp.Trim();
            }

            Statistics = Statistics.Replace("CM D", "CMD");
            Pos = Statistics.IndexOf("CMD");

            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("CMD", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace(";", string.Empty);
                _sbcommonBaseInput.MonsterSB.CMD = temp.Trim();
            }

            Pos = Statistics.IndexOf("CMB");

            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("CMB", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace(";", string.Empty);
                _sbcommonBaseInput.MonsterSB.CMB = temp.Trim();
            }

            Pos = Statistics.IndexOf("Base Atk");

            if (Pos >= 0)
            {
                temp = Statistics.Substring(Pos);
                Statistics = Statistics.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Base Atk", string.Empty)
                    .Replace(CR, PathfinderConstants.SPACE)
                    .Replace(";", string.Empty);
                _sbcommonBaseInput.MonsterSB.BaseAtk = temp.Trim();
            }
            else
            {
                ErrorMessage = "No Base Atk found, missing or Stat Block Order Issue.";
                return sourceSuperScript;
            }

            _sbcommonBaseInput.MonsterSB.AbilityScores = Statistics.Replace("Abilities", string.Empty).Trim()
                 .Replace(CR, PathfinderConstants.SPACE)
                .Replace("  ", PathfinderConstants.SPACE) //doulbe space
                .Replace("\n", PathfinderConstants.SPACE); //char 10 new line
            return sourceSuperScript;
        }

        private string FindSuperScriptString(string findString, int pos, string CR)
        {
            int CRPos = findString.IndexOf(CR, pos);
            if (CRPos == -1) CRPos = findString.LastIndexOf(CR);

            string temp = findString.Substring(CRPos);
            int Pos2 = temp.IndexOf(CR);
            if (Pos2 >= 0 && Pos2 > CRPos)  temp = temp.Substring(0, CRPos);

            return temp;
        }
    }
}
