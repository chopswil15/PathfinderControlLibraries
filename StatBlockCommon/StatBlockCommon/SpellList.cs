using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonStatBlockInfo;
using CommonStrings;
using PathfinderGlobals;
using Utilities;

namespace StatBlockCommon
{
    public enum SpellListType
    {
        SpellBook = 0,
        SpellLikeAbility = 1,
        Class = 2
    }

    public class SpellLists
    {
        public List<SpellList> ListOfSpellsLists { get; set; }
    }

    public class OrderedSpells
    {
        public int Level { get; set; }
        public List<SpellData> ListOfOrderedSpells { get; set; }
    }
   

    public class SpellList
    {
        public string Source { get; set; }
        public List<SpellData> ListOfSpells { get; set; }
        public List<OrderedSpells> OrderedSpellList { get; set; }
        public int CasterLevel { get; set; }
        public int Concentration { get; set; }
        public SpellListType Type { get; set; }
        public string Errors { get; private set; }

        private List<string> MetaMagicList;

        public SpellList()
        {
            ListOfSpells = new List<SpellData>();
            OrderedSpellList = new List<OrderedSpells>();
            Errors = string.Empty;
            MetaMagicList = Utility.GetMetaMagicPowers();
        }

        public string SpellBase
        {
            get
            {
                string Start = "(CL ";
                switch (CasterLevel)
                {
                    case 1:
                        Start += "1st)";
                        break;
                    case 2:
                        Start += "2nd)";
                        break;
                    case 3:
                        Start += "3rd)";
                        break;
                    default:
                        Start += CasterLevel.ToString() + "th)";
                        break;
                }
                return Source + PathfinderConstants.SPACE + Start;
            }
        }

        public string CasterLevelName()
        {
            string Start = "(CL ";
            switch (CasterLevel)
            {
                case 1:
                    return Start + "1st)";
                case 2:
                    return Start + "2nd)";
                case 3:
                    return Start + "3rd)";
                default:
                    return Start + CasterLevel.ToString() + "th)";
            }
        }

        public void AddSpell(SpellData Spell)
        {
            ListOfSpells.Add(Spell);
        }

        public void RemoveSpell(SpellData Spell)
        {
            ListOfSpells.Remove(Spell);
        }

        public void CastSpell(string Spell)
        {
            for (int a = ListOfSpells.Count - 1; a >= 0; a--)
            {
                if (ListOfSpells[a].Name == Spell)
                {
                    SpellData temp = ListOfSpells[a];
                    if (temp.Count == -1) break; //at will
                    if (temp.Count >= 1)
                    {
                        temp.Count--;
                        ListOfSpells[a] = temp;
                    }
                    else
                    {
                        if (Type == SpellListType.SpellBook)
                        {
                            ListOfSpells.Remove(ListOfSpells[a]);
                        }
                    }
                    break;
                }
            }
        }

        public bool SpellExists(string SpellName)
        {
            return ListOfSpells.Where(x => x.Name == SpellName).Any();
        }

        public void ParseSpellList(string spellListString, List<string> _sourceSuperScripts)
        {
            if (spellListString.Length == 0) return;

            int Pos = spellListString.IndexOf("(CL");
            if (Pos == -1) throw new Exception("ParseSpellList: Missing CL, have caster level?");

            spellListString = Utility.RemoveSuperScripts(spellListString);
            spellListString = spellListString.Replace(", D ", "D ").Replace(", D, ", "D, ");
            
            string hold = spellListString;
            string temp = hold;
            Pos = temp.IndexOf(PathfinderConstants.PAREN_RIGHT);
            temp = temp.Substring(0, Pos + 1);
            spellListString = spellListString.Replace(temp, string.Empty);
                
            Pos = temp.IndexOf("(CL");
            if (Pos == -1)  throw new Exception("ParseSpellList: Missing CL, have caster level?");

            Source = temp.Substring(0, Pos).Trim();
            temp = temp.Replace(Source, string.Empty).Replace("(CL", string.Empty).Trim();
            Pos = temp.IndexOf(PathfinderConstants.SPACE);

            if (Pos == -1) Pos = temp.IndexOf(PathfinderConstants.PAREN_RIGHT);

            string concentration = temp.Substring(Pos + 1);           
            if (concentration.Contains("concentration"))  Parseconcentration(ref concentration);

            temp = temp.Substring(0, Pos).Replace(";", string.Empty).Replace(",", string.Empty).Replace("th", string.Empty)
                  .Replace("rd", string.Empty).Replace("nd", string.Empty).Replace("st", string.Empty).Trim();
            CasterLevel = Convert.ToInt32(temp);

            spellListString = spellListString.Trim();
            GoodLineBreaks(ref spellListString);
            Utility.RemoveCrsFromWitinParens(spellListString);
            List<string> ListOfSpells = spellListString.Split('\n').ToList();
            ListOfSpells.RemoveAll(x=> x =="\r");
            ListOfSpells.RemoveAll(x=> x == string.Empty);
            List<string> Spells;

            foreach (string SpellLevel in ListOfSpells)
            {
                try
                {
                    Spells = ParseOneSpellLevel(SpellLevel);
                }
                catch
                {
                    Errors += PathfinderConstants.SPACE + SpellLevel;
                }
            }
        }

