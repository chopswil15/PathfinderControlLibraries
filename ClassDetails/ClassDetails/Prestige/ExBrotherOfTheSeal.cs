using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathfinderGlobals;


namespace ClassDetails
{
    public class ExBrotherOfTheSeal : BrotherOfTheSeal
    {
        public ExBrotherOfTheSeal()
        {
            Name = "Ex-Brother of the Seal";
            ClassAlignments = CommonMethods.GetNonLawfulAlignments();
        }
    }
}
