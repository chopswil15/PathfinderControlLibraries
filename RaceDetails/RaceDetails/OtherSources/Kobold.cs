using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Kobold : RaceFoundation
    {
        public Kobold()
        {
            Name = "Kobold";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Small;
            RaceLanguages = new List<string> { "Draconic" };
            Senses = "darkvision 60 ft.";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { { "Craft (trapmaking)" , 2}
                                                  , {StatBlockInfo.SkillNames.PERCEPTION, 2},
                                                  { "Profession (miner)", 2 }
                                               };
        }
    }
}
