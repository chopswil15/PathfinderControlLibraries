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
    public class Cyphermage : ClassFoundation 
    {
        public Cyphermage()
        {
            Name = "Cyphermage";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d6;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            BABType = StatBlockInfo.BABType.Slow;
            IsPrestigeClass = true;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.APPRAISE , StatBlockInfo.SkillNames.CLIMB , StatBlockInfo.SkillNames.DISABLE_DEVICE,
                StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.KNOWLEDGE_ALL, StatBlockInfo.SkillNames.LINGUISTICS, 
                StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.SURVIVAL, StatBlockInfo.SkillNames.USE_MAGIC_DEVICE };
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Cypher Magic", "Scribe Scroll"};
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill{SkillName = SkillData.SkillNames.KnowledgeArcana,Value=5});
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeHistory, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Linguistics, Value = 5 });
            return temp;
        }

        public override void GetSpellOverLoads(ref int overloadLevel, ref int overloadClassLevel, int casterLevel, int classLevel)
        {
            overloadClassLevel += classLevel;
            overloadLevel += base.GetOverloadPrestige(classLevel, OverloadPrestigeType.Full);
        }
    }
}
