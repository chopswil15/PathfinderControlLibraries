using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace OracleMysteries
{
    public class Spellscar : IMystery
    {
        #region IMystery Members

        public List<string> Deities()
        {
            return new List<string>();
        }

        public List<string> ClassSkills()
        {
            return new List<string> { StatBlockInfo.SkillNames.KNOWLEDGE_ARCANA, StatBlockInfo.SkillNames.KNOWLEDGE_NATURE, StatBlockInfo.SkillNames.SURVIVAL, StatBlockInfo.SkillNames.USE_MAGIC_DEVICE };
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();

            if (ClassLevel >= 2) temp.Add("ray of enfeeblement", 1);
            if (ClassLevel >= 4) temp.Add("obscure object", 2);
            if (ClassLevel >= 6) temp.Add("dispel magic", 3);
            if (ClassLevel >= 8) temp.Add("lesser globe of invulnerability", 4);
            if (ClassLevel >= 10) temp.Add("break enchantment", 5);
            if (ClassLevel >= 12) temp.Add("antimagic field", 6);
            if (ClassLevel >= 14) temp.Add("spell turning", 7);
            if (ClassLevel >= 16) temp.Add("spellscar", 8);
            if (ClassLevel >= 18) temp.Add("mage's disjunction", 9);

            return temp;
        }

        #endregion
    }
}
