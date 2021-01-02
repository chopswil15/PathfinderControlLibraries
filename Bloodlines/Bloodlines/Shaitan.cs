using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skills;
using OnGoing;

namespace Bloodlines
{
    public class Shaitan : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Shaitan()
        {
            Name = "Shaitan";
            Description = "";
            ClassSkillName = SkillData.SkillNames.KnowledgePlanes;
            BonusSpells = new Dictionary<string, int> { {"true strike",3}, 
                                                        {"glitterdust", 5},
                                                        {"greater magic weapon", 7},
                                                        {"stoneskin", 9},
                                                        {"wall of stone", 11},
                                                        {"wall of iron", 13},
                                                        {"plane shift", 15},
                                                        {"iron body", 17},
                                                        {"wish", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
