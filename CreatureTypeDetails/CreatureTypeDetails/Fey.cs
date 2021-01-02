using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using CreatureTypeFoundational;

namespace CreatureTypeDetails
{
    public class Fey : CreatureTypeFoundation
    {
        public Fey()
        {
            Name = "Fey";
            SkillRanksPerLevel = 6;
            HitDiceType = StatBlockInfo.HitDiceCategories.d6;
            FortSaveType = StatBlockInfo.SaveBonusType.Poor;
            RefSaveType = StatBlockInfo.SaveBonusType.Good;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.DISGUISE, StatBlockInfo.SkillNames.ESCAPE_ARTIST, StatBlockInfo.SkillNames.FLY,
                                      StatBlockInfo.SkillNames.KNOWLEDGE_GEOGRAPHY, StatBlockInfo.SkillNames.KNOWLEDGE_LOCAL, StatBlockInfo.SkillNames.KNOWLEDGE_NATURE, StatBlockInfo.SkillNames.PERCEPTION,
                                      StatBlockInfo.SkillNames.PERFORM, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SLEIGHT_OF_HAND, StatBlockInfo.SkillNames.STEALTH, StatBlockInfo.SkillNames.SWIM, StatBlockInfo.SkillNames.USE_MAGIC_DEVICE };
        }
    }
}
