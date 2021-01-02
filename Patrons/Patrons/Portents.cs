using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public class Portents : IPatron
    {
        public List<string> BonusSpells(int ClassLevel)
        {
            List<string> temp = new List<string>();

            if (ClassLevel >= 2) temp.Add("ill omen");
            if (ClassLevel >= 4) temp.Add("locate object");
            if (ClassLevel >= 6) temp.Add("blood biography");
            if (ClassLevel >= 8) temp.Add("divination");
            if (ClassLevel >= 10) temp.Add("contact other plane");
            if (ClassLevel >= 12) temp.Add("legend lore");
            if (ClassLevel >= 14) temp.Add("vision");
            if (ClassLevel >= 16) temp.Add("moment of prescience");
            if (ClassLevel >= 18) temp.Add("foresight");

            return temp;
        }
    }
}