        private void Parseconcentration(ref string concentration)
        {
            int Pos2 = concentration.IndexOf("concentration");
            concentration = concentration.Substring(Pos2);
            Pos2 = concentration.IndexOf(";");
            if (Pos2 != -1)  concentration = concentration.Substring(0, Pos2);
            Pos2 = concentration.IndexOf(",");
            if (Pos2 != -1)  concentration = concentration.Substring(0, Pos2);
            concentration = concentration.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty).Replace("concentration", string.Empty).Trim();
            int Pos3 = concentration.IndexOf("[");
            if (concentration.Contains("[")) concentration = concentration.Substring(0, Pos3);

            try
            {
                if (concentration.Trim().Length > 0) Concentration = Convert.ToInt32(concentration);
            }
            catch (Exception ex)
            {
                throw new Exception("Parse SpellList-- concentration numeric conversion error " + concentration);
            }
        }

        private void GoodLineBreaks(ref string list)
        {
            List<string> ordinal = new List<string> {"0 ","0-", "1st", "2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th" };            
            int Pos;   

            foreach (string ord in ordinal)
            {
                Pos = list.IndexOf(ord);
                if (Pos >= 0 && !list.Contains(Environment.NewLine + ord))
                {    
                    if (ord == "0 " && (list.Contains("0 hit") || list.Contains("10 rounds")|| list.Contains("+10 ") 
                        || list.Contains("d10 ")|| list.Contains("0 lbs.") || list.Contains("0 pounds") || list.Contains("0 feet")
                            || list.Contains("0 damage") ))
                    {

                    }
                    else if (ord == "0-" && list.Contains("0-ft") || list.Contains("0 ft"))
                    {

                    }
                    else if (!list.Contains(ord + PathfinderConstants.PAREN_RIGHT))
                    {
                        list = list.Replace(ord, Environment.NewLine + ord);
                    }             
                }
            }
        }

