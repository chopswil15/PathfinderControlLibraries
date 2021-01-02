using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Android : RaceFoundation
    {
        public Android()
        {
            Name = "Android";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Common" };
            Senses = "low-light vision,darkvision 60 ft.";
            Immune = "disease, exhaustion, fatigue, fear, sleep";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { { StatBlockInfo.SkillNames.PERCEPTION , 2}
                                                 ,{ StatBlockInfo.SkillNames.SENSE_MOTIVE , -4}
                                               };
        }
    }
}
