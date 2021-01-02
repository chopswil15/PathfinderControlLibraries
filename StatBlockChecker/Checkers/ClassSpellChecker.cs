
using PathfinderGlobals;
using StatBlockCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using static StatBlockCommon.StatBlockGlobals;

namespace StatBlockChecker.Checkers
{
    public class ClassSpellChecker : IClassSpellChecker
    {
        private Dictionary<string, SpellList> _classSpells;
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        private readonly string SPELLBLOCKTYPE_SPELLPREAPRED = "Spells Prepared";
        private readonly string SPELLBLOCKTYPE_SPELLKNOWN = "Spells Known";

        public ClassSpellChecker(ISBCheckerBaseInput sbCheckerBaseInput)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
        }

        public Dictionary<string, SpellList> ParseClassSpells()
        {
            List<string> classNames = _sbCheckerBaseInput.CharacterClasses.GetClassNames();
            SpellList spellList;

            _classSpells = new Dictionary<string, SpellList>();
            Dictionary<string, int> classPos = new Dictionary<string, int>();  //class name, class string position
            Dictionary<string, string> spellBlocks = new Dictionary<string, string>();
            bool hasRacialSpells = _sbCheckerBaseInput.Race_Base.RaceSB.SpellsKnown.Length > 0;

            try
            {
                string className = string.Empty;
                FindSpellsKnown(classNames, classPos, spellBlocks, hasRacialSpells);

                //spells prepared
                FindSpellsPrepared(classNames, ref classPos, spellBlocks, hasRacialSpells);

                //extracts prepared               
                FindExtractsPrepared(classNames, ref classPos, spellBlocks, hasRacialSpells);

                if (!classNames.Any() && _sbCheckerBaseInput.MonsterSB.SpellsPrepared.Length > 0)
                {
                    spellList = new SpellList();
                    spellList.ParseSpellList(_sbCheckerBaseInput.MonsterSB.SpellsPrepared, _sbCheckerBaseInput.SourceSuperScripts);
                    if (spellList.Errors.Length > 0)
                    {
                        _sbCheckerBaseInput.MessageXML.AddFail("Spells Prepared-" + className, spellList.Errors);
                    }
                    string temp2 = spellList.Source.Substring(0, spellList.Source.IndexOf(PathfinderConstants.SPACE)).Trim();
                    _classSpells.Add(temp2, spellList);
                }

                if (!classNames.Any() && _sbCheckerBaseInput.MonsterSB.SpellsKnown.Length > 0)
                {
                    spellList = new SpellList();
                    spellList.ParseSpellList(_sbCheckerBaseInput.MonsterSB.SpellsKnown, _sbCheckerBaseInput.SourceSuperScripts);
                    if (spellList.Errors.Length > 0)
                    {
                        _sbCheckerBaseInput.MessageXML.AddFail("Spells Known-" + className, spellList.Errors);
                    }
                    string temp2 = spellList.Source.Substring(0, spellList.Source.IndexOf(PathfinderConstants.SPACE)).Trim();
                    _classSpells.Add(temp2, spellList);
                }
            }
            catch (Exception ex)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("ParseClassSpells", ex.Message);
            }

