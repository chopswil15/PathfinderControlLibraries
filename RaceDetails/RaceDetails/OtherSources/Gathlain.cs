using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Gathlain : RaceFoundation
    {
        public Gathlain()
        {
            Name = "Gathlain";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Small;
            RaceLanguages = new List<string> { "Common" , "Sylvan" };
            Senses = "low-light vision";
            NaturalArmor = 1;
        }
    }
}
