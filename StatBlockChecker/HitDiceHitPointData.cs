using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockChecker
{
    public class HitDiceHitPointData : IHitDiceHitPointData
    {
        public int TotalHD { get; set; }
        public int HDModifier { get; set; }
        public int MaxHPMod { get; set; }
        public string MaxHPModFormula { get; set; }
        public int MaxFalseLife { get; set; }
        public int FalseLife { get; set; }
    }
}