            return _classSpells;
        }

        private void FindSpellsKnown(List<string> classNames,
             Dictionary<string, int> classPos, Dictionary<string, string> spellBlocks, bool hasRacialSpells)
        {
            string spellBlock;
            if (_sbCheckerBaseInput.MonsterSB.SpellsKnown.Length == 0) return;
            
            //spells known
            FindClassSpellStringPositions(classNames, classPos, _sbCheckerBaseInput.MonsterSB.SpellsKnown, hasRacialSpells);
            if (!classPos.Any()) _sbCheckerBaseInput.MessageXML.AddFail("SpellsKnown", "Can't find SpellsKnown spellBlock");

            string hold = GetSpellBlockText(classPos, ref spellBlocks, "Known");
            string className, spellsHold;
            SpellList spellList;

            foreach (KeyValuePair<string, string> kvp in spellBlocks)
            {
                className = kvp.Key;
                spellsHold = kvp.Value;
                spellBlock = GetOneSpellBlock(ref spellsHold, className, classNames);

                spellList = new SpellList();
                spellList.ParseSpellList(spellBlock, _sbCheckerBaseInput.SourceSuperScripts);
                if (spellList.Errors.Length > 0)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("Class Spells-" + className, spellList.Errors);
                }
                _classSpells.Add(className, spellList);
            }          
        }

        private void FindExtractsPrepared(List<string> classNames, ref Dictionary<string, int> classPos,
            Dictionary<string, string> spellBlocks, bool hasRacialSpells)
        {
            if (_sbCheckerBaseInput.MonsterSB.ExtractsPrepared.Length == 0) return;
           
            int beforeCount = classPos.Count;
            FindClassSpellStringPositions(classNames, classPos, _sbCheckerBaseInput.MonsterSB.ExtractsPrepared, hasRacialSpells);

            if (classPos.Count == beforeCount)  _sbCheckerBaseInput.MessageXML.AddFail("ExtractsPrepared", "Can't find ExtractsPrepared spellBlock");

            string hold = GetSpellBlockText(classPos, ref spellBlocks, "Extracts");

            string className, spellsHold, spellBlock;
            SpellList spellList;

            foreach (KeyValuePair<string, string> kvp in spellBlocks)
            {
                className = kvp.Key;
                spellsHold = kvp.Value;
                spellBlock = GetOneSpellBlock(ref spellsHold, className, classNames);

                spellList = new SpellList();
                spellList.ParseSpellList(spellBlock, _sbCheckerBaseInput.SourceSuperScripts);
                if (spellList.Errors.Length > 0)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("Extracts Prepared-" + className, spellList.Errors);
                }
                if (!_classSpells.ContainsKey(className))
                {
                    _classSpells.Add(className, spellList);
                }
            }            
        }

        private void FindSpellsPrepared(List<string> classNames, ref Dictionary<string, int> classPos,
            Dictionary<string, string> spellBlocks, bool hasRacialSpells)
        {
            if (_sbCheckerBaseInput.MonsterSB.SpellsPrepared.Length == 0) return;
           
            int beforeCount = classPos.Count;
            FindClassSpellStringPositions(classNames, classPos, _sbCheckerBaseInput.MonsterSB.SpellsPrepared, hasRacialSpells);

            if (classPos.Count == beforeCount)
            {
                _sbCheckerBaseInput.MessageXML.AddFail("SpellsPrepared", "Can't find SpellsPrepared spellBlock");
            }
            string hold = GetSpellBlockText(classPos, ref spellBlocks, "Prepared");

            string className, spellsHold, spellBlock;

            foreach (KeyValuePair<string, string> kvp in spellBlocks)
            {
                className = kvp.Key;
                spellsHold = kvp.Value;
                spellBlock = GetOneSpellBlock(ref spellsHold, className, classNames);

                SpellList spellList = new SpellList();
                spellList.ParseSpellList(spellBlock, _sbCheckerBaseInput.SourceSuperScripts);
                if (spellList.Errors.Length > 0)
                {
                    _sbCheckerBaseInput.MessageXML.AddFail("Spells Prepared-" + className, spellList.Errors);
                }
                if (!_classSpells.ContainsKey(className))
                {
                    _classSpells.Add(className, spellList);
                }
            }           
        }

        private string GetOneSpellBlock(ref string SpellBlocks, string ClassName, List<string> Names)
        {
            int Pos = SpellBlocks.IndexOf(ClassName);
            int EndPos, CRPos;
            if (Pos == -1) Pos = 0;
            int CLPos = SpellBlocks.IndexOf("CL ", Pos);
            string temp = string.Empty;

            if (CLPos >= 0) //block after
            {
                EndPos = SpellBlocks.Length;
                foreach (string name in Names)
                {
                    if (_sbCheckerBaseInput.CharacterClasses.CanClassCastSpells(name.ToLower()) && name != ClassName)
                    {
                        CRPos = SpellBlocks.IndexOf(name, Pos);
                        if (CRPos > Pos && CRPos <= EndPos) EndPos = CRPos;
                    }
                }
                temp = SpellBlocks.Substring(Pos, EndPos - Pos).Trim();
            }

            return temp;
        }

        private string GetSpellBlockText(Dictionary<string, int> classPos, ref Dictionary<string, string> spellBlocks, string spellBlockType)
        {
            string hold = string.Empty;
            string temp = string.Empty;
            string keyHold = string.Empty;
            int Pos = 0;
            var sortedDict = (from entry in classPos
                              orderby entry.Value ascending
                              select entry);
            int count = 1;

            switch (spellBlockType)
            {
                case "Known":
                    hold = _sbCheckerBaseInput.MonsterSB.SpellsKnown;
                    break;
                case "Prepared":
                    hold = _sbCheckerBaseInput.MonsterSB.SpellsPrepared;
                    break;
                case "Extracts":
                    hold = _sbCheckerBaseInput.MonsterSB.ExtractsPrepared;
                    break;
            }

            if (hold.Length > 0)
            {
                foreach (KeyValuePair<string, int> kvp in sortedDict)
                {
                    Pos = kvp.Value;
                    if (Pos > 0)
                    {
                        temp = hold.Substring(0, Pos).Trim();
                        hold = hold.Replace(temp, string.Empty).Trim();
                        spellBlocks.Add(temp, hold);
                    }

                    keyHold = kvp.Key;
                    if (count == sortedDict.Count())
                    {
                        if (!spellBlocks.ContainsKey(kvp.Key))
                        {
                            spellBlocks.Add(kvp.Key, hold);
                        }
                    }

                    count++;
                }
            }

            return hold;
        }

        private bool IsWizard(string ClassName)
        {
            List<string> temp = CommonMethods.GetWizardSpecializations();

            return temp.Contains(ClassName);
        }

        private void FindClassSpellStringPositions(List<string> classNames, Dictionary<string, int> classPos,
            string spellBlockTypeString, bool hasRacialSpells)
        {
            string spellBockTypeUsed = string.Empty;
            List<string> blocksToCheck = new List<string>();
            if (spellBlockTypeString.Contains(SPELLBLOCKTYPE_SPELLPREAPRED)) blocksToCheck.Add(SPELLBLOCKTYPE_SPELLPREAPRED);
            if (spellBlockTypeString.Contains(SPELLBLOCKTYPE_SPELLKNOWN)) blocksToCheck.Add(SPELLBLOCKTYPE_SPELLKNOWN);

            int Pos;
            if (!classNames.Any())
            {
                FindBlockToUse(blocksToCheck, classPos, spellBlockTypeString);
            }
            else
            {
                foreach (string name in classNames)
                {
                    if (_sbCheckerBaseInput.CharacterClasses.CanClassCastSpells(name.ToLower()))
                    {
                        Pos = spellBlockTypeString.IndexOf(name);
                        if (spellBlockTypeString.Contains(name))
                        {
                            classPos.Add(name, Pos);
                        }
                        else if (IsWizard(name))
                        {
                            Pos = spellBlockTypeString.IndexOf("Wizard");
                            if (spellBlockTypeString.Contains("Wizard"))
                            {
                                classPos.Add("Wizard", Pos);
                            }
                            else if (classNames.Count == 1)
                            {
                                FindBlockToUse(blocksToCheck, classPos, spellBlockTypeString);
                            }
                        }
                        else if (hasRacialSpells || classNames.Count == 1 || _sbCheckerBaseInput.CharacterClasses.CanCastSpellsCount() == 1)
                        {
                            FindBlockToUse(blocksToCheck, classPos, spellBlockTypeString);
                        }
                    }
                    else
                    {
                        if (hasRacialSpells)
                        {
                            FindBlockToUse(blocksToCheck, classPos, spellBlockTypeString);
                        }
                    }
                }
            }
        }

        private void FindBlockToUse(List<string> blockToFind, Dictionary<string, int> classPos, string spellBlockTypeString)
        {
            foreach (var oneBlock in blockToFind)
            {
                int Pos = spellBlockTypeString.IndexOf(oneBlock);
                classPos.Add("None", Pos);
            }
        }
    }
}
