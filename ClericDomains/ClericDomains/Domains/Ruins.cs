using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Ruins  : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Ruins()
        {
            Name = "Ruins";
            Dieties = new List<string>();
            Description = "You sense nature’s creeping reclamation of what once belonged to the civilized world and understand how to ensure ruins that hold power or significance will persist.";
            DomainSpells = new Dictionary<string, int>()
            {
                {"magic stone",1},
                {"stone call", 2},
                {"meld into stone",3},
                {"rusting grasp", 4},
                {"commune with nature",5},
                {"stone tell",6},
                {"statue",7},
                {"earthquake",8},
                {"clashing rocks",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
           // AddAbilities();
        }    
    }
}
