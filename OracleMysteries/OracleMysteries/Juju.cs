using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace OracleMysteries
{
    public class Juju : IMystery
    {
        #region IMystery Members

        public List<string> Deities()
        {
            return new List<string>();
        }

        public List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_NATURE, "Perform (oratory)", StatBlockInfo.SkillNames.SURVIVAL };
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            if (ClassLevel >= 2) temp.Add("speak with animal", 1);
            if (ClassLevel >= 4) temp.Add("hideous laughter", 2);
            if (ClassLevel >= 6) temp.Add("fear", 3);
            if (ClassLevel >= 8) temp.Add("charm monster", 4);
            if (ClassLevel >= 10) temp.Add("create undead", 5);
            if (ClassLevel >= 12) temp.Add("magic jar", 6);
            if (ClassLevel >= 14) temp.Add("creeping doom", 7);
            if (ClassLevel >= 16) temp.Add("trap the soul", 8);
            if (ClassLevel >= 18) temp.Add("shapechange", 9);

            return temp;
        }

        #endregion
    }
}
