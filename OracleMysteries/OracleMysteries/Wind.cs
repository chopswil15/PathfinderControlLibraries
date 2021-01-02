using System.Collections.Generic;
using CommonStatBlockInfo;

namespace OracleMysteries
{
    public class Wind : IMystery
    {
        #region IMystery Members

        public List<string> Deities()
        {
            return new List<string> { "Gozreh", "Shelyn" };
        }

        public List<string> ClassSkills()
        {
            return new List<string> {StatBlockInfo.SkillNames.ACROBATICS, StatBlockInfo.SkillNames.ESCAPE_ARTIST, StatBlockInfo.SkillNames.FLY, StatBlockInfo.SkillNames.STEALTH};
        }

        public Dictionary<string, int> BonusSpells(int ClassLevel)
        {
            Dictionary<string, int> temp = new Dictionary<string, int>();
              
            if(ClassLevel >= 2) temp.Add("alter winds",1);
            if(ClassLevel >= 4) temp.Add("gust of wind",2);
            if(ClassLevel >= 6) temp.Add("cloak of winds",3);
            if(ClassLevel >= 8) temp.Add("river of wind",4);
            if(ClassLevel >= 10) temp.Add("control winds",5);
            if(ClassLevel >= 12) temp.Add("sirocco",6);
            if(ClassLevel >= 14) temp.Add("control weather",7);
            if(ClassLevel >= 16) temp.Add("whirlwind",8);
            if(ClassLevel >= 18) temp.Add("winds of vengeance",9);

            return temp;
        }

        #endregion
    }
}
