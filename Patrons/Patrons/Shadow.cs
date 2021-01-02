using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public class Shadow : IPatron
    {
        public List<string> BonusSpells(int ClassLevel)
        {
            List<string> temp = new List<string>();

            if (ClassLevel >= 2) temp.Add("silent image");
            if (ClassLevel >= 4) temp.Add("darkness");
            if (ClassLevel >= 6) temp.Add("deeper darkness");
            if (ClassLevel >= 8) temp.Add("shadow conjuration");
            if (ClassLevel >= 10) temp.Add("shadow evocation");
            if (ClassLevel >= 12) temp.Add("shadow walk");
            if (ClassLevel >= 14) temp.Add("greater shadow conjuration");
            if (ClassLevel >= 16) temp.Add("greater shadow evocation");
            if (ClassLevel >= 18) temp.Add("shades");

            return temp;
        }
    }
}
