using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class AquaticElf : RaceFoundation
    {
        public AquaticElf()
        {
            Name = "Aquatic Elf";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Elven", "Common" };
            Senses = "low-light vision";
            Immune = "sleep";
            RaceWeapons = "tridents, rapier, short sword";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { {StatBlockInfo.SkillNames.PERCEPTION,2} };
        }
    }
}
