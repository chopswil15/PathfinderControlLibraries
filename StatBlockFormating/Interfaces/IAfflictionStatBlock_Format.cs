using StatBlockCommon.Affliction_SB;
using System.Collections.Generic;

namespace StatBlockFormating
{
    public interface IAfflictionStatBlock_Format
    {
        AfflictionStatBlock AfflictionSB { get; set; }
        List<string> BoldPhrases { get; set; }
        List<string> ItalicPhrases { get; set; }

        string CreateFullText(AfflictionStatBlock SB);
    }
}