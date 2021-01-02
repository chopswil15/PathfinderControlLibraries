using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonStrings;
using OnGoing;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace Utilities
{
    public static class Utility
    {
        public static void ParenCommaFix(ref string Block)
        {
            int leftParenPos = Block.IndexOf(PathfinderConstants.PAREN_LEFT);
            if (leftParenPos == -1) return;
            int rightParenPos = Block.IndexOf(PathfinderConstants.PAREN_RIGHT);
            int commaPos = Block.IndexOf(",", leftParenPos);


            string temp = Block;
            string hold;
            while (leftParenPos >= 0)
            {
                if ((commaPos > leftParenPos) && (commaPos < rightParenPos))
                {
                    temp = Block.Substring(leftParenPos, rightParenPos - leftParenPos);
                    hold = temp.Replace(",", "|");
                    Block = Block.Replace(temp, hold);
                }
                leftParenPos = Block.IndexOf(PathfinderConstants.PAREN_LEFT, leftParenPos + 1);

                if (leftParenPos >= 0)
                {
                    rightParenPos = Block.IndexOf(PathfinderConstants.PAREN_RIGHT, leftParenPos);
                    commaPos = Block.IndexOf(",", leftParenPos);
                }
            }
        }

        public static string FindLastNonCapital(string textValue)
        {
            string Temp;
            string Temp2 = string.Empty;
            int Pos;

            textValue = textValue.Replace(PathfinderConstants.ASCII_10, PathfinderConstants.SPACE).Replace(">", PathfinderConstants.SPACE).Replace("<", PathfinderConstants.SPACE);
            List<string> Words = textValue.Split(' ').ToList();

            foreach (string word in Words)
            {
                if (word.Length > 0)
                {
                    Temp = word.Substring(0, 1);
                    if (Temp.Length > 0)
                    {
                        if (!(Temp.StringToAscii() >= 65 && Temp.StringToAscii() <= 90))
                        {
                            Pos = textValue.IndexOf(word);
                            Temp2 = word;
                        }
                        else
                        {
                            //   break;
                        }
                    }
                }
            }
            return Temp2;
        }

        public static string FindSecondToLastCapital(string Text)
        {
            List<string> wordsList = Text.Split(' ').ToList();
            string Hold = string.Empty;
            string PrevHold = string.Empty;
            string temp;

            foreach (string word in wordsList)
            {
                if (word.Length > 0)
                {
                    temp = word.Substring(0, 1);
                    if (temp.StringToAscii() >= 65 && temp.StringToAscii() <= 90)
                    {
                        PrevHold = Hold;
                        Hold = word;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return PrevHold;
        }

        public static string GetNonParenValue(string Value)
        {
            int Pos = Value.IndexOf(PathfinderConstants.PAREN_LEFT);
            if (Pos >= 0)
            {
                return Value.Substring(0, Pos).Trim();
            }
            return Value.Trim();
        }

        public static string SearchMod(string search)
        {
            string hold = search;
            if (search.Contains("oil of")) hold = search.Replace("oil of ", string.Empty);
            if (search.Contains("potion of")) hold = search.Replace("potion of ", string.Empty);
            if (search.Contains("wand of")) hold = search.Replace("wand of ", string.Empty);
            if (search.Contains("scroll of")) hold = search.Replace("scroll of ", string.Empty);
            if (search.Contains("clairaudience/ clairvoyance")) hold = "clairaudience/clairvoyance";
            if (search.Contains("blindness/ deafness")) hold = "blindness/deafness";

            List<string> metaMagic = GetMetaMagicPowers();
            List<string> Exclude = new List<string> { "silent image", "heightened awareness" };
            foreach (string meta in metaMagic)
            {
                if (hold.Contains(meta) && !Exclude.Contains(hold))
                {
                    hold = hold.Replace(meta, string.Empty);
                }
            }
            hold = hold.Trim();

            int Pos = hold.IndexOf(PathfinderConstants.SPACE);
            //   if (Pos == -1) return search;
            string temp = hold;
            if (Pos >= 0)
            {
                temp = temp.Substring(0, Pos).Trim();
            }

            if (hold.EndsWith("M")) hold = hold.Substring(0, hold.Length - 1);
            if (hold.EndsWith("D")) hold = hold.Substring(0, hold.Length - 1);
            if (hold.EndsWith("S")) hold = hold.Substring(0, hold.Length - 1);

            switch (temp)
            {
                case "greater":
                case "mass":
                case "lesser":
                case "communal":
                case "improved":
                case "supreme":
                    //how to handle "curse, major" vs "major creation"?
                    hold = hold.Replace(temp, string.Empty).Trim();
                    hold += ", " + temp;
                    break;
                case "major":
                    if (hold.Contains("major creation"))
                    {

                    }
                    else
                    {
                        hold = hold.Replace(temp, string.Empty).Trim();
                        hold += ", " + temp;
                    }
                    break;
            }


            return hold;
        }

        public static OnGoingStatBlockModifier.StatBlockModifierSubTypes GetOnGoingAbilitySubTypeFromString(string AbilityName)
        {
            OnGoingStatBlockModifier.StatBlockModifierSubTypes type = OnGoingStatBlockModifier.StatBlockModifierSubTypes.None;
            switch (AbilityName)
            {
                case StatBlockInfo.STR:
                    type = OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Str;
                    break;
                case StatBlockInfo.INT:
                    type = OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Int;
                    break;
                case StatBlockInfo.WIS:
                    type = OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Wis;
                    break;
                case StatBlockInfo.DEX:
                    type = OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Dex;
                    break;
                case StatBlockInfo.CON:
                    type = OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Con;
                    break;
                case StatBlockInfo.CHA:
                    type = OnGoingStatBlockModifier.StatBlockModifierSubTypes.Ability_Cha;
                    break;
            }

            return type;
        }

        public static bool IsThrownWeapon(string weaponName)
        {
            List<string> names = new List<string> { "dagger", "halfling sling staff" };

            return names.Contains(weaponName);
        }

        public static string GetSign(int value)
        {
            return value < 0 ? "-" : string.Empty;
        }

        public static string GetSign(short? value)
        {
            return value < 0 ? "-" : string.Empty;
        }

        public static string RemoveParentheses(string superS)
        {
            superS = superS.Replace(PathfinderConstants.PAREN_LEFT, string.Empty);
            return superS.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Trim();
        }

        public static void RemoveCrsFromWitinParens(string value)
        {
            int openParenPos = value.IndexOf(PathfinderConstants.PAREN_LEFT);
            int closeParenPos = value.IndexOf(PathfinderConstants.PAREN_RIGHT);
            if (openParenPos < 0 || closeParenPos <= 0) return;

            while (openParenPos >= 0)
            {
                string betweenParensValueOrig = value.Substring(openParenPos, closeParenPos - openParenPos);
                string betweenParensValueCopy = betweenParensValueOrig;

                foreach (var crValue in PathfinderConstants.CRVaules)
                {
                    betweenParensValueCopy = betweenParensValueCopy.Replace(crValue, PathfinderConstants.SPACE);
                }
                if (betweenParensValueOrig != betweenParensValueCopy) value = value.Replace(betweenParensValueOrig, betweenParensValueCopy);
                openParenPos = value.IndexOf(PathfinderConstants.PAREN_LEFT, closeParenPos);
                if (openParenPos > 0) closeParenPos = value.IndexOf(PathfinderConstants.PAREN_RIGHT, openParenPos);
            }
        }

        public static string FindCR(string findText)
        {
            foreach (var crValue in PathfinderConstants.CRVaules)
            {
                if (findText.Contains(crValue)) return crValue;
            }
            return string.Empty;
        }

        public static void RemoveSuperScriptsFromList(List<string> sourceList)
        {
            for (int a = 0; a < sourceList.Count; a++)
            {
                sourceList[a] = RemoveSuperScripts(sourceList[a]);
            }
        }

        public static string RemoveSuperScripts(string source)
        {
            if (string.IsNullOrEmpty(source)) return source;
            List<string> superScripts = GetSuperScripts();
            superScripts.Add("B");

            foreach (string superS in superScripts)
            {
                if (source.EndsWith(superS))
                {
                    source = source.Remove(source.LastIndexOf(superS));
                    break;
                }
            }

            return source.Trim();
        }

        public static string CommonMonsterFindReplace(string textToFix, string CR)
        {
            textToFix = textToFix.Replace((char)(8217), char.Parse("'")).Replace((char)(8212), char.Parse("-"))
                .Replace((char)(8211), char.Parse("-")).Replace((char)(150), char.Parse("-"))
                .Replace((char)(151), char.Parse("-")).Replace(char.Parse("-"), char.Parse("-")).Replace(char.Parse("‑"), char.Parse("-"));

            textToFix = textToFix.Replace(char.Parse("×"), char.Parse("x")).Replace("“", ((char)34).ToString())
                .Replace("”", ((char)34).ToString()).Replace((char)(160), char.Parse(PathfinderConstants.SPACE))
                .Replace("á", "&#225;").Replace("Á", "&#193;").Replace("ï", "&#239;").Replace("°", "&#176;")
                .Replace("\b", string.Empty).Replace("−", "-").Replace((char)(8226), char.Parse("•"))
                .Replace("•", "&#8226;");

            textToFix = textToFix.Replace("DEFENSES" + CR, "Defense" + CR).Replace("Defens e" + CR, "Defense" + CR)
                .Replace("DEFEN SE" + CR, "Defense" + CR).Replace("Defe nse" + CR, "Defense" + CR).Replace("DEFENSE" + CR, "Defense" + CR)
                .Replace("DEFENSE " + CR, "Defense" + CR);

            textToFix = textToFix.Replace("Off ense" + CR, "Offense" + CR).Replace("Offens e" + CR, "Offense" + CR)
                .Replace("Offe nse" + CR, "Offense" + CR).Replace("OFFEN SE" + CR, "Offense" + CR)
                .Replace("OFFENSE" + CR, "Offense" + CR).Replace("OFFENSE " + CR, "Offense" + CR);

            textToFix = textToFix.Replace("Spell-like Abilities", "Spell-Like Abilities");

            textToFix = textToFix.Replace("Statist ics" + CR, "Statistics" + CR).Replace("Statistic s" + CR, "Statistics" + CR)
                .Replace("Statis tics" + CR, "Statistics" + CR).Replace("STATIS TICS" + CR, "Statistics" + CR)
                .Replace("STATISTI CS" + CR, "Statistics" + CR).Replace("STATISTICS" + CR, "Statistics" + CR)
                .Replace("STATISTICS " + CR, "Statistics" + CR).Replace("St atistic s" + CR, "Statistics" + CR)
                .Replace("St atistics" + CR, "Statistics" + CR).Replace("Stat istics" + CR, "Statistics" + CR)
                .Replace("Statisti cs" + CR, "Statistics" + CR).Replace("Stati stic s" + CR, "Statistics" + CR)
                .Replace("St atistics" + CR, "Statistics" + CR).Replace("Stat ist ics" + CR, "Statistics" + CR);


            textToFix = textToFix.Replace("TACTI CS" + CR, "Tactics" + CR).Replace("Tac tics" + CR, "Tactics" + CR)
                .Replace("Tact ics" + CR, "Tactics" + CR).Replace("Ta ctics" + CR, "Tactics" + CR).Replace("Tactic s" + CR, "Tactics" + CR)
                .Replace("Tacti cs" + CR, "Tactics" + CR).Replace("TA CTI CS" + CR, "Tactics" + CR).Replace("TACTICS " + CR, "Tactics" + CR)
                .Replace("TAC TICS" + CR, "Tactics" + CR).Replace("Tactics", "Tactics" + CR).Replace("TA CTI CS" + CR, "Tactics" + CR);


            textToFix = textToFix.Replace("ECOLOGY " + CR, "Ecology" + CR).Replace("Ecolog y" + CR, "Ecology" + CR)
                .Replace("Ec ology" + CR, "Ecology" + CR).Replace("Ecolog y", "Ecology")
                .Replace("Eco logy", "Ecology").Replace("Ecolo gy", "Ecology").Replace("ecology", "Ecology").Replace("ECOLOGY", "Ecology");

            textToFix = textToFix.Replace("SPECIAL ABILITIE S" + CR, "Special Abilities" + CR).Replace("Special Abili ties" + CR, "Special Abilities" + CR)
               .Replace("Sp ecial Abiliti es" + CR, "Special Abilities" + CR).Replace("SPECIAL ABILITY " + CR, "Special Abilities" + CR)
               .Replace("SPECIAL ABILITIES" + CR, "Special Abilities" + CR).Replace("SPECIAL ABILITIES " + CR, "Special Abilities" + CR)
               .Replace("SPE CIAL ABILITIES" + CR, "Special Abilities" + CR).Replace("Sp eci al Abi liti es" + CR, "Special Abilities" + CR)
               .Replace("Special Ab ilities" + CR, "Special Abilities" + CR).Replace("Spe cia l Abi lities" + CR, "Special Abilities" + CR)
               .Replace("Speci al Abi lities" + CR, "Special Abilities" + CR).Replace("Specia l Abi lities" + CR, "Special Abilities" + CR)
               .Replace("SPE CIAL ABILITIES" + CR, "Special Abilities" + CR).Replace("SPEC IAL ABILITIES" + CR, "Special Abilities" + CR)
               .Replace("Spec ial Ab ilities" + CR, "Special Abilities" + CR).Replace("Spe cia l Abi lities" + CR, "Special Abilities" + CR)
               .Replace("Speci al Abi lities" + CR, "Special Abilities" + CR).Replace("Special Abiliti es", "Special Abilities")
               .Replace("Sp ecial Abilities", "Special Abilities");

            textToFix = textToFix.Replace("Constr uctio n", "Construction").Replace("CONSTRUCTION", "Construction");
            textToFix = textToFix.Replace("Habitat and Society", "Habitat & Society").Replace("HABITAT AND SOCIETY", "Habitat & Society")
                .Replace("Habitat & Society ", "Habitat & Society");

            textToFix = textToFix.Replace("C R ", "CR ").Replace("inf lict", "inflict").Replace("inf luence", "influence")
                .Replace("ef lex", "eflex").Replace("CM D", "CMD").Replace("CM B", "CMB")
                .Replace("fl ", "fl").Replace("fi ", "fi");

            return textToFix.Trim();
        }

        public static List<string> GetSuperScripts()
        {
            return new List<string> { "UE", "APG", "POP", "ISM", "UM","UCA", "UC", "ISWG", "ARG", "POTN", "HOG","GMG",
                "MA", "BOTD2", "PSFG", "B4", "B2","B3","ISG","TG","DW","ACG","MC","OA","AA","UI","PSP","ADA","POTR","ISR","HA","POTIS","AG","UW" };
        }

        public static List<string> GetMetaMagicPowers()
        {
            //update enum MetaMagicPowers too
            return new List<string> { "empowered", "enlarged", "extended", "heightened", "maximized",
                        "quickened", "silenced", "stilled", "widened","silent","corrupt","still","bouncing","merciful","reach","dazing","intensified","aquatic" };
        }
    }
}
