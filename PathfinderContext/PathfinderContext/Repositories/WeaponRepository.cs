using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernateLibraries;
using PathfinderDomains;

namespace PathfinderContext.Repositories
{
    internal class WeaponRepository : Repository<weapon>
    {
        public WeaponRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            
        }      
    }
}
