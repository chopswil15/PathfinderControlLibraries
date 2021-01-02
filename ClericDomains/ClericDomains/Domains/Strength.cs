using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Strength : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Strength()
        {
            Name = "Strength";
            Dieties = new List<string>() { "Cayden Cailean", "Gorum", "Irori", "Lamashtu", "Urgathoa" };
            Description = "In strange and eldritch runes you find potent magic.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"enlarge person",1},
                {"bull's strength", 2},
                {"magic vestment",3},
                {"spell immunity", 4},
                {"righteous might",5},
                {"stoneskin",6},
                {"grasping hand",7},
                {"clenched fist",8},
                {"crushing hand",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }   
        
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("StrengthSurge", 0, "Strength Surge",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("MightOfTheGods", 0, "Might of the Gods",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
