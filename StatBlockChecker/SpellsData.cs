using StatBlockCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockChecker
{
    public class SpellsData : ISpellsData
    {
        public List<string> BeforeCombatMagic { get; set; }
        public List<string> ConstantSpells { get; set; }
        public Dictionary<string, SpellList> ClassSpells { get; set; }
        public Dictionary<string, SpellList> SLA { get; set; }  //Spell-Like Abilities
        public List<string> MagicInEffect { get; set; }

        public SpellsData()
        {
            ClassSpells = new Dictionary<string, SpellList>();
            SLA = new Dictionary<string, SpellList>();
        }
    }
}
