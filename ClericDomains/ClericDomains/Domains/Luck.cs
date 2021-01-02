using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Luck : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Luck()
        {
            Name = "Luck";
            Dieties = new List<string>() { "Calistria", "Desna", "Shelyn" };
            Description = "You are infused with luck, and your mere presence can spread good fortune.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"true strike",1},
                {"aid", 2},
                {"protection from energy",3},
                {"freedom of movement", 4},
                {"break enchantment",5},
                {"mislead",6},
                {"spell turning",7},
                {"moment of prescience",8},
                {"miracle",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }   
 
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("BitOfLuck", 0, "Bit of Luck",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("GoodFortune", 0, "Good Fortune",
                OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 6));
        }
    }
}
