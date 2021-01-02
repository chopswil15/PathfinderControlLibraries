using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernateLibraries;
using PathfinderDomains;

namespace PathfinderContext.Repositories
{
    public class FeatRepository : Repository<feats>
    {
        public FeatRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            
        }      
    }
}
