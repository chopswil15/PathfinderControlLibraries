using ClassFoundational;
using CommonStatBlockInfo;
using PathfinderGlobals;
using Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassDetails
{
    public class Archwizard : ClassFoundation
    {
        public Archwizard()
        {
            Name = "Archwizard";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 2;
            HitDiceType = StatBlockInfo.HitDiceCategories.d6;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            BABType = StatBlockInfo.BABType.Medium;
            IsPrestigeClass = true;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.KNOWLEDGE_ALL,
                StatBlockInfo.SkillNames.LINGUISTICS, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SPELLCRAFT,
                StatBlockInfo.SkillNames.USE_MAGIC_DEVICE };
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Skill Focus (Knowledge [arcana])", "Skill Focus (Spellcraft)", "Spell Penetration" };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeArcana, Value = 15 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Spellcraft, Value = 15 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.UseMagicDevice, Value = 15 });
            return temp;
        }

        public override void GetSpellOverLoads(ref int overloadLevel, ref int overloadClassLevel, int casterLevel, int classLevel)
        {
            overloadClassLevel += classLevel;
            overloadLevel += base.GetOverloadPrestige(classLevel, OverloadPrestigeType.Full);
        }
    }
}
