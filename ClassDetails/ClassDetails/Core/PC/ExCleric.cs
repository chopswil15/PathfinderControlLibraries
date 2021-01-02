using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OnGoing;
using ClericDomains;
using CommonStatBlockInfo;
using PathfinderGlobals;

namespace ClassDetails
{
    public class ExCleric : Cleric
    {
        public ExCleric()
        {
            Name = "Ex-Cleric";
            ClassAlignments = CommonMethods.GetAlignments();
            DomainSpellUse = false;
            CanCastSpells = false;
         }
    }
}
