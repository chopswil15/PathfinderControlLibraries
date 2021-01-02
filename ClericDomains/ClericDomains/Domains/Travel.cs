using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Travel : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Travel()
        {
            Name = "Travel";
            Dieties = new List<string>() { "Abadar", "Cayden Cailean", "Desna" };
            Description = "You are an explorer and find enlightenment in the simple joy of travel, be it by foot or conveyance or magic.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"longstrider",1},
                {"locate object", 2},
                {"fly",3},
                {"dimension door", 4},
                {"teleport",5},
                {"find the path",6},
                {"greater teleport",7},
                {"phase door",8},
                {"astral projection",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }   
        
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("AgileFeet", 0, "Agile Feet",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("DimensionalHop", 0, "Dimensional Hop",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
