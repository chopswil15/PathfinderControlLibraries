using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Sun : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Sun()
        {
            Name = "Sun";
            Dieties = new List<string>() { "Iomedae", "Sarenrae" };
            Description = "You see truth in the pure and burning light of the sun, and can call upon its blessing or wrath to work great deeds.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"endure elements",1},
                {"heat metal", 2},
                {"searing light",3},
                {"fire shield", 4},
                {"flame strike",5},
                {"fire seeds",6},
                {"sunbeam",7},
                {"sunburst",8},
                {"prismatic sphere",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }   
        
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("SunsBlessing", 0, "Sun’s Blessing",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("NimbusOfLight", 0, "Nimbus of Light",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
