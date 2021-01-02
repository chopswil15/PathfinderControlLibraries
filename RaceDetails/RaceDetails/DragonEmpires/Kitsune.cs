using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Kitsune : RaceFoundation
    {
        public Kitsune()
        {
            Name = "Kitsune";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string>{"Tien","Senzar"};
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { 
                                                  {StatBlockInfo.SkillNames.ACROBATICS,2}
                                               };
        }
    }
}
