using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Heroism : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Heroism()
        {
            Name = "Heroism";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"shield of faith",1},
                {"bless weapon", 2},
                {"heroism",3},
                {"holy smite", 4},
                {"righteous might",5},
                {"heroism, greater",6},
                {"holy sword",7},
                {"holy aura",8},
                {"gate",9}
            };
          //  GrantedAbilities = new List<OnGoingSpecialAbility>();
          //  AddAbilities();
        }   
    }
}
