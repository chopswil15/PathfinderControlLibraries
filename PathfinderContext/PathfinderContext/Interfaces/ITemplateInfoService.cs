using PathfinderDomains;
using System.Collections.Generic;

namespace PathfinderContext.Services
{
    public interface ITemplateInfoService
    {
        List<TemplateInfo> GetAllTemplateNames();
    }
}