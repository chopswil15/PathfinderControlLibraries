using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class MonkeyGoblin : RaceFoundation
    {
        public MonkeyGoblin()
        {
            Name = "MonkeyGoblin";
            BaseSpeed = 20;
            Size = StatBlockInfo.SizeCategories.Small;
            RaceLanguages = new List<string> { "Goblin" };
            Senses = "low-light vision";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { { StatBlockInfo.SkillNames.ACROBATICS , 2}
                                                 , { StatBlockInfo.SkillNames.STEALTH , 2}
                                               };
        }
    }
}
