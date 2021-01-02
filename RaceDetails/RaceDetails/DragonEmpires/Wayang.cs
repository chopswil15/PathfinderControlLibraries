using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Wayang : RaceFoundation
    {
        public Wayang()
        {
            Name = "Wayang";
            BaseSpeed = 20;
            Size = StatBlockInfo.SizeCategories.Small;
            RaceLanguages = new List<string> { "Tien", "Wayang" };
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { 
                                                  {StatBlockInfo.SkillNames.PERCEPTION,2},
                                                  {StatBlockInfo.SkillNames.STEALTH,2}
                                               };
        }
    }
}
