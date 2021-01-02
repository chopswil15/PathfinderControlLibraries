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
    public class WinterWitch : ClassFoundation
    {
        public WinterWitch()
        {
            Name = "Winter Witch";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 2;
            HitDiceType = StatBlockInfo.HitDiceCategories.d6;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            BABType = StatBlockInfo.BABType.Slow;
            IsPrestigeClass = true;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.BLUFF , StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA, 
                StatBlockInfo.SkillNames.KNOWLEDGE_NOBILITY , StatBlockInfo.SkillNames.SURVIVAL, StatBlockInfo.SkillNames.USE_MAGIC_DEVICE };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeArcana, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Spellcraft, Value = 5 });
            return temp;
        }

        public override void GetSpellOverLoads(ref int overloadLevel, ref int overloadClassLevel, int casterLevel, int classLevel)
        {
            overloadClassLevel += classLevel;
            overloadLevel += base.GetOverloadPrestige(classLevel, OverloadPrestigeType.MinusOne);
        }
    }
}
