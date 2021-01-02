using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Chaos : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Chaos()
        {
            Name = "Chaos";
            Dieties = new List<string>() { "Calistria", "Cayden Cailean", "Desna", "Gorum", "Lamashtu", "Rovagug" };
            Description = "Your touch infuses life and weapons with chaos, and you revel in all things anarchic.";
            DomainSpells = new Dictionary<string, int>()
            {
                {"protection from law",1},
                {"align weapon [chaos]", 2},
                {"magic circle against law",3},
                {"chaos hammer", 4},
                {"dispel law",5},
                {"animate objects",6},
                {"word of chaos",7},
                {"cloak of chaos",8},
                {"summon monster IX [chaos]",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }

        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("TouchOfChaos", 0, "Touch of Chaos", 
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("ChaosBlade", 0, "Chaos Blade",   
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
