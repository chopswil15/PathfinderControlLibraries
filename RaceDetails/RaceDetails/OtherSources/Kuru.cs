using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Kuru : RaceFoundation
    {
        public Kuru()
        {
            Name = "Kuru";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { };
            Senses = "low-light vision";
        }      
    }
}
