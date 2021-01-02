using OnGoing;
using Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bloodlines
{
    public class Efreeti : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Efreeti()
        {
            Name = "Efreeti";
            Description = "";
            ClassSkillName = SkillData.SkillNames.KnowledgeEngineering;
            BonusSpells = new Dictionary<string, int> { {"enlarge person",3},
                                                        {"scorching ray", 5},
                                                        {"fireball", 7},
                                                        {"wall of fire", 9},
                                                        {"persistent image", 11},
                                                        {"planar binding", 13},
                                                        {"plane shift", 15},
                                                        {"giant form II", 17},
                                                        {"wish", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
