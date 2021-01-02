using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OnGoing;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace ClassDetails
{
    public class ExDruid : Druid
    {
        public ExDruid()
        {
            Name = "Ex-Druid";
            ClassAlignments = CommonMethods.GetAlignments();            
            CanCastSpells = false;
        }
    }
}
