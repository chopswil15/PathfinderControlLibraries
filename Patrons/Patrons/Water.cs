using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public class Water : IPatron
    {
        public List<string> BonusSpells(int ClassLevel)
        {
            List<string> temp = new List<string>();

            if (ClassLevel >= 2) temp.Add("bless water/curse water");
            if (ClassLevel >= 4) temp.Add("slipstream");
            if (ClassLevel >= 6) temp.Add("water breathing");
            if (ClassLevel >= 8) temp.Add("control water");
            if (ClassLevel >= 10) temp.Add("geyser");
            if (ClassLevel >= 12) temp.Add("elemental body III");
            if (ClassLevel >= 14) temp.Add("elemental body IV");
            if (ClassLevel >= 16) temp.Add("seamantle");
            if (ClassLevel >= 18) temp.Add("tsunami");

            return temp;
        }
    }
}
