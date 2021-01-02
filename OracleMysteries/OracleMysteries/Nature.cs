using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace OracleMysteries
{
    public class Nature : IMystery
    {
        #region IMystery Members

        public List<string> Deities()
        {
            return new List<string>();
        }

        public List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.KNOWLEDGE_NATURE, StatBlockInfo.SkillNames.RIDE, StatBlockInfo.SkillNames.SURVIVAL, StatBlockInfo.SkillNames.SWIM };
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            if (ClassLevel >= 2) temp.Add("charm animal", 1);
            if (ClassLevel >= 4) temp.Add("barkskin", 2);
            if (ClassLevel >= 6) temp.Add("speak with plants", 3);
            if (ClassLevel >= 8) temp.Add("grove of respite", 4);
            if (ClassLevel >= 10) temp.Add("awaken", 5);
            if (ClassLevel >= 12) temp.Add("stone tell", 6);
            if (ClassLevel >= 14) temp.Add("creeping doom", 7);
            if (ClassLevel >= 16) temp.Add("animal shapes", 8);
            if (ClassLevel >= 18) temp.Add("world wave", 9);

            return temp;
        }

        #endregion
    }
}
