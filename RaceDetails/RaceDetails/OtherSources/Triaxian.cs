using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonStatBlockInfo;
using RaceFoundational;

namespace RaceDetails
{
    public class Triaxian : RaceFoundation
    {
        public Triaxian()
        {
            Name = "Triaxian";
            BaseSpeed = 30;
            Size = StatBlockInfo.SizeCategories.Medium;
            RaceLanguages = new List<string> { "Triaxian" };
            Senses = "low-light vision";
        }

        public override Dictionary<string, int> SkillsRacialBonus()
        {
            return new Dictionary<string, int> { { StatBlockInfo.SkillNames.PERCEPTION , 2}
                                               };
        }

        public override int BonusFeatCount()
        {
            //Triaxian one bonus feat
            return 1;
        }
    }
}
