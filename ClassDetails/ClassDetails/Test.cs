using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassDetails
{
    class Test
    {
        Barbarian Bar = new Barbarian();
        List<ClassFoundation> ClassTypes = new List<ClassFoundation>();
        public Test()
        {
            ClassTypes.Add(Bar);
        }
    }
}
