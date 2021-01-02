using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Tactics : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Tactics()
        {
            Name = "Tactics";
            Dieties = new List<string>();
            Description = "";
            DomainSpells = new Dictionary<string, int>()
            {
                {"magic weapon",1},
                {"aid", 2},
                {"magic vestment",3},
                {"divine power", 4},
                {"greater command",5},
                {"blade barrier",6},
                {"power word blind",7},
                {"greater planar ally",8},
                {"power word kill",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
           // AddAbilities();
        }
    }
}
