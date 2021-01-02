using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Syrinx : RaceFoundation
    {
        public Syrinx()
        {
            Name = "Syrinx";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Syrinx" };
            Senses = "low-light vision,darkvision 60 ft.";
        }

       
    }
}
