using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RaceFoundational;

using CommonStatBlockInfo;

namespace RaceDetails
{
    public class Dwarf : RaceFoundation
    {
        public Dwarf()
        {
            Name = "Dwarf";
            BaseSpeed = 20;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Dwarven", "Common" };
            Senses = "darkvision 60 ft.";
            RaceWeapons = "battleaxe, heavy pick, warhammer";
        }
    }
}
