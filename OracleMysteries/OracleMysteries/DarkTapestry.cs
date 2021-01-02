using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace OracleMysteries
{
    public class DarkTapestry : IMystery
    {
        #region IMystery Members

        public List<string> Deities()
        {
            return new List<string>();
        }

        public List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.DISGUISE, StatBlockInfo.SkillNames.INTIMIDATE, StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA, StatBlockInfo.SkillNames.STEALTH };
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            if (ClassLevel >= 2) temp.Add("entropic shield", 1);
            if (ClassLevel >= 4) temp.Add("dust of twilight", 2);
            if (ClassLevel >= 6) temp.Add("tongues", 3);
            if (ClassLevel >= 8) temp.Add("black tentacles", 4);
            if (ClassLevel >= 10) temp.Add("feeblemind", 5);
            if (ClassLevel >= 12) temp.Add("planar binding", 6);
            if (ClassLevel >= 14) temp.Add("insanity", 7);
            if (ClassLevel >= 16) temp.Add("reverse gravity", 8);
            if (ClassLevel >= 18) temp.Add("interplanetary teleport", 9);

            return temp;
        }

        #endregion
    }
}
