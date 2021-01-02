using AutoMapper;
using PathfinderContext.Services;
using PathfinderDomains;
using StatBlockCommon;
using StatBlockCommon.Spell_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockBusiness
{
    public class SpellStatBlockBusiness : StatBlockCommonBusinessBase, ISpellStatBlockBusiness
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<SpellMappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;


        private static spell MapThisToSpellObject(SpellStatBlock SB)
        {
            return Mapper.Map<SpellStatBlock, spell>(SB);
        }

        private static SpellStatBlock MapThisToSpellStatBlockObject(spell Spell)
        {
            return Mapper.Map<spell, SpellStatBlock>(Spell);
        }

        public ISpellStatBlock GetSpellByName(string name)
        {
            SpellService spellService = new SpellService(ConnectionString);
            spell tempSpell = spellService.GetSpellByName(name);
            return MapThisToSpellStatBlockObject(tempSpell);
        }

        public ISpellStatBlock FindById(int Id)
        {
            SpellService spellService = new SpellService(ConnectionString);
            spell tempSpell = spellService.FindBy(Id);
            return MapThisToSpellStatBlockObject(tempSpell);
        }
    }

    public class SpellMappingProfile : Profile
    {
        public SpellMappingProfile()
        {
            CreateMap<SpellStatBlock, spell>();
            CreateMap<spell, SpellStatBlock>();
        }
    }
}
