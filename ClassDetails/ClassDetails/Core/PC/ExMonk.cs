using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OnGoing;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace ClassDetails
{
    public class ExMonk : Monk
    {
        public ExMonk()
        {
            Name = "Ex-Monk";
            ClassAlignments = CommonMethods.GetAlignments();
         }
    }
}
