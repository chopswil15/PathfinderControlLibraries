using System.Collections.Generic;

namespace OracleMysteries
{
    public interface IMystery
    {
        List<string> Deities();
        List<string> ClassSkills();
        Dictionary<string, int> BonusSpells(int ClassLevel);
    }
}
