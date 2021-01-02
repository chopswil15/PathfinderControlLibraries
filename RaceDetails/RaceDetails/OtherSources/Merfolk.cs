using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Merfolk : RaceFoundation
    {
        public Merfolk()
        {
            Name = "Merfolk";
            BaseSpeed = 5;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Common", "Aquan" };
            Senses = "low-light vision";
        }
       
    }
}
