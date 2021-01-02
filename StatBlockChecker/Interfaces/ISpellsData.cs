using StatBlockCommon;
using System.Collections.Generic;

namespace StatBlockChecker
{
    public interface ISpellsData
    {
        List<string> BeforeCombatMagic { get; set; }
        Dictionary<string, SpellList> ClassSpells { get; set; }
        List<string> ConstantSpells { get; set; }
        List<string> MagicInEffect { get; set; }
        Dictionary<string, SpellList> SLA { get; set; }
    }
}