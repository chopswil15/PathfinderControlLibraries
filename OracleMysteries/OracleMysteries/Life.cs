using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace OracleMysteries
{
    public class Life : IMystery
    {
        #region IMystery Members

        public List<string> Deities()
        {
            return new List<string>();
        }

        public List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.HANDLE_ANIMAL, StatBlockInfo.SkillNames.KNOWLEDGE_NATURE, StatBlockInfo.SkillNames.SURVIVAL };
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            if (ClassLevel >= 2) temp.Add("detect undead", 1);
            if (ClassLevel >= 4) temp.Add("lesser restoration", 2);
            if (ClassLevel >= 6) temp.Add("neutralize poison", 3);
            if (ClassLevel >= 8) temp.Add("restoration", 4);
            if (ClassLevel >= 10) temp.Add("breath of life", 5);
            if (ClassLevel >= 12) temp.Add("heal", 6);
            if (ClassLevel >= 14) temp.Add("greater restoration", 7);
            if (ClassLevel >= 16) temp.Add("mass heal", 8);
            if (ClassLevel >= 18) temp.Add("true resurrection", 9);

            return temp;
        }

        #endregion
    }
}
