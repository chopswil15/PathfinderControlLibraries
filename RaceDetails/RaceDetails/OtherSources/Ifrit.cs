using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Ifrit : RaceFoundation
    {
        public Ifrit()
        {
            Name = "Ifrit";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Common" ,"Ignan" };
            Senses = "darkvision 60 ft.";
            Resistance = "fire 5";
        }

     
    }
}
