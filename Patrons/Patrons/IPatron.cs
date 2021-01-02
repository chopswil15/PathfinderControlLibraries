using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patrons
{
    public interface IPatron
    {
        List<string> BonusSpells(int ClassLevel);
    }
}
