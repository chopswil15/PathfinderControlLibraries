using StatBlockCommon.Individual_SB;
using StatBlockCommon.Monster_SB;

namespace StatBlockCommon
{
    public interface ISBCommonBaseInput
    {
        MonsterStatBlock MonsterSB { get; set; }
        IndividualStatBlock IndvidSB { get; set; }
    }
}