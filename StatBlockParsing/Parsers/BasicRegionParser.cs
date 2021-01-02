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
    public class BasicRegionParser : IBasicRegionParser
    {
        private ISBCommonBaseInput _sbcommonBaseInput;

        public BasicRegionParser(ISBCommonBaseInput sbcommonBaseInput)
        {
            _sbcommonBaseInput = sbcommonBaseInput;
        }

        public void ParseBasic(string Basic, ref string ErrorMessage, string CR, string TAB)
        {
            bool AlignmentFound = false;
            Basic = Basic.Replace("&#8211;", "-");
            int CRPos = Basic.IndexOf(CR);
            if (CRPos == -1)
            {
                CR = "\n";
                CRPos = Basic.IndexOf(CR);
            }
            string temp = string.Empty;
            string temp2 = string.Empty;
            int Pos = 0;


            int TabPos = Basic.IndexOf(TAB);
            if (TabPos == -1) TabPos = Basic.IndexOf("CR ");
            if (TabPos == -1 && Basic.Contains(" cr "))
            {
                Basic = Basic.Replace(" cr ", " CR ");
                TabPos = Basic.IndexOf("CR ");
            }

            if (TabPos >= 0)
            {
                temp = Basic.Substring(0, TabPos).Trim().ProperCase();
            }
            else
            {
                TabPos = 0;
            }

            if (_sbcommonBaseInput.MonsterSB.name.Length == 0)
            {
                temp = temp.Replace("\b", string.Empty);
                if (temp.Contains(PathfinderConstants.PAREN_LEFT))
                {
                    Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
                    string AltName = temp.Substring(Pos);
                    temp = temp.Replace(AltName, string.Empty).Trim();
                    AltName = Utility.RemoveParentheses(AltName);
                    _sbcommonBaseInput.MonsterSB.AlternateNameForm = AltName.ProperCase();
                }
                _sbcommonBaseInput.MonsterSB.name = temp.Trim();
            }
            else
            {
                if (_sbcommonBaseInput.MonsterSB.name != temp)
                {
                    _sbcommonBaseInput.MonsterSB.Group = _sbcommonBaseInput.MonsterSB.name;
                    if (temp.IndexOf(PathfinderConstants.PAREN_LEFT) > 0)
                    {
                        Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
                        string AltName = temp.Substring(Pos);
                        temp = temp.Replace(AltName, string.Empty).Trim();
                        AltName = Utility.RemoveParentheses(AltName);
                        _sbcommonBaseInput.MonsterSB.AlternateNameForm = AltName.ProperCase();
                    }
                    _sbcommonBaseInput.MonsterSB.name = temp;
                    if (_sbcommonBaseInput.MonsterSB.Group == _sbcommonBaseInput.MonsterSB.name) _sbcommonBaseInput.MonsterSB.Group = string.Empty;
                }
            }

            Basic = Basic.Remove(0, TabPos).Trim();

            CRPos = Basic.IndexOf(CR);
            temp = Basic.Substring(0, CRPos);
            Basic = Basic.Replace(temp, string.Empty).Trim();
            temp = temp.Replace("CR", string.Empty).Trim()
                .Replace("½", "1/2");
            if (temp.Contains("MR "))
            {
                _sbcommonBaseInput.MonsterSB.Mythic = true;
                Pos = temp.IndexOf("MR");
                string tempMythic = temp.Substring(Pos);
                temp = temp.Replace(tempMythic, string.Empty)
                    .Replace("/", string.Empty).Trim();
                tempMythic = tempMythic.Replace("MR", string.Empty);
                int tempMR;
                int.TryParse(tempMythic, out tempMR);
                _sbcommonBaseInput.MonsterSB.MR = tempMR;

            }
            _sbcommonBaseInput.MonsterSB.CR = temp;

            CRPos = Basic.IndexOf(CR);
            temp = Basic.Substring(0, CRPos);
            int Pos3 = temp.IndexOf("XP ");
            if (Pos3 == -1 && _sbcommonBaseInput.MonsterSB.CR != "-")
            {
                ErrorMessage = "Issue with XP";
                return;
            }
            if (Pos3 >= 0)
            {
                if (Pos3 >= 5) //NPC Codex SB type
                {
                    Pos3 = Basic.IndexOf(CR);
                    Pos = Basic.IndexOf(CR, Pos3 + 3);
                    string temp3 = Basic.Substring(0, Pos);

                    temp = ParseNPCCodexIntro(temp3);
                    Basic = Basic.Replace(temp3, temp);
                }
                else  //old style
                {
                    Basic = Basic.Replace(temp, string.Empty).Trim();
                    temp = temp.Replace("each", string.Empty)
                        .Replace("XP", string.Empty).Trim();
                    _sbcommonBaseInput.MonsterSB.XP = temp;
                }
            }

            Basic = Basic.Replace("Init:", "Init");
            Pos = Basic.IndexOf("Init ");
            if (Pos == -1)
            {
                Pos = Basic.IndexOf("Init-");
                if (Pos > 0)
                {
                    Basic = Basic.Replace("Init-", "Init -");
                    Pos = Basic.IndexOf("Init ");
                }
                else
                {
                    ErrorMessage = "Issue with Init";
                    return;
                }
            }
            int Pos2 = Basic.IndexOf(";", Pos);
            temp = Basic.Substring(Pos, Pos2 - Pos);
            Basic = Basic.Replace(temp + ";", string.Empty);
            temp = temp.Replace("Init", string.Empty).Trim();
            _sbcommonBaseInput.MonsterSB.Init = temp;

            Pos = Basic.IndexOf("Aura ");
            if (Pos >= 0)
            {
                temp = Basic.Substring(Pos);
                Basic = Basic.Replace(temp, string.Empty);
                temp = temp.Replace("Aura", string.Empty).Trim()
                    .Replace(CR, PathfinderConstants.SPACE);
                _sbcommonBaseInput.MonsterSB.Aura = temp;
            }

            Basic = Basic.Replace("Senses" + CR, "Senses ");
            Pos = Basic.IndexOf("Senses ");

            try
            {
                temp = Basic.Substring(Pos);
                Basic = Basic.Replace(temp, string.Empty);
            }
            catch
            {
                ErrorMessage = "Issues with Senses";
                return;
            }

            temp = temp.Replace("Senses", string.Empty).Trim()
                .Replace(CR, PathfinderConstants.SPACE);
            _sbcommonBaseInput.MonsterSB.Senses = temp;

            CRPos = Basic.IndexOf(CR);
            temp = Basic.Substring(0, CRPos);
            Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
            if (Pos >= 0)
            {
                Pos = temp.IndexOf(PathfinderConstants.PAREN_RIGHT, Pos);
                if (Pos == -1)
                {
                    CRPos = Basic.IndexOf(CR, CRPos + 1);
                    temp = Basic.Substring(0, CRPos);
                    Basic = Basic.Replace(temp, string.Empty).Trim();
                    temp = temp.Replace(CR, PathfinderConstants.SPACE);
                }
                else
                {
                    Basic = Basic.Replace(temp, string.Empty).Trim();
                }
            }
            else
            {
                Basic = Basic.ReplaceFirst(temp, string.Empty).Trim();
            }

            
            foreach (string AL in CommonMethods.GetAlignments())
            {
                if (temp.Contains(AL + PathfinderConstants.SPACE))
                {
                    AlignmentFound = true;
                    _sbcommonBaseInput.MonsterSB.Alignment = AL;
                    temp = temp.Replace(AL + PathfinderConstants.SPACE, string.Empty).Trim();
                    break;
                }
            }

            if (!AlignmentFound && temp.Contains("Any "))
            {
                Pos = temp.IndexOf(PathfinderConstants.PAREN_RIGHT);
                temp2 = temp.Substring(0, Pos + 1);
                _sbcommonBaseInput.MonsterSB.Alignment = temp2;
                temp = temp.Replace(_sbcommonBaseInput.MonsterSB.Alignment, string.Empty).Trim();
                AlignmentFound = true;
            }

            temp = temp.Trim();
            if (!AlignmentFound)
            {
                //race, class line
                Pos = 0;

                Pos = temp.LastIndexOf(PathfinderConstants.PAREN_RIGHT);
                if (Pos == temp.Length - 1)
                {
                    Pos2 = temp.LastIndexOf(PathfinderConstants.PAREN_LEFT);
                    temp2 = temp.Substring(Pos2);
                    temp = temp.Replace(temp2, string.Empty);
                    temp2 = temp2.Replace(PathfinderConstants.PAREN_LEFT, string.Empty)
                        .Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);


                    bool findNumber = CommonMethods.FindNumber(temp2);

                    if (findNumber)
                    {
                        _sbcommonBaseInput.MonsterSB.MonsterSource = temp2;
                    }
                    else
                    {
                        _sbcommonBaseInput.MonsterSB.ClassArchetypes = temp2.Replace("*", string.Empty).Trim();
                    }
                }


                Pos = 0;                
                foreach (string one_class in CommonMethods.GetAllClasses())
                {
                    var oneClassLower = one_class.ToLower();
                    var tempLower = temp.ToLower();
                    if (temp.Contains(oneClassLower) || temp.Contains("/" + oneClassLower))
                    {
                        if (temp.IndexOf(oneClassLower) < Pos || Pos == 0)
                        {
                            if (Pos >= 0 && oneClassLower == "ranger" && temp.Contains("stranger")) continue;
                            if (oneClassLower == "slayer" && tempLower.Contains("dark slayer")) continue;
                            if (oneClassLower == "witch" && tempLower.Contains("witchfire")) continue;
                            if (oneClassLower == "eidolon" && tempLower.Contains("unfettered eidolon")) continue;
                            if (oneClassLower == "bard" && tempLower.Contains("bombardier")) continue;
                            if (oneClassLower == "hunter" && tempLower.Contains("shadow hunter")) continue;
                            if (oneClassLower == "warrior" && tempLower.Contains("clockwork warrior")) continue;
                            Pos = tempLower.IndexOf(oneClassLower); //lowest class Pos
                        }
                    }
                }
                if (Pos > 0)
                {
                    temp2 = temp.Substring(Pos);

                    _sbcommonBaseInput.MonsterSB.Class = temp2.Trim();
                    if (temp2.Contains(PathfinderConstants.PAREN_LEFT))
                    {
                        FindClassArchetypes();
                    }
                    FindMythicPath();
                    temp = temp.Replace(temp2, string.Empty).Trim();

                    _sbcommonBaseInput.MonsterSB.Race = temp.Trim();
                    temp = temp.Replace(_sbcommonBaseInput.MonsterSB.Race, string.Empty).Trim();
                }
                else
                {
                    bool findNumber = CommonMethods.FindNumber(temp);
                    if (_sbcommonBaseInput.MonsterSB.MonsterSource.Length == 0 && findNumber)
                    {
                        _sbcommonBaseInput.MonsterSB.MonsterSource = temp.Trim();
                    }
                    else
                    {
                        var tempLower = temp.ToLower();
                        foreach (string template in CommonMethods.GetSimpleClassTemplates())
                        {
                            var templateLower = template.ToLower();
                            if (tempLower.Contains(template.ToLower()))
                            {
                                if (templateLower == "bard" && tempLower.Contains("bombardier")) continue;
                                if (templateLower == "ranger" && tempLower.Contains("stranger")) continue;
                                temp = tempLower.Replace(templateLower, string.Empty);
                                break;
                            }
                        }
                        _sbcommonBaseInput.MonsterSB.Race = temp.Replace("*", string.Empty).ProperCase().Trim();
                        if (_sbcommonBaseInput.MonsterSB.TemplatesApplied.Length > 0)
                        {
                            List<string> templates = _sbcommonBaseInput.MonsterSB.TemplatesApplied.Split('|').ToList();
                            templates.RemoveAll(x => x == string.Empty);
                            foreach (var template in templates)
                            {
                                _sbcommonBaseInput.MonsterSB.Race = _sbcommonBaseInput.MonsterSB.Race.Replace(template, string.Empty)
                                    .Replace(template.ProperCase(), string.Empty);
                            }
                            _sbcommonBaseInput.MonsterSB.Race = _sbcommonBaseInput.MonsterSB.Race.Trim();
                        }
                    }
                }

                Pos = Basic.IndexOf(PathfinderConstants.PAREN_LEFT);

                if (Pos == 0)
                {
                    Pos = Basic.IndexOf(PathfinderConstants.PAREN_RIGHT);
                    temp = Basic.Substring(0, Pos + 1);
                    Basic = Basic.Replace(temp, string.Empty).Trim();
                    temp = temp.Replace(PathfinderConstants.PAREN_LEFT, string.Empty)
                        .Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                    _sbcommonBaseInput.MonsterSB.MonsterSource = temp;
                }

                Pos = Basic.IndexOf(PathfinderConstants.SPACE);
                if (Pos == -1)
                {
                    ErrorMessage = "Missing Alignment";
                    return;
                }
                temp = Basic.Substring(0, Pos);
                Basic = Basic.Replace(temp, string.Empty).Trim();
                _sbcommonBaseInput.MonsterSB.Alignment = temp;
            }
            else
            {
                Basic = temp;
            }
            
            foreach (string size in CommonMethods.GetSizes())
            {
                if (Basic.Contains(size + PathfinderConstants.SPACE))
                {
                    _sbcommonBaseInput.MonsterSB.Size = size;
                    Basic = Basic.Replace(_sbcommonBaseInput.MonsterSB.Size + PathfinderConstants.SPACE, string.Empty).Trim();
                    break;
                }
            }

            if (_sbcommonBaseInput.MonsterSB.Size.Length == 0) ErrorMessage = "Issue Size, mising or not capitalized";

            if (Basic.Contains("(formerly "))
            {
                Pos = Basic.IndexOf(PathfinderConstants.PAREN_RIGHT);
                Basic = Basic.Substring(Pos + 1).Trim();
            }
            Pos = Basic.IndexOf(PathfinderConstants.PAREN_LEFT);
            if (Pos >= 0)
            {
                temp = Basic.Substring(0, Pos);
                if (temp.Length > 0)
                {
                    Basic = Basic.Replace(temp, string.Empty).Trim();
                    _sbcommonBaseInput.MonsterSB.Type = temp.ToLower().Trim();
                    Pos = Basic.IndexOf(PathfinderConstants.PAREN_LEFT, 2);
                    temp = Basic.Replace(PathfinderConstants.PAREN_LEFT, string.Empty)
                        .Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                    if (Pos > 0 || HasNumberInText(temp))
                    {
                        if (Pos == -1) Pos = 0;
                        temp = Basic.Substring(Pos);
                        Basic = Basic.Replace(temp, string.Empty).Trim();
                        temp = temp.Replace(PathfinderConstants.PAREN_LEFT, string.Empty)
                            .Replace(PathfinderConstants.PAREN_RIGHT, string.Empty);
                        _sbcommonBaseInput.MonsterSB.MonsterSource = temp;
                    }

                    _sbcommonBaseInput.MonsterSB.SubType = Basic.Replace(" )", PathfinderConstants.PAREN_RIGHT);
                }
                else
                {
                    Pos = Basic.IndexOf(PathfinderConstants.PAREN_RIGHT);
                    temp = Basic.Substring(Pos + 1);
                    Basic = Basic.Replace(temp, string.Empty).Trim();
                    _sbcommonBaseInput.MonsterSB.Type = temp.ToLower().Trim();
                }
            }
            else
            {
                _sbcommonBaseInput.MonsterSB.Type = Basic.ToLower();
            }

            if (_sbcommonBaseInput.MonsterSB.SubType == string.Empty && _sbcommonBaseInput.MonsterSB.Type == "humanoid")
            {
                temp = _sbcommonBaseInput.MonsterSB.Race.ToLower().Trim();
                switch (temp)
                {
                    case "half-elf":
                        _sbcommonBaseInput.MonsterSB.SubType = "(elf,human)";
                        break;
                    case "half-orc":
                        _sbcommonBaseInput.MonsterSB.SubType = "(orc,human)";
                        break;
                    default:
                        _sbcommonBaseInput.MonsterSB.SubType = PathfinderConstants.PAREN_LEFT + _sbcommonBaseInput.MonsterSB.Race.ToLower().Trim() + PathfinderConstants.PAREN_RIGHT;
                        break;
                }
            }
        }

        private void FindMythicPath()
        {
            if (_sbcommonBaseInput.MonsterSB.Class.Length == 0) return;

            var mathicPaths = new List<string> { "Archmage", "Champion", "Guardian", "Hierophant", "Marshal", "Trickster" };

            foreach (string path in mathicPaths)
            {
                if (_sbcommonBaseInput.MonsterSB.Class.Contains(path.ToLower()))
                {
                    if (path == "Marshal" && _sbcommonBaseInput.MonsterSB.Class.Contains("shieldmarshal")) continue;
                    if (path == "Trickster" && _sbcommonBaseInput.MonsterSB.Class.Contains("arcane trickster")) continue;
                    if (path == "Guardian" && _sbcommonBaseInput.MonsterSB.Class.Contains("ancient guardian")) continue;
                    if (path == "Champion" && _sbcommonBaseInput.MonsterSB.Class.Contains("hooded champion")) continue;
                    if (path == "Champion" && _sbcommonBaseInput.MonsterSB.Class.Contains("shield champion")) continue;
                    if (path == "Champion" && _sbcommonBaseInput.MonsterSB.Class.Contains("champion of the faith")) continue;

                    int Pos = _sbcommonBaseInput.MonsterSB.Class.IndexOf(path.ToLower());
                    string temp = _sbcommonBaseInput.MonsterSB.Class.Substring(Pos);
                    Pos = temp.IndexOf(PathfinderConstants.SPACE);
                    int mythicTier = int.Parse(temp.Substring(Pos));
                    _sbcommonBaseInput.MonsterSB.Mythic = true;
                    _sbcommonBaseInput.MonsterSB.MT = mythicTier;
                    return; //only one path possible
                }
            }
        }

        private bool HasNumberInText(string textString)
        {
            List<string> tempSource = textString.Split(' ').ToList();
            bool findNumber = false;
            foreach (string TS in tempSource)
            {
                try
                {
                    int dummy = Convert.ToInt32(TS);
                    findNumber = true;
                    break;
                }
                catch { }
            }

            return findNumber;
        }

        private void FindClassArchetypes()
        {
            string temp = _sbcommonBaseInput.MonsterSB.Class;
            int Pos = temp.IndexOf(PathfinderConstants.PAREN_LEFT);
            int Pos2 = temp.IndexOf(PathfinderConstants.PAREN_RIGHT, Pos);

            string temp2 = temp.Substring(Pos, Pos2 - Pos + 1);
            temp2 = Utility.RemoveParentheses(temp2);
            List<string> ArchetypesHold = temp2.Replace("*", string.Empty).Split(',').ToList();

            _sbcommonBaseInput.MonsterSB.ClassArchetypes = string.Join(",", ArchetypesHold.ToArray());
            if (_sbcommonBaseInput.MonsterSB.ClassArchetypes.Contains("/"))
            {
                temp = _sbcommonBaseInput.MonsterSB.ClassArchetypes;
                _sbcommonBaseInput.MonsterSB.ClassArchetypes = _sbcommonBaseInput.MonsterSB.ClassArchetypes.Replace("/", ", ");
                _sbcommonBaseInput.MonsterSB.Class = _sbcommonBaseInput.MonsterSB.Class.Replace(temp, _sbcommonBaseInput.MonsterSB.ClassArchetypes);
            }
        }

        private string ParseNPCCodexIntro(string inBlock)
        {
            List<string> inBlockStrings = inBlock.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> outBlocks = new List<string> { string.Empty, string.Empty };

            // outBlocks[0] = inBlockStrings[0]; //name cr

            string temp = inBlockStrings[1].Trim();
            int Pos = temp.LastIndexOf(PathfinderConstants.SPACE);
            string temp2 = temp.Substring(Pos);
            temp = temp2 + PathfinderConstants.SPACE + temp.Replace(temp2, string.Empty).Trim();
            outBlocks[1] = temp.Trim();

            temp = inBlockStrings[0].Trim();
            temp = temp.Replace("\t", PathfinderConstants.SPACE);
            Pos = temp.IndexOf(" XP");
            temp2 = temp.Substring(Pos);
            temp = temp.Replace(temp2, string.Empty).Trim();

            _sbcommonBaseInput.MonsterSB.XP = temp2.Replace("XP", string.Empty).Trim(); //xp
            outBlocks[0] = temp.ToLower().Trim(); //race class

            return string.Join(Environment.NewLine, outBlocks.ToArray());
        }
    }
}
