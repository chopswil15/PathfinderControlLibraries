using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Vishkanya : RaceFoundation
    {
        public Vishkanya()
        {
            Name = "Vishkanya";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Common", "Vishkanya" };
            Senses = "low-light vision";
            RaceWeapons = "blowgun,kukri, shuriken";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { { StatBlockInfo.SkillNames.PERCEPTION , 2}
                                                 ,{ StatBlockInfo.SkillNames.ESCAPE_ARTIST , 2}
                                                 ,{ StatBlockInfo.SkillNames.STEALTH , 2}
                                               };
        }
    }
}
