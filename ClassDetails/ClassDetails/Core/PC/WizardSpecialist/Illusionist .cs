using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassDetails
{
    public class Illusionist : Wizard
    {
        public Illusionist()
        {
            Name = "Illusionist";
            ClassArchetypes = new List<string>() { "Shadow" };
        }

        public override List<int> GetSpellsPerLevel(int ClassLevel)
        {
            List<int> temp = base.GetSpellsPerLevel(ClassLevel);
            int newValue = 0;

            for (int a = 1; a < temp.Count; a++)
            {
                newValue = temp[a] +1;
                temp[a] = temp[a] + 1;
            }

            return temp;
        }
    }
}
