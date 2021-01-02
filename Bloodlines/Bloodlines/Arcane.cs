using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using Skills;

namespace Bloodlines
{
    public class Arcane : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Arcane()
        {
            Name = "Arcane";
            Description = "Your family has always been skilled in the eldritch art of magic. While many of your relatives were accomplished wizards, your powers developed without the need for study and practice.";
            ClassSkillName = SkillData.SkillNames.KnowledgeAnyOne;
            BonusSpells = new Dictionary<string, int> { {"identify",3}, 
                                                        {"invisibility", 5},
                                                        {"dispel magic", 7},
                                                        {"dimension door", 9},
                                                        {"overland flight", 11},
                                                        {"true seeing", 13},
                                                        {"greater teleport", 15},
                                                        {"power word stun", 17},
                                                        {"wish", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }

        private void AddBloodlineBloodlineSpecialAbilities()
        {
            OnGoingSpecialAbility SA = new OnGoingSpecialAbility("ArcaneBond", 0, "Arcane Bond",
                 OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                 OnGoingSpecialAbility.SpecialAbilityActivities.Constant,  1);
            BloodlineSpecialAbilities.Add(SA);

            SA = new OnGoingSpecialAbility("MetamagicAdept", 0, "Metamagic Adept",
                 OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
                  OnGoingSpecialAbility.SpecialAbilityActivities.Constant,
                  3);
            BloodlineSpecialAbilities.Add(SA);

            SA = new OnGoingSpecialAbility("NewArcana", 0, "New Arcana",
                 OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
                  OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 9);
            BloodlineSpecialAbilities.Add(SA);

            SA = new OnGoingSpecialAbility("SchoolPower", 0, "School Power",
                 OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
                  OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 15);
            BloodlineSpecialAbilities.Add(SA);

            SA = new OnGoingSpecialAbility("ArcaneApotheosis", 0, "Arcane Apotheosis",
                 OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities,
                  OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 20);
            BloodlineSpecialAbilities.Add(SA);
        }
    }
}
