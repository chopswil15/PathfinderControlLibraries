using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonStatBlockInfo;
using Skills;
using PathfinderGlobals;
using ClassFoundational;

namespace ClassDetails
{
    public class BrotherOfTheSeal : ClassFoundation
    {
        public BrotherOfTheSeal()
        {
            Name = "Brother of the Seal";
            ClassAlignments = CommonMethods.GetLawfulAlignments();
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            BABType = StatBlockInfo.BABType.Fast;
            IsPrestigeClass = true;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.DISABLE_DEVICE, StatBlockInfo.SkillNames.KNOWLEDGE_ENGINEERING, StatBlockInfo.SkillNames.KNOWLEDGE_HISTORY, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.SENSE_MOTIVE,
                   StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.STEALTH };
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            int value = 0;
            switch(ClassLevel)
            {
                case 4:
                    value = 1;
                    break;
            }
            return value;
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeArcana, Value = 5 });
            return temp;
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Improved Unarmed Strike", "Stunning Fist" };
        }
    }
}