        private List<string> ParseOneSpellLevel(string spellLevel)
        {
            List<string> spellList;
            SpellData SD;
            string temp = spellLevel;
            string temp2 = string.Empty;
            string hold = temp;
            int Level = -1;
            string HyphenChar = "—";

            temp = temp.Trim();
            int Pos = temp.IndexOf("—");
            if (Pos == -1)
            {
                Pos = temp.IndexOf("-");
                HyphenChar = "-";
            }
            temp2 = temp.Substring(0, Pos + 1);
            temp = temp.Replace(temp2, string.Empty).Trim();

            
            Pos = temp2.IndexOf("th" + HyphenChar);
            
            if (Pos == -1 || temp2.Contains("month" + HyphenChar))  Pos = temp2.IndexOf("rd" + HyphenChar);
            if (Pos == -1) Pos = temp2.IndexOf("nd" + HyphenChar);
            if (Pos == -1) Pos = temp2.IndexOf("st" + HyphenChar);
            if (Pos == -1)  Pos = temp2.IndexOf("0 ");
            if (Pos == -1)  Pos = temp2.IndexOf("0-");

            OrderedSpells orderedSpells = new OrderedSpells();
            List<SpellData> tempSD = new List<SpellData>();

            if (Pos >= 0) //spell level
            {
                if (Pos == 0) Pos = 1;
                temp2 = temp2.Substring(0, Pos).Trim();
                Level = Convert.ToInt32(temp2);
                ParenCommaFix(ref temp);
                if (temp.Contains("D, ") || temp.Contains("D,"))
                {                    
                    foreach (string SS in Utility.GetSuperScripts())
                    {
                        temp = temp.Replace("D, " + SS, "D " + SS)
                            .Replace("D," + SS, "D" + SS);
                    }
                }
                if(temp.EndsWith("D,"))
                {
                    temp = temp.Substring(0, temp.Length - 1);
                }
               
                foreach (var power in Utility.GetMetaMagicPowers())
                {
                    if (temp.Contains(power + ","))
                    {
                        temp = temp.Replace(power + ",", power);
                    }
                }
                spellList = temp.Split(',').ToList();
                orderedSpells.Level = Level;

                foreach (string spell in spellList)
                {                    
                    SD = ParseOneSpell(spell, Level, string.Empty);
                    AddSpell(SD);
                    tempSD.Add(SD);
                }
                orderedSpells.ListOfOrderedSpells = tempSD;
            }
            else //frequency, i.e. 3/day
            {
                Pos = temp2.IndexOf(HyphenChar);
                temp2 = temp2.Substring(0, Pos).Trim();
                Pos = temp2.IndexOf("th");
                if (Pos == -1)
                {
                    Pos = temp2.IndexOf("rd");
                }
                if (Pos == -1)
                {
                    //Pos = temp2.IndexOf("rounds");
                    temp2 = temp2.Replace("rounds", string.Empty);
                }
                if (Pos == -1) Pos = temp2.IndexOf("nd");
                if (Pos == -1)  Pos = temp2.IndexOf("st");
                 

                ParenCommaFix(ref temp);
                foreach (var power in Utility.GetMetaMagicPowers())
                {
                    if (temp.Contains(power + ","))
                    {
                        temp = temp.Replace(power + ",", power);
                    }
                }             

                spellList = temp.Split(',').ToList();
                spellList.RemoveAll(x => x== string.Empty);
                if (Pos >= 0)
                {
                    temp = temp2.Substring(0, Pos).Trim();
                    try
                    {
                        Level = Convert.ToInt32(temp);
                    }
                    catch
                    { }
                                        
                    Pos = temp2.IndexOf(PathfinderConstants.SPACE);
                    if (Pos != -1)
                    {
                        temp2 = temp2.Substring(Pos);
                    }
                    temp2 = Utility.RemoveParentheses(temp2);
                }
                orderedSpells.Level = Level;

                foreach (string spell in spellList)
                {
                    try
                    {
                        SD = ParseOneSpell(spell, Level, temp2);
                        AddSpell(SD);
                        tempSD.Add(SD);
                    }
                    catch
                    {
                        Errors += " Error with " + spell + " Level: " + Level.ToString() + PathfinderConstants.SPACE + temp2;
                    }
                }
                orderedSpells.ListOfOrderedSpells = tempSD;
            }
            OrderedSpellList.Add(orderedSpells);
            return spellList;
        }

        private void ParenCommaFix(ref string Block)
        {
            int LeftParenPos = Block.IndexOf(PathfinderConstants.PAREN_LEFT);
            if (LeftParenPos == -1) return;
            int RightParenPos = Block.IndexOf(PathfinderConstants.PAREN_RIGHT);
            int CommaPos = Block.IndexOf(",", LeftParenPos);
            

            string temp = Block;
            string hold = string.Empty;
            while (LeftParenPos >= 0)
            {
                if ((CommaPos > LeftParenPos) && (CommaPos < RightParenPos))
                {
                    temp = Block.Substring(LeftParenPos, RightParenPos - LeftParenPos);
                    hold = temp.Replace(",", "|");
                    Block = Block.Replace(temp, hold);
                }
                LeftParenPos = Block.IndexOf(PathfinderConstants.PAREN_LEFT, LeftParenPos + 1);

                if (LeftParenPos >= 0)
                {
                    RightParenPos = Block.IndexOf(PathfinderConstants.PAREN_RIGHT, LeftParenPos);
                    CommaPos = Block.IndexOf(",", LeftParenPos);
                }
            }
        }

