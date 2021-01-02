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
    public class Fighter : ClassFoundation 
    {
        public Fighter()
        {
            Name = "Fighter";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 2;
            HitDiceType = StatBlockInfo.HitDiceCategories.d10;
            FortSaveType = StatBlockInfo.SaveBonusType.Good;
            RefSaveType = StatBlockInfo.SaveBonusType.Poor;
            WillSaveType = StatBlockInfo.SaveBonusType.Poor;
            BABType = StatBlockInfo.BABType.Fast;
            ClassArchetypes = new List<string>() { "Aldori Swordlord","Archer", "Armor Master", "Brawler", "Cad", "Crossbowman", "Dawnflower Dervish",
                                                   "Dragoon", "Free Hand Fighter", "Gladiator", "Mobile Fighter", "Phalanx Soldier", "Polearm Master",
                                                   "Rondelero Duelist", "Roughrider", "Savage Warrior", "Shielded Fighter", "Tactician", "Thunderstriker",
                                                   "Tower Shield Specialist", "Two-Handed Fighter", "Two-Weapon Warrior", "Unarmed Fighter",
                                                   "Unbreakable", "Weapon Master", "Roughrider","Trench Fighter","Dirty Fighter","Airborne Ambusher"
                                                   ,"Seasoned Commander","Sensate","Mutation Warrior","Lore Warden"};
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Martial;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Shield;
            ShieldProficiencies |= StatBlockInfo.ShieldProficiencies.Tower;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Medium;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Heavy;
            SetFeatures();
        }

        public override List<string> ClassArchetypeSkills(string Archetype)
        {
            switch (Archetype)
            {
                case "Tactician":
                    List<string> skills = ClassSkills();
                    skills.AddRange(new List<string> {StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.KNOWLEDGE_GEOGRAPHY, StatBlockInfo.SkillNames.KNOWLEDGE_NOBILITY, StatBlockInfo.SkillNames.LINGUISTICS, StatBlockInfo.SkillNames.SENSE_MOTIVE});
                    return skills;
            }
            return ClassSkills();
        }

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_DUNGEONEERING, StatBlockInfo.SkillNames.KNOWLEDGE_ENGINEERING,
                                     StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SURVIVAL, StatBlockInfo.SkillNames.SWIM };
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            //one new feat on even levels plus one at 1st
            int count = (ClassLevel / 2) + 1;
            switch (Archetype)
            {
                case "Cad":
                    if (ClassLevel >= 3) count++; //Catch Off-Guard
                    break;
                case "Unbreakable":
                    count ++; //Tough as Nails, two feats replace one so only +1
                    if (ClassLevel >= 5) count++; //Heroic Recovery (Ex)
                    if (ClassLevel >= 9) count++; //Heroic Defiance (Ex)
                    break;
            }

            return count;  
        }

        private void SetFeatures()
        {           
            ClassFeature = new OnGoingSpecialAbility("Bravery", 0, "Bravery",
               OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
               OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 2);            

            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("ArmorTraining", 0, "Armor Training",
               OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
               OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 3);
            
            Features.Add(ClassFeature);


            ClassFeature = new OnGoingSpecialAbility("WeaponTraining", 0, "Weapon Training",
               OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
               OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 5);
            
            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("ArmorMastery", 0, "Armor Mastery",
               OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
               OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 19);
            
            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("WeaponMastery", 0, "Weapon Mastery",
               OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
               OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 20);
            
            Features.Add(ClassFeature);
        }
   
    }
}
