using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Duergar : RaceFoundation
    {
        public Duergar()
        {
            Name = "Duergar";
            BaseSpeed = 20;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Common", "Dwarven", "Undercommon" };
            Senses = "darkvision 120 ft.";
            Immune = "paralysis, phantasms, poison";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { { StatBlockInfo.SkillNames.STEALTH, 4 }
                                               };
        }
    }
}
