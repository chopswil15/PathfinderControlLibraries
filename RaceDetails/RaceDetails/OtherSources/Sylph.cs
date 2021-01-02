using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Sylph : RaceFoundation
    {
        public Sylph()
        {
            Name = "Sylph";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Common", "Auran" };
            Senses = "darkvision 60 ft.";
            Resistance = "electricity 5";
        }
    }
}