        private SpellData ParseOneSpell(string spell, int Level, string Frequency)
        {
            string Name, temp, temp2;
            int Count = 1;
            int DC = 0;
            string Limitations = string.Empty;
            bool Domain = false;
            bool Spirit = false;
            bool AlreadyCast = false;
            int Pos;

            if (Frequency.Length > 0)
            {
                Pos = Frequency.IndexOf("/");
                if (Pos >= 0)
                {
                    temp = Frequency.Substring(0, Pos);
                    Count = Convert.ToInt32(temp);
                }
                else
                {
                    Count = -1;
                }
            }

            Pos = spell.IndexOf("already cast");
            if (Pos >= 0)
            {
                AlreadyCast = true;
                spell = spell.Replace("; already cast", string.Empty).Trim();
                spell = spell.Replace("already cast", string.Empty).Trim();
            }

            Pos = spell.IndexOf(PathfinderConstants.PAREN_LEFT);
            if (Pos == -1)
            {
                if (spell.Substring(spell.Length - 1, 1) == "D")
                {
                    Domain = true;
                    spell = spell.Substring(0, spell.Length - 1);
                }
                if (spell.EndsWith("S"))
                {
                    Spirit = true;
                    spell = spell.Substring(0, spell.Length - 1);
                }
                
                spell = spell.Replace("*", string.Empty).Trim();
                StatBlockInfo.MetaMagicPowers powers = ParseMetaMagicPowers(ref spell);
                return new SpellData(spell, Count, Level, -1, -1 ,string.Empty, Frequency, Domain,Spirit, false, powers);
            }
            else
            {
                Name = spell.Substring(0, Pos).Trim();
                temp = spell.Replace(Name, string.Empty).Trim();
                Name = Name.Replace("*", string.Empty);
                if(Name.EndsWith("APG"))
                {
                    Name = Name.Substring(0, Name.Length - "APG".Length);
                }
                
                if (Name.Substring(Name.Length - 1, 1) == "D")
                {
                    Domain = true;
                    Name = Name.Substring(0, Name.Length - 1);
                }
                if (Name.EndsWith("S"))
                {
                    Spirit = true;
                    Name = Name.Substring(0, Name.Length - 1);
                }
                Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
                while (Pos >= 0)
                {
                    Pos = temp.IndexOf(PathfinderConstants.PAREN_RIGHT);
                    if (Pos == -1) Pos = temp.IndexOf(";");
                    temp2 = temp.Substring(0, Pos + 1);
                    temp = temp.Replace(temp2, string.Empty).Trim();
                    temp2 = temp2.Replace(PathfinderConstants.PAREN_LEFT, string.Empty);
                    temp2 = temp2.Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                    temp2 = temp2.Replace("*", string.Empty).Trim();
                    Pos = temp2.IndexOf(";");
                    if (Pos >= 0)
                    {
                        temp2 = temp2.Substring(Pos + 1).Trim();
                    }

                    Pos = temp2.LastIndexOf("|"); //commas get changed to pipes | for splitting

                    if (Pos >= 0)
                    {
                        string temp3 = temp2.Substring(0, Pos);
                        int holdCount = Count;
                        int.TryParse(temp3, out Count);
                        if (Count == 0) Count = holdCount;
                        temp2 = temp2.Substring(Pos + 1).Trim();
                    }

                    
                    Pos = temp2.IndexOf("DC");

                    if (Pos >= 0)
                    {
                        temp2 = temp2.Replace("DC", string.Empty).Trim();
                        temp2 = temp2.Replace("half", string.Empty).Trim();
                        DC = Convert.ToInt32(temp2);
                    }
                    else
                    {
                        try
                        {
                            if (Frequency.Length > 0)
                            {
                                Count = Convert.ToInt32(temp2);
                            }
                            else
                            {
                                try
                                {
                                    Count = Convert.ToInt32(temp2);
                                }
                                catch
                                {
                                    Limitations = temp2.Trim();
                                    Limitations = Limitations.Replace("|", ",");
                                }
                            }
                        }
                        catch
                        {
                            Limitations = temp2.Trim();
                            Limitations = Limitations.Replace("|", ",");
                        }
                    }

                    Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
                }
                StatBlockInfo.MetaMagicPowers powers = ParseMetaMagicPowers(ref spell);
                if (powers != StatBlockInfo.MetaMagicPowers.None)
                {
                    Name = Name.Replace(powers.ToString(), string.Empty).Trim();
                }
                return new SpellData(Name, Count, Level, DC, 0, Limitations, Frequency, Domain,Spirit, AlreadyCast, powers);
            }
        }

