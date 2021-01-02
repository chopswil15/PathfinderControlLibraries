﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassFoundational;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace ClassDetails
{
    public class Mesmerist : ClassFoundation
    {
        public Mesmerist()
        {
            Name = "Mesmerist";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 6;
            CanCastSpells = true;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.Poor;
            RefSaveType = StatBlockInfo.SaveBonusType.Good;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
            BABType = StatBlockInfo.BABType.Medium;
            ClassArchetypes = new List<string>{"Vizier","Cult Master", "Spirit Walker" };
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Extra;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
           // SetFeatures();
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.APPRAISE, StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.DISGUISE, StatBlockInfo.SkillNames.ESCAPE_ARTIST, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA, 
                  StatBlockInfo.SkillNames.KNOWLEDGE_DUNGEONEERING, StatBlockInfo.SkillNames.KNOWLEDGE_HISTORY, StatBlockInfo.SkillNames.KNOWLEDGE_LOCAL, StatBlockInfo.SkillNames.KNOWLEDGE_NOBILITY, StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION, 
                  StatBlockInfo.SkillNames.LINGUISTICS, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.PERFORM, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SLEIGHT_OF_HAND, StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.STEALTH, StatBlockInfo.SkillNames.USE_MAGIC_DEVICE };
        }

        public override List<string> GetWeaponProficienciesExtra()
        {
            return new List<string> { "hand crossbow", "sap", "sword cane", "whip" };
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
                    Levels.Add(3); //4th
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
                    Levels.Add(5); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(4); //5th
                    Levels.Add(3); //6th
                    break;
                case 18:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(4); //5th
                    Levels.Add(4); //6th
                    break;
                case 19:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(5); //5th
                    Levels.Add(4); //6th
                    break;
                case 20:
                    Levels.Add(6); //0th
                    Levels.Add(6); //1st
                    Levels.Add(6); //2nd
                    Levels.Add(6); //3rd
                    Levels.Add(5); //4th
                    Levels.Add(5); //5th
                    Levels.Add(5); //6th
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
                    Levels.Add(2); //3rd
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
                    Levels.Add(5); //4th
                    Levels.Add(3); //5th
                    Levels.Add(1); //6th
                    break;
                case 17:
                    Levels.Add(5); //1st
                    Levels.Add(5); //2nd
                    Levels.Add(5); //3rd
                    Levels.Add(5); //4th
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
    }
}
