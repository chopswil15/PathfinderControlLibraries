using CommonStatBlockInfo;
using RaceFoundational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RaceDetails
{
    public class Shabti : RaceFoundation
    {
        public Shabti()
        {
            Name = "Shabti";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Common" };
        }
    }
}
