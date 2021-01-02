using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skills;
using OnGoing;

namespace Bloodlines
{
    public class Serpentine : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Serpentine()
        {
            Name = "Serpentine";
            Description = "";
            ClassSkillName = SkillData.SkillNames.Diplomacy;
            BonusSpells = new Dictionary<string, int> { {"hypnotism",3}, 
                                                        {"delay poison", 5},
                                                        {"summon monster III", 7},
                                                        {"poison", 9},
                                                        {"hold monster", 11},
                                                        {"mass suggestion", 13},
                                                        {"summon monster VII", 15},
                                                        {"irresistible dance", 17},
                                                        {"dominate monster", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
