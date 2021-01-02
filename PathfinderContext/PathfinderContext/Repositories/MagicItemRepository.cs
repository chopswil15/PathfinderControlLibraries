using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernateLibraries;
using PathfinderDomains;

namespace PathfinderContext.Repositories
{
    internal class MagicItemRepository : Repository<magic_item>
    {
        public MagicItemRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
          
        }  
    }
}
