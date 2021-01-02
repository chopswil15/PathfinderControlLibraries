using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace OracleMysteries
{
    public class Bones : IMystery
    {
        #region IMystery Members

        public List<string> Deities()
        {
            return new List<string>();
        }

        public List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.DISGUISE, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.STEALTH };
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            if (ClassLevel >= 2) temp.Add("cause fear", 1);
            if (ClassLevel >= 4) temp.Add("false life", 2);
            if (ClassLevel >= 6) temp.Add("animate dead", 3);
            if (ClassLevel >= 8) temp.Add("fear", 4);
            if (ClassLevel >= 10) temp.Add("slay living", 5);
            if (ClassLevel >= 12) temp.Add("circle of death", 6);
            if (ClassLevel >= 14) temp.Add("control undead", 7);
            if (ClassLevel >= 16) temp.Add("horrid wilting", 8);
            if (ClassLevel >= 18) temp.Add("wail of the banshee", 9);

            return temp;
        }

        #endregion
    }
}
