using ClassManager;
using CreatureTypeFoundational;
using Skills;
using StatBlockCommon;
using StatBlockCommon.Individual_SB;
using StatBlockCommon.Monster_SB;
using StatBlockParsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockChecker
{
    public class SBCheckerBaseInput : ISBCheckerBaseInput
    {
        public StatBlockMessageWrapper MessageXML { get; set; }
        public MonsterStatBlock MonsterSB { get; set; }
        public IndividualStatBlock_Combat IndvSB { get; set; }
        public MonSBSearch MonsterSBSearch { get; set; }
        public ClassMaster CharacterClasses { get; set; }
        public RaceBase Race_Base { get; set; }
        public List<string> SourceSuperScripts { get; set; }
        public List<string> MagicInEffect { get; set; }
        public List<SkillsInfo.SkillInfo> SkillsValues { get; set; }
        public CreatureTypeFoundation CreatureType { get; set; }
        public AbilityScores.AbilityScores AbilityScores { get; set; }
    }
}
