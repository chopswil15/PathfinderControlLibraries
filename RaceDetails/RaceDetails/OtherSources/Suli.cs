using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Suli : RaceFoundation
    {
        public Suli()
        {
            Name = "Suli";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Common"  };
            Senses = "low-light vision";
            Resistance = "acid 5, cold 5,electricity 5,  fire 5";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { { StatBlockInfo.SkillNames.DIPLOMACY , 2}
                                                  , {StatBlockInfo.SkillNames.SENSE_MOTIVE, 2}
                                               };
        }
    }
}
