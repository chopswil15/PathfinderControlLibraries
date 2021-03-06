﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Devil : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Devil()
        {
            Name = "Devil";
            Dieties = new List<string>() ;
            Description = "";
            DomainSpells = new Dictionary<string, int>()
            {
                {"command",1},
                {"align weapon [evil]", 2},
                {"suggestion",3},
                {"unholy blight", 4},
                {"dispel good",5},
                {"planar binding",6},
                {"blasphemy",7},
                {"unholy aura",8},
                {"summon monster IX [evil]",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
           // AddAbilities();
        }

    }
}
