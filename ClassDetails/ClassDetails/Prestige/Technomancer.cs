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
    public class Technomancer : ClassFoundation 
    {
        public Technomancer()
        {
            Name = "Technomancer";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d6;
            FortSaveType = StatBlockInfo.SaveBonusType.Poor;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
            BABType = StatBlockInfo.BABType.Medium;
            IsPrestigeClass = true;
            ClassArchetypes = new List<string>() {  };
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Extra;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.APPRAISE, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DISABLE_DEVICE,
                StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_ALL, 
                     StatBlockInfo.SkillNames.LINGUISTICS, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.PROFESSION,
                StatBlockInfo.SkillNames.SPELLCRAFT };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.DisableDevice, Value = 6 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeEngineering, Value = 6 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Spellcraft, Value = 6 });
            return temp;
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Skill Focus (Knowledge [engineering]), Technologist" };
        }

        public override void GetSpellOverLoads(ref int overloadLevel, ref int overloadClassLevel, int casterLevel, int classLevel)
        {
            overloadClassLevel += classLevel;
            overloadLevel += base.GetOverloadPrestige(classLevel, OverloadPrestigeType.MinusOne);
        }
    }
}
