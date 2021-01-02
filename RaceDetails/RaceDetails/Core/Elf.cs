using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RaceFoundational;

using CommonStatBlockInfo;

namespace RaceDetails
{
    public class Elf : RaceFoundation
    {
        public Elf()
        {
            Name = "Elf";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Elven", "Common" };
            Senses = "low-light vision";
            Immune = "sleep";
            RaceWeapons = "longbow, composite longbow, longsword, rapier, shortbow, composite shortbow";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { {StatBlockInfo.SkillNames.PERCEPTION,2} };
        }
    }
}
