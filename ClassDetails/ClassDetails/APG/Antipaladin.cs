using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using CommonStatBlockInfo;
using ClassFoundational;

namespace ClassDetails
{
    public class Antipaladin : ClassFoundation 
    {
        public Antipaladin()
        {
            Name = "Antipaladin";
            ClassAlignments = new List<string>() { "CE" };
            SkillRanksPerLevel = 2;
            CanCastSpells = true;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.Good;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
            BABType = StatBlockInfo.BABType.Fast;
            ClassArchetypes = new List<string>() { "Knight of the Sepulcher","Fearmonger","Tyrant", "Seal-Breaker" };
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Martial;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Shield;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Medium;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Heavy;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DISGUISE, StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_RELIGION, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.RIDE,
                                      StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.STEALTH };
        }


        public override string GetSpellBonusAbility()
        {
            return StatBlockInfo.CHA;
        }

        public override string AlternateName()
        {
            return "Paladin";
        }

        public override List<int> GetSpellsPerLevel(int ClassLevel)
        {
            //Bonus Spells handled in elsewhere
            List<int> Levels = new List<int>();
            Levels.Add(0); //no 0 level spells

            switch (ClassLevel)
            {
                case 4:
                    Levels.Add(0); //1st Cha bonus only
                    break;
                case 5:
                case 6:
                    Levels.Add(1); //1st
                    break;
                case 7:
                    Levels.Add(1); //1st
                    Levels.Add(0); //2nd Wis bonus only
                    break;
                case 8:
                    Levels.Add(1); //1st
                    Levels.Add(1); //2nd
                    break;
                case 9:
                    Levels.Add(2); //1st
                    Levels.Add(1); //2nd
                    break;
                case 10:
                    Levels.Add(2); //1st
                    Levels.Add(1); //2nd
                    Levels.Add(0); //3rd Cha bonus only
                    break;
                case 11:
                    Levels.Add(2); //1st
                    Levels.Add(1); //2nd
                    Levels.Add(1); //3rd 
                    break;
                case 12:
                    Levels.Add(2); //1st
                    Levels.Add(2); //2nd
                    Levels.Add(1); //3rd 
                    break;
                case 13:
                    Levels.Add(3); //1st
                    Levels.Add(2); //2nd
                    Levels.Add(1); //3rd 
                    Levels.Add(0); //4th Cha bonus only
                    break;
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
                    Levels.Add(3); //2nd
                    Levels.Add(3); //3rd 
                    Levels.Add(3); //4th
                    break;
            }
            return Levels;
        }

    }
}
