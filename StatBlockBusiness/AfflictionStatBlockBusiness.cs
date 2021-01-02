using AutoMapper;
using PathfinderContext.Services;
using PathfinderDomains;
using StatBlockCommon.Affliction_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockBusiness
{
    public class AfflictionStatBlockBusiness : StatBlockCommonBusinessBase, IAfflictionStatBlockBusiness
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<AfflictionMappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
     

        private static affliction MapThisToAfflictionObject(AfflictionStatBlock SB)
        {
            return Mapper.Map<AfflictionStatBlock, affliction>(SB);
        }

        private static AfflictionStatBlock MapThisToAfflictionStatBlockObject(affliction Affliction)
        {
            return Mapper.Map<affliction, AfflictionStatBlock>(Affliction);
        }

        public AfflictionStatBlock GetAfflictionByName(string name)
        {
            AfflictionService afflictionService = new AfflictionService(ConnectionString);
            affliction tempAffliction = afflictionService.GetAfflictionByName(name);
            return MapThisToAfflictionStatBlockObject(tempAffliction);
        }
    }

    public class AfflictionMappingProfile : Profile
    {
        public AfflictionMappingProfile()
        {
            CreateMap<AfflictionStatBlock, affliction>();
            CreateMap<affliction, AfflictionStatBlock>();
        }
    }
}
