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
    public class Alchemist : ClassFoundation 
    {
        public Alchemist()
        {
            Name = "Alchemist";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 4;
            CanCastSpells = true;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.Good;
            RefSaveType = StatBlockInfo.SaveBonusType.Good;
            WillSaveType = StatBlockInfo.SaveBonusType.Poor;
            BABType = StatBlockInfo.BABType.Medium;
            ClassArchetypes = new List<string>() { "Beastmorph", "Chirurgeon", "Clone Master", "Crypt Breaker", "Internal Alchemist", "Mindchemist",
                                                   "Oenopion Researcher", "Preservationist", "Psychonaut", "Ragechemist", "Reanimator", "Vivisectionist",
                                                   "Plague Bringer","Winged Marauder","Grenadier","Alchemical Trapper","Promethean Alchemist",
                                                   "Vaultbreaker","Horticulturist" };
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.APPRAISE, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DISABLE_DEVICE, StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.HEAL, StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA, StatBlockInfo.SkillNames.KNOWLEDGE_NATURE, StatBlockInfo.SkillNames.PERCEPTION, 
                                     StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.SLEIGHT_OF_HAND, StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.SURVIVAL, StatBlockInfo.SkillNames.USE_MAGIC_DEVICE };
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            //one at 1st, Throw Anything, Brew Potion
            return 2;
        }

        public override string GetSpellBonusAbility()
        {
            return StatBlockInfo.INT;
        }

        public override List<int> GetSpellsPerLevel(int ClassLevel)
        {
            //Bonus Spells handled in elsewhere
            List<int> Levels = new List<int>();
            Levels.Add(0); //0th
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
                    Levels.Add(5); //6th
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
