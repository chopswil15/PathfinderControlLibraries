using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathfinderGlobals;

namespace ClassDetails
{
    public class ExDemoniac : Demoniac
    {
        public ExDemoniac()
        {
            Name = "Ex-Demoniac";
            ClassAlignments = CommonMethods.GetAlignments();
            CanCastSpells = false;
        }
    }
}
