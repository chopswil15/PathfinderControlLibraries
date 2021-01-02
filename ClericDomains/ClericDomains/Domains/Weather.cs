using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Weather : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Weather()
        {
            Name = "Weather";
            Dieties = new List<string>() { "Gozreh", "Rovagug" };
            Description = "With power over storm and sky, you can call down the wrath of the gods upon the world below.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"obscuring mist",1},
                {"fog cloud", 2},
                {"call lightning",3},
                {"sleet storm", 4},
                {"ice storm",5},
                {"control winds",6},
                {"control weather",7},
                {"whirlwind",8},
                {"storm of vengeance",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }
        
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("StormBurst", 0, "Storm Burst",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("LightningLord", 0, "Lightning Lord",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 6));
        }
    }
}
