using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Blood : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Blood()
        {
            Name = "Blood";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"magic weapon",1},
                {"spiritual weapon", 2},
                {"vampiric touch",3},
                {"divine power", 4},
                {"wall of thorns",5},
                {"blade barrier",6},
                {"inflict serious wounds, mass",7},
                {"power word stun",8},
                {"power word kill",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
           // AddAbilities();
        }   
    }
}
