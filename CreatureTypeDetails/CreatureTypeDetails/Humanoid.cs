using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CreatureTypeFoundational;
using CommonStatBlockInfo;

namespace CreatureTypeDetails
{
    public class Humanoid : CreatureTypeFoundation
    {
        public Humanoid()
        {
            Name = "Humanoid";
            SkillRanksPerLevel = 2;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.Varies;
            RefSaveType = StatBlockInfo.SaveBonusType.Varies;
            WillSaveType = StatBlockInfo.SaveBonusType.Varies;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.HEAL, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SURVIVAL };
        }
    }
}
