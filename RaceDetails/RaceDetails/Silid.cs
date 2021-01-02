using CommonStatBlockInfo;
using RaceFoundational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceDetails
{
    public class Silid : RaceFoundation
    {
        public Silid()
        {
            Name = "Silid";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Small;
            RaceLanguages = new List<string> { "Goblin" };
            Senses = "darkvision 60 ft.";
            RaceWeapons = string.Empty;
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> {
                                                  {StatBlockInfo.SkillNames.STEALTH,4 }
                                               };
        }
    }
}
