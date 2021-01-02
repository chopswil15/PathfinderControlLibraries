using AutoMapper;
using PathfinderContext.Services;
using PathfinderDomains;
using StatBlockCommon.Monster_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockBusiness
{
    public class MonsterStatBlockBusiness : StatBlockCommonBusinessBase, IMonsterStatBlockBusiness
    {

        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<MonsterMappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;


        private static monster MapThisToMonsterObject(MonsterStatBlock SB)
        {
            return Mapper.Map<MonsterStatBlock, monster>(SB);
        }

        public static MonsterStatBlock MapThisToMonsterStatBlockObject(monster Monster)
        {
            return Mapper.Map<monster, MonsterStatBlock>(Monster);
        }

        public bool AddMonster(MonsterStatBlock SB, ref IEnumerable<string> Error)
        {
            MonsterService _monsterService = new MonsterService(ConnectionString);
            Error = _monsterService.AddMonster(MapThisToMonsterObject(SB));
            return Error.Any() ? true : false;
        }

        public bool UpdateMonster(MonsterStatBlock SB, ref IEnumerable<string> Error)
        {
            MonsterService _monsterService = new MonsterService(ConnectionString);
            Error = _monsterService.UpdateMonster(MapThisToMonsterObject(SB));
            return Error.Any() ? true : false;
        }

        public MonsterStatBlock GetMonsterByName(string name)
        {
            MonsterService _monsterService = new MonsterService(ConnectionString);
            monster tempMonster = _monsterService.GetMonsterByName(name);
            return MapThisToMonsterStatBlockObject(tempMonster);
        }

        public MonsterStatBlock GetBestiaryMonsterByNamePathfinderDefault(string name)
        {
            MonsterService _monsterService = new MonsterService(ConnectionString);
            monster tempMonster = _monsterService.GetBestiaryMonsterByNamePathfinderDefault(name);
            return MapThisToMonsterStatBlockObject(tempMonster);
        }
    }

    public class MonsterMappingProfile : Profile
    {
        public MonsterMappingProfile()
        {
            CreateMap<MonsterStatBlock, monster>();
            CreateMap<monster, MonsterStatBlock>();
        }
    }
}
