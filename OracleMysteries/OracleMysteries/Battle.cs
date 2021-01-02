using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace OracleMysteries
{
    public class Battle : IMystery
    {
        #region IMystery Members

        public List<string> Deities()
        {
            return new List<string> ();
        }

        public List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_ENGINEERING, StatBlockInfo.SkillNames.PERCEPTION, StatBlockInfo.SkillNames.RIDE };
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            if (ClassLevel >= 2) temp.Add("enlarge person", 1);
            if (ClassLevel >= 4) temp.Add("fog cloud", 2);
            if (ClassLevel >= 6) temp.Add("magic vestment", 3);
            if (ClassLevel >= 8) temp.Add("wall of fire", 4);
            if (ClassLevel >= 10) temp.Add("righteous might", 5);
            if (ClassLevel >= 12) temp.Add("mass bull's strength", 6);
            if (ClassLevel >= 14) temp.Add("control weather", 7);
            if (ClassLevel >= 16) temp.Add("earthquake", 8);
            if (ClassLevel >= 18) temp.Add("storm of vengeance", 9);

            return temp;
        }

        #endregion
    }
}
