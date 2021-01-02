using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CreatureTypeFoundational;
using CommonStatBlockInfo;

namespace CreatureTypeDetails
{
    public class MonstrousHumanoid : CreatureTypeFoundation
    {
        public MonstrousHumanoid()
        {
            Name = "Monstrous Humanoid";
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.Poor;
            RefSaveType = StatBlockInfo.SaveBonusType.Good;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.STEALTH, StatBlockInfo.SkillNames.SURVIVAL, StatBlockInfo.SkillNames.SWIM };
        }
    }
}
