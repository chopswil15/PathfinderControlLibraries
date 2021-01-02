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
    public class Gunslinger : ClassFoundation 
    {
        public Gunslinger()
        {
            Name = "Gunslinger";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 4;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.Good;
            RefSaveType = StatBlockInfo.SaveBonusType.Good;
            WillSaveType = StatBlockInfo.SaveBonusType.Poor;
            BABType = StatBlockInfo.BABType.Fast;
            ClassArchetypes = new List<string>() { "Gun Tank", "Musket Master", "Mysterious Stranger", "Pistolero", "Techslinger","Bolt Ace" };
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Martial;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            int temp = 0;
            if (ClassLevel >= 1) temp++; //Gunsmithing
            if (ClassLevel >= 4) temp++;
            if (ClassLevel >= 8) temp++;
            if (ClassLevel >= 12) temp++;
            if (ClassLevel >= 16) temp++;
            if (ClassLevel >= 20) temp++;

            return temp;
        }

        public override List<string> ClassArchetypeSkills(string Archetype)
        {
            return ClassSkills();
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.HEAL, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_ENGINEERING,
                        StatBlockInfo.SkillNames.KNOWLEDGE_LOCAL, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SLEIGHT_OF_HAND, StatBlockInfo.SkillNames.SURVIVAL, StatBlockInfo.SkillNames.SWIM };
        }

    }
}
