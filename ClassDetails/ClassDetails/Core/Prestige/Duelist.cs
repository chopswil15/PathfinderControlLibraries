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
    public class Duelist : ClassFoundation 
    {
        public Duelist()
        {
            Name = "Duelist";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            RefSaveType = StatBlockInfo.SaveBonusType.PrestigeGood;
            WillSaveType = StatBlockInfo.SaveBonusType.PrestigePoor;
            BABType = StatBlockInfo.BABType.Fast;
            IsPrestigeClass = true;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Martial;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
            PrestigeBABMin = 6;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.ESCAPE_ARTIST, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.PERFORM, StatBlockInfo.SkillNames.SENSE_MOTIVE };
        }

        public override List<string> PrestigePreReqFeats()
        {
            return new List<string> { "Dodge", "Mobility", "Weapon Finesse" };
        }

        public override List<PreReqSkill> PrestigePreReqSkills()
        {
            List<PreReqSkill> temp = new List<PreReqSkill>();
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Acrobatics, Value = 2 });
            temp.Add(new PreReqSkill { SkillName = SkillData.SkillNames.Perform, Value = 2 });
            return temp;
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            int count = 0;

            if (ClassLevel >= 4) count++; //Combat Reflexes
            if (ClassLevel >= 9) count++; //Deflect Arrows, light weapn only

            return count;
        }

    }
}
