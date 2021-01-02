using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Plant : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Plant()
        {
            Name = "Plant";
            Dieties = new List<string>() { "Erastil", "Gozreh" };
            Description = "You find solace in the green, can grow defensive thorns, and can communicate with plants.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"entangle",1},
                {"barkskin", 2},
                {"plant growth",3},
                {"command plants", 4},
                {"wall of thorns",5},
                {"repel wood",6},
                {"animate plants",7},
                {"control plants",8},
                {"shambler",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }    
 
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("WoodenFist", 0, "Wooden Fist",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("BrambleArmor", 0, "Bramble Armor",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 6));
        }
    }
}
