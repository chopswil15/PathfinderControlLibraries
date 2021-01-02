using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public class Ancestors : IPatron
    {
        public List<string> BonusSpells(int ClassLevel)
        {
            List<string> temp = new List<string>();

            if (ClassLevel >= 2) temp.Add("bless");
            if (ClassLevel >= 4) temp.Add("aid");
            if (ClassLevel >= 6) temp.Add("prayer");
            if (ClassLevel >= 8) temp.Add("blessing of fervor");
            if (ClassLevel >= 10) temp.Add("commune");
            if (ClassLevel >= 12) temp.Add("greater heroism");
            if (ClassLevel >= 14) temp.Add("refuge");
            if (ClassLevel >= 16) temp.Add("euphoric tranquility");
            if (ClassLevel >= 18) temp.Add("weird");

            return temp;
        }
    }
}
