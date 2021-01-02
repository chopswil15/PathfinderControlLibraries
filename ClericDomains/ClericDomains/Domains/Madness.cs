using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Madness : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Madness()
        {
            Name = "Madness";
            Dieties = new List<string>() { "Lamashtu" };
            Description = "You embrace the madness that lurks deep in your heart, and can unleash it to drive your foes insane or to sacrifice certain abilities to hone others.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"lesser confusion",1},
                {"touch of idiocy", 2},
                {"rage",3},
                {"confusion", 4},
                {"nightmare",5},
                {"phantasmal killer",6},
                {"insanity",7},
                {"scintillating pattern",8},
                {"weird",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }   
 
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("VisionOfMadness", 0, "Vision of Madness",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("AuraofMadness", 0, "Aura of Madness",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
