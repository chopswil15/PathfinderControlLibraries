using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassFoundational;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace ClassDetails
{
    public class Medium : ClassFoundation 
    {
        public Medium()
        {
            Name = "Medium";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 4;
            CanCastSpells = true;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.Poor;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
            BABType = StatBlockInfo.BABType.Medium;
            ClassArchetypes = new List<string> { "Relic Channeler", "Voice Of The Void" };
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Medium;
           // SetFeatures();
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.HEAL, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA, StatBlockInfo.SkillNames.KNOWLEDGE_PLANES, 
                StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION, StatBlockInfo.SkillNames.LINGUISTICS, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.PERFORM, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.USE_MAGIC_DEVICE };
        }

        public override string GetSpellBonusAbility()
        {
            return StatBlockInfo.CHA;
        }

        public override List<int> GetSpellsPerLevel(int ClassLevel)
        {
            //Bonus Spells handled in elsewhere
            List<int> Levels = new List<int>();
            switch (ClassLevel)
            {
                case 1:
                    Levels.Add(2); //0th
                    break;
                case 2:
                    Levels.Add(3); //0th
                    break;
                case 3:
                    Levels.Add(4); //0th
                    break;
                case 4:
                    Levels.Add(4); //0th
                    Levels.Add(2); //1st
                    break;
                case 5:
                    Levels.Add(5); //0th
                    Levels.Add(3); //1st
                    break;
                case 6:
                    Levels.Add(5); //0th
                    Levels.Add(4); //1st
                    break;
                case 7:
                    Levels.Add(6); //0th
                    Levels.Add(4); //1st
                    Levels.Add(2); //2nd
                    break;
                case 8:
                    Levels.Add(6); //0th
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    break;
                case 9:
                    Levels.Add(6); //0th
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    break;
                case 10:
                    Levels.Add(6); //0th
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(2); //3rd
                    break;
                case 11:
                    Levels.Add(6); //0th
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd
                    break;
                case 12:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    break;
                case 13:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(2); //4th
                    break;
                case 14:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(3); //4th
                    break;
                case 15:
                case 16:
                case 17:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(4); //4th
                    break;               
                case 18:
                case 19:
                case 20:
                    Levels.Add(6); //0th
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
            List<int> Levels = new List<int>();
            Levels.Add(0); //no 0 level spell per day limits

            switch (ClassLevel)
            {
                case 1:                   
                case 2:                    
                case 3:
                    break;  //nothing
                case 4:                   
                case 5:                   
                case 6:
                    Levels.Add(1); //1st
                    break;
                case 7:
                case 8:
                    Levels.Add(1); //1st
                    Levels.Add(1); //2nd  
                    break;
                case 9:
                    Levels.Add(2); //1st
                    Levels.Add(1); //2nd
                    break;
                case 10:                   
                case 11:
                case 12:
                    Levels.Add(2); //1st
                    Levels.Add(1); //2nd
                    Levels.Add(1); //3rd
                    break;              
                case 13:
                case 14:
                    Levels.Add(3); //1st
                    Levels.Add(2); //2nd
                    Levels.Add(1); //3rd
                    Levels.Add(1); //4th                   
                    break;               
                case 15:
                    Levels.Add(3); //1st
                    Levels.Add(2); //2nd
                    Levels.Add(2); //3rd
                    Levels.Add(1); //4th 
                    break;
                case 16:
                    Levels.Add(3); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(2); //3rd
                    Levels.Add(1); //4th 
                    break;
                case 17:
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(2); //3rd
                    Levels.Add(1); //4th
                    break;
                case 18:
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(2); //3rd
                    Levels.Add(2); //4th
                    break;
                case 19:
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(3); //3rd
                    Levels.Add(2); //4th
                    break;
                case 20:
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd
                    Levels.Add(2); //4th
                    break;
            }

            return Levels;
        }
    }
}
