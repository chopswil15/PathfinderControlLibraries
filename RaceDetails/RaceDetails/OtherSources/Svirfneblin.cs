using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Svirfneblin : RaceFoundation
    {
        public Svirfneblin()
        {
            Name = "Svirfneblin";
            BaseSpeed = 20;
            Size = StatBlockInfo.SizeCategories.Small;
            RaceLanguages = new List<string> { "Gnome" , "Undercommon" };
            Senses = "darkvision 120 ft.,low-light vision";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { { StatBlockInfo.SkillNames.STEALTH , 2}
                                                  , {StatBlockInfo.SkillNames.PERCEPTION, 2},
                                                  { "Craft (alchemy)", 2 }
                                               };
        }
    }
}
