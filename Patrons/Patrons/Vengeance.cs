using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public class Vengeance : IPatron
    {
        public List<string> BonusSpells(int ClassLevel)
        {
            List<string> temp = new List<string>();

            if (ClassLevel >= 2) temp.Add("burning hands");
            if (ClassLevel >= 4) temp.Add("burning gaze");
            if (ClassLevel >= 6) temp.Add("pain strike");
            if (ClassLevel >= 8) temp.Add("shout");
            if (ClassLevel >= 10) temp.Add("symbol of pain");
            if (ClassLevel >= 12) temp.Add("mass pain strike");
            if (ClassLevel >= 14) temp.Add("phantasmal revenge");
            if (ClassLevel >= 16) temp.Add("incendiary cloudn");
            if (ClassLevel >= 18) temp.Add("winds of vengeance");

            return temp;
        }
    }
}
