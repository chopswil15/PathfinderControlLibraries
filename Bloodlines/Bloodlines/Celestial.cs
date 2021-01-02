using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using Skills;

namespace Bloodlines
{
    public class Celestial : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Celestial()
        {
            Name = "Celestial";
            Description = "Your bloodline is blessed by a celestial power, either from a celestial ancestor or through divine intervention. Although this power drives you along the path of good, your fate (and alignment) is your own to determine.";
            ClassSkillName = SkillData.SkillNames.Heal;
            BonusSpells = new Dictionary<string, int> { {"bless",3}, 
                                                        {"resist energy", 5},
                                                        {"magic circle against evil", 7},
                                                        {"remove curse", 9},
                                                        {"flame strike", 11},
                                                        {"greater dispel magic", 13},
                                                        {"banishment", 15},
                                                        {"sunburst", 17},
                                                        {"gate", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
