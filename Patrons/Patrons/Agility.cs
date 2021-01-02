using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public class Agility : IPatron
    {
        public List<string> BonusSpells(int ClassLevel)
        {
            List<string> temp = new List<string>();

            if (ClassLevel >= 2) temp.Add("jump");
            if (ClassLevel >= 4) temp.Add("cat's grace");
            if (ClassLevel >= 6) temp.Add("haste");
            if (ClassLevel >= 8) temp.Add("freedom of movement");
            if (ClassLevel >= 10) temp.Add("polymorph");
            if (ClassLevel >= 12) temp.Add("mass cat's grace");
            if (ClassLevel >= 14) temp.Add("ethereal jaunt");
            if (ClassLevel >= 16) temp.Add("animal shapes");
            if (ClassLevel >= 18) temp.Add("shapechange");

            return temp;
        }
    }
}
