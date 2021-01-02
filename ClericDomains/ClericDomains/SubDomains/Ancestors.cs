using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Ancestors : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Ancestors()
        {
            Name = "Ancestors";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"deathwatch",1},
                {"gentle repose", 2},
                {"speak with dead",3},
                {"death ward", 4},
                {"rest eternal",5},
                {"geas/quest",6},
                {"destruction",7},
                {"waves of exhaustion",8},
                {"wail of the banshee",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
           // AddAbilities();
        }   
    }
}
