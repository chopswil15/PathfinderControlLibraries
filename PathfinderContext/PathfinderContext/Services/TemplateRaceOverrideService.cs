using PathfinderDomains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderContext.Services
{
    public class TemplateRaceOverrideService : PathfinderServiceBase, ITemplateRaceOverrideService
    {
        public TemplateRaceOverrideService(string connectionString) : base(connectionString) { }

        public List<TemplateRaceOverride> GetAllTemplateRaceOverrides()
        {
            return base.FilterBy<TemplateRaceOverride>(x => x.Id > 0).OrderBy(x => x.Id).ToList();
            //using (IRepository<TemplateRaceOverride> templateRaceOverrideRepository = CreateRepository<TemplateRaceOverride>())
            //{
            //    var tempList = templateRaceOverrideRepository.FilterBy(x => x.Id > 0).OrderBy(x => x.Id).ToList();
            //    HandleSpace(tempList);

            //    return tempList;
            //}
        }

        private static void HandleSpace(List<TemplateRaceOverride> tempList)
        {
            foreach (var item in tempList)
            {
                item.RaceName = item.RaceName.Replace("SPACE", " ");
                item.TemplateName = item.TemplateName.Replace("SPACE", " ");
            }
        }

        public List<TemplateRaceOverride> GetTemplateRaceOverridesByTemplateName(string templateName)
        {
            return base.FilterBy<TemplateRaceOverride>(x => x.Id > 0 && x.TemplateName == templateName).OrderBy(x => x.Id).ToList();
            //using (IRepository<TemplateRaceOverride> templateRaceOverrideRepository = CreateRepository<TemplateRaceOverride>())
            //{
            //    var tempList = templateRaceOverrideRepository.FilterBy(x => x.Id > 0 && x.TemplateName == templateName).OrderBy(x => x.Id).ToList();
            //    HandleSpace(tempList);

            //    return tempList;
            //}
        }
    }
}
