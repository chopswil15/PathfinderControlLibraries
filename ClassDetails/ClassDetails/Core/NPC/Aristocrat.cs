using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonStatBlockInfo;
using ClassFoundational;
using PathfinderGlobals;

namespace ClassDetails
{
    public class Aristocrat : ClassFoundation
    {
        public Aristocrat()
        {
            Name = "Aristocrat";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.Poor;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
            BABType = StatBlockInfo.BABType.Medium;
            IsNPC_Class = true;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Martial;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Shield;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Tower;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Medium;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Heavy;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.APPRAISE, StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.DISGUISE, StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_ALL, StatBlockInfo.SkillNames.LINGUISTICS, 
                                     StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.PERFORM, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SWIM, StatBlockInfo.SkillNames.SURVIVAL };
        }

        public override List<CheckClassError> CheckClass(int classLevel)
        {
            return new List<CheckClassError>();
        }
    }
}
