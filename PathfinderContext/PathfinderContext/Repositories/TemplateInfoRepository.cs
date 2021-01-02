using NHibernateLibraries;
using PathfinderDomains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderContext.Repositories
{
    public class TemplateInfoRepository : Repository<TemplateInfo>
    {
        public TemplateInfoRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
