using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public class Animals : IPatron
    {
        public List<string> BonusSpells(int ClassLevel)
        {
            List<string> temp = new List<string>();

            if (ClassLevel >= 2) temp.Add("charm animals");
            if (ClassLevel >= 4) temp.Add("speak with animals");
            if (ClassLevel >= 6) temp.Add("dominate animal");
            if (ClassLevel >= 8) temp.Add("summon nature's ally IV");
            if (ClassLevel >= 10) temp.Add("animal growth");
            if (ClassLevel >= 12) temp.Add("antilife shell");
            if (ClassLevel >= 14) temp.Add("beast shape IV");
            if (ClassLevel >= 16) temp.Add("animal shapes");
            if (ClassLevel >= 18) temp.Add("summon nature's ally IX");

            return temp;
        }
    }
}
