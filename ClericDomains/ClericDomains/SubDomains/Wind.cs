using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Wind : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Wind()
        {
            Name = "Wind";
            Dieties = 
                
                
                  new List<string>();
            Description = "";
            DomainSpells = new Dictionary<string, int>()
            {
                {"whispering wind",1},
                {"wind wall", 2},
                {"gaseous form",3},
                {"air walk", 4},
                {"control winds",5},
                {"wind walk",6},
                {"elemental body IV [air]",7},
                {"whirlwind",8},
                {"winds of vengeance",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            //AddAbilities();
        }
    }
}
