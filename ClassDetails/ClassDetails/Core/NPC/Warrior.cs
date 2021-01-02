using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonStatBlockInfo;
using ClassFoundational;
using PathfinderGlobals;

namespace ClassDetails
{
    public class Warrior : ClassFoundation
    {
        public Warrior()
        {
            Name = "Warrior";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 2;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.Good;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Poor;
            BABType = StatBlockInfo.BABType.Fast;
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
            return new List<string> {StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SWIM };
        }

        public override List<CheckClassError> CheckClass(int classLevel)
        {
            return new List<CheckClassError>();
        }
    }
}
