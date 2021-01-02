using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public class Winter : IPatron
    {
        public List<string> BonusSpells(int ClassLevel)
        {
            List<string> temp = new List<string>();

            if (ClassLevel >= 2) temp.Add("unshakable chill");
            if (ClassLevel >= 4) temp.Add("resist energy");
            if (ClassLevel >= 6) temp.Add("ice storm");
            if (ClassLevel >= 8) temp.Add("wall of ice");
            if (ClassLevel >= 10) temp.Add("cone of cold");
            if (ClassLevel >= 12) temp.Add("freezing sphere");
            if (ClassLevel >= 14) temp.Add("control weather");
            if (ClassLevel >= 16) temp.Add("polar ray");
            if (ClassLevel >= 18) temp.Add("polar midnight");

            return temp;
        }
    }
}
