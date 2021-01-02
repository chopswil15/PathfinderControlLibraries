using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassFoundational;
using CommonStatBlockInfo;
using Skills;

namespace ClassDetails
{
    public class Demoniac : ClassFoundation 
    {
        public Demoniac()
        {
            Name = "Demoniac";
            ClassAlignments = new List<string>{ "CE"};
            SkillRanksPerLevel = 2;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8; ;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            BABType = StatBlockInfo.BABType.Fast;
            IsPrestigeClass = true;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_PLANES, StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.STEALTH };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Intimidate, Value = 7 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgePlanes, Value = 7 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Spellcraft, Value = 7 });
            return temp;
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Demonic Obedience", "Iron Will" };
        }

        public override void GetSpellOverLoads(ref int overloadLevel, ref int overloadClassLevel, int casterLevel, int classLevel)
        {
            overloadClassLevel += classLevel;
            overloadLevel += base.GetOverloadPrestige(classLevel, OverloadPrestigeType.MinusOne);
        }
    }
}
