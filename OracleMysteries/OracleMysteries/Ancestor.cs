using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace OracleMysteries
{
    public class Ancestor : IMystery
    {
        #region IMystery Members

        public List<string> Deities()
        {
            return new List<string>();
        }

        public List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.KNOWLEDGE_ALL, StatBlockInfo.SkillNames.LINGUISTICS };
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            if (ClassLevel >= 2) temp.Add("unseen servant", 1);
            if (ClassLevel >= 4) temp.Add("spiritual weapon", 2);
            if (ClassLevel >= 6) temp.Add("heroism", 3);
            if (ClassLevel >= 8) temp.Add("spiritual ally", 4);
            if (ClassLevel >= 10) temp.Add("telekinesis", 5);
            if (ClassLevel >= 12) temp.Add("greater heroism", 6);
            if (ClassLevel >= 14) temp.Add("ethereal jaunt", 7);
            if (ClassLevel >= 16) temp.Add("vision", 8);
            if (ClassLevel >= 18) temp.Add("astral projection", 9);

            return temp;
        }

        #endregion
    }
}
