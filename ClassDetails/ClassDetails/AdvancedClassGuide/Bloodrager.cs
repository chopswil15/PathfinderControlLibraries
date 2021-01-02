using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassFoundational;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace ClassDetails
{
    public class Bloodrager : ClassFoundation
    {
        public Bloodrager()
        {
            Name = "Bloodrager";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 4;
            CanCastSpells = true;
            HasSpellsPerDay = true;
            BloodlineUse = true;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.Good;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Poor;
            BABType = StatBlockInfo.BABType.Fast;
            ClassArchetypes = new List<string> { "Untouchable Rager" };
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Martial;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Shield;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Medium;
            //SetFeatures();
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.INTIMIDATE,StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA,
                                                  StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.RIDE,StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.SURVIVAL, StatBlockInfo.SkillNames.SWIM};
        }


        public override string GetSpellBonusAbility()
        {
            return StatBlockInfo.CHA;
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            int count = 0;
            if (ClassLevel >= 4) count++; //Eschew Materials

            return count;
        }

        public override void ProcessBloodline(string BloodlineName)
        {
            Bloodline = BloodlineReflectionWrapper.AddBloodline(BloodlineName,this.Name);

            if (Bloodline == null) throw new Exception("Bloodline not defined- " + BloodlineName);
        }

        public override List<int> GetSpellsPerLevel(int ClassLevel)
        {
            //Bonus Spells handled in elsewhere
            List<int> Levels = new List<int>();
            Levels.Add(0); //no 0 level spells

            switch (ClassLevel)
            {
                case 4:
                    Levels.Add(2); //1st
                    break;
                case 5:
                    Levels.Add(3); //1st
                    break;
                case 6:
                    Levels.Add(4); //1st
                    break;
                case 7:
                    Levels.Add(4); //1st
                    Levels.Add(2); //2nd 
                    break;
                case 8:
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    break;
                case 9:
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    break;
                case 10:
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(2); //3rd 
                    break;
                case 11:
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd 
                    break;
                case 12:
                    Levels.Add(6); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd 
                    break;
                case 13:
                    Levels.Add(6); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(2); //4th 
                    break;
                case 14:
                    Levels.Add(6); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(3); //4th
                    break;
                case 15:
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(4); //4th
                    break;
                case 16:
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(4); //4th
                    break;
                case 17:
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(4); //4th
                    break;
                case 18:
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(5); //4th
                    break;
                case 19:
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(5); //4th
                    break;
                case 20:
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(5); //4th
                    break;
            }
            return Levels;
        }

        public override List<int> GetSpellsPerDay(int ClassLevel)
        {
            List<int> temp = new List<int>();
            temp.Add(0); //0

            switch (ClassLevel)
            {
                case 1:                    
                case 2:                    
                case 3:
                    temp.Add(0); //1st
                    break;
                case 4:
                    temp.Add(2); //1st
                    break;
                case 5:
                    temp.Add(3); //1st
                    break;
                case 6:
                    temp.Add(4); //1st
                    break;
                case 7:
                    temp.Add(4); //1st
                    temp.Add(2); //2nd
                    break;
                case 8:
                    temp.Add(4); //1st
                    temp.Add(3); //2nd
                    break;
                case 9:
                    temp.Add(5); //1st
                    temp.Add(4); //2nd
                    break;
                case 10:
                    temp.Add(5); //1st
                    temp.Add(4); //2nd
                    temp.Add(2); //3rd
                    break;
                case 11:
                    temp.Add(5); //1st
                    temp.Add(4); //2nd
                    temp.Add(3); //3rd
                    break;
                case 12:
                    temp.Add(6); //1st
                    temp.Add(4); //2nd
                    temp.Add(4); //3rd
                    break;
                case 13:
                    temp.Add(6); //1st
                    temp.Add(5); //2nd
                    temp.Add(4); //3rd
                    temp.Add(2); //4th
                    break;
                case 14:
                    temp.Add(6); //1st
                    temp.Add(5); //2nd
                    temp.Add(4); //3rd
                    temp.Add(3); //4th
                    break;
                case 15:                    
                case 16:                    
                case 17:
                    temp.Add(6); //1st
                    temp.Add(6); //2nd
                    temp.Add(5); //3rd
                    temp.Add(4); //4th
                    break;
                case 18:                   
                case 19:                    
                case 20:
                    temp.Add(6); //1st
                    temp.Add(6); //2nd
                    temp.Add(6); //3rd
                    temp.Add(5); //4th
                    break;
            }

            return temp;
        }

    }
}
