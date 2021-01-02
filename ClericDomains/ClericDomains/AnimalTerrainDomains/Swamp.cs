using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Swamp : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Swamp()
        {
            Name = "Swamp";
            Dieties = new List<string>();
            Description = "";
            DomainSpells = new Dictionary<string, int>()
            {
                {"hydraulic push",1},
                {"burst of nettles", 2},
                {"lily pad stride",3},
                {"cape of wasps", 4},
                {"insect plague",5},
                {"mass fester",6},
                {"animate plants",7},
                {"blood mist",8},
                {"shambler",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
           // AddAbilities();
        }
    }
}
