using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Strix : RaceFoundation
    {
        public Strix()
        {
            Name = "Strix";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Strix" };
            Senses = "low-light vision,darkvision 60 ft.";
        }
    }
}
