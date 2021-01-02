using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public class Elements : IPatron
    {
        public List<string> BonusSpells(int ClassLevel)
        {
            List<string> temp = new List<string>();

            if (ClassLevel >= 2) temp.Add("shocking grasp");
            if (ClassLevel >= 4) temp.Add("flaming sphere");
            if (ClassLevel >= 6) temp.Add("fireball");
            if (ClassLevel >= 8) temp.Add("wall of ice");
            if (ClassLevel >= 10) temp.Add("flame strike");
            if (ClassLevel >= 12) temp.Add("freezing spheree");
            if (ClassLevel >= 14) temp.Add("vortex");
            if (ClassLevel >= 16) temp.Add("fire storm");
            if (ClassLevel >= 18) temp.Add("meteor swarm");

            return temp;
        }
    }
}
