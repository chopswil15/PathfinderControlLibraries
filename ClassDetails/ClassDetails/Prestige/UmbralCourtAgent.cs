using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassFoundational;
using CommonStatBlockInfo;
using Skills;

namespace ClassDetails
{
    public class UmbralCourtAgent : ClassFoundation 
    {
        public UmbralCourtAgent()
        {
            Name = "Umbral Court Agent";
            ClassAlignments = new List<string>() { "LE" };
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            BABType = StatBlockInfo.BABType.Medium;
            IsPrestigeClass = true;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_NOBILITY, StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.STEALTH };
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            //Tenebrous Magic
            return 1;
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Bluff, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeNobility, Value = 7 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeReligion, Value = 7 });
            return temp;
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Deceitful" };
        }

        public override void GetSpellOverLoads(ref int overloadLevel, ref int overloadClassLevel, int casterLevel, int classLevel)
        {
            overloadClassLevel += classLevel;
            overloadLevel += base.GetOverloadPrestige(classLevel, OverloadPrestigeType.BlocksOfThree);
        }
    }
}
