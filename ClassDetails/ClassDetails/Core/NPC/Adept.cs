using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonStatBlockInfo;
using ClassFoundational;
using PathfinderGlobals;

namespace ClassDetails
{
    public class Adept : ClassFoundation
    {
        public Adept()
        {
            Name = "Adept";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 2;
            CanCastSpells = true;
            HitDiceType = StatBlockInfo.HitDiceCategories.d6;
            FortSaveType = StatBlockInfo.SaveBonusType.Poor;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
            BABType = StatBlockInfo.BABType.Slow;
            IsNPC_Class = true;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.None;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.None;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.HEAL, StatBlockInfo.SkillNames.KNOWLEDGE_ALL, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.SURVIVAL };
        }

        public override string GetSpellBonusAbility()
        {
            return StatBlockInfo.WIS;
        }

        public override List<int> GetSpellsPerLevel(int ClassLevel)
        {
            //Bonus Spells handled in elsewhere
            List<int> Levels = new List<int>();
            switch (ClassLevel)
            {
                case 1:
                case 2:
                    Levels.Add(3); //0th
                    Levels.Add(1); //1st
                    break;               
                case 3:
                    Levels.Add(3); //0th
                    Levels.Add(2); //1st
                    break;
                case 4:
                    Levels.Add(3); //0th
                    Levels.Add(2); //1st
                    Levels.Add(0); //2nd
                    break;
                case 5:
                case 6:
                    Levels.Add(3); //0th
                    Levels.Add(2); //1st
                    Levels.Add(1); //2nd
                    break;
                case 7:
                    Levels.Add(3); //0th
                    Levels.Add(3); //1st
                    Levels.Add(2); //2nd
                    break;
                case 8:
                    Levels.Add(3); //0th
                    Levels.Add(3); //1st
                    Levels.Add(2); //2nd
                    Levels.Add(0); //3rd
                    break;
                case 9:
                case 10:
                    Levels.Add(3); //0th
                    Levels.Add(3); //1st
                    Levels.Add(2); //2nd
                    Levels.Add(1); //3rd
                    break;
                case 11:
                    Levels.Add(3); //0th
                    Levels.Add(3); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(2); //3rd
                    break;
                case 12:
                    Levels.Add(3); //0th
                    Levels.Add(3); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(2); //3rd
                    Levels.Add(0); //4th
                    break;
                case 13:
                case 14:
                    Levels.Add(3); //0th
                    Levels.Add(3); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(2); //3rd
                    Levels.Add(1); //4th
                    break;
                case 15:
                    Levels.Add(3); //0th
                    Levels.Add(3); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(3); //3rd
                    Levels.Add(2); //4th
                    break;
                case 16:
                    Levels.Add(3); //0th
                    Levels.Add(3); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(3); //3rd
                    Levels.Add(2); //4th
                    Levels.Add(0); //5th
                    break;
                case 17:
                case 18:
                    Levels.Add(3); //0th
                    Levels.Add(3); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(3); //3rd
                    Levels.Add(2); //4th
                    Levels.Add(1); //5th
                    break;
                case 19:
                case 20:
                    Levels.Add(3); //0th
                    Levels.Add(3); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(3); //3rd
                    Levels.Add(3); //4th
                    Levels.Add(2); //5th
                    break;
            }

            return Levels;
        }

        public override List<CheckClassError> CheckClass(int classLevel)
        {
            return new List<CheckClassError>();
        }
    }
}
