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
    public class NaturalWeaponBusiness : EquipmentBusinessBase, INaturalWeaponBusiness
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<NaturalWeaponMappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;

       

        private static natural_weapon MapThisToNaturalWeaponObject(NaturalWeapon SB)
        {
            return Mapper.Map<NaturalWeapon, natural_weapon>(SB);
        }

        private static NaturalWeapon MapThisToNaturalWeaponObject(natural_weapon NaturalWeapon)
        {
            return Mapper.Map<natural_weapon, NaturalWeapon>(NaturalWeapon);
        }

        public NaturalWeapon GetNaturalWeaponByName(string name)
        {
            NaturalWeaponService _naturalWeaponService = new NaturalWeaponService(ConnectionString);
            natural_weapon tempNaturalWeapon = _naturalWeaponService.GetNaturalWeaponByName(name);
            return MapThisToNaturalWeaponObject(tempNaturalWeapon);
        }
    }

    public class NaturalWeaponMappingProfile : Profile
    {
        public NaturalWeaponMappingProfile()
        {
            CreateMap<NaturalWeapon, natural_weapon>();
            CreateMap<natural_weapon, NaturalWeapon>();
        }
    }
}
