using CommonStrings;
using PathfinderGlobals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace StatBlockParsing
{
    public class OffenseRegionParser : IOffenseRegionParser
    {
        private ISBCommonBaseInput _sbcommonBaseInput;

        public OffenseRegionParser(ISBCommonBaseInput sbcommonBaseInput)
        {
            _sbcommonBaseInput = sbcommonBaseInput;
        }

        public void ParseOffense(string offense, string CR)
        {
            offense = offense.Replace("offense", string.Empty).Trim()
                .Replace("&#8211;", "-");

            string temp = string.Empty;
            //work your way back
            int Pos = offense.LastIndexOf("Bloodline");
            if (Pos >= 0)
            {
                temp = offense.Substring(Pos);
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Bloodline", string.Empty)
                    .Replace("M Mythic spell", string.Empty)
                    .Replace("M mythic spells", string.Empty)
                    .Replace("M mythic spell", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Bloodline = temp;
            }

            Pos = offense.LastIndexOf(CR + "* ");

            if (Pos >= 0)
            {
                temp = offense.Substring(Pos);
                offense = offense.Replace(temp, string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.OffenseNote = temp.Trim();
            }

            Pos = offense.LastIndexOf(CR + "** ");

            if (Pos >= 0)
            {
                temp = offense.Substring(Pos);
                offense = offense.Replace(temp, string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.OffenseNote = temp.Trim();
            }

            Pos = offense.IndexOf("Opposition Schools");
            if (Pos >= 0)
            {
                temp = offense.Substring(Pos);
                Pos = temp.IndexOf("* ");
                if (Pos >= 0)
                {
                    temp = temp.Substring(0, Pos);
                }
                if (temp.Contains(CR))
                {
                    Pos = temp.IndexOf(CR);
                    temp = temp.Substring(0, Pos);
                }
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace(":", string.Empty).Replace(";", string.Empty)
                    .Replace("M Mythic spell", string.Empty)
                       .Replace("M mythic spells", string.Empty)
                       .Replace("M mythic spell", string.Empty)
                       .Replace("Opposition Schools", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.ProhibitedSchools = temp;
            }

            Pos = offense.IndexOf("Spirit");

            if (Pos >= 0 && !offense.Contains("Spiritualist"))
            {
                temp = offense.Substring(Pos);
                offense = offense.Replace(temp, string.Empty).Trim()
                    .Replace("S spirit magic spell;", string.Empty)
                    .Replace("S spirit spell;", string.Empty);
                temp = temp.ReplaceFirst("Spirit", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Spirit = temp;
            }


            Pos = offense.IndexOf("Mystery");

            if (Pos >= 0)
            {
                temp = offense.Substring(Pos);
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Mystery", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Mystery = temp;
            }

            offense = offense.Replace("M Mythic spell", string.Empty).Replace("M mythic spells", string.Empty).Replace("M mythic spell", string.Empty);
           
            Pos = offense.IndexOf("Patron");
            if (Pos >= 0)
            {
                temp = offense.Substring(Pos);
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Patron", string.Empty)
                    .Replace(";", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Patron = temp;
            }

            Pos = offense.LastIndexOf("Domain");
            if (Pos >= 0 && _sbcommonBaseInput.MonsterSB.Class.Contains("inquisitor"))
            {
                temp = offense.Substring(Pos);
                Pos = temp.IndexOf(CR);
                if (Pos > 0)
                {
                    temp = temp.Substring(0, Pos);
                }
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Domain", string.Empty)
                    .Replace(";", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.SpellDomains = temp;
            }



            Pos = offense.IndexOf("D domain");
            if (Pos == -1) Pos = offense.IndexOf("D Domain");

            if (Pos >= 0)
            {
                temp = offense.Substring(0, Pos);
                Pos = temp.LastIndexOf(CR);
                temp = offense.Substring(Pos + 1).Trim();
                Pos = temp.IndexOf("CL ");
                if (Pos >= 0)
                {
                    Pos = temp.IndexOf(CR);
                    temp = temp.Substring(0, Pos);
                }
                offense = offense.Replace(temp, string.Empty).Trim();
                Pos = temp.IndexOf("; Domain");
                if (Pos == -1)
                {
                    Pos = temp.IndexOf("Domains");
                    if (Pos == -1)
                    {
                        Pos = temp.IndexOf("; Domain");
                        Pos += 9;
                    }
                    else
                    {
                        Pos += 8;
                    }
                }
                Pos = temp.IndexOf(PathfinderConstants.SPACE, Pos + 3);
                if (Pos >= 0) temp = temp.Substring(Pos);
                Pos = temp.IndexOf(CR);
                if (Pos >= 0)  temp = temp.Substring(0, Pos);

                temp = temp.Replace(CR, PathfinderConstants.SPACE)
                    .Replace(";", string.Empty)
                    .Replace("M Mythic spell", string.Empty)
                    .Replace("M mythic spells", string.Empty)
                    .Replace("M mythic spell", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.SpellDomains = temp;
            }

            Pos = offense.IndexOf("Psychic Discipline");
            if (Pos >= 0)
            {
                temp = offense.Substring(0, Pos);
                Pos = temp.LastIndexOf(CR);
                temp = offense.Substring(Pos + 1);
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Psychic Discipline", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.PsychicDiscipline = temp;
            }

            Pos = offense.IndexOf("Spells Prepared");

            if (Pos >= 0)
            {
                temp = offense.Substring(0, Pos);
                Pos = temp.LastIndexOf(CR);
                temp = offense.Substring(Pos + 1);
                if (temp.Contains("Spells Known"))
                {
                    Pos = temp.IndexOf("Spells Known");
                    temp = temp.Substring(0, Pos);
                    Pos = temp.LastIndexOf(CR);
                    temp = temp.Substring(0, Pos);
                }
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = CommonMethods.KeepCRs(temp, CR);
                CommonMethods.GoodLineBreaks(ref temp);
                Utility.RemoveCrsFromWitinParens(temp);
                _sbcommonBaseInput.MonsterSB.SpellsPrepared = temp.Trim();
            }

            Pos = offense.IndexOf("Spells Known");

            if (Pos >= 0)
            {
                temp = offense.Substring(0, Pos);
                Pos = temp.LastIndexOf(CR);
                temp = offense.Substring(Pos + 1);
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = CommonMethods.KeepCRs(temp, CR);
                temp = temp.Replace(PathfinderConstants.BREAK, CR);
                CommonMethods.GoodLineBreaks(ref temp);
                Utility.RemoveCrsFromWitinParens(temp);
                _sbcommonBaseInput.MonsterSB.SpellsKnown = temp.Trim();
            }

            Pos = offense.IndexOf("Additional Extracts Known");

            if (Pos >= 0)
            {
                temp = offense.Substring(0, Pos);
                Pos = temp.LastIndexOf(CR);
                temp = offense.Substring(Pos + 1);
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Additional Extracts Known", string.Empty);
                temp = CommonMethods.KeepCRs(temp, CR);
                CommonMethods.GoodLineBreaks(ref temp);
                Utility.RemoveCrsFromWitinParens(temp);
                _sbcommonBaseInput.MonsterSB.AdditionalExtractsKnown = temp.Trim();
            }

            Pos = offense.IndexOf("Extracts Prepared");

            if (Pos >= 0)
            {
                temp = offense.Substring(0, Pos);
                Pos = temp.LastIndexOf(CR);
                temp = offense.Substring(Pos + 1);
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = CommonMethods.KeepCRs(temp, CR);
                CommonMethods.GoodLineBreaks(ref temp);
                Utility.RemoveCrsFromWitinParens(temp);
                _sbcommonBaseInput.MonsterSB.ExtractsPrepared = temp.Trim();
            }

            Pos = offense.IndexOf("Extracts Known");

            if (Pos >= 0)
            {
                temp = offense.Substring(0, Pos);
                Pos = temp.LastIndexOf(CR);
                temp = offense.Substring(Pos + 1);
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = CommonMethods.KeepCRs(temp, CR);
                CommonMethods.GoodLineBreaks(ref temp);
                Utility.RemoveCrsFromWitinParens(temp);
                _sbcommonBaseInput.MonsterSB.ExtractsPrepared = temp.Trim();
            }

            Pos = offense.IndexOf("Implement Schools");
            if (Pos >= 0)
            {
                temp = offense.Substring(0, Pos);
                Pos = temp.LastIndexOf(CR);
                temp = offense.Substring(Pos + 1);
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = CommonMethods.KeepCRs(temp, CR);
                CommonMethods.GoodLineBreaks(ref temp);
                _sbcommonBaseInput.MonsterSB.Implements = temp.Trim();
            }

            Pos = offense.IndexOf("Implements");
            if (Pos >= 0)
            {
                temp = offense.Substring(0, Pos);
                Pos = temp.LastIndexOf(CR);
                temp = offense.Substring(Pos + 1);
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = CommonMethods.KeepCRs(temp, CR);
                CommonMethods.GoodLineBreaks(ref temp);
                _sbcommonBaseInput.MonsterSB.Implements = temp.Trim();
            }

            Pos = offense.IndexOf("Spell Like-Abilities");
            if (Pos >= 0) offense = offense.Replace("Spell Like-Abilities", "Spell-Like Abilities");

            Pos = offense.IndexOf("Spell-Like Abilities");

            if (Pos >= 0)
            {
                temp = offense.Substring(0, Pos);
                Pos = temp.LastIndexOf(CR);
                temp = offense.Substring(Pos + 1);
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = CommonMethods.KeepCRs(temp, CR);
                CommonMethods.GoodLineBreaks(ref temp);
                temp = temp.Trim();
                KeepSLA_LineBreaks(ref temp);
                Utility.RemoveCrsFromWitinParens(temp);
                _sbcommonBaseInput.MonsterSB.SpellLikeAbilities = temp.Trim();
            }

            Pos = offense.IndexOf("Spell-Like Ability");


            if (Pos >= 0)
            {
                temp = offense.Substring(0, Pos);
                Pos = temp.LastIndexOf(CR);
                temp = offense.Substring(Pos + 1);
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = CommonMethods.KeepCRs(temp, CR);
                CommonMethods.GoodLineBreaks(ref temp);
                temp = temp.Trim();
                KeepSLA_LineBreaks(ref temp);
                Utility.RemoveCrsFromWitinParens(temp);
                _sbcommonBaseInput.MonsterSB.SpellLikeAbilities = temp.Trim();
            }

            Pos = offense.IndexOf("Psychic Magic");
            if (Pos >= 0)
            {
                temp = offense.Substring(0, Pos);
                Pos = temp.LastIndexOf(CR);
                temp = offense.Substring(Pos + 1);
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = CommonMethods.KeepCRs(temp, CR);
                CommonMethods.GoodLineBreaks(ref temp);
                _sbcommonBaseInput.MonsterSB.PsychicMagic = temp.Trim();
            }

            Pos = offense.IndexOf("Kineticist Wild Talents");
            if (Pos >= 0)
            {
                temp = offense.Substring(0, Pos);
                Pos = temp.LastIndexOf(CR);
                temp = offense.Substring(Pos + 1);
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = CommonMethods.KeepCRs(temp, CR);
                CommonMethods.GoodLineBreaks(ref temp);
                _sbcommonBaseInput.MonsterSB.KineticistWildTalents = temp.Trim();
            }

            offense = offense.Replace("Special Attack ", "Special Attacks ");
            Pos = offense.IndexOf("Special Attacks");

            if (Pos >= 0)
            {
                temp = offense.Substring(Pos);
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Special Attacks", string.Empty)
                       .Replace(CR, PathfinderConstants.SPACE).Trim();
                _sbcommonBaseInput.MonsterSB.SpecialAttacks = temp;
            }



            _sbcommonBaseInput.MonsterSB.Reach = "5 ft."; //default
            _sbcommonBaseInput.MonsterSB.Space = "5 ft."; //default
            Pos = offense.IndexOf("Reach");

            if (Pos >= 0)
            {
                temp = offense.Substring(Pos);
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Reach", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Reach = temp;
            }

            Pos = offense.IndexOf("Space");

            if (Pos >= 0)
            {
                temp = offense.Substring(Pos);
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Space", string.Empty)
                    .Replace(";", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Space = temp;
            }

            Pos = offense.IndexOf("Ranged");

            if (Pos >= 0)
            {
                temp = offense.Substring(Pos);
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Ranged", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Ranged = temp;
            }

            Pos = offense.IndexOf("Melee");

            if (Pos >= 0)
            {
                temp = offense.Substring(Pos);
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Melee", string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Melee = temp;
            }

            Pos = offense.IndexOf("Speed");
            if (Pos == -1)
            {
                Pos = offense.IndexOf("Spd");
            }

            if (Pos >= 0)
            {
                temp = offense.Substring(Pos);
                offense = offense.Replace(temp, string.Empty).Trim();
                temp = temp.Replace("Speed", string.Empty)
                    .Replace("Spd", string.Empty);
                Pos = temp.IndexOf(";");
                if (Pos > 0)
                {
                    if (!(Pos > temp.IndexOf(PathfinderConstants.PAREN_LEFT) && Pos < temp.IndexOf(PathfinderConstants.PAREN_RIGHT)))
                    {
                        _sbcommonBaseInput.MonsterSB.Speed_Mod = temp.Substring(Pos);
                        temp = temp.Replace(_sbcommonBaseInput.MonsterSB.Speed_Mod, string.Empty);
                        _sbcommonBaseInput.MonsterSB.Speed_Mod = _sbcommonBaseInput.MonsterSB.Speed_Mod.Replace(";", string.Empty);
                    }
                }
                _sbcommonBaseInput.MonsterSB.Speed = temp.Trim();
                bool Found = false;
                List<string> Types = _sbcommonBaseInput.MonsterSB.Speed.Split(',').ToList();
                for (int a = Types.Count - 1; a >= 0; a--)
                {
                    if (Types[a].Contains("fly"))
                    {
                        _sbcommonBaseInput.MonsterSB.Fly = true;
                        Found = true;
                    }
                    if (Types[a].Contains("swim"))
                    {
                        _sbcommonBaseInput.MonsterSB.Swim = true;
                        Found = true;
                    }
                    if (Types[a].Contains("climb"))
                    {
                        _sbcommonBaseInput.MonsterSB.Climb = true;
                        Found = true;
                    }
                    if (Types[a].Contains("burrow"))
                    {
                        _sbcommonBaseInput.MonsterSB.Burrow = true;
                        Found = true;
                    }
                    if (Found)
                    {
                        Found = false;
                        Types.Remove(Types[a]);
                    }
                }
                if (Types.Any())
                {
                    _sbcommonBaseInput.MonsterSB.Land = true;
                }
            }
        }

        private void KeepSLA_LineBreaks(ref string temp)
        {
            string stringSLA = "Spell-Like Abilities";
            int Count = (temp.Length - temp.Replace(stringSLA, string.Empty).Length) / stringSLA.Length;
            if (Count == 1) return;

            int Pos = temp.IndexOf(stringSLA);
            Pos = temp.IndexOf(stringSLA, Pos + 1);

            while (Pos >= 0)
            {
                string hold = temp.Substring(0, Pos);
                string LastNonCapital = Utility.FindLastNonCapital(hold);
                if (LastNonCapital.IndexOf(Environment.NewLine) == -1)
                {
                    int Pos2 = temp.LastIndexOf(LastNonCapital, Pos);
                    temp = temp.Insert(Pos2 + LastNonCapital.Length, Environment.NewLine);
                }
                Pos = temp.IndexOf(stringSLA, Pos + stringSLA.Length);
            }
        }
    }
}
