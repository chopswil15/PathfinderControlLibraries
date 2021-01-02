using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public class Death : IPatron
    {
        public List<string> BonusSpells(int ClassLevel)
        {
            List<string> temp = new List<string>();

            if (ClassLevel >= 2) temp.Add("deathwatch");
            if (ClassLevel >= 4) temp.Add("blessing of courage and life");
            if (ClassLevel >= 6) temp.Add("speak with dead");
            if (ClassLevel >= 8) temp.Add("rest eternal");
            if (ClassLevel >= 10) temp.Add("suffocation");
            if (ClassLevel >= 12) temp.Add("circle of death");
            if (ClassLevel >= 14) temp.Add("finger of death");
            if (ClassLevel >= 16) temp.Add("symbol of death");
            if (ClassLevel >= 18) temp.Add("power word kill");

            return temp;
        }
    }
}
