using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;

namespace ClericDomains
{
    public class Rune : IDomain
    {
        public string Name { get; set; }
        public List<string> Dieties { get; set; }
        public string Description { get; set; }
        public List<string> BonusFeats { get; set; }
        public Dictionary<string, int> DomainSpells { get; set; }
        public List<OnGoingSpecialAbility> GrantedAbilities { get; set; }

        public Rune()
        {
            Name = "Rune";
            Dieties = new List<string>() { "Irori", "Nethys" };
            Description = "In strange and eldritch runes you find potent magic.";
            BonusFeats = new List<string>() { "Scribe Scroll" };
            DomainSpells = new Dictionary<string, int>()
            {
                {"erase",1},
                {"secret page", 2},
                {"glyph of warding",3},
                {"explosive runes", 4},
                {"lesser planar binding",5},
                {"greater glyph of warding",6},
                {"instant summons",7},
                {"symbol of death",8},
                {"teleportation circle",9}
            };
            GrantedAbilities = new List<OnGoingSpecialAbility>();
            AddAbilities();
        }   
        
        private void AddAbilities()
        {
            GrantedAbilities.Add(new OnGoingSpecialAbility("BlastRune", 0, "Blast Rune",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Activate, 1));

            GrantedAbilities.Add(new OnGoingSpecialAbility("SpellRune", 0, "Spell Rune",
                OnGoingSpecialAbility.SpecialAbilityTypes.Sp_SpellLikeAbilities, 
                OnGoingSpecialAbility.SpecialAbilityActivities.Constant, 8));
        }
    }
}
