using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace OracleMysteries
{
    public class Apocalypse : IMystery
    {
        #region IMystery Members

        public List<string> Deities()
        {
            return new List<string>();
        }

        public List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.DISGUISE, StatBlockInfo.SkillNames.SURVIVAL, StatBlockInfo.SkillNames.STEALTH };
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            if (ClassLevel >= 2) temp.Add("deathwatch", 1);
            if (ClassLevel >= 4) temp.Add("summon swarm", 2);
            if (ClassLevel >= 6) temp.Add("explosive runes", 3);
            if (ClassLevel >= 8) temp.Add("ice storm", 4);
            if (ClassLevel >= 10) temp.Add("insect plague", 5);
            if (ClassLevel >= 12) temp.Add("circle of death", 6);
            if (ClassLevel >= 14) temp.Add("vision", 7);
            if (ClassLevel >= 16) temp.Add("incendiary cloud", 8);
            if (ClassLevel >= 18) temp.Add("meteor swarm", 9);

            return temp;
        }

        #endregion
    }
}
