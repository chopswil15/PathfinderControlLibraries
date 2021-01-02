using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Samsaran : RaceFoundation
    {
        public Samsaran()
        {
            Name = "Samsaran";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Tien", "Samsaran" };
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { 
                                                  {"Knowledge (2)",2}
                                               };
        }
    }
}
