using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Oread : RaceFoundation
    {
        public Oread()
        {
            Name = "Oread";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Common", "Terran" };
            Senses = "darkvision 60 ft.";
            Resistance = "acid 5";
        }

    }
}
