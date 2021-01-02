using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace OracleMysteries
{
    public class Metal : IMystery
    {
        public List<string> Deities()
        {
            return new List<string>();
        }

        public List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.APPRAISE, StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.DISABLE_DEVICE, StatBlockInfo.SkillNames.INTIMIDATE };
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            if (ClassLevel >= 2) temp.Add("lead blades", 1);
            if (ClassLevel >= 4) temp.Add("heat metal", 2);
            if (ClassLevel >= 6) temp.Add("keen edge", 3);
            if (ClassLevel >= 8) temp.Add("versatile weapon", 4);
            if (ClassLevel >= 10) temp.Add("major creation", 5);
            if (ClassLevel >= 12) temp.Add("wall of iron", 6);
            if (ClassLevel >= 14) temp.Add("statue", 7);
            if (ClassLevel >= 16) temp.Add("repel metal or stone", 8);
            if (ClassLevel >= 18) temp.Add("iron body", 9);

            return temp;
        }
    }
}
