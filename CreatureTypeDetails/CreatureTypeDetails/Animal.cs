using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using CreatureTypeFoundational;

namespace CreatureTypeDetails
{
    public class Animal : CreatureTypeFoundation
    {
        public Animal()
        {
            Name = "Animal";
            SkillRanksPerLevel = 2;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.Good;
            RefSaveType = StatBlockInfo.SaveBonusType.Good;
            WillSaveType = StatBlockInfo.SaveBonusType.Poor;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.STEALTH, StatBlockInfo.SkillNames.SWIM };
        }
    }
}
