using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skills;
using OnGoing;

namespace Bloodlines
{
    public class Accursed : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Accursed()
        {
            Name = "Accursed";
            Description = "";
            ClassSkillName = SkillData.SkillNames.Perception;
            BonusSpells = new Dictionary<string, int> { {"ray of enfeeblement",3}, 
                                                        {"touch of idiocy", 5},
                                                        {"ray of exhaustion", 7},
                                                        {"bestow curse", 9},
                                                        {"feeblemind", 11},
                                                        {"eyebite", 13},
                                                        {"insanity", 15},
                                                        {"dimensional lock", 17},
                                                        {"energy drain", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
