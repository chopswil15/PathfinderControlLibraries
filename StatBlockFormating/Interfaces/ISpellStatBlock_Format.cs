using StatBlockCommon;
using StatBlockCommon.Spell_SB;
using System.Collections.Generic;

namespace StatBlockFormating
{
    public interface ISpellStatBlock_Format
    {
        List<string> BoldPhrases { get; set; }
        List<string> ItalicPhrases { get; set; }
        ISpellStatBlock SpellSB { get; set; }

        string CreateFullText(ISpellStatBlock SB);
        string ReCreateFullText(SpellStatBlock SB);
        void ReplaceTables(ISpellStatBlock SB_old, ISpellStatBlock SB_new);
    }
}