using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OnGoing;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace ClassDetails
{
    public class ExInquisitor : Inquisitor
    {
        public ExInquisitor()
        {
            Name = "Ex-Inquisitor";
            ClassAlignments = CommonMethods.GetAlignments();
            CanCastSpells = false;
        }
    }
}
