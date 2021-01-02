using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RaceFoundational;
using CommonStatBlockInfo;

namespace RaceDetails
{
    public class HalfElf : RaceFoundation
    {
        public HalfElf()
        {
            Name = "Half-Elf";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Elven", "Common" };
            Senses = "low-light vision";
            Immune = "sleep";
        }

        public override int BonusFeatCount()
        {
            //one bonus feat, Skill Focus
            return 1;
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { {StatBlockInfo.SkillNames.PERCEPTION,2} };
        }
    }
}
