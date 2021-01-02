using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OnGoing;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace ClassDetails
{
    public class ExPaladin : Paladin
    {
        public ExPaladin()
        {
            Name = "Ex-Paladin";
            ClassAlignments = CommonMethods.GetAlignments();            
            CanCastSpells = false;
         }
    }
}
