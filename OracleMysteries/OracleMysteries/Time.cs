using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace OracleMysteries
{
    public class Time : IMystery
    {
        #region IMystery Members

        public List<string> Deities()
        {
            return new List<string>();
        }

        public List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.USE_MAGIC_DEVICE };
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            if (ClassLevel >= 2) temp.Add("memory lapse", 1);
            if (ClassLevel >= 4) temp.Add("gentle repose", 2);
            if (ClassLevel >= 6) temp.Add("sands of time", 3);
            if (ClassLevel >= 8) temp.Add("threefold aspect", 4);
            if (ClassLevel >= 10) temp.Add("permanency", 5);
            if (ClassLevel >= 12) temp.Add("contingency", 6);
            if (ClassLevel >= 14) temp.Add("disintegrate", 7);
            if (ClassLevel >= 16) temp.Add("temporal stasis", 8);
            if (ClassLevel >= 18) temp.Add("time stop", 9);

            return temp;
        }

        #endregion
    }
}
