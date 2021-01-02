using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public class Entropy : IPatron
    {
        public List<string> BonusSpells(int ClassLevel)
        {
            List<string> temp = new List<string>();

            if (ClassLevel >= 2) temp.Add("lesser confusion");
            if (ClassLevel >= 4) temp.Add("plague carrier");
            if (ClassLevel >= 6) temp.Add("babble");
            if (ClassLevel >= 8) temp.Add("wandering star motes");
            if (ClassLevel >= 10) temp.Add("feeblemind");
            if (ClassLevel >= 12) temp.Add("antimagic field");
            if (ClassLevel >= 14) temp.Add("insanity");
            if (ClassLevel >= 16) temp.Add("symbol of insanity");
            if (ClassLevel >= 18) temp.Add("interplanetary teleport");

            return temp;
        }
    }
}