        private StatBlockInfo.MetaMagicPowers ParseMetaMagicPowers(ref string spell)
        {
            StatBlockInfo.MetaMagicPowers powers = StatBlockInfo.MetaMagicPowers.None;
            foreach (string Meta in MetaMagicList)
            {
                if (spell.Contains(Meta))
                {
                    if ((Meta == "silent" && spell.Contains("silent image"))) continue;
                    if ((Meta == "heightened" && spell.Contains("heightened awareness"))) continue;
                    
                    spell = spell.ReplaceFirst(Meta, string.Empty);
                    string temp = Meta;
                    if (temp == "still") temp = "stilled";
                    try
                    {
                        powers |= (StatBlockInfo.MetaMagicPowers)Enum.Parse(typeof(StatBlockInfo.MetaMagicPowers), temp);
                    }
                    catch
                    {
                        throw new Exception("ParseMetaMagicPowers--mising MetaMagicPowers enum for " + temp);
                    }
                    
                }
            }
            return powers;
        }
    }


    public struct SpellData
    {
        private string _Name;
        private int _Level;
        private int _Count;
        private int _DC;
        private int _CL;
        private string _Limitations;
        private string _Frequency;
        private bool _Domain;
        private bool _Spirit;
        private bool _alreadyCast;
        private StatBlockInfo.MetaMagicPowers _metaMagicPowers;

        #region Properties

        public StatBlockInfo.MetaMagicPowers metaMagicPowers
        {
            get { return _metaMagicPowers; }
        }

        public string Name
        {
            get { return _Name; }
        }

        public int Count
        {
            get { return _Count; }
            set { _Count = value; }
        }

        public bool Domain
        {
            get { return _Domain; }
        }

        public int Level
        {
            get { return _Level; }
        }

        public int DC
        {
            get { return _DC; }
        }

        public string Limitations
        {
            get { return _Limitations; }
        }

        public string Frequency
        {
            get { return _Frequency; }
        }

        public bool AlreadyCast
        {
            get { return _alreadyCast; }
        }

        public string GroupName()
        {
            if (Level >= 0)
            {
                switch (Level)
                {
                    case 0:
                        return "0";
                    case 1:
                        return "1st";
                    case 2:
                        return "2nd";
                    case 3:
                        return "3rd";
                    default:
                        return Level.ToString() + "th";
                }
            }
            else
            {
                return _Frequency;
            }
        }

        public string SpellOutPut
        {
            get
            {
                StringBuilder SB = new StringBuilder();
                SB.Append(_Name);

                if (_Limitations.Length > 0)
                {
                    SB.Append(" (").Append(_Limitations).Append(PathfinderConstants.PAREN_RIGHT);
                }
                if (_Count > 1 && _Frequency.Length == 0)
                {
                    SB.Append(" (").Append(_Count.ToString()).Append(PathfinderConstants.PAREN_RIGHT);
                }
                if (_DC > 0)
                {
                    SB.Append(" (DC ").Append(_DC.ToString()).Append(PathfinderConstants.PAREN_RIGHT);
                }

                return SB.ToString();
            }
        }

        public override string ToString()
        {
            return SpellOutPut;
        }
        #endregion

        public SpellData(string Name, int Count, int Level, int DC, int CL, string Limitations,
                         string Frequency, bool Domain,bool Spirit, bool AlreadyCast, StatBlockInfo.MetaMagicPowers MetaMagicPowers)
        {
            _Name = Name.Trim();
            _Count = Count;
            _Level = Level;
            _DC = DC;
            _CL = CL;
            _Limitations = Limitations.Trim();
            _Frequency = Frequency.Trim();
            _Domain = Domain;
            _Spirit = Spirit;
            _alreadyCast = AlreadyCast;
            _metaMagicPowers = MetaMagicPowers;
        }       
    }
}
