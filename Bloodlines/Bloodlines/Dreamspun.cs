using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Skills;
using OnGoing;

namespace Bloodlines
{
    public class Dreamspun : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Dreamspun()
        {
            Name = "Dreamspun";
            Description = "";
            ClassSkillName = SkillData.SkillNames.KnowledgePlanes;
            BonusSpells = new Dictionary<string, int> { {"sleep",3}, 
                                                        {"augury", 5},
                                                        {"deep slumber", 7},
                                                        {"divination", 9},
                                                        {"dream", 11},
                                                        {"shadow walk", 13},
                                                        {"vision", 15},
                                                        {"moment of prescience", 17},
                                                        {"astral projection", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
