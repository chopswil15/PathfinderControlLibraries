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
    public class WeaponBusiness : EquipmentBusinessBase, IWeaponBusiness
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<WeaponMappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;      

        private static weapon MapThisToWeaponObject(Weapon SB)
        {
            return Mapper.Map<Weapon, weapon>(SB);
        }

        private static Weapon MapThisToWeaponObject(weapon Weapon)
        {
            return Mapper.Map<weapon, Weapon>(Weapon);
        }

        public Weapon GetWeaponByName(string name)
        {
            WeaponService _weaponService = new WeaponService(ConnectionString);
            weapon tempWeapon = _weaponService.GetWeaponByName(name);
            return MapThisToWeaponObject(tempWeapon);
        }
    }

    public class WeaponMappingProfile : Profile
    {
        public WeaponMappingProfile()
        {
            CreateMap<Weapon, weapon>();
            CreateMap<weapon, Weapon>();
        }
    }
}
