using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RaceFoundational;

using CommonStatBlockInfo;

namespace RaceDetails
{
    public class HalfOrc : RaceFoundation
    {
        public HalfOrc()
        {
            Name = "Half-Orc";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Orc", "Common" };
            Senses = "darkvision 60 ft.";
            RaceWeapons = "greataxe, falchion";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { {StatBlockInfo.SkillNames.INTIMIDATE,2} };
        }
    }
}
