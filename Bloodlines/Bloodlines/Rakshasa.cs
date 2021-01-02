using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skills;
using OnGoing;

namespace Bloodlines
{
    public class Rakshasa : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Rakshasa()
        {
            Name = "Rakshasa";
            Description = "";
            ClassSkillName = SkillData.SkillNames.Disguise;
            BonusSpells = new Dictionary<string, int> { {"charm person",3}, 
                                                        {"invisibility", 5},
                                                        {"suggestion", 7},
                                                        {"detect scrying", 9},
                                                        {"prying eyes", 11},
                                                        {"mass suggestion", 13},
                                                        {"greater polymorph", 15},
                                                        {"mind blank", 17},
                                                        {"dominate monster", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
