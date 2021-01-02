using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PathfinderDomains;

namespace PathfinderContext.Services
{
    public class EquipmentGoodsService : PathfinderServiceBase, IEquipmentGoodsService
    {
        public EquipmentGoodsService(string connectionString) : base(connectionString) { }

        public goods_services GetGoodsServicesByName(string name)
        {
            return base.FilterBy<goods_services>(c => c.name == name).FirstOrDefault();
            //using (IRepository<goods_services> goodsServicesRepository = CreateRepository<goods_services>())
            //{
            //    return goodsServicesRepository.FilterBy(c => c.name == name).FirstOrDefault();
            //}
        }
    }
}
