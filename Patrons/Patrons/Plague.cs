using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public class Plague : IPatron
    {
        public List<string> BonusSpells(int ClassLevel)
        {
            List<string> temp = new List<string>();

            if (ClassLevel >= 2) temp.Add("detect undead");
            if (ClassLevel >= 4) temp.Add("command undead");
            if (ClassLevel >= 6) temp.Add("contagion");
            if (ClassLevel >= 8) temp.Add("animate dead");
            if (ClassLevel >= 10) temp.Add("giant vermin");
            if (ClassLevel >= 12) temp.Add("create undead");
            if (ClassLevel >= 14) temp.Add("control undead");
            if (ClassLevel >= 16) temp.Add("create greater undead");
            if (ClassLevel >= 18) temp.Add("energy drain");

            return temp;
        }
    }
}
