using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace OracleMysteries
{
    public class Heavens : IMystery
    {
        #region IMystery Members

        public List<string> Deities()
        {
            return new List<string>();
        }

        public List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.SURVIVAL };
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            if (ClassLevel >= 2) temp.Add("color spray", 1);
            if (ClassLevel >= 4) temp.Add("hypnotic pattern", 2);
            if (ClassLevel >= 6) temp.Add("daylight", 3);
            if (ClassLevel >= 8) temp.Add("rainbow pattern", 4);
            if (ClassLevel >= 10) temp.Add("overland flight", 5);
            if (ClassLevel >= 12) temp.Add("chain lightning", 6);
            if (ClassLevel >= 14) temp.Add("prismatic spray", 7);
            if (ClassLevel >= 16) temp.Add("sunburst", 8);
            if (ClassLevel >= 18) temp.Add("meteor swarm", 9);

            return temp;
        }

        #endregion
    }
}
