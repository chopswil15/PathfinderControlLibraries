using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skills;
using OnGoing;

namespace Bloodlines
{
    public class Ghoul : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Ghoul()
        {
            Name = "Ghoul";
            Description = "";
            ClassSkillName = SkillData.SkillNames.Stealth;
            BonusSpells = new Dictionary<string, int> { {"ray of enfeeblement",3}, 
                                                        {"feast of ashes", 5},
                                                        {"vampiric touch", 7},
                                                        {"fear", 9},
                                                        {"hungry earth", 11},
                                                        {"move earth", 13},
                                                        {"control undead", 15},
                                                        {"unholy aura", 17},
                                                        {"wail of the banshee", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
