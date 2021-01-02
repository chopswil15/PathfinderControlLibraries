using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Loss : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Loss()
        {
            Name = "Loss";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>() { "Blind-Fight" };
            DomainSpells = new Dictionary<string, int>()
            {
                {"obscuring mist",1},
                {"blindness/deafness", 2},
                {"deeper darkness",3},
                {"shadow conjuration", 4},
                {"enervation",5},
                {"modify memory",6},
                {"power word blind",7},
                {"greater shadow evocation",8},
                {"energy drain",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            //AddAbilities();
        } 
    }
}
