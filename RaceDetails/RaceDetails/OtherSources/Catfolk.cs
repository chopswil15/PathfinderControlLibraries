using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Catfolk : RaceFoundation
    {
        public Catfolk()
        {
            Name = "Catfolk";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Common","Catfolk"  };
            Senses = "low-light vision";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { { StatBlockInfo.SkillNames.PERCEPTION , 2}
                                                  , {StatBlockInfo.SkillNames.STEALTH, 2}
                                                  , {StatBlockInfo.SkillNames.SURVIVAL, 2}
                                               };
        }
    }
}
