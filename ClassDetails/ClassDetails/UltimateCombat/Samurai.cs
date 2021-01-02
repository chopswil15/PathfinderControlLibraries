using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using OnGoing;
using CommonStatBlockInfo;
using PathfinderGlobals;
using ClassFoundational;

namespace ClassDetails
{
    public class Samurai : ClassFoundation 
    {
        public Samurai()
        {
            Name = "Samurai";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.Good;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Poor;
            BABType = StatBlockInfo.BABType.Fast;
            ClassArchetypes = new List<string>() {"Sword Saint" };
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Martial;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Extra;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Heavy;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Medium;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Shield;
        }

        public override List<string> GetWeaponProficienciesExtra()
        {
            return new List<string> { "katana", "naginata", "wakizashi" };
        }

        public override string AlternateName()
        {
            return "Cavalier";
        }

        public override List<string> ClassArchetypeSkills(string Archetype)
        {
            return ClassSkills();
        }


        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            int temp = 0;
            if (ClassLevel >= 6) temp++;
            if (ClassLevel >= 12) temp++;
            if (ClassLevel >= 18) temp++;

            return temp;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SWIM };
        }
    }
}
