using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public class Dimensions : IPatron
    {
        public List<string> BonusSpells(int ClassLevel)
        {
            List<string> temp = new List<string>();

            if (ClassLevel >= 2) temp.Add("hold portal");
            if (ClassLevel >= 4) temp.Add("rope trick");
            if (ClassLevel >= 6) temp.Add("blink");
            if (ClassLevel >= 8) temp.Add("dimensional anchor");
            if (ClassLevel >= 10) temp.Add("lesser planar binding");
            if (ClassLevel >= 12) temp.Add("planar binding");
            if (ClassLevel >= 14) temp.Add("banishment");
            if (ClassLevel >= 16) temp.Add("greater planar binding");
            if (ClassLevel >= 18) temp.Add("gate");

            return temp;
        }
    }
}
