using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Law : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Law()
        {
            Name = "Law";
            Dieties = new List<string>() { "Abadar", "Asmodeus", "Erastil", "Iomedae", "Irori", "Torag", "Zon-Kuthon" };
            Description = "You follow a strict and ordered code of laws, and in so doing, achieve enlightenment.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"protection from chaos",1},
                {"align weapon [law]", 2},
                {"magic circle against chaos",3},
                {"order’s wrath", 4},
                {"dispel chaos",5},
                {"hold monster",6},
                {"dictum",7},
                {"shield of law",8},
                {"summon monster IX [law]",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }     
 
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("TouchOfLaw", 0, "Touch of Law",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("StaffOfOrder", 0, "Staff of Order",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
