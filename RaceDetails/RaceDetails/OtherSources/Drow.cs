using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Drow : RaceFoundation
    {
        public Drow()
        {
            Name = "Drow";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Elven", "Undercommon" };
            Senses = "darkvision 120 ft.";
            Immune = "sleep";
            RaceWeapons = "hand crossbow, rapier, short sword";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { { StatBlockInfo.SkillNames.PERCEPTION, 2 }
                                               };
        }
    }
}
