using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassFoundational;

namespace ClassManager
{
    public class ClassWrapper
    {
        public int Level { get; set; }
        public ClassFoundation ClassInstance { get; set; }
        public string Archetype { get; set; }

        public string Name
        {
            get
            {
                return ClassInstance != null ? ClassInstance.Name : string.Empty;
            }
        }
    }
}
