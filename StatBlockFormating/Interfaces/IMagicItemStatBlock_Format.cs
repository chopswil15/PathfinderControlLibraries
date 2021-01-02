using StatBlockCommon.MagicItem_SB;
using System.Collections.Generic;

namespace StatBlockFormating
{
    public interface IMagicItemStatBlock_Format
    {
        List<string> BoldPhrases { get; set; }
        List<string> ItalicPhrases { get; set; }

        string CreateFullText(MagicItemStatBlock SB);
    }
}