using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonInterFacesDD;
using AutoMapper;
using PathfinderDomains;
using PathfinderContext.Services;

namespace EquipmentBasic
{
    public class GoodsServices : IEquipment
    {
       // private static GoodsServicesService _goodsServicesService;

        public bool Masterwork { get; set; }
        public bool Broken { get; set; }

        public EquipmentType EquipmentType
        {
            get { return EquipmentType.Goods; }
        }

         #region Properties
        public int id { get; set; }
        public virtual string name { get; set; }
        public virtual string category { get; set; }
        public virtual string cost { get; set; }
        public virtual string weight { get; set; }
        public virtual string source { get; set; }
        public virtual short? craft_dc { get; set; }
        #endregion Properties

        static GoodsServices()
        {
            Mapper.CreateMap<GoodsServices, goods_services>();
            Mapper.CreateMap<goods_services, GoodsServices>();
        }

        private static goods_services MapThisToGoodsServicesObject(GoodsServices SB)
        {
            return Mapper.Map<GoodsServices, goods_services>(SB);
        }

        private static GoodsServices MapThisToGoodsServicesObject(goods_services GoodsServices)
        {
            return Mapper.Map<goods_services, GoodsServices>(GoodsServices);
        }

        public GoodsServices()
        {
            name = string.Empty;
            category = string.Empty;
            cost = string.Empty;
            weight = string.Empty;
            source = string.Empty;
        }

        public static GoodsServices GetGoodsServicesByName(string name)
        {
            GoodsServicesService _goodsServicesService = new GoodsServicesService(EquipmentGlobals.ConnectionString);
            goods_services tempGoodsServices = _goodsServicesService.GetGoodsServicesByName(name);
            return MapThisToGoodsServicesObject(tempGoodsServices);
        }

        //private static void SetGoodsServicesService()
        //{
        //    GoodsServicesService _goodsServicesService = new GoodsServicesService(EquipmentGlobals.ConnectionString);
        //}
    }
}
