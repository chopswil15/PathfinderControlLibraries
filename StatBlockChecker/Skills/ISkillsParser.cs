using Skills;
using System.Collections.Generic;

namespace StatBlockChecker.Skills
{
    public interface ISkillsParser
    {
        List<SkillsInfo.SkillInfo> ParseSkills(string skillsSB);
    }
}