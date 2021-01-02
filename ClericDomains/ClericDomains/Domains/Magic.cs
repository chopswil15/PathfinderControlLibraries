using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Magic : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Magic()
        {
            Name = "Magic";
            Dieties = new List<string>() { "Asmodeus", "Nethys", "Urgathoa" };
            Description = "You are a true student of all things mystical, and see divinity in the purity of magic.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"identify",1},
                {"magic mouth", 2},
                {"dispel magic",3},
                {"imbue with spell ability", 4},
                {"spell resistance",5},
                {"antimagic field",6},
                {"spell turning",7},
                {"protection from spells",8},
                {"mage’s disjunction",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }  
 
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("HandOfTheAcolyte", 0, "Hand of the Acolyte",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("DispellingTouch", 0, "Dispelling Touch",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
