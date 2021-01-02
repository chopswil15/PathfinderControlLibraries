using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Good : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Good()
        {
            Name = "Good";
            Dieties = new List<string>() { "Cayden Cailean", "Desna", "Erastil", "Iomedae", "Sarenrae","Shelyn", "Torag" };
            Description = "You have pledged your life and soul to goodness and purity.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"protection from evil",1},
                {"align weapon [good]", 2},
                {"magic circle against evil",3},
                {"holy smite", 4},
                {"dispel evil",5},
                {"blade barrier",6},
                {"holy word",7},
                {"holy aura",8},
                {"summon monster IX [good]",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }    
 
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("TouchOfGood", 0, "Touch of Good",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("HolyLance", 0, "Holy Lance",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
