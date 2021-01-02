using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernateLibraries;
using PathfinderDomains;

namespace PathfinderContext.Repositories
{
    internal class GoodsServicesRepository : Repository<goods_services>
    {
        public GoodsServicesRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            
        }      
    }
}
