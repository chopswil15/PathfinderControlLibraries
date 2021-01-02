using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public class Transformation : IPatron
    {
        public List<string> BonusSpells(int ClassLevel)
        {
            List<string> temp = new List<string>();

            if (ClassLevel >= 2) temp.Add("jump");
            if (ClassLevel >= 4) temp.Add("bear's endurance");
            if (ClassLevel >= 6) temp.Add("beast shape I");
            if (ClassLevel >= 8) temp.Add("beast shape II");
            if (ClassLevel >= 10) temp.Add("beast shape III");
            if (ClassLevel >= 12) temp.Add("form of the dragon I");
            if (ClassLevel >= 14) temp.Add("form of the dragon II");
            if (ClassLevel >= 16) temp.Add("form of the dragon III");
            if (ClassLevel >= 18) temp.Add("shapechange");

            return temp;
        }
    }
}
