using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnGoing;
using Skills;

namespace Bloodlines
{
    public class Fey : IBloodline
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SkillData.SkillNames ClassSkillName { get; set; }
        public Dictionary<string, int> BonusSpells { get; set; }
        public List<OnGoingSpecialAbility> BloodlineSpecialAbilities { get; set; }

        public Fey()
        {
            Name = "Fey";
            Description = "The capricious nature of the fey runs in your family due to some intermingling of fey blood or magic. You are more emotional than most, prone to bouts of joy and rage.";
           ClassSkillName = SkillData.SkillNames.KnowledgeNature;
            BonusSpells = new Dictionary<string, int> { {"entangle",3}, 
                                                        {"hideous laughter", 5},
                                                        {"deep slumber", 7},
                                                        {"poison", 9},
                                                        {"tree stride", 11},
                                                        {"mislead", 13},
                                                        {"phase door", 15},
                                                        {"irresistible dance", 17},
                                                        {"shapechange", 19}
                                                       };
            BloodlineSpecialAbilities = new List<OnGoingSpecialAbility>();
        }
    }
}
