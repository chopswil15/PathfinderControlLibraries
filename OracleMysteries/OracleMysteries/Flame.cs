using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace OracleMysteries
{
    public class Flame : IMystery
    {
        #region IMystery Members

        public List<string> Deities()
        {
            return new List<string>();
        }

        public List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.PERFORM };
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            if (ClassLevel >= 2) temp.Add("burning hands", 1);
            if (ClassLevel >= 4) temp.Add("resist energy", 2);
            if (ClassLevel >= 6) temp.Add("fireball", 3);
            if (ClassLevel >= 8) temp.Add("wall of fire", 4);
            if (ClassLevel >= 10) temp.Add("summon monster V", 5);
            if (ClassLevel >= 12) temp.Add("fire seeds", 6);
            if (ClassLevel >= 14) temp.Add("fire storm", 7);
            if (ClassLevel >= 16) temp.Add("incendiary cloud", 8);
            if (ClassLevel >= 18) temp.Add("fiery body", 9);

            return temp;
        }

        #endregion
    }
}
