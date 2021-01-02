using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public class Fate : IPatron
    {
        public List<string> BonusSpells(int ClassLevel)
        {
            List<string> temp = new List<string>();

            if (ClassLevel >= 2) temp.Add("doom");
            if (ClassLevel >= 4) temp.Add("anticipate peril");
            if (ClassLevel >= 6) temp.Add("helping hand");
            if (ClassLevel >= 8) temp.Add("blessing of fervor");
            if (ClassLevel >= 10) temp.Add("greater forbid action");
            if (ClassLevel >= 12) temp.Add("contingency");
            if (ClassLevel >= 14) temp.Add("jolting portent");
            if (ClassLevel >= 16) temp.Add("maze");
            if (ClassLevel >= 18) temp.Add("wish");

            return temp;
        }
    }
}
