using NHibernateLibraries;
using PathfinderDomains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderContext.Repositories
{
    public class TemplateRaceOverrideRepository : Repository<TemplateRaceOverride>
    {
        public TemplateRaceOverrideRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
