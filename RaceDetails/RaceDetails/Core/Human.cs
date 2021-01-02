using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RaceFoundational;

using CommonStatBlockInfo;

namespace RaceDetails
{
    public class Human : RaceFoundation
    {
        public Human()
        {
            Name = "Human";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Common" };
        }

        public override int BonusFeatCount()
        {
            //Human one bonus feat
            return 1;
        }
    }
}
