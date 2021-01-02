using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using Skills;

namespace Bloodlines
{
    public class Abyssal : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Abyssal()
        {
            Name = "Abyssal";
            Description = "Generations ago, a demon spread its filth into your heritage. While it does not manifest in all of your kin, for you it is particularly strong. You might sometimes have urges to chaos or evil, but your destiny (and alignment) is up to you.";
           ClassSkillName = SkillData.SkillNames.KnowledgePlanes;
            BonusSpells = new Dictionary<string, int> { {"cause fear",3}, 
                                                        {"bull's strength", 5},
                                                        {"rage", 7},
                                                        {"stoneskin", 9},
                                                        {"dismissal", 11},
                                                        {"transformation", 13},
                                                        {"greater teleport", 15},
                                                        {"unholy aura", 17},
                                                        {"summon monster IX", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
