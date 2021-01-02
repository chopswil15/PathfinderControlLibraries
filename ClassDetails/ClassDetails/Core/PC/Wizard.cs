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
    public class Wizard : ClassFoundation 
    {  
        public Wizard()
        {
            Name = "Wizard";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 2;
            CanCastSpells = true;
            HitDiceType = StatBlockInfo.HitDiceCategories.d6;
            FortSaveType = StatBlockInfo.SaveBonusType.Poor;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Good;
            BABType = StatBlockInfo.BABType.Slow;
            ClassArchetypes = new List<string>() { "Arcane Bomber", "Arcane Discoveries", "Primalist", "Scrollmaster", "Shadowcaster",
                                                   "Siege Mage", "Spellslinger","Spell Sage" };
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Extra;
            SetFeatures();
        }

        public override List<string> GetWeaponProficienciesExtra()
        {
            return new List<string> { "club", "dagger", "heavy crossbow", "light crossbow", "quarterstaff" };
        }

        public override List<string> ClassArchetypeSkills(string Archetype)
        {
            return ClassSkills();
        }

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.APPRAISE, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.KNOWLEDGE_ALL, StatBlockInfo.SkillNames.LINGUISTICS, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.SPELLCRAFT };
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
                    Levels.Add(3); //0th
                    Levels.Add(1); //1st
                    break;
                case 2:
                    Levels.Add(4); //0th
                    Levels.Add(2); //1st
                    break;
                case 3:
                    Levels.Add(4); //0th
                    Levels.Add(2); //1st
                    Levels.Add(1); //2nd
                    break;
                case 4:
                    Levels.Add(4); //0th
                    Levels.Add(3); //1st
                    Levels.Add(2); //2nd
                    break;
                case 5:
                    Levels.Add(4); //0th
                    Levels.Add(3); //1st
                    Levels.Add(2); //2nd
                    Levels.Add(1); //3rd
                    break;
                case 6:
                    Levels.Add(4); //0th
                    Levels.Add(3); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(2); //3rd
                    break;
                case 7:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(2); //3rd
                    Levels.Add(1); //4th
                    break;
                case 8:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(3); //2nd
                    Levels.Add(3); //3rd
                    Levels.Add(2); //4th
                    break;
                case 9:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd
                    Levels.Add(2); //4th
                    Levels.Add(1); //5th
                    break;
                case 10:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(3); //3rd
                    Levels.Add(3); //4th
                    Levels.Add(2); //5th
                    break;
                case 11:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(3); //4th
                    Levels.Add(2); //5th
                    Levels.Add(1); //6th
                    break;
                case 12:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(3); //4th
                    Levels.Add(3); //5th
                    Levels.Add(2); //6th
                    break;
                case 13:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(3); //5th
                    Levels.Add(2); //6th
                    Levels.Add(1); //7th
                    break;
                case 14:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(3); //5th
                    Levels.Add(3); //6th
                    Levels.Add(2); //7th
                    break;
                case 15:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(3); //6th
                    Levels.Add(2); //7th
                    Levels.Add(1); //8th
                    break;
                case 16:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(3); //6th
                    Levels.Add(3); //7th
                    Levels.Add(2); //8th
                    break;
                case 17:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(4); //6th
                    Levels.Add(3); //7th
                    Levels.Add(2); //8th
                    Levels.Add(1); //9th
                    break;
                case 18:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(4); //6th
                    Levels.Add(3); //7th
                    Levels.Add(3); //8th
                    Levels.Add(2); //9th
                    break;
                case 19:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(4); //6th
                    Levels.Add(4); //7th
                    Levels.Add(3); //8th
                    Levels.Add(3); //9th
                    break;
                case 20:
                    Levels.Add(4); //0th
                    Levels.Add(4); //1st
                    Levels.Add(4); //2nd
                    Levels.Add(4); //3rd
                    Levels.Add(4); //4th
                    Levels.Add(4); //5th
                    Levels.Add(4); //6th
                    Levels.Add(4); //7th
                    Levels.Add(4); //8th
                    Levels.Add(4); //9th
                    break;
            }
            return Levels;
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            //one bonus feat at 1st
            int Count = 1;

            //bonus bloodline feats
            if (ClassLevel >= 5) Count++;
            if (ClassLevel >= 10) Count++;
            if (ClassLevel >= 15) Count++;
            if (ClassLevel >= 20) Count++;
            return Count;
        }

        private void SetFeatures()
        {
            ClassFeature = new OnGoingSpecialAbility("ArcaneBond", 0, "Arcane Bond",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, OnGoingSpecialAbility.SpecialAbilityActivities.Constant,
                1);
            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("Cantrips", 0, "Cantrips",
              OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities,
              OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 1);

            Features.Add(ClassFeature);
        }
    }
}
