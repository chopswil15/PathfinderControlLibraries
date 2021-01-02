using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathfinderGlobals;


namespace ClassDetails
{
    public class ExProphetOfKalistrade : ProphetOfKalistrade
    {
        public ExProphetOfKalistrade()
        {
            Name = "Ex-Prophet of Kalistrade";
            ClassAlignments = CommonMethods.GetAlignments();   
        }
    }
}
