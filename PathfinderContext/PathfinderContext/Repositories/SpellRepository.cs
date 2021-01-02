using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernateLibraries;
using PathfinderDomains;

namespace PathfinderContext.Repositories
{
    public class SpellRepository : Repository<spell>
    {
        public SpellRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            
        }     
    }
}
