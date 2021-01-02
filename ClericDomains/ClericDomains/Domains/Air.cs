using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Air : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Air()
        {
            Name = "Air";
            Dieties = new List<string>() { "Gozreh", "Shelyn" };
            Description = "You can manipulate lightning, mist, and wind, traffic with air creatures, and are resistant to electricity damage.";
            DomainSpells = new Dictionary<string, int>()
            {
                {"obscuring mist",1},
                {"wind wall", 2},
                {"gaseous form",3},
                {"air walk", 4},
                {"control winds",5},
                {"chain lightning",6},
                {"elemental body IV [air]",7},
                {"whirlwind",8},
                {"elemental swarm [air]",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }

        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("LightningArc", 0, "Lightning Arc", 
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("ElectricityResistance", 0, "Electricity Resistance",   
                OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 6));
        }
    }
}
