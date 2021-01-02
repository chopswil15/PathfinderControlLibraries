using FeatManager;
using System.Collections.Generic;

namespace StatBlockChecker
{
    public interface IFeatCheckerInput
    {
        int BAB { get; set; }
        List<FeatData> Feats { get; set; }
        bool HasEnvirmonment { get; set; }
        int HDValue { get; set; }
        int IntAbilityScoreValue { get; set; }
        List<SkillCalculation> SkillCalculations { get; set; }
    }
}