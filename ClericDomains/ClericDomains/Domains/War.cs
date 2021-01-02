using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class War : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public War()
        {
            Name = "War";
            Dieties = new List<string>() { "Gorum", "Iomedae", "Rovagug", "Urgathoa" };
            Description = "You are a crusader for your god, always ready and willing to fight to defend your faith.";
            BonusFeats = new List<string>();
            DomainSpells = new Dictionary<string, int>()
            {
                {"magic weapon",1},
                {"spiritual weapon", 2},
                {"magic vestment",3},
                {"divine power", 4},
                {"flame strike",5},
                {"blade barrier",6},
                {"power word blind",7},
                {"power word stun",8},
                {"power word kill",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }   
        
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("BattleRage", 0, "Battle Rage",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("WeaponMaster", 0, "Weapon Master",
                OnGoingSpecialAbility.SpecialAbilityTypes.Su_SupernaturalAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
