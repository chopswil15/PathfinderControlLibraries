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
    public class Rogue : ClassFoundation 
    {       
        private List<OnGoingSpecialAbility> RogueTalents { get; set; }
        private List<OnGoingSpecialAbility> AdvancedTalents { get; set; }

        public Rogue()
        {
            Name = "Rogue";
            ClassAlignments = CommonMethods.GetAlignments();
            SkillRanksPerLevel = 8;
            HitDiceType = StatBlockInfo.HitDiceCategories.d8;
            FortSaveType = StatBlockInfo.SaveBonusType.Poor;
            RefSaveType = StatBlockInfo.SaveBonusType.Good;
            WillSaveType = StatBlockInfo.SaveBonusType.Poor;
            BABType = StatBlockInfo.BABType.Medium;
            ClassArchetypes = new List<string>() {"Acrobat", "Bandit", "Burglar", "Chameleon", "Charlatan", "Cutpurse", "Driver", "Investigator",
                                                  "Knife Master", "Pirate", "Poisoner", "Rake", "Roof Runner", "Sanctified Rogue", "Scout","Sczarni Swindler",
                                                  "Sniper", "Spy", "Survivalist", "Swashbuckler", "Thug", "Trapsmith","Numerian Scavenger","Underground Chemist"
                                                  ,"Sharper","Smuggler","Filcher","Skulking Slayer","Deadly Courtesan","Heister","Snoop","Counterfeit Mage"};
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Simple;
            WeaponProficiencies |= StatBlockInfo.WeaponProficiencies.Extra;
            ArmorProficiencies |= StatBlockInfo.ArmorProficiencies.Light;
            SetFeatures();
        }

        public override List<string> GetWeaponProficienciesExtra()
        {
            return new List<string> { "hand crossbow", "rapier", "sap", "shortbow", "composite shortbow", "short sword" };
        }

        public override List<string> ClassArchetypeSkills(string Archetype)
        {
            List<string> temp = ClassSkills();
            switch (Archetype)
            {
                case "skulking slayer":
                    temp.Remove(StatBlockInfo.SkillNames.DISABLE_DEVICE);
                    temp.Remove(StatBlockInfo.SkillNames.LINGUISTICS);
                    temp.Remove(StatBlockInfo.SkillNames.SLEIGHT_OF_HAND);
                    return temp;
                default:
                    return temp;
            }
        }

        public override int ClassFeatCount(int ClassLevel, string Archetype)
        {
            int count = 0;

            switch (Archetype)
            {
                case "Sharper":
                    if (ClassLevel >= 2) count++; //Sticky Fingers (Ex)
                    if (ClassLevel >= 6) count++;
                    if (ClassLevel >= 8) count++;
                    break;
            }

            return count;
        }

        public override List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.APPRAISE, StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.CRAFT, StatBlockInfo.SkillNames.DIPLOMACY, StatBlockInfo.SkillNames.DISABLE_DEVICE, StatBlockInfo.SkillNames.DISGUISE, StatBlockInfo.SkillNames.ESCAPE_ARTIST,
                                     StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_DUNGEONEERING, StatBlockInfo.SkillNames.KNOWLEDGE_LOCAL, StatBlockInfo.SkillNames.LINGUISTICS, StatBlockInfo.SkillNames.PERCEPTION,
                                     StatBlockInfo.SkillNames.PERFORM, StatBlockInfo.SkillNames.PROFESSION, StatBlockInfo.SkillNames.SENSE_MOTIVE, StatBlockInfo.SkillNames.SLEIGHT_OF_HAND, StatBlockInfo.SkillNames.STEALTH, StatBlockInfo.SkillNames.SWIM, StatBlockInfo.SkillNames.USE_MAGIC_DEVICE };
        }

        private void SetFeatures()
        {
            ClassFeature = new OnGoingSpecialAbility("SneakAttack", 0, "Sneak Attack",
               OnGoingSpecialAbility.SpecialAbilityTypes.None,
               OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1);
            
            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("Trapfinding", 0, "Trapfinding",
               OnGoingSpecialAbility.SpecialAbilityTypes.None,
               OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1);
            
            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("Evasion", 0, "Evasion",
               OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
               OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 2);
            
            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("RogueTalents", 0, "Rogue Talents",
               OnGoingSpecialAbility.SpecialAbilityTypes.None,
               OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 2);
            
            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("TrapSense", 0, "Trap Sense",
               OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
               OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 3);
            
            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("UncannyDodge", 0, "Uncanny Dodge",
              OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
              OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 4);
            
            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("ImprovedUncannyDodge", 0, "Improved Uncanny Dodge",
              OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
              OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8);
            
            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("AdvancedTalents", 0, "Advanced Talents",
               OnGoingSpecialAbility.SpecialAbilityTypes.None,
               OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 10);
            
            Features.Add(ClassFeature);

            ClassFeature = new OnGoingSpecialAbility("MasterStrike", 0, "Master Strike",
               OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
               OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 20);
            
            Features.Add(ClassFeature);
        }


    }
}
