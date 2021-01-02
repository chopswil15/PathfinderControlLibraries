using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Ghoran : RaceFoundation
    {
        public Ghoran()
        {
            Name = "Ghoran";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Common","Sylvan" };
            NaturalArmor = 2;
        }

        
    }
}
