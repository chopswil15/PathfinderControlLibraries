using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathfinderDomains;
using NHibernateLibraries;

namespace PathfinderContext.Repositories
{
    internal class MonsterRepository : Repository<monster>
    {
        public MonsterRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
          
        }  
    }
}
