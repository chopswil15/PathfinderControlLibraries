using AutoMapper;
using PathfinderContext.Services;
using PathfinderDomains;
using StatBlockCommon.MagicItem_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockBusiness
{
    public class MagicItemStatBlockBusiness : StatBlockCommonBusinessBase, IMagicItemStatBlockBusiness
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<MagicItemMappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;     

        private static magic_item MapThisToMagicItemObject(MagicItemStatBlock SB)
        {
            return Mapper.Map<MagicItemStatBlock, magic_item>(SB);
        }

        private static MagicItemStatBlock MapThisToMagicItemStatBlockObject(magic_item MagicItem)
        {
            return Mapper.Map<magic_item, MagicItemStatBlock>(MagicItem);
        }

        public MagicItemStatBlock GetMagicItemByName(string name)
        {
            MagicItemService _magicItemService = new MagicItemService(ConnectionString);
            magic_item tempMagicItem = _magicItemService.GetMagicItemByName(name);
            return MapThisToMagicItemStatBlockObject(tempMagicItem);
        }

        public MagicItemStatBlock GetMagicItemByNameSource(string name, string source)
        {
            MagicItemService _magicItemService = new MagicItemService(ConnectionString);
            magic_item tempMagicItem = _magicItemService.GetMagicItemByNameSource(name, source);
            return MapThisToMagicItemStatBlockObject(tempMagicItem);
        }
    }

    public class MagicItemMappingProfile : Profile
    {
        public MagicItemMappingProfile()
        {
            CreateMap<MagicItemStatBlock, magic_item>();
            CreateMap<magic_item, MagicItemStatBlock>();
        }
    }
}
