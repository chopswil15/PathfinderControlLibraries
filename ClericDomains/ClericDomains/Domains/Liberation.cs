using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Liberation : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Liberation()
        {
            Name = "Liberation";
            Dieties = new List<string>() { "Desna" };
            Description = "You are a spirit of freedom and a staunch foe against all who would enslave and oppress.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"remove fear",1},
                {"remove paralysis", 2},
                {"remove curse",3},
                {"freedom of movement", 4},
                {"break enchantment",5},
                {"greater dispel magic",6},
                {"refuge",7},
                {"mind blank",8},
                {"freedom",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }  
 
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("Liberation", 0, "Liberation",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("FreedomsCall", 0, "Freedom’s Call",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
