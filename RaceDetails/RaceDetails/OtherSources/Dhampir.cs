using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Dhampir : RaceFoundation
    {
        public Dhampir()
        {
            Name = "Dhampir";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Common"  };
            Senses = "low-light vision, darkvision 60 ft.";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { { StatBlockInfo.SkillNames.PERCEPTION , 2}
                                                  , {StatBlockInfo.SkillNames.BLUFF, 2}
                                               };
        }
    }
}
