using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Repose : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Repose()
        {
            Name = "Repose";
            Dieties = new List<string>() { "Pharasma" };
            Description = "You see death not as something to be feared, but as a final rest and reward for a life well spent. The taint of undeath is a mockery of what you hold dear.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"deathwatch",1},
                {"gentle repose", 2},
                {"speak with dead",3},
                {"death ward", 4},
                {"slay living",5},
                {"undeath to death",6},
                {"destruction",7},
                {"waves of exhaustion",8},
                {"wail of the banshee",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }   
        
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("GentleRest", 0, "Gentle Rest",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("WardAgainstDeath", 0, "Ward Against Death",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
