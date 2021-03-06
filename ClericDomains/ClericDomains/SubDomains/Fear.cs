﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Fear : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Fear()
        {
            Name = "Fear";
            Dieties = new List<string>();
            Description = "";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"cause fear",1},
                {"align weapon [evil]", 2},
                {"scare",3},
                {"fear", 4},
                {"dispel good",5},
                {"create undead",6},
                {"blasphemy",7},
                {"unholy aura",8},
                {"summon monster IX [evil]",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            //AddAbilities();
        }  
    }
}
