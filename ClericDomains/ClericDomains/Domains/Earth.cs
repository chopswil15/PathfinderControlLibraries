using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Earth : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Earth()
        {
            Name = "Earth";
            Dieties = new List<string>() { "Abadar", "Torag" };
            Description = "You have mastery over earth, metal, and stone, can fire darts of acid, and command earth creatures.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"magic stone",1},
                {"soften earth and stone", 2},
                {"stone shape",3},
                {"spike stones", 4},
                {"wall of stone",5},
                {"stoneskin",6},
                {"elemental body IV [earth]",7},
                {"earthquake",8},
                {"elemental swarm [earth]",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        } 
 
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("AcidDart", 0, "Acid Dart",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("AcidResistance", 0, "Acid Resistance",
                OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
