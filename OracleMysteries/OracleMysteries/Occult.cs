using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace OracleMysteries
{
    public class Occult : IMystery
    {
        #region IMystery Members

        public List<string> Deities()
        {
            return new List<string>();
        }

        public List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.BLUFF, StatBlockInfo.SkillNames.DISGUISE, StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA, StatBlockInfo.SkillNames.USE_MAGIC_DEVICE };
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            if (ClassLevel >= 2) temp.Add("unseen servant", 1);
            if (ClassLevel >= 4) temp.Add("spectral hand", 2);
            if (ClassLevel >= 6) temp.Add("clairaudience/clairvoyance", 3);
            if (ClassLevel >= 8) temp.Add("scrying", 4);
            if (ClassLevel >= 10) temp.Add("contact other plane", 5);
            if (ClassLevel >= 12) temp.Add("project image", 6);
            if (ClassLevel >= 14) temp.Add("vision", 7);
            if (ClassLevel >= 16) temp.Add("moment of prescience", 8);
            if (ClassLevel >= 18) temp.Add("astral projection", 9);

            return temp;
        }

        #endregion
    }
}
