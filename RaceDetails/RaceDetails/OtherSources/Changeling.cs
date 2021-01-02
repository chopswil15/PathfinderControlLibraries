using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Changeling : RaceFoundation
    {
        public Changeling()
        {
            Name = "Changeling";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Common","primary language of their host society" };
            Senses = "darkvision 60 ft.";
            NaturalArmor = 1;
        }
    }
}
