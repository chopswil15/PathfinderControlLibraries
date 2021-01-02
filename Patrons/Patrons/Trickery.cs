using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public class Trickery : IPatron
    {
        public List<string> BonusSpells(int ClassLevel)
        {
            List<string> temp = new List<string>();

            if (ClassLevel >= 2) temp.Add("animate rope");
            if (ClassLevel >= 4) temp.Add("mirror image");
            if (ClassLevel >= 6) temp.Add("major image");
            if (ClassLevel >= 8) temp.Add("hallucinatory terrain");
            if (ClassLevel >= 10) temp.Add("mirage arcana");
            if (ClassLevel >= 12) temp.Add("mislead");
            if (ClassLevel >= 14) temp.Add("reverse gravity");
            if (ClassLevel >= 16) temp.Add("screen");
            if (ClassLevel >= 18) temp.Add("time stop");

            return temp;
        }
    }
}
