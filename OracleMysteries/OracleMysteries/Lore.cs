using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace OracleMysteries
{
    public class Lore : IMystery
    {
        #region IMystery Members

        public List<string> Deities()
        {
            return new List<string>();
        }

        public List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.APPRAISE, StatBlockInfo.SkillNames.SPELLCRAFT, StatBlockInfo.SkillNames.KNOWLEDGE_ALL };
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();
           
            if (ClassLevel >= 2) temp.Add("identify", 1);
            if (ClassLevel >= 4) temp.Add("tongues", 2);
            if (ClassLevel >= 6) temp.Add("locate object", 3);
            if (ClassLevel >= 8) temp.Add("legend lore", 4);
            if (ClassLevel >= 10) temp.Add("contact other plane", 5);
            if (ClassLevel >= 12) temp.Add("mass owl’s wisdom", 6);
            if (ClassLevel >= 14) temp.Add("vision", 7);
            if (ClassLevel >= 16) temp.Add("moment of prescience", 8);
            if (ClassLevel >= 18) temp.Add("time stop", 9);

            return temp;
        }

        #endregion
    }
}
