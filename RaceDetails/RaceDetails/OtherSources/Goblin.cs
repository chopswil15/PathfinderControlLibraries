using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Goblin : RaceFoundation
    {
        public Goblin()
        {
            Name = "Goblin";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Small;
            RaceLanguages = new List<string> { "Goblin" };
            Senses = "darkvision 60 ft.";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { { StatBlockInfo.SkillNames.STEALTH, 4 },
                                                 { StatBlockInfo.SkillNames.RIDE, 4 }
                                               };
        }
    }
}
