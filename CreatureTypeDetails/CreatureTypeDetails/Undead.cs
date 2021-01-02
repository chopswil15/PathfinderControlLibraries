using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CreatureTypeFoundational;
using CommonStatBlockInfo;

namespace CreatureTypeDetails
{
    public class Undead : CreatureTypeFoundation
    {
        public Undead()
        {
            Name = "Undead";
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.Poor;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
        }

        public override string FortMod()
        {
            return StatBlockInfo.CHA;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.DISGUISE, StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA, StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION,
                                    StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.STEALTH };
        }
    }
}
