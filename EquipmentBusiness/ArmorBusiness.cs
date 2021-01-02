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
    public class ArmorBusiness : EquipmentBusinessBase, IArmorBusiness
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<ArmorMappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;    

        private static armor MapThisToArmorObject(Armor SB)
        {
            return Mapper.Map<Armor, armor>(SB);
        }

        private static Armor MapThisToArmorObject(armor Armor)
        {
            return Mapper.Map<armor, Armor>(Armor);
        }

        public Armor GetArmorByName(string name)
        {
            ArmorService _armorService = new ArmorService(ConnectionString);
            armor tempArmor = _armorService.GetArmorByName(name);
            return MapThisToArmorObject(tempArmor);
        }
    }

    public class ArmorMappingProfile : Profile
    {
        public ArmorMappingProfile()
        {
            CreateMap<Armor, armor>();
            CreateMap<armor, Armor>();
        }
    }
}
