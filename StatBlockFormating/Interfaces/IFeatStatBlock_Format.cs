using StatBlockCommon.Feat_SB;
using System.Collections.Generic;

namespace StatBlockFormating
{
    public interface IFeatStatBlock_Format
    {
        List<string> BoldPhrases { get; set; }
        FeatStatBlock FeatSB { get; set; }
        List<string> ItalicPhrases { get; set; }

        string CreateFullText(FeatStatBlock SB);
    }
}