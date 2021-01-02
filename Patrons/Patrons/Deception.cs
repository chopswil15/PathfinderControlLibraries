using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public class Deception : IPatron
    {
        public List<string> BonusSpells(int ClassLevel)
        {
            List<string> temp = new List<string>();

            if (ClassLevel >= 2) temp.Add("ventriloquism");
            if (ClassLevel >= 4) temp.Add("invisibility");
            if (ClassLevel >= 6) temp.Add("blink");
            if (ClassLevel >= 8) temp.Add("confusion");
            if (ClassLevel >= 10) temp.Add("passwall");
            if (ClassLevel >= 12) temp.Add("programmed image");
            if (ClassLevel >= 14) temp.Add("mass invisibility");
            if (ClassLevel >= 16) temp.Add("scintillating pattern");
            if (ClassLevel >= 18) temp.Add("time stop");

            return temp;
        }
    }
}
