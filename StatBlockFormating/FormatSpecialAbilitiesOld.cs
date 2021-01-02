using CommonStatBlockInfo;
using CommonStrings;
using PathfinderGlobals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace StatBlockFormating
{
    public class FormatSpecialAbilitiesOld
    {
        private string CRLF = Environment.NewLine;

        public string FormatSpecialAbilities(string specialAbilitiesText, string crlf)
        {
            CRLF = crlf;

            string temp, Temp2, hold, Hold2;
            int PosBegin, PosHold, Pos, PosPeriod, Low, TempValue;
            int Pos2 = 0;
            bool bH6 = false;
            bool IsBoldSpecial = false;

            int PosStart = -1;
            List<string> specialAbilitiesList = new List<string> { "(Ex)", "(Su)", "(Sp)" };
            foreach (var item in specialAbilitiesList)
            {
                if (PosStart == -1 || (specialAbilitiesText.Contains(item) && specialAbilitiesText.IndexOf(item) < PosStart))
                {
                    PosStart = specialAbilitiesText.IndexOf(item);
                }
            }

            if (specialAbilitiesText.Substring(PosStart + 1, 1) == ":") PosStart = PosStart + 1;

            if (PosStart >= 0)
            {
                hold = specialAbilitiesText.Substring(0, PosStart + 3);
                Pos = hold.LastIndexOf(PathfinderConstants.ASCII_10);
                if (Pos == -1) Pos = 0;
                hold = hold.Substring(Pos, hold.Length - Pos);
                if (hold.Substring(0, 1).StringToAscii() == 10)
                {
                    hold = hold.ReplaceFirst(PathfinderConstants.ASCII_10, string.Empty);
                    hold = hold.Trim();
                    PosStart = PosStart + 2;
                }

                temp = PathfinderConstants.H5 + PathfinderConstants.BOLD + hold + PathfinderConstants.EBOLD;
                if (specialAbilitiesText.Contains("hp ") || specialAbilitiesText.Contains("AC "))
                {
                    if (specialAbilitiesText.Contains("as follows:")) bH6 = true;
                }
                specialAbilitiesText = specialAbilitiesText.ReplaceFirst(hold, temp);
                PosStart = PosStart + 5;
            }
            else
            {
                PosStart = specialAbilitiesText.IndexOf("Ex)");
                if (PosStart == -1) PosStart = specialAbilitiesText.IndexOf("Su)");
                if (PosStart == -1) PosStart = specialAbilitiesText.IndexOf("Sp)");
                if (PosStart >= 0) PosStart = PosStart + 1;

                if (PosStart == -1)
                {
                    Temp2 = Utility.FindSecondToLastCapital(specialAbilitiesText);
                    PosStart = specialAbilitiesText.IndexOf(Temp2) + Temp2.Length;
                    IsBoldSpecial = true;
                }

                hold = specialAbilitiesText.Substring(0, PosStart);
                temp = PathfinderConstants.H5 + PathfinderConstants.BOLD + hold + PathfinderConstants.EBOLD;
                if (IsBoldSpecial)
                {

                }
                else
                {
                    if (specialAbilitiesText.Contains("hp ") || specialAbilitiesText.Contains("AC "))
                    {
                        if (specialAbilitiesText.Contains("as follows:"))
                        {
                            bH6 = true;
                        }
                        else
                        {
                            temp = temp + PathfinderConstants.EH5;
                        }
                    }
                    else
                    {
                        temp = temp + PathfinderConstants.EH5;
                    }
                }
                if (hold.Length > 0)
                    specialAbilitiesText = specialAbilitiesText.Replace(hold, temp);

                PosStart = PosStart + 5;
            }

            //the rest
            //all (Ex)
            PosHold = PosStart;
            PosBegin = PosStart;


            if (bH6)
            {
                specialAbilitiesText = specialAbilitiesText.ReplaceFirst("as follows:", "as follows:" + PathfinderConstants.EH5);
                Pos = specialAbilitiesText.IndexOf("as follows:" + PathfinderConstants.EH5) + 16;
                Low = specialAbilitiesText.IndexOf("(Ex)", Pos);
                if (Low > specialAbilitiesText.IndexOf("(Sp)", Pos) && specialAbilitiesText.IndexOf("(Sp)", Pos) != -1 || Low == -1)
                    Low = specialAbilitiesText.IndexOf("(Sp)", Pos);

                if (Low > specialAbilitiesText.IndexOf("(Su)", Pos) && specialAbilitiesText.IndexOf("(Su)", Pos) != -1 || Low == -1)
                    Low = specialAbilitiesText.IndexOf("(Su)", Pos);

                if (Low == -1) Low = Pos;

                temp = Utility.FindLastNonCapital(specialAbilitiesText.Substring(Low - 50, 50));
                Pos2 = specialAbilitiesText.IndexOf(temp, Pos);
                temp = specialAbilitiesText.Substring(Pos, Pos2 - Pos);
                if (temp == string.Empty)
                {
                    temp = specialAbilitiesText.Substring(Pos, specialAbilitiesText.Length - Pos + 1);
                    temp = temp.Trim();
                }
                hold = FormatSecondStats(temp);
                specialAbilitiesText = specialAbilitiesText.Replace(temp, hold);
                bH6 = false;
            }

            Pos = specialAbilitiesText.IndexOf("as follows:") + 5;
            Pos = specialAbilitiesText.IndexOf("as follows:", Pos);
            //PosHold2 = Pos
            while (Pos >= 0)
            {
                if (specialAbilitiesText.IndexOf("hp ", Pos) >= 0 || specialAbilitiesText.IndexOf("AC ", Pos) >= 0
                     || specialAbilitiesText.IndexOf("AC<", Pos) >= 0 || specialAbilitiesText.IndexOf("Spd ", Pos) >= 0)
                {
                    TempValue = Pos;
                    Low = specialAbilitiesText.IndexOf("(Ex)", TempValue);
                    if (Low > specialAbilitiesText.IndexOf("(Sp)", TempValue) && specialAbilitiesText.IndexOf("(Sp)", TempValue) != -1 || Low == -1)
                        Low = specialAbilitiesText.IndexOf("(Sp)", TempValue);

                    if (Low > specialAbilitiesText.IndexOf("(Su)", TempValue) && specialAbilitiesText.IndexOf("(Su)", TempValue) != -1 || Low == -1)
                        Low = specialAbilitiesText.IndexOf("(Su)", TempValue);

                    if (Low >= 0)
                    {
                        temp = Utility.FindLastNonCapital(specialAbilitiesText.Substring(Low - 50, 50));
                        Pos2 = specialAbilitiesText.IndexOf(temp + PathfinderConstants.SPACE, Pos);
                        temp = specialAbilitiesText.Substring(TempValue + 11, Pos2 - Pos - 11);
                    }
                    else
                    {
                        temp = specialAbilitiesText.Substring(Pos2);
                        TempValue = temp.IndexOf("as follows:");
                        temp = temp.Replace(temp.Substring(0, TempValue + 11), string.Empty);
                    }
                    hold = FormatSecondStats(temp);
                    specialAbilitiesText = specialAbilitiesText.Replace(temp, hold);
                    Pos = specialAbilitiesText.IndexOf("as follows:", Pos + 20);
                }
                else
                    break;
            }

            if (PosBegin < 51) PosBegin = 51;
            Pos = specialAbilitiesText.IndexOf("(Ex)", PosBegin);
            while (Pos >= 0)
            {
                temp = specialAbilitiesText.Substring(Pos - 50, 55);
                PosPeriod = temp.LastIndexOf(".");
                if (PosPeriod == -1) PosPeriod = temp.Length;
                temp = temp.Substring(PosPeriod);
                PosPeriod = specialAbilitiesText.IndexOf(temp);
                hold = specialAbilitiesText.Substring(PosPeriod + 1, Pos - PosPeriod + 3);
                if (hold.IndexOf("</h6>") >= 0)
                {
                    hold = hold.Replace("</h6>", string.Empty).Replace(PathfinderConstants.ASCII_10, string.Empty).Trim();
                }
                temp = PathfinderConstants.EH5 + PathfinderConstants.H5 + PathfinderConstants.BOLD + hold + PathfinderConstants.EBOLD;

                specialAbilitiesText = specialAbilitiesText.Replace(hold, temp);
                PosHold = Pos + 15;
                Pos = specialAbilitiesText.IndexOf("(Ex)", PosHold);
            }

            //all (Sp)
            Pos = specialAbilitiesText.IndexOf("(Sp)", PosBegin);
            while (Pos >= 0)
            {
                temp = specialAbilitiesText.Substring(Pos - 50, 55);
                PosPeriod = temp.LastIndexOf(".");
                temp = temp.Substring(PosPeriod);
                PosPeriod = specialAbilitiesText.IndexOf(temp);
                hold = specialAbilitiesText.Substring(PosPeriod + 1, Pos - PosPeriod + 3);
                temp = PathfinderConstants.EH5 + PathfinderConstants.H5 + PathfinderConstants.BOLD + hold + PathfinderConstants.EBOLD;
                specialAbilitiesText = specialAbilitiesText.Replace(hold, temp);
                PosHold = Pos + 15;
                Pos = specialAbilitiesText.IndexOf("(Sp)", PosHold);
            }

            //all (Su)
            Pos = specialAbilitiesText.IndexOf("(Su)", PosBegin);
            while (Pos >= 0)
            {
                if (Pos > 50)
                {
                    temp = specialAbilitiesText.Substring(Pos - 50, 55);
                    PosPeriod = temp.LastIndexOf(".");
                    if (PosPeriod == -1)
                    {
                        PosPeriod = temp.LastIndexOf("</h6>");
                        if (PosPeriod >= 0)
                            PosPeriod = PosPeriod + 4;
                    }
                    if (PosPeriod >= 0)
                    {
                        temp = temp.Substring(PosPeriod);
                        if (temp.Contains(">"))
                        {
                            // sTemp = sTemp.Replace(">", string.Empty);
                            temp = temp.Trim();
                        }
                        if (temp.Substring(0, 1) == PathfinderConstants.ASCII_10)
                        {
                            temp = temp.ReplaceFirst(PathfinderConstants.ASCII_10, string.Empty);
                            temp = temp.Trim();
                        }
                        if (temp.Substring(0, 1) == ".")
                        {
                            temp = temp.ReplaceFirst(".", string.Empty);
                            temp = temp.Trim();
                        }
                        PosPeriod = specialAbilitiesText.IndexOf(temp);
                        hold = specialAbilitiesText.Substring(PosPeriod, temp.Length);
                        Hold2 = specialAbilitiesText.Substring(0, PosPeriod - 1);
                        specialAbilitiesText = specialAbilitiesText.Replace(Hold2, string.Empty);
                        temp = PathfinderConstants.EH5 + PathfinderConstants.H5 + PathfinderConstants.BOLD + hold + PathfinderConstants.EBOLD;
                        specialAbilitiesText = specialAbilitiesText.Replace(hold, temp);
                        specialAbilitiesText = Hold2 + specialAbilitiesText;
                    }
                }
                PosHold = Pos + 15;
                Pos = specialAbilitiesText.IndexOf("(Su)", PosHold);
            }

            PosHold = specialAbilitiesText.IndexOf(" Feat ");
            while (PosHold >= 0)
            {
                Pos = specialAbilitiesText.IndexOf(" Feat ");
                Pos2 = specialAbilitiesText.LastIndexOf(PathfinderConstants.ASCII_10, Pos);
                temp = specialAbilitiesText.Substring(Pos2 + 1, Pos + 5 - Pos2);
                hold = temp.Trim();
                temp = PathfinderConstants.EH5 + PathfinderConstants.H5 + PathfinderConstants.BOLD + hold + PathfinderConstants.EBOLD;
                specialAbilitiesText = specialAbilitiesText.Replace(hold, temp);
                PosHold = specialAbilitiesText.IndexOf(" Feat ", PosHold + 5);
            }

            PosHold = specialAbilitiesText.IndexOf("Feats ");
            while (PosHold >= 0)
            {
                Pos = specialAbilitiesText.IndexOf("Feats ");
                Pos2 = specialAbilitiesText.LastIndexOf(PathfinderConstants.ASCII_10, Pos);
                temp = specialAbilitiesText.Substring(Pos2 + 1, Pos + 5 - Pos2);
                hold = temp.Trim();
                temp = PathfinderConstants.EH5 + PathfinderConstants.H5 + PathfinderConstants.BOLD + hold + PathfinderConstants.EBOLD;
                specialAbilitiesText = specialAbilitiesText.Replace(hold, temp);
                PosHold = specialAbilitiesText.IndexOf("Feats ", PosHold + 5);
            }

            PosHold = specialAbilitiesText.IndexOf("Skills ");
            while (PosHold >= 0)
            {
                Pos = specialAbilitiesText.IndexOf("Skills ");
                Pos2 = specialAbilitiesText.LastIndexOf(PathfinderConstants.ASCII_10, Pos);
                temp = specialAbilitiesText.Substring(Pos2 + 1, Pos + 5 - Pos2);
                hold = temp.Trim();
                temp = PathfinderConstants.EH5 + PathfinderConstants.H5 + PathfinderConstants.BOLD + hold + PathfinderConstants.EBOLD;
                specialAbilitiesText = specialAbilitiesText.Replace(hold, temp);
                PosHold = specialAbilitiesText.IndexOf("Skills ", PosHold + 5);
            }

            PosHold = specialAbilitiesText.IndexOf("Spells ");
            while (PosHold >= 0)
            {
                Pos = specialAbilitiesText.IndexOf("Spells ");
                Pos2 = specialAbilitiesText.LastIndexOf(PathfinderConstants.ASCII_10, Pos);
                temp = specialAbilitiesText.Substring(Pos2 + 1, Pos + 5 - Pos2);
                hold = temp.Trim();
                temp = PathfinderConstants.EH5 + PathfinderConstants.H5 + PathfinderConstants.BOLD + hold + PathfinderConstants.EBOLD;
                specialAbilitiesText = specialAbilitiesText.Replace(hold, temp);
                PosHold = specialAbilitiesText.IndexOf("Spells ", PosHold + 5);
            }

            PosHold = specialAbilitiesText.IndexOf("Construct Form ");
            while (PosHold >= 0)
            {
                Pos = specialAbilitiesText.IndexOf("Construct Form ");
                Pos2 = specialAbilitiesText.LastIndexOf(PathfinderConstants.ASCII_10, Pos);
                temp = specialAbilitiesText.Substring(Pos2 + 1, Pos + 13 - Pos2);
                hold = temp.Trim();
                temp = PathfinderConstants.EH5 + PathfinderConstants.H5 + PathfinderConstants.BOLD + hold + PathfinderConstants.EBOLD;
                specialAbilitiesText = specialAbilitiesText.Replace(hold, temp);
                PosHold = specialAbilitiesText.IndexOf("Construct Form ", PosHold + 13);
            }

            PosHold = specialAbilitiesText.IndexOf("Contingency ");
            while (PosHold >= 0)
            {
                Pos = specialAbilitiesText.IndexOf("Contingency ");
                Pos2 = specialAbilitiesText.LastIndexOf(PathfinderConstants.ASCII_10, Pos);
                temp = specialAbilitiesText.Substring(Pos2 + 1, Pos + 10 - Pos2);
                hold = temp.Trim();
                temp = PathfinderConstants.EH5 + PathfinderConstants.H5 + PathfinderConstants.BOLD + "Contingency " + PathfinderConstants.EBOLD;
                specialAbilitiesText = specialAbilitiesText.Replace(hold, temp);
                PosHold = specialAbilitiesText.IndexOf("Contingency ", PosHold + 10);
            }

            temp = "•";
            if (specialAbilitiesText.Contains(temp))//list
            {
                Pos = specialAbilitiesText.IndexOf(temp);
                specialAbilitiesText = specialAbilitiesText.Insert(Pos, PathfinderConstants.LIST);
                specialAbilitiesText = specialAbilitiesText + PathfinderConstants.ELIST;
                specialAbilitiesText = specialAbilitiesText.Replace(temp, PathfinderConstants.LIST_ITEM);
            }

            specialAbilitiesText = specialAbilitiesText + PathfinderConstants.EH5;
            specialAbilitiesText = specialAbilitiesText.Replace("<h6> </h6>", string.Empty);
            specialAbilitiesText = specialAbilitiesText.Replace("<h6></h6>", string.Empty);
            specialAbilitiesText = specialAbilitiesText.Replace("<h5><b></h5>", string.Empty);

            return specialAbilitiesText;
        }

        private string FormatSecondStats(string secondStatsText)
        {
            string[] Bold = new string[38];
            string Temp, Hold;
            int Pos, Pos2;
            bool Abilities = false;
            bool Start = false;

            Temp = secondStatsText;
            Bold[0] = " CR ";
            Bold[1] = "Init";
            Bold[2] = "Senses";
            Bold[3] = "Languages";
            Bold[4] = "AC";
            Bold[5] = "hp";
            Bold[6] = "Fort ";
            Bold[7] = "Ref ";
            Bold[8] = "Will";
            Bold[9] = "Spd";
            Bold[10] = "Melee";
            Bold[11] = "Ranged";
            Bold[12] = "Special Attack";
            Bold[13] = "Base Atk";
            Bold[14] = "Grp";
            Bold[15] = "SR";
            // sBold[16] = "Abilities ";
            Bold[16] = "Defensive Abilities ";
            Bold[17] = "SQ";
            Bold[18] = "Feats";
            Bold[19] = "Skills";
            Bold[20] = "Atk Options";
            Bold[21] = "Combat Gear";
            Bold[22] = "Possessions";
            Bold[23] = "Spellbook";
            Bold[24] = "Spells Prepared";
            Bold[25] = "DR";
            Bold[26] = "Immune";
            Bold[27] = "Space ";
            Bold[28] = "Reach ";
            Bold[29] = "Aura";
            Bold[30] = "Weaknesses";
            Bold[31] = "Special Actions";
            Bold[32] = "Spell-Like Abilities";
            Bold[33] = "Resist";
            Bold[34] = "Special Atks";
            Bold[35] = StatBlockInfo.STR;
            Bold[36] = StatBlockInfo.CON;
            Bold[37] = StatBlockInfo.DEX;

            if (secondStatsText.Substring(0, 1) == PathfinderConstants.ASCII_10) secondStatsText = secondStatsText.ReplaceFirst(PathfinderConstants.ASCII_10, PathfinderConstants.SPACE);

            secondStatsText = secondStatsText.ReplaceFirst(PathfinderConstants.ASCII_10, PathfinderConstants.EH6);
            secondStatsText = PathfinderConstants.H6 + secondStatsText;
            for (int a = 0; a <= Bold.GetUpperBound(0); a++)
            {
                if (secondStatsText.IndexOf(Bold[a]) >= 0)
                {
                    switch (a)
                    {
                        case 8:
                        case 7:
                            secondStatsText = secondStatsText.Replace(Bold[a], PathfinderConstants.BOLD + Bold[a] + PathfinderConstants.EBOLD);
                            break;
                        case 10:
                            Pos = secondStatsText.IndexOf(Bold[a]);
                            Pos2 = secondStatsText.IndexOf(PathfinderConstants.ASCII_10, Pos);
                            if (Pos2 == -1)
                                Pos2 = secondStatsText.IndexOf(PathfinderConstants.PAREN_RIGHT, Pos) + 1;

                            Temp = secondStatsText.Substring(Pos, Pos2 - Pos);
                            Hold = Temp;
                            FormatCombat(ref Temp);
                            secondStatsText = secondStatsText.Replace(Hold, Temp);
                            secondStatsText = secondStatsText.Replace(Bold[a], PathfinderConstants.H6 + PathfinderConstants.BOLD + Bold[a] + PathfinderConstants.EBOLD);
                            break;
                        //case 16:
                        //    bAbilities = true;
                        //    sText = sText.Replace(sBold[a], PathfinderConstants.H6 + PathfinderConstants.BOLD + sBold[a] + PathfinderConstants.EBOLD);
                        //    break;
                        case 35:
                        case 36:
                        case 37:
                            if (!Abilities)
                            {
                                if (!Start)
                                {
                                    Start = true;
                                    secondStatsText = secondStatsText.Replace(Bold[a], PathfinderConstants.H6 + PathfinderConstants.BOLD + Bold[a] + PathfinderConstants.EBOLD);
                                }
                                secondStatsText = secondStatsText.Replace(Bold[a], PathfinderConstants.BOLD + Bold[a] + PathfinderConstants.EBOLD);
                            }

                            break;
                        default:
                            secondStatsText = secondStatsText.Replace(Bold[a], PathfinderConstants.H6 + PathfinderConstants.BOLD + Bold[a] + PathfinderConstants.EBOLD);
                            break;
                    }
                }
                else
                {
                    if (a == 16) Abilities = false;
                }
            }
            secondStatsText = secondStatsText.Replace(PathfinderConstants.H6, PathfinderConstants.EH6 + PathfinderConstants.H6);
            if (secondStatsText.Substring(0, 5) == PathfinderConstants.EH6) secondStatsText = secondStatsText.ReplaceFirst(PathfinderConstants.EH6, string.Empty);

            return secondStatsText + PathfinderConstants.EH6;
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

    }
}
