using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Protection : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Protection()
        {
            Name = "Protection";
            Dieties = new List<string>() { "Erastil", "Gozreh" };
            Description = "Your faith is your greatest source of protection, and you can use that faith to defend others.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"sanctuary",1},
                {"shield other", 2},
                {"protection from energy",3},
                {"spell immunity", 4},
                {"spell resistance",5},
                {"antimagic field",6},
                {"repulsion",7},
                {"mind blank",8},
                {"prismatic sphere",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }   
         
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("ResistantTouch", 0, "Resistant Touch",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("AuraOfProtection", 0, "Aura of Protection",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
