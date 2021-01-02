using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace OracleMysteries
{
    public class Stone: IMystery
    {
        #region IMystery Members

        public List<string> Deities()
        {
            return new List<string> ();
        }

        public List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.APPRAISE, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.SURVIVAL };
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            if (ClassLevel >= 2) temp.Add("magic stone", 1);
            if (ClassLevel >= 4) temp.Add("stone call", 2);
            if (ClassLevel >= 6) temp.Add("meld into stone", 3);
            if (ClassLevel >= 8) temp.Add("wall of stone", 4);
            if (ClassLevel >= 10) temp.Add("stoneskin", 5);
            if (ClassLevel >= 12) temp.Add("stone tell", 6);
            if (ClassLevel >= 14) temp.Add("statue", 7);
            if (ClassLevel >= 16) temp.Add("repel metal or stone", 8);
            if (ClassLevel >= 18) temp.Add("clashing rocks", 9);

            return temp;
        }

        #endregion    
    }
}
