using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Fur: IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Fur()
        {
            Name = "Fur";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"magic fang",1},
                {"hold animal", 2},
                {"beast shape I [animals]",3},
                {"summon nature’s ally IV [animals]", 4},
                {"beast shape III [animals]",5},
                {"antilife shell",6},
                {"animal shapes",7},
                {"summon nature’s ally VIII [animals]",8},
                {"shapechange",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
        }   
    }
}
