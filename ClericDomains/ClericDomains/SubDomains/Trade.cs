using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Trade : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Trade()
        {
            Name = "Trade";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"floating disk",1},
                {"locate object", 2},
                {"fly",3},
                {"dimension door", 4},
                {"overland flight",5},
                {"find the path",6},
                {"greater teleport",7},
                {"phase door",8},
                {"gate",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
          //  AddAbilities();
        }   
    }
}
