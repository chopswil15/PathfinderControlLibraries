using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace OracleMysteries
{
    public class Ascetic : IMystery
    {
        #region IMystery Members

        public List<string> Deities()
        {
            return new List<string>();
        }

        public List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.CLIMB, StatBlockInfo.SkillNames.ESCAPE_ARTIST, StatBlockInfo.SkillNames.SWIM };
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            if (ClassLevel >= 2) temp.Add("stone fist", 1);
            if (ClassLevel >= 4) temp.Add("glide", 2);
            if (ClassLevel >= 6) temp.Add("force punch", 3);
            if (ClassLevel >= 8) temp.Add("ethereal fists", 4);
            if (ClassLevel >= 10) temp.Add("contact other plan", 5);
            if (ClassLevel >= 12) temp.Add("legend lore", 6);
            if (ClassLevel >= 14) temp.Add("vision", 7);
            if (ClassLevel >= 16) temp.Add("frightful aspect", 8);
            if (ClassLevel >= 18) temp.Add("iron body", 9);

            return temp;
        }

        #endregion
    }
}
