using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernateLibraries;
using PathfinderDomains;

namespace PathfinderContext.Repositories
{
    internal class NaturalWeaponsRepository : Repository<natural_weapon>
    {
        public NaturalWeaponsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            
        }      
    }
}
