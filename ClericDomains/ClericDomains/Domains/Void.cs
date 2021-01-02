using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Void : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Void()
        {
            Name = "Void";
            Dieties = new List<string>();
            Description = "You can call upon the cold darkness between the stars to gain flight, travel to other worlds, or summon monsters from beyond to do your bidding.";
            DomainSpells = new Dictionary<string, int>()
            {
                {"feather fall",1},
                {"levitat", 2},
                {"fly",3},
                {"lesser planar binding", 4},
                {"overland flight",5},
                {"planar binding",6},
                {"reverse gravity",7},
                {"greater planar binding",8},
                {"interplanetary teleport",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
           // AddAbilities();
        }
    }
}
