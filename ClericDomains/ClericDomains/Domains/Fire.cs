using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Fire : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Fire()
        {
            Name = "Fire";
            Dieties = new List<string>() { "Asmodeus", "Sarenrae" };
            Description = "You can call forth fire, command creatures of the inferno, and your flesh does not burn.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"burning hands",1},
                {"produce flame", 2},
                {"fireball",3},
                {"wall of fire", 4},
                {"fire shield",5},
                {"fire seeds",6},
                {"elemental body IV [fire]",7},
                {"incendiary cloud",8},
                {"elemental swarm [fire]",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }   
 
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("FireBolt", 0, "Fire Bolt",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("FireResistance", 0, "Fire Resistance",
                OnGoingSpecialAbility.SpecialAbilityTypes.Ex_ExtraordinaryAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
