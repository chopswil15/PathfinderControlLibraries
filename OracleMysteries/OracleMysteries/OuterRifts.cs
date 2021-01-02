using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace OracleMysteries
{
    public class OuterRifts : IMystery
    {
        #region IMystery Members

        public List<string> Deities()
        {
            return new List<string>();
        }

        public List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA, StatBlockInfo.SkillNames.SURVIVAL };
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            if (ClassLevel >= 2) temp.Add("endure elements", 1);
            if (ClassLevel >= 4) temp.Add("resist energy", 2);
            if (ClassLevel >= 6) temp.Add("vermin shape I", 3);
            if (ClassLevel >= 8) temp.Add("confusion", 4);
            if (ClassLevel >= 10) temp.Add("lesser planar binding", 5);
            if (ClassLevel >= 12) temp.Add("planar binding", 6);
            if (ClassLevel >= 14) temp.Add("insanity", 7);
            if (ClassLevel >= 16) temp.Add("greater planar binding", 8);
            if (ClassLevel >= 18) temp.Add("imprisonment", 9);

            return temp;
        }

        #endregion
    }
}
