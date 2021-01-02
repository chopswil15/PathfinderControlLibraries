using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace OracleMysteries
{
    public class Winter : IMystery
    {
        #region IMystery Members

        public List<string> Deities()
        {
            return new List<string>();
        }

        public List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_NATURE, StatBlockInfo.SkillNames.STEALTH, StatBlockInfo.SkillNames.SURVIVAL };
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            if (ClassLevel >= 2) temp.Add("endure elements", 1);
            if (ClassLevel >= 4) temp.Add("frost fall", 2);
            if (ClassLevel >= 6) temp.Add("sleet storm", 3);
            if (ClassLevel >= 8) temp.Add("ice storm", 4);
            if (ClassLevel >= 10) temp.Add("icy prison", 5);
            if (ClassLevel >= 12) temp.Add("cone of cold", 6);
            if (ClassLevel >= 14) temp.Add("ice body", 7);
            if (ClassLevel >= 16) temp.Add("polar ray", 8);
            if (ClassLevel >= 18) temp.Add("mass icy prison", 9);

            return temp;
        }

        #endregion
    }
}
