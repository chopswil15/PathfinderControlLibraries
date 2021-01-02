using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Murder : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Murder()
        {
            Name = "Murder";
            Dieties = new List<string>() ;
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"cause fear",1},
                {"death knell", 2},
                {"keen edge",3},
                {"death ward", 4},
                {"suffocation",5},
                {"create undead",6},
                {"destruction",7},
                {"create greater undead",8},
                {"mass suffocation",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
           // AddAbilities();
        } 
    }
}
