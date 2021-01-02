using System;
using System.Collections.Generic;
using CommonStatBlockInfo;
using System.Linq;
using System.Text;

namespace ClassDetails
{
    public class Pyrokineticist : Kineticist
    {
        public Pyrokineticist()
        {
            Name = "Pyrokineticist";
        }

        public override List<string> ClassSkills()
        {
            List<string> temp = base.ClassSkills();
            temp.Add(StatBlockInfo.SkillNames.ESCAPE_ARTIST);
            temp.Add(StatBlockInfo.SkillNames.KNOWLEDGE_NATURE);

            return temp;
        }
    }
}
