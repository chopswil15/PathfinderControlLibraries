using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Wyrwood : RaceFoundation
    {
        public Wyrwood()
        {
            Name = "Wyrwood";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Small;
            RaceLanguages = new List<string> { "Common" };
            Senses = "darkvision 60 ft.,low-light vision";
        }

      
    }
}
