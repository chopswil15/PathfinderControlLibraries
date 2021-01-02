using OnGoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClericDomains
{
    public class Wards : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Wards()
        {
            Name = "Wards";
            Dieties = new List<string>();
            Description = "";
            DomainSpells = new Dictionary<string, int>()
            {
                {"arcane lock",1},
                {"stone call", 2},
                {"meld into stone",3},
                {"dimensional anchor", 4},
                {"commune with nature",5},
                {"guards and wards",6},
                {"statue",7},
                {"earthquake",8},
                {"clashing rocks",9}
            };
        }
    }
}
