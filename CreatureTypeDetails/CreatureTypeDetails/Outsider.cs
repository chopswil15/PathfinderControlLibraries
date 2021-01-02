using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CreatureTypeFoundational;
using CommonStatBlockInfo;

namespace CreatureTypeDetails
{
    public class Outsider : CreatureTypeFoundation
    {
        public Outsider()
        {
            Name = "Outsider";
            SkillRanksPerLevel = 6;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.Varies;
            RefSaveType = StatBlockInfo.SaveBonusType.Varies;
            WillSaveType = StatBlockInfo.SaveBonusType.Varies;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.KNOWLEDGE_PLANES, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.STEALTH, "Extra 4" };
        }
    }
}
