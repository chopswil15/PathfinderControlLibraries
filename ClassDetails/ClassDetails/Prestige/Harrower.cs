using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using OnGoing;
using CommonStatBlockInfo;
using Skills;
using PathfinderGlobals;
using ClassFoundational;

namespace ClassDetails
{
    public class Harrower : ClassFoundation 
    {
        public Harrower()
        {
            Name = "Harrower";
            ClassAlignments = CommonMethods.GetAllButTrueNeutralAlignments();
            SkillRanksPerLevel = 2;
            HitDiceType = StatBlockInfo.HitDiceCategories.d6;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            BABType = StatBlockInfo.BABType.Slow;
            IsPrestigeClass = true;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DIPLOMACY, 
                StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA, StatBlockInfo.SkillNames.KNOWLEDGE_LOCAL, 
                   StatBlockInfo.SkillNames.KNOWLEDGE_PLANES, StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION, StatBlockInfo.SkillNames.PERFORM, 
                StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.USE_MAGIC_DEVICE };
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Harrowed" };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeArcana, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Perform, Value = 5 });
            return temp;
        }

        public override void GetSpellOverLoads(ref int overloadLevel, ref int overloadClassLevel, int casterLevel, int classLevel)
        {
            overloadClassLevel += classLevel;
            overloadLevel += base.GetOverloadPrestige(classLevel, OverloadPrestigeType.Full);
        }
    }
}
