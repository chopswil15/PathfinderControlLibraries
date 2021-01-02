using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Artifice : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Artifice()
        {
            Name = "Artifice";
            Dieties = new List<string>() { "Torag" };
            Description = "You can repair damage to objects, animate objects with life, and create objects from nothing.";
            DomainSpells = new Dictionary<string, int>()
            {
                {"animate rope",1},
                {"wood shape", 2},
                {"stone shape",3},
                {"minor creation", 4},
                {"fabricate",5},
                {"major creation",6},
                {"wall of iron",7},
                {"instant summons",8},
                {"prismatic sphere",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }

        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("ArtificersTouch", 0, "Artificer’s Touch", 
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("DancingWeapons", 0, "Dancing Weapons",   
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
