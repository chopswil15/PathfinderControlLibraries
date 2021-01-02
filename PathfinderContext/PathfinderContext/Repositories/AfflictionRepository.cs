using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernateLibraries;
using PathfinderDomains;

namespace PathfinderContext.Repositories
{
    internal class AfflictionRepository : Repository<affliction>
    {
        public AfflictionRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            
        }      
    }
}
