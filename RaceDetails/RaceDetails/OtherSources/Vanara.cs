using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Vanara : RaceFoundation
    {
        public Vanara()
        {
            Name = "Vanara";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Common" ,"Vanaran" };
            Senses = "low-light vision";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { { StatBlockInfo.SkillNames.ACROBATICS , 2}
                                                  , {StatBlockInfo.SkillNames.STEALTH, 2}
                                               };
        }
    }
}
