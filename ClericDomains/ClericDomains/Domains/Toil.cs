using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Toil : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Toil()
        {
            Name = "Toil";
            Dieties = new List<string>();
            Description = "";
            DomainSpells = new Dictionary<string, int>()
            {
                {"command",1},
                {"wood shape", 2},
                {"stone shape",3},
                {"minor creation", 4},
                {"waves of fatigue",5},
                {"major creation",6},
                {"wall of iron",7},
                {"waves of exhaustion",8},
                {"prismatic sphere",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
          //  AddAbilities();
        }
    }
}
