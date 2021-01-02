using PathfinderGlobals;
using StatBlockBusiness;
using StatBlockCommon;
using StatBlockCommon.Individual_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace StatBlockChecker.Parsers
{
    public class ConstantSpellsParser : IConstantSpellsParser
    {
        private ISBCheckerBaseInput _sbCheckerBaseInput;
        private ISpellsData _spellsData;
        private ISpellStatBlockBusiness _spellStatBlockBusiness;

        public ConstantSpellsParser(ISBCheckerBaseInput sbCheckerBaseInput, ISpellsData spellsData, ISpellStatBlockBusiness spellStatBlockBusiness)
        {
            _sbCheckerBaseInput = sbCheckerBaseInput;
            _spellsData = spellsData;
            _spellStatBlockBusiness = spellStatBlockBusiness;
        }

        public void ParseConstantSpells()
        {
            string text = _sbCheckerBaseInput.MonsterSB.SpellLikeAbilities;
            string CR = Environment.NewLine;
            _spellsData.ConstantSpells = new List<string>();

            if (!text.Contains("Constant")) return;

            int Pos = text.IndexOf("Constant");
            int Pos3 = text.IndexOf(CR);
            if (Pos3 == -1) Pos3 = text.Length;
            ISpellStatBlock spell = null;
            bool found = false;
            _sbCheckerBaseInput.IndvSB = new IndividualStatBlock_Combat();
            _sbCheckerBaseInput.IndvSB.HD = _sbCheckerBaseInput.MonsterSB.HD;


            List<IndividualStatBlock_Combat> list = new List<IndividualStatBlock_Combat> { _sbCheckerBaseInput.IndvSB };
            int tempPos = Pos3 - Pos;
            if (tempPos < 0) tempPos = text.Length - Pos;

            string hold = text.Substring(Pos, tempPos);
            if (hold.Contains(CR))
            {
                Pos = hold.IndexOf(CR);
                hold = hold.Substring(0, Pos).Trim();
            }
            hold = hold.Replace("Constant-", string.Empty).Trim();
            List<string> spells = hold.Split(',').ToList();

            foreach (string magic in spells)
            {
                string search = magic.Trim();
                if (search.Contains(PathfinderConstants.PAREN_LEFT))
                    search = search.Substring(0, search.IndexOf(PathfinderConstants.PAREN_LEFT));

                search = Utility.SearchMod(search);
                spell = _spellStatBlockBusiness.GetSpellByName(search);
                if (spell != null)
                {
                    try
                    {
                        _sbCheckerBaseInput.IndvSB.CastSpell(spell.name, FindSpellCasterLevel(magic), list);
                        found = true;
                        _spellsData.ConstantSpells.Add(spell.name);
                    }
                    catch (Exception ex)
                    {
                        _sbCheckerBaseInput.MessageXML.AddFail("ParseConstantSpells", ex.Message);
                    }
                }

                if (!found)
                    _sbCheckerBaseInput.MessageXML.AddInfo("Constant Spell: Issue with " + magic);

                found = false;
            }

            if (_spellsData.ConstantSpells.Any())
            {
                string temp = string.Join(", ", _spellsData.ConstantSpells.ToArray());
                _sbCheckerBaseInput.MessageXML.AddInfo("Constant Spells In Effect: " + temp);
            }
        }

        private int FindSpellCasterLevel(string spell)
        {
            if (!_spellsData.ClassSpells.Any() && !_spellsData.SLA.Any()) return -1;
            SpellList tempList = null;

            if (spell.Contains("self only"))
            {
                int pos = spell.IndexOf(PathfinderConstants.PAREN_LEFT);
                spell = spell.Substring(0, pos - 1);
            }

            foreach (KeyValuePair<string, SpellList> kvp in _spellsData.ClassSpells)
            {
                tempList = kvp.Value;
                if (tempList.SpellExists(spell))
                {
                    return tempList.CasterLevel;
                }
            }

            foreach (KeyValuePair<string, SpellList> kvp in _spellsData.SLA)
            {
                tempList = kvp.Value;
                if (tempList.SpellExists(spell))
                {
                    return tempList.CasterLevel;
                }
            }

            return 0;
        }
    }
}
