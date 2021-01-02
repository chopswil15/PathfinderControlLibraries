using PathfinderDomains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderContext.Services
{
    public class TemplateInfoService : PathfinderServiceBase, ITemplateInfoService
    {
        public TemplateInfoService(string connectionString) : base(connectionString) { }

        public List<TemplateInfo> GetAllTemplateNames()
        {
            return base.FilterBy<TemplateInfo>(x => x.id > 0).ToList();
            //using (IRepository<TemplateInfo> templateInfoRepository = CreateRepository<TemplateInfo>())
            //{
            //    return templateInfoRepository.FilterBy(x => x.id > 0).ToList();
            //}
        }
    }
}
