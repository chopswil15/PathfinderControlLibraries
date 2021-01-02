using StatBlockCommon;
using System.Collections.Generic;

namespace StatBlockChecker.Checkers
{
    public interface IClassSpellChecker
    {
        Dictionary<string, SpellList> ParseClassSpells();
    }
}