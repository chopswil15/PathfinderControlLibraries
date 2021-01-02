using AutoMapper;
using PathfinderDomains;
using StatBlockCommon.Affliction_SB;
using StatBlockCommon.Feat_SB;
using StatBlockCommon.Individual_SB;
using StatBlockCommon.MagicItem_SB;
using StatBlockCommon.Monster_SB;
using StatBlockCommon.Spell_SB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderDomainMappers
{
    public static class DomainMappers
    {
        static DomainMappers()
        {
            Mapper.CreateMap<MonsterStatBlock, monster>();
            Mapper.CreateMap<monster, MonsterStatBlock>();

            Mapper.CreateMap<IndividualStatBlock, monster>();
            Mapper.CreateMap<monster, IndividualStatBlock>();

            Mapper.CreateMap<AfflictionStatBlock, affliction>();
            Mapper.CreateMap<affliction, AfflictionStatBlock>();

            Mapper.CreateMap<FeatStatBlock, feats>();
            Mapper.CreateMap<feats, FeatStatBlock>();

            Mapper.CreateMap<MagicItemStatBlock, magic_item>();
            Mapper.CreateMap<magic_item, MagicItemStatBlock>();

            Mapper.CreateMap<SpellStatBlock, spell>();
            Mapper.CreateMap<spell, SpellStatBlock>();
        }

        public static monster MapThisToMonsterObject(MonsterStatBlock SB)
        {
            return Mapper.Map<MonsterStatBlock, monster>(SB);
        }

        public static MonsterStatBlock MapThisToMonsterStatBlockObject(monster Monster)
        {
            return Mapper.Map<monster, MonsterStatBlock>(Monster);
        }

        public static affliction MapThisToAfflictionObject(AfflictionStatBlock SB)
        {
            return Mapper.Map<AfflictionStatBlock, affliction>(SB);
        }

        public static AfflictionStatBlock MapThisToAfflictionStatBlockObject(affliction Affliction)
        {
            return Mapper.Map<affliction, AfflictionStatBlock>(Affliction);
        }

        public static feats MapThisToFeatObject(FeatStatBlock SB)
        {
            return Mapper.Map<FeatStatBlock, feats>(SB);
        }

        public static FeatStatBlock MapThisToFeatStatBlockObject(feats Feats)
        {
            return Mapper.Map<feats, FeatStatBlock>(Feats);
        }

        public static IndividualStatBlock MapThisToIndividualStatBlockObject(monster Monster)
        {
            return Mapper.Map<monster, IndividualStatBlock>(Monster);
        }

        public static monster MapThisToMonsterObject(IndividualStatBlock SB)
        {
            return Mapper.Map<IndividualStatBlock, monster>(SB);
        }

        public static magic_item MapThisToMagicItemObject(MagicItemStatBlock SB)
        {
            return Mapper.Map<MagicItemStatBlock, magic_item>(SB);
        }

        public static MagicItemStatBlock MapThisToMagicItemStatBlockObject(magic_item MagicItem)
        {
            return Mapper.Map<magic_item, MagicItemStatBlock>(MagicItem);
        }

        public static spell MapThisToSpellObject(SpellStatBlock SB)
        {
            return Mapper.Map<SpellStatBlock, spell>(SB);
        }

        public static SpellStatBlock MapThisToSpellStatBlockObject(spell Spell)
        {
            return Mapper.Map<spell, SpellStatBlock>(Spell);
        }
    }
}
