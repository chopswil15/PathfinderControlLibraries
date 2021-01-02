using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Entropy : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Entropy()
        {
            Name = "Entropy";
            Dieties = new List<string>();
            Description = "";
            DomainSpells = new Dictionary<string, int>()
            {
                {"entropic shield",1},
                {"align weapon [chaos]", 2},
                {"dispel magic",3},
                {"chaos hammer", 4},
                {"confusion",5},
                {"animate objects",6},
                {"destruction",7},
                {"cloak of chaos",8},
                {"summon monster IX [chaos]",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
         //   AddAbilities();
        }
    }
}
