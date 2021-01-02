using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassFoundational;
using CommonStatBlockInfo;
using PathfinderGlobals;


namespace ClassDetails
{
    public class Psychic : ClassFoundation 
    {
        public Psychic()
        {
            Name = "Psychic";
            ClassAlignments = CommonMethods.GetAlignments();
            CanCastSpells = true;
            SkillRanksPerLevel = 2;
            HitDiceType = StatBlockInfo.HitDiceCategories.d6;
            FortSaveType = StatBlockInfo.SaveBonusType.Poor;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
            BABType = StatBlockInfo.BABType.Medium;
            ClassArchetypes = new List<string>(){};
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
           // SetFeatures();
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_ALL, StatBlockInfo.SkillNames.LINGUISTICS, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.PROFESSION, 
                                       StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SPELLCRAFT };
        }

        public override string GetSpellBonusAbility()
        {
            return StatBlockInfo.INT;
        }

        public override List<int> GetSpellsPerLevel(int ClassLevel)
        {
            //Bonus Spells handled in elsewhere
            List<int> Levels = new List<int>();
            switch (ClassLevel)
            {
                case 1:
                    Levels.Add(4); //0th
                    Levels.Add(2); //1st
                    break;
                case 2:
                    Levels.Add(5); //0th
                    Levels.Add(2); //1st
                    break;
                case 3:
                    Levels.Add(5); //0th
                    Levels.Add(3); //1st
                    break;
                case 4:
                    Levels.Add(6); //0th
                    Levels.Add(3); //1st
                    Levels.Add(1); //2nd
                    break;
                case 5:
                    Levels.Add(6); //0th
                    Levels.Add(4); //1st
                    Levels.Add(2); //2nd
                    break;
                case 6:
                    Levels.Add(7); //0th
                    Levels.Add(4); //1st
                    Levels.Add(2); //2nd
                    Levels.Add(1); //3rd
                    break;
                case 7:
                    Levels.Add(7); //0th
                    Levels.Add(5); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(2); //3rd
                    break;
                case 8:
                    Levels.Add(8); //0th
                    Levels.Add(5); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(2); //3rd
                    Levels.Add(1); //4th
                    break;
                case 9:
                    Levels.Add(8); //0th
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd
                    Levels.Add(2); //4th
                    break;
                case 10:
                    Levels.Add(9); //0th
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd
                    Levels.Add(2); //4th
                    Levels.Add(1); //5th
                    break;
                case 11:
                    Levels.Add(9); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(3); //4th
                    Levels.Add(2); //5th
                    break;
                case 12:
                    Levels.Add(9); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(3); //4th
                    Levels.Add(2); //5th
                    Levels.Add(1); //6th
                    break;
                case 13:
                    Levels.Add(9); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(3); //5th
                    Levels.Add(2); //6th
                    break;
                case 14:
                    Levels.Add(9); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(3); //5th
                    Levels.Add(2); //6th
                    Levels.Add(1); //7th
                    break;
                case 15:
                    Levels.Add(9); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(3); //6th
                    Levels.Add(2); //7th
                    break;
                case 16:
                    Levels.Add(9); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(3); //6th
                    Levels.Add(2); //7th
                    Levels.Add(1); //8th
                    break;
                case 17:
                    Levels.Add(9); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(3); //6th
                    Levels.Add(3); //7th
                    Levels.Add(2); //8th
                    break;
                case 18:
                    Levels.Add(9); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(3); //6th
                    Levels.Add(3); //7th
                    Levels.Add(2); //8th
                    Levels.Add(1); //9th
                    break;
                case 19:
                    Levels.Add(9); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(3); //6th
                    Levels.Add(3); //7th
                    Levels.Add(3); //8th
                    Levels.Add(2); //9th
                    break;
                case 20:
                    Levels.Add(9); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(3); //6th
                    Levels.Add(3); //7th
                    Levels.Add(3); //8th
                    Levels.Add(3); //9th
                    break;
            }

            return Levels;
        }

        public override List<int> GetSpellsPerDay(int ClassLevel)
        {
            List<int> Levels = new List<int>();
            Levels.Add(0); //no 0 level spell per day limits

            switch (ClassLevel)
            {
                case 1:
                    Levels.Add(3); //1st
                    break;
                case 2:
                    Levels.Add(4); //1st
                    break;
                case 3:
                    Levels.Add(5); //1st
                    break;
                case 4:
                    Levels.Add(6); //1st
                    Levels.Add(3); //2nd
                    break;
                case 5:
                    Levels.Add(6); //1st
                    Levels.Add(4); //2nd
                    break;
                case 6:
                    Levels.Add(6); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(3); //3rd
                    break;
                case 7:
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(4); //3rd
                    break;
                case 8:
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(3); //4th
                    break;
                case 9:
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(4); //4th
                    break;
                case 10:
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(3); //5th
                    break;
                case 11:
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(6); //4th
                    Levels.Add(4); //5th
                    break;
                case 12:
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(6); //4th
                    Levels.Add(5); //5th
                    Levels.Add(3); //6th
                    break;
                case 13:
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(6); //4th
                    Levels.Add(6); //5th
                    Levels.Add(4); //6th
                    Levels.Add(3); //7th
                    break;
                case 14:
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(6); //4th
                    Levels.Add(6); //5th
                    Levels.Add(5); //6th
                    Levels.Add(3); //7th
                    break;
                case 15:
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(6); //4th
                    Levels.Add(6); //5th
                    Levels.Add(6); //6th
                    Levels.Add(4); //7th
                    break;
                case 16:
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(6); //4th
                    Levels.Add(6); //5th
                    Levels.Add(6); //6th
                    Levels.Add(5); //7th
                    Levels.Add(3); //8th
                    break;
                case 17:
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(6); //4th
                    Levels.Add(6); //5th
                    Levels.Add(6); //6th
                    Levels.Add(6); //7th
                    Levels.Add(4); //8th
                    break;
                case 18:
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(6); //4th
                    Levels.Add(6); //5th
                    Levels.Add(6); //6th
                    Levels.Add(6); //7th
                    Levels.Add(5); //8th
                    Levels.Add(3); //9th
                    break;
                case 19:
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(6); //4th
                    Levels.Add(6); //5th
                    Levels.Add(6); //6th
                    Levels.Add(6); //7th
                    Levels.Add(6); //8th
                    Levels.Add(4); //9th
                    break;
                case 20:
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(6); //4th
                    Levels.Add(6); //5th
                    Levels.Add(6); //6th
                    Levels.Add(6); //7th
                    Levels.Add(6); //8th
                    Levels.Add(6); //9th
                    break;
            }

            return Levels;
        }

    }
}
