using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using PathfinderGlobals;

namespace ClassDetails
{
    public class ExBarbarian : Barbarian
    {
        public ExBarbarian()
        {
            Name = "Ex-Barbarian";
            ClassAlignments = CommonMethods.GetLawfulAlignments();
         }
    }
}
