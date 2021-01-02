using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Glory : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Glory()
        {
            Name = "Glory";
            Dieties = new List<string>() { "Gorum", "Iomedae", "Sarenrae" };
            Description = "You are infused with the glory of the divine, and are a true foe of the undead.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"shield of faith",1},
                {"bless weapon", 2},
                {"searing light",3},
                {"holy smite", 4},
                {"righteous might",5},
                {"undeath to death",6},
                {"holy sword",7},
                {"holy aura",8},
                {"gate",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }   
 
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("TouchOfGlory", 0, "Touch of Glory",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("DivinePresence", 0, "Divine Presence",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
