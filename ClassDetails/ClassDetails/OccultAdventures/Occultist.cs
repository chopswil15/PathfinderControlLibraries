using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassFoundational;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace ClassDetails
{
    public class Occultist : ClassFoundation
    {
        public Occultist()
        {
            Name = "Occultist";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 4;
            CanCastSpells = true;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.Good;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
            BABType = StatBlockInfo.BABType.Medium;
            ClassArchetypes = new List<string> { "Tome Eater" } ;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Martial;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Medium;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Shield;
           // SetFeatures();
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.APPRAISE, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.DISABLE_DEVICE, StatBlockInfo.SkillNames.DISGUISE, StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA,
                StatBlockInfo.SkillNames.KNOWLEDGE_ENGINEERING, StatBlockInfo.SkillNames.KNOWLEDGE_HISTORY, StatBlockInfo.SkillNames.KNOWLEDGE_PLANES, StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION, StatBlockInfo.SkillNames.LINGUISTICS, 
                StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SLEIGHT_OF_HAND, StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.USE_MAGIC_DEVICE };
        }

        public override string GetSpellBonusAbility()
        {
            return StatBlockInfo.INT;
        }

        private int GetImplementsCount(int ClassLevel)
        {
            int count = 2;
            if (ClassLevel >= 2) count++;
            if (ClassLevel >= 6) count++;
            if (ClassLevel >= 10) count++;
            if (ClassLevel >= 14) count++;
            if (ClassLevel >= 18) count++;

            return count;
        }

        public override List<int> GetSpellsPerDay(int ClassLevel)
        {
            //Bonus Spells handled in elsewhere
            List<int> Levels = new List<int>();
            Levels.Add(0); //no 0 level spells

            switch (ClassLevel)
            {
                case 1:                    
                    Levels.Add(1); //1st
                    break;
                case 2:
                    Levels.Add(2); //1st
                    break;
                case 3:
                    Levels.Add(3); //1st
                    break;
                case 4:
                    Levels.Add(3); //1st
                    Levels.Add(1); //2nd
                    break;
                case 5:
                    Levels.Add(4); //1st
                    Levels.Add(2); //2nd
                    break;
                case 6:
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    break;
                case 7:
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(1); //3rd
                    break;
                case 8:
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(2); //3rd
                    break;
                case 9:
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd
                    break;
                case 10:
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd
                    Levels.Add(1); //4th
                    break;
                case 11:
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(2); //4th
                    break;
                case 12:
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(3); //4th
                    break;
                case 13:
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(3); //4th
                    Levels.Add(1); //5th
                    break;
                case 14:
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(2); //5th
                    break;
                case 15:
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(3); //5th
                    break;
                case 16:
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(3); //5th
                    Levels.Add(1); //6th
                    break;
                case 17:
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(2); //6th
                    break;
                case 18:
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(4); //5th
                    Levels.Add(3); //6th
                    break;
                case 19:
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(5); //5th
                    Levels.Add(4); //6th
                    break;
                case 20:
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(5); //5th
                    Levels.Add(5); //6th
                    break;
            }
            return Levels;
        }

        public override List<int> GetSpellsPerLevel(int ClassLevel)
        {
            int ImplementsCount = GetImplementsCount(ClassLevel);
            List<int> SpellsPerDay = GetSpellsPerDay(ClassLevel);
            //Bonus Spells handled in elsewhere
            List<int> Levels = new List<int>();

            for (int level = 0; SpellsPerDay.Count -1 <level;level++ )
            {
                Levels.Add(ImplementsCount);
            }
           

            return Levels;
        }
    }
}
