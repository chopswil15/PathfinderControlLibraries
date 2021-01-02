using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Tengu : RaceFoundation
    {
        public Tengu()
        {
            Name = "Tengu";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Common" , "Tengu" };
            Senses = "low-light vision";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { { StatBlockInfo.SkillNames.STEALTH , 2}
                                                  , {StatBlockInfo.SkillNames.PERCEPTION, 2},
                                                  { StatBlockInfo.SkillNames.LINGUISTICS, 4 }
                                               };
        }
    }
}
