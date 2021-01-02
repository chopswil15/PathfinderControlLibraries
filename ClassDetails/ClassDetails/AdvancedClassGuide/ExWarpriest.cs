using PathfinderGlobals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ClassDetails
{
    public class ExWarpriest : Warpriest
    {
        public ExWarpriest()
        {
            Name = "Ex-Warpriest";
            ClassAlignments = CommonMethods.GetAlignments();
            CanCastSpells = false;
        }
    }
}
