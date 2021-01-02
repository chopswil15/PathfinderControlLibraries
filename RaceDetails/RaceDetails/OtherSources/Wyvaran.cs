using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Wyvaran : RaceFoundation
    {
        public Wyvaran()
        {
            Name = "Wyvaran";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Common","Draconic" };
            Senses = "darkvision 60 ft.,low-light vision";
        }

    }
}
