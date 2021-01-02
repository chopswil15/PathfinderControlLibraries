using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Grippli : RaceFoundation
    {
        public Grippli()
        {
            Name = "Grippli";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Small;
            RaceLanguages = new List<string> { "Common","Grippli"  };
            Senses = "darkvision 60 ft.";
            RaceWeapons = "net";
        }

       
    }
}
