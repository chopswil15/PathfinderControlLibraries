using OnGoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClericDomains
{
    public class Sahkil : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Sahkil()
        {
            Name = "Sahkil";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"protection from good",1},
                {"haunting mists", 2},
                {"magic circle against good",3},
                {"they know", 4},
                {"dispel good",5},
                {"phobia",6},
                {"blasphemy",7},
                {"unholy aura",8},
                {"summon monster IX [evil]",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
        }
        }
}
