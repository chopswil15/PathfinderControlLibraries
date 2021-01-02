using ClassFoundational;
using CommonStatBlockInfo;
using Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassDetails
{
    public class Justicar : ClassFoundation
    {
        public Justicar()
        {
            Name = "Justicar"; //Justicar of Muir
            ClassAlignments = new List<string> { "LG" };
            SkillRanksPerLevel = 2;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            BABType = StatBlockInfo.BABType.Medium;
            IsPrestigeClass = true;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.HEAL, StatBlockInfo.SkillNames.KNOWLEDGE_NOBILITY
                 , StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION,StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SPELLCRAFT };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.KnowledgeReligion, Value = 8 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.SenseMotive, Value = 2 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Diplomacy, Value = 2 });
            return temp;
        }
    }
}
