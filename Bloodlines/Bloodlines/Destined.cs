using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using Skills;

namespace Bloodlines
{
    public class Destined : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Destined()
        {
            Name = "Destined";
            Description = "Your family is destined for greatness in some way. Your birth could have been foretold in prophecy, or perhaps it occurred during an especially auspicious event, such as a solar eclipse. Regardless of your bloodline's origin, you have a great future ahead.";
            ClassSkillName = SkillData.SkillNames.KnowledgeHistory;
            BonusSpells = new Dictionary<string, int> { {"alarm",3}, 
                                                        {"blur", 5},
                                                        {"protection from energy", 7},
                                                        {"freedom of movement", 9},
                                                        {"break enchantment", 11},
                                                        {"mislead", 13},
                                                        {"spell turning", 15},
                                                        {"moment of prescience", 17},
                                                        {"foresight", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
