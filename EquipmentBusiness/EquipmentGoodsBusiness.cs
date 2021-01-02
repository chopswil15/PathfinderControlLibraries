using AutoMapper;
using EquipmentBasic;
using PathfinderContext.Services;
using PathfinderDomains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentBusiness
{
    public class EquipmentGoodsBusiness : EquipmentBusinessBase, IEquipmentGoodsBusiness
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<EquipmentGoodsMappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;       

        private static goods_services MapThisToGoodsServicesObject(EquipmentGoods SB)
        {
            return Mapper.Map<EquipmentGoods, goods_services>(SB);
        }

        private static EquipmentGoods MapThisToGoodsServicesObject(goods_services GoodsServices)
        {
            return Mapper.Map<goods_services, EquipmentGoods>(GoodsServices);
        }

        public EquipmentGoods GetGoodsServicesByName(string name)
        {
            EquipmentGoodsService _goodsServicesService = new EquipmentGoodsService(ConnectionString);
            goods_services tempGoodsServices = _goodsServicesService.GetGoodsServicesByName(name);
            return MapThisToGoodsServicesObject(tempGoodsServices);
        }
    }

    public class EquipmentGoodsMappingProfile : Profile
    {
        public EquipmentGoodsMappingProfile()
        {
            CreateMap<EquipmentGoods, goods_services>();
            CreateMap<goods_services, EquipmentGoods>();
        }
    }
}
