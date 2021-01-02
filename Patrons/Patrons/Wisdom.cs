using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public class Wisdom : IPatron
    {
        public List<string> BonusSpells(int ClassLevel)
        {
            List<string> temp = new List<string>();

            if (ClassLevel >= 2) temp.Add("shield of faith");
            if (ClassLevel >= 4) temp.Add("owl's wisdom");
            if (ClassLevel >= 6) temp.Add("magic vestment");
            if (ClassLevel >= 8) temp.Add("lesser globe of invulnerability");
            if (ClassLevel >= 10) temp.Add("dream");
            if (ClassLevel >= 12) temp.Add("greater globe of invulnerability");
            if (ClassLevel >= 14) temp.Add("spell turning");
            if (ClassLevel >= 16) temp.Add("protection from spells");
            if (ClassLevel >= 18) temp.Add("mage's disjunction");

            return temp;
        }
    }
}
