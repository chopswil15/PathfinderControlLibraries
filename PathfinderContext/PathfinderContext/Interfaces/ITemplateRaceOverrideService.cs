using PathfinderDomains;
using System.Collections.Generic;

namespace PathfinderContext.Services
{
    public interface ITemplateRaceOverrideService
    {
        List<TemplateRaceOverride> GetAllTemplateRaceOverrides();
    }
}