using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CreatureTypeFoundational;
using CommonStatBlockInfo;

namespace CreatureTypeDetails
{
    public class Aberration : CreatureTypeFoundation
    {
        public Aberration()
        {
            Name = "Aberration";
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.Poor;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.ESCAPE_ARTIST, StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_ONE, 
                                       StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.STEALTH, StatBlockInfo.SkillNames.SURVIVAL, StatBlockInfo.SkillNames.SWIM };
        }
    }
}
