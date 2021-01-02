using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Tiefling : RaceFoundation
    {
        public Tiefling()
        {
            Name = "Tiefling";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Common" , "Abyssal" };
            Senses = "darkvision 60 ft.";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { { StatBlockInfo.SkillNames.STEALTH , 2}
                                                  , {StatBlockInfo.SkillNames.BLUFF, 2}
                                               };
        }
    }
}
