using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Undine : RaceFoundation
    {
        public Undine()
        {
            Name = "Undine";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Common", "Aquan" };
            Senses = "darkvision 60 ft.";
            Resistance = "cold 5";
        }
    }
}
