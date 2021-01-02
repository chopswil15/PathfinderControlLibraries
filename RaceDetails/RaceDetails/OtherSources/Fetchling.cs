using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Fetchling : RaceFoundation
    {
        public Fetchling()
        {
            Name = "Fetchling";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Common"  };
            Senses = "low-light vision, darkvision 60 ft.";
            Resistance = "cold 5, electricity 5";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { { StatBlockInfo.SkillNames.KNOWLEDGE_PLANES , 2}
                                                  , {StatBlockInfo.SkillNames.STEALTH, 2}
                                               };
        }
    }
}
