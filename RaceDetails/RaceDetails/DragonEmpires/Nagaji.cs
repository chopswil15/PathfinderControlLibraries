using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Nagaji : RaceFoundation
    {
        public Nagaji()
        {
            Name = "Nagaji";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string>{"Tien","Senzar"};
            NaturalArmor = 1;
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { 
                                                  {StatBlockInfo.SkillNames.HANDLE_ANIMAL,2},
                                                   {StatBlockInfo.SkillNames.PERCEPTION,2}
                                               };
        }
    }
}
