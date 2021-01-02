using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using OnGoing;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace ClassDetails
{
    public class ExAntipaladin : Antipaladin
    {
        public ExAntipaladin()
        {
            Name = "Ex-Antipaladin";
            ClassAlignments = CommonMethods.GetAlignments();
            CanCastSpells = false;
        }

        public override string AlternateName()
        {
            return "Paladin";
        }
    }
}
