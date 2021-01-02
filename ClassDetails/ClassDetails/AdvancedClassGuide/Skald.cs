using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassFoundational;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace ClassDetails
{
    public class Skald : ClassFoundation 
    {
        public Skald()
        {
            Name = "Skald";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 4;
            CanCastSpells = true;
            HasSpellsPerDay = true;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.Good;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
            BABType = StatBlockInfo.BABType.Medium;
            ClassArchetypes = new List<string>() { "Totemic Skald", "Spell Warrior" };
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Martial;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Shield;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Medium;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.APPRAISE, StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.ESCAPE_ARTIST, 
                StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_ALL, StatBlockInfo.SkillNames.LINGUISTICS, StatBlockInfo.SkillNames.PERCEPTION, "Perform (oratory)", "Perform (percussion)", "Perform (sing)", 
                "Perform (string)", "Perform (wind)", StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SPELLCRAFT, 
                StatBlockInfo.SkillNames.SWIM, StatBlockInfo.SkillNames.USE_MAGIC_DEVICE };
        }

        public override string GetSpellBonusAbility()
        {
            return StatBlockInfo.CHA;
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            int count = 0;
            if (ClassLevel >= 1) count++; //Scribe Scroll

            return count;
        }


        public override List<int> GetSpellsPerLevel(int ClassLevel)
        {
            //Bonus Spells due to Cha handled in elsewhere
            List<int> Levels = new List<int>();
            switch (ClassLevel)
            {
                case 1:
                    Levels.Add(4); //0th
                    Levels.Add(2); //1st
                    break;
                case 2:
                    Levels.Add(5); //0th
                    Levels.Add(3); //1st
                    break;
                case 3:
                    Levels.Add(6); //0th
                    Levels.Add(4); //1st
                    break;
                case 4:
                    Levels.Add(6); //0th
                    Levels.Add(4); //1st
                    Levels.Add(2); //2nd
                    break;
                case 5:
                    Levels.Add(6); //0th
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    break;
                case 6:
                    Levels.Add(6); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    break;
                case 7:
                    Levels.Add(6); //0th
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(2); //3rd
                    break;
                case 8:
                    Levels.Add(6); //0th
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd
                    break;
                case 9:
                    Levels.Add(6); //0th
                    Levels.Add(5); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    break;
                case 10:
                    Levels.Add(6); //0th
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(2); //4th
                    break;
                case 11:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(3); //4th
                    break;
                case 12:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    break;
                case 13:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(2); //5th
                    break;
                case 14:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(3); //5th
                    break;
                case 15:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    break;
                case 16:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(4); //5th
                    Levels.Add(2); //6th
                    break;
                case 17:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(4); //5th
                    Levels.Add(3); //6th
                    break;
                case 18:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(4); //5th
                    Levels.Add(4); //6th
                    break;
                case 19:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(5); //5th
                    Levels.Add(4); //6th
                    break;
                case 20:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(6); //4th
                    Levels.Add(5); //5th
                    Levels.Add(5); //6th
                    break;
            }

            return Levels;
        }

        public override List<int> GetSpellsPerDay(int ClassLevel)
        {
            List<int> temp = new List<int>();
            temp.Add(0); // level 0 unlimited freq

            switch (ClassLevel)
            {
                case 1:
                    temp.Add(1); //1st
                    break;
                case 2:
                    temp.Add(2); //1st
                    break;
                case 3:
                    temp.Add(3); //1st
                    break;
                case 4:
                    temp.Add(3); //1st
                    temp.Add(1); //2nd
                    break;
                case 5:
                    temp.Add(4); //1st
                    temp.Add(2); //2nd
                    break;
                case 6:
                    temp.Add(4); //1st
                    temp.Add(3); //2nd
                    break;
                case 7:
                    temp.Add(4); //1st
                    temp.Add(3); //2nd
                    temp.Add(1); //3rd
                    break;
                case 8:
                    temp.Add(4); //1st
                    temp.Add(4); //2nd
                    temp.Add(2); //3rd
                    break;
                case 9:
                    temp.Add(5); //1st
                    temp.Add(4); //2nd
                    temp.Add(3); //3rd
                    break;
                case 10:
                    temp.Add(5); //1st
                    temp.Add(4); //2nd
                    temp.Add(3); //3rd
                    temp.Add(1); //4th
                    break;
                case 11:
                    temp.Add(5); //1st
                    temp.Add(4); //2nd
                    temp.Add(4); //3rd
                    temp.Add(2); //4th
                    break;
                case 12:
                    temp.Add(5); //1st
                    temp.Add(5); //2nd
                    temp.Add(4); //3rd
                    temp.Add(3); //4th
                    break;
                case 13:
                    temp.Add(5); //1st
                    temp.Add(5); //2nd
                    temp.Add(4); //3rd
                    temp.Add(3); //4th
                    temp.Add(1); //5th
                    break;
                case 14:
                    temp.Add(5); //1st
                    temp.Add(5); //2nd
                    temp.Add(4); //3rd
                    temp.Add(4); //4th
                    temp.Add(2); //5th
                    break;
                case 15:
                    temp.Add(5); //1st
                    temp.Add(5); //2nd
                    temp.Add(5); //3rd
                    temp.Add(4); //4th
                    temp.Add(3); //5th
                    break;
                case 16:
                    temp.Add(5); //1st
                    temp.Add(5); //2nd
                    temp.Add(5); //3rd
                    temp.Add(4); //4th
                    temp.Add(3); //5th
                    temp.Add(1); //6th
                    break;
                case 17:
                    temp.Add(5); //1st
                    temp.Add(5); //2nd
                    temp.Add(5); //3rd
                    temp.Add(4); //4th
                    temp.Add(4); //5th
                    temp.Add(2); //6th
                    break;
                case 18:
                    temp.Add(5); //1st
                    temp.Add(5); //2nd
                    temp.Add(5); //3rd
                    temp.Add(5); //4th
                    temp.Add(4); //5th
                    temp.Add(3); //6th
                    break;
                case 19:
                    temp.Add(5); //1st
                    temp.Add(5); //2nd
                    temp.Add(5); //3rd
                    temp.Add(5); //4th
                    temp.Add(5); //5th
                    temp.Add(4); //6th
                    break;
                case 20:
                    temp.Add(5); //1st
                    temp.Add(5); //2nd
                    temp.Add(5); //3rd
                    temp.Add(5); //4th
                    temp.Add(5); //5th
                    temp.Add(5); //6th
                    break;
            }

            return temp;
        }
    }
}
