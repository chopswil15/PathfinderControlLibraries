using PathfinderDomains;
using StatBlockCommon.Individual_SB;
using System.Collections.Generic;

namespace StatBlockBusiness
{
    public interface IIndividualStatBlockBusiness
    {
        List<TemplateRaceOverride> GetAllTemplateRaceOverrides();
        IndividualStatBlock GetByNameSource(string indiv_name, string source, string altNameForm);
        IndividualStatBlock GetIndividualByName(string name);
        List<string> GetTemplateNames();
        List<TemplateRaceOverride> GetTemplateRaceOverridesByTemplateName(string templateName);
    }
}