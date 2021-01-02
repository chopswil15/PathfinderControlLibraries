using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Ice : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Ice()
        {
            Name = "Ice";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"obscuring mist",1},
                {"fog cloud", 2},
                {"water breathing",3},
                {"control water", 4},
                {"ice storm",5},
                {"cone of cold",6},
                {"freezing sphere",7},
                {"horrid wilting",8},
                {"polar ray",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
           // AddAbilities();
        }  
    }
}
