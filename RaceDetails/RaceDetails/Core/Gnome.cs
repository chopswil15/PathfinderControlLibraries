using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RaceFoundational;

using CommonStatBlockInfo;

namespace RaceDetails
{
    public class Gnome : RaceFoundation
    {
        public Gnome()
        {
            Name = "Gnome";
            BaseSpeed = 20;
            Size = StatBlockInfo.SizeCategories.Small;
            RaceLanguages = new List<string> { "Gnome", "Common", "Sylvan" };
            Senses = "low-light vision";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { {StatBlockInfo.SkillNames.PERCEPTION,2} };
        }
    }
}
