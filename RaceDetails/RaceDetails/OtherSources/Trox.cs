using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Trox : RaceFoundation
    {
        public Trox()
        {
            Name = "Trox";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Large;
            RaceLanguages = new List<string> { "Terran" };
            Senses = "darkvision 60 ft.";
        }

    }
}
