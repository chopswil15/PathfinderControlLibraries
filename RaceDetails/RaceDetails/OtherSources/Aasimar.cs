using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Aasimar : RaceFoundation
    {
        public Aasimar()
        {
            Name = "Aasimar";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Celestial", "Common" };
            Senses = "darkvision 60 ft.";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { { StatBlockInfo.SkillNames.PERCEPTION, 2 },
                                                 {StatBlockInfo.SkillNames.DIPLOMACY, 2 }
                                               };
        }
    }
}
