using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bloodlines;
using CommonStatBlockInfo;

namespace ClassDetails
{
    public class Transmuter : Wizard
    {
        public Transmuter()
        {
            Name = "Transmuter";
        }

        public override List<int> GetSpellsPerLevel(int ClassLevel)
        {
            List<int> temp = base.GetSpellsPerLevel(ClassLevel);
            int newValue = 0;

            for (int a = 1; a < temp.Count; a++)
            {
                newValue = temp[a] + 1;
                temp[a] = temp[a] + 1;
            }

            return temp;
        }
    }
}
