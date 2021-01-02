using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using Skills;

namespace Bloodlines
{
    public class Shadow : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Shadow()
        {
            Name = "Shadow";
            Description = "Spirits from the shadow plane dally at times in the world of light, and such as these lay with your ancestors once upon a time, imparting the mystery of shadow-stuff into your lineage. You are often sullen and withdrawn, preferring to skulk at the fringes of social circles and keep to yourself, cultivating an air of mystery and majesty that is all your own.";
            ClassSkillName = SkillData.SkillNames.Stealth;
            BonusSpells = new Dictionary<string, int> { {"ray of enfeeblement",3}, 
                                                        {"darkvision", 5},
                                                        {"deeper darkness", 7},
                                                        {"shadow conjuration", 9},
                                                        {"shadow evocation", 11},
                                                        {"shadow walk", 13},
                                                        {"power word blind", 15},
                                                        {"greater shadow evocation", 17},
                                                        {"shades", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
