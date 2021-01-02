using StatBlockCommon.Monster_SB;
using System.Collections.Generic;
using System.Text;

namespace StatBlockFormating
{
    public interface IMonsterStatBlock_Format
    {
        List<string> BoldPhrases { get; set; }
        List<string> BoldPhrasesSpecialAbilities { get; set; }
        List<string> ItalicPhrases { get; set; }
        MonsterStatBlock MonSB { get; set; }
        string SourceSuperScript { get; set; }

        string CreateFullText(MonsterStatBlock statBlock);
        void Get_CSS_Text(StringBuilder oText);
        void ReplaceTables(MonsterStatBlock SB_old, MonsterStatBlock SB_new);
    }
}