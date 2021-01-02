using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skills;
using OnGoing;

namespace Bloodlines
{
    public class Kobold : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Kobold()
        {
            Name = "Kobold";
            Description = "";
            ClassSkillName = SkillData.SkillNames.DisableDevice;
            BonusSpells = new Dictionary<string, int> { {"alarm",3}, 
                                                        {"create pit", 5},
                                                        {"explosive runes", 7},
                                                        {"dragon's breath", 9},
                                                        {"transmute rock to mud", 11},
                                                        {"guards and wards", 13},
                                                        {"delayed blast fireball", 15},
                                                        {"form of the dragon III", 17},
                                                        {"imprisonment", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
