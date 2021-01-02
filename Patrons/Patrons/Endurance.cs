using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public class Endurance : IPatron
    {
        public List<string> BonusSpells(int ClassLevel)
        {
            List<string> temp = new List<string>();

            if (ClassLevel >= 2) temp.Add("endure elements");
            if (ClassLevel >= 4) temp.Add("bear’s endurance");
            if (ClassLevel >= 6) temp.Add("protection from energy");
            if (ClassLevel >= 8) temp.Add("spell immunity");
            if (ClassLevel >= 10) temp.Add("spell resistance");
            if (ClassLevel >= 12) temp.Add("mass bear’s endurance");
            if (ClassLevel >= 14) temp.Add("greater restoration");
            if (ClassLevel >= 16) temp.Add("iron body");
            if (ClassLevel >= 18) temp.Add("miracle");

            return temp;
        }
    }
}
