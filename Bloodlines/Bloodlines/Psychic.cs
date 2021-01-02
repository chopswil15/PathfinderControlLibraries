using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skills;
using OnGoing;

namespace Bloodlines
{
    public class Psychic : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Psychic()
        {
            Name = "Psychic";
            Description = "";
            ClassSkillName = SkillData.SkillNames.SenseMotive;
            BonusSpells = new Dictionary<string, int> { {"mind thrust I",3}, 
                                                        {"id insinuation I", 5},
                                                        {"ego whip I", 7},
                                                        {"intellect fortress I", 9},
                                                        {"psychic crush I", 11},
                                                        {"mental barrier V", 13},
                                                        {"tower of iron will III", 15},
                                                        {"bilocation", 17},
                                                        {"microcosm", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
