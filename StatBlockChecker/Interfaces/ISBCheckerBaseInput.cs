using ClassManager;
using CreatureTypeFoundational;
using Skills;
using StatBlockCommon;
using StatBlockCommon.Individual_SB;
using StatBlockCommon.Monster_SB;
using StatBlockParsing;
using System.Collections.Generic;

namespace StatBlockChecker
{
    public interface ISBCheckerBaseInput
    {
        AbilityScores.AbilityScores AbilityScores { get; set; }
        ClassMaster CharacterClasses { get; set; }
        CreatureTypeFoundation CreatureType { get; set; }
        IndividualStatBlock_Combat IndvSB { get; set; }
        List<string> MagicInEffect { get; set; }
        StatBlockMessageWrapper MessageXML { get; set; }
        MonsterStatBlock MonsterSB { get; set; }
        MonSBSearch MonsterSBSearch { get; set; }
        RaceBase Race_Base { get; set; }
        List<SkillsInfo.SkillInfo> SkillsValues { get; set; }
        List<string> SourceSuperScripts { get; set; }
    }
}