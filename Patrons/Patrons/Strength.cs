using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public class Strength : IPatron
    {
        public List<string> BonusSpells(int ClassLevel)
        {
            List<string> temp = new List<string>();

            if (ClassLevel >= 2) temp.Add("divine favor");
            if (ClassLevel >= 4) temp.Add("bull's strength");
            if (ClassLevel >= 6) temp.Add("greater magic weapon");
            if (ClassLevel >= 8) temp.Add("divine power");
            if (ClassLevel >= 10) temp.Add("righteous might");
            if (ClassLevel >= 12) temp.Add("mass bull's strength");
            if (ClassLevel >= 14) temp.Add("giant form I");
            if (ClassLevel >= 16) temp.Add("giant form II");
            if (ClassLevel >= 18) temp.Add("shapechange");

            return temp;
        }   
    }
}
