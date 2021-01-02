using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassFoundational;
using CommonStatBlockInfo;
using Skills;

namespace ClassDetails
{
    public class BellflowerTiller : ClassFoundation 
    {
        public BellflowerTiller()
        {
            Name = "Bellflower Tiller";
            ClassAlignments = new List<string>{ "CG"};
            SkillRanksPerLevel = 6;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            BABType = StatBlockInfo.BABType.Medium;
            IsPrestigeClass = true;
        }      

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.DISGUISE, StatBlockInfo.SkillNames.ESCAPE_ARTIST, StatBlockInfo.SkillNames.HEAL, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_GEOGRAPHY, 
                StatBlockInfo.SkillNames.KNOWLEDGE_LOCAL, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SLEIGHT_OF_HAND, StatBlockInfo.SkillNames.STEALTH, StatBlockInfo.SkillNames.SURVIVAL };
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            int Count = 0;

            if (ClassLevel >= 2) Count++;
            if (ClassLevel >= 6) Count++;
            if (ClassLevel >= 10) Count++;

            return Count;
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Disguise, Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeLocal, Value = 3 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Stealth,  Value = 5 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Survival, Value = 5 });
            return temp;
        }
    }
}
