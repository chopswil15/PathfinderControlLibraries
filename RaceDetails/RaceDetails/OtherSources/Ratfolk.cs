using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Ratfolk : RaceFoundation
    {
        public Ratfolk()
        {
            Name = "Ratfolk";
            BaseSpeed = 20;
            Size = StatBlockInfo.SizeCategories.Small;
            RaceLanguages = new List<string> { "Common","Catfolk"  };
            Senses = "darkvision 60 ft.";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { { StatBlockInfo.SkillNames.PERCEPTION , 2}
                                                  , {StatBlockInfo.SkillNames.USE_MAGIC_DEVICE, 2}
                                                  , {"Craft (alchemy)", 2}
                                               };
        }
    }
}
