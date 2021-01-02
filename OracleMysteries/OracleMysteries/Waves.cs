using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace OracleMysteries
{
    public class Waves : IMystery
    {
        #region IMystery Members

        public List<string> Deities()
        {
            return new List<string> ();
        }

        public List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.ESCAPE_ARTIST, StatBlockInfo.SkillNames.KNOWLEDGE_NATURE, StatBlockInfo.SkillNames.SWIM };
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            if (ClassLevel >= 2) temp.Add("touch of the sea", 1);
            if (ClassLevel >= 4) temp.Add("slipstream", 2);
            if (ClassLevel >= 6) temp.Add("water breathing", 3);
            if (ClassLevel >= 8) temp.Add("wall of ice", 4);
            if (ClassLevel >= 10) temp.Add("geyser", 5);
            if (ClassLevel >= 12) temp.Add("fluid form", 6);
            if (ClassLevel >= 14) temp.Add("vortex", 7);
            if (ClassLevel >= 16) temp.Add("seamantle", 8);
            if (ClassLevel >= 18) temp.Add("tsunami", 9);

            return temp;
        }

        #endregion
    }
}
