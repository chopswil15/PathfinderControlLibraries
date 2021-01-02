using AutoMapper;
using PathfinderContext.Services;
using PathfinderDomains;
using StatBlockCommon;
using StatBlockCommon.Feat_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockBusiness
{
    public class FeatStatBlockBusiness : StatBlockCommonBusinessBase, IFeatStatBlockBusiness
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<FeatMappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;      

        private static feats MapThisToFeatObject(FeatStatBlock SB)
        {
            return Mapper.Map<FeatStatBlock, feats>(SB);
        }

        private static FeatStatBlock MapThisToFeatStatBlockObject(feats Feats)
        {
            return Mapper.Map<feats, FeatStatBlock>(Feats);
        }

        public IFeatStatBlock GetFeatByName(string name)
        {
            FeatService _featService = new FeatService(ConnectionString);
            feats tempFeat = _featService.GetFeatByName(name);
            return MapThisToFeatStatBlockObject(tempFeat);
        }

        public IFeatStatBlock GetMythicFeatByName(string name)
        {
            FeatService _featService = new FeatService(ConnectionString);
            feats tempFeat = _featService.GetMythicFeatByName(name);
            return MapThisToFeatStatBlockObject(tempFeat);
        }

        public IFeatStatBlock GetFeatByNameSource(string name, string source)
        {
            FeatService _featService = new FeatService(ConnectionString);
            feats tempFeat = _featService.GetFeatByNameSource(name, source);
            return MapThisToFeatStatBlockObject(tempFeat);
        }
    }

    public class FeatMappingProfile : Profile
    {
        public FeatMappingProfile()
        {
            CreateMap<FeatStatBlock, feats>();
            CreateMap<feats, FeatStatBlock>();
        }
    }
}
