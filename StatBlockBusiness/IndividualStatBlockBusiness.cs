using AutoMapper;
using PathfinderContext.Services;
using PathfinderDomains;
using StatBlockCommon.Individual_SB;
using StatBlockCommon.Monster_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockBusiness
{
    public class IndividualStatBlockBusiness : StatBlockCommonBusinessBase, IIndividualStatBlockBusiness
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<IndividualMappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;

        private static monster MapThisToMonsterObject(MonsterStatBlock SB)
        {
            return Mapper.Map<MonsterStatBlock, monster>(SB);
        }

        private static monster MapThisToMonsterObject(IndividualStatBlock SB)
        {
            return Mapper.Map<IndividualStatBlock, monster>(SB);
        }

        private static MonsterStatBlock MapThisToMonsterStatBlockObject(monster Monster)
        {
            return Mapper.Map<monster, MonsterStatBlock>(Monster);
        }

        private static IndividualStatBlock MapThisToIndividualStatBlockObject(monster Monster)
        {
            return Mapper.Map<monster, IndividualStatBlock>(Monster);
        }

        public IndividualStatBlock GetIndividualByName(string name)
        {
            MonsterService _monsterService = new MonsterService(ConnectionString);
            monster tempMonster = _monsterService.GetMonsterByName(name);
            return (IndividualStatBlock)MapThisToIndividualStatBlockObject(tempMonster);
        }

        public IndividualStatBlock GetByNameSource(string indiv_name, string source, string altNameForm)
        {
            MonsterService _monsterService = new MonsterService(ConnectionString);
            monster tempMonster = _monsterService.GetByNameSource(indiv_name, source, altNameForm);
            return (IndividualStatBlock)MapThisToIndividualStatBlockObject(tempMonster);
        }

        public List<string> GetTemplateNames()
        {
            TemplateInfoService _templateInfoService = new TemplateInfoService(ConnectionString);
            var templateInfos = _templateInfoService.GetAllTemplateNames();
            return templateInfos.OrderBy(x => x.TemplateOrderTypeId).Select(x => x.TemplateName).ToList();

        }

        public List<TemplateRaceOverride> GetAllTemplateRaceOverrides()
        {
            TemplateRaceOverrideService templateRaceOverrideService = new TemplateRaceOverrideService(ConnectionString);
            return templateRaceOverrideService.GetAllTemplateRaceOverrides();
        }

        public List<TemplateRaceOverride> GetTemplateRaceOverridesByTemplateName(string templateName)
        {
            TemplateRaceOverrideService templateRaceOverrideService = new TemplateRaceOverrideService(ConnectionString);
            return templateRaceOverrideService.GetTemplateRaceOverridesByTemplateName(templateName);
        }
    }

    public class IndividualMappingProfile : Profile
    {
        public IndividualMappingProfile()
        {
            CreateMap<MonsterStatBlock, monster>();
            CreateMap<monster, MonsterStatBlock>();
            CreateMap<IndividualStatBlock, monster>();
            CreateMap<monster, IndividualStatBlock>();
        }
    }   
}
