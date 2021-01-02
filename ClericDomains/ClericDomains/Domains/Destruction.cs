using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Destruction : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Destruction()
        {
            Name = "Destruction";
            Dieties = new List<string>() { "Gorum", "Nethys", "Rovagug", "Zon-Kuthon" };
            Description = "You revel in ruin and devastation, and can deliver particularly destructive attacks.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"true strike",1},
                {"shatter", 2},
                {"rage",3},
                {"inflict critical wounds", 4},
                {"shout",5},
                {"harm",6},
                {"disintegrate",7},
                {"earthquake",8},
                {"implosion",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        } 
 
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("DestructiveSmite", 0, "Destructive Smite",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("DestructiveAura", 0, "Destructive Aura",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
