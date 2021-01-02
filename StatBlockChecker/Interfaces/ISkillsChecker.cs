using Skills;
using System.Collections.Generic;

namespace StatBlockChecker
{
    public interface ISkillsChecker
    {
        void CheckSkillMath();
        List<SkillsInfo.SkillInfo> GetSkillsValues();
    }
}