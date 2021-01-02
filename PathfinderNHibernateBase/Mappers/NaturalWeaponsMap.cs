using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;
using PathfinderDomains; 

namespace PathfinderNHibernateBase
{
    public class NaturalWeaponsMap : ClassMap<natural_weapon>
    {        
        public NaturalWeaponsMap() {
			Table("natural_weapons");
			LazyLoad();
            Id(x => x.id).GeneratedBy.Identity().Column("id");
			Map(x => x.name).Column("name").Not.Nullable().Length(50);
			Map(x => x.damage_fine).Column("damage_fine").Not.Nullable().Length(50);
			Map(x => x.damage_diminutive).Column("damage_diminutive").Not.Nullable().Length(50);
			Map(x => x.damage_tiny).Column("damage_tiny").Not.Nullable().Length(50);
			Map(x => x.damage_small).Column("damage_small").Not.Nullable().Length(50);
			Map(x => x.damage_medium).Column("damage_medium").Not.Nullable().Length(50);
			Map(x => x.damage_large).Column("damage_large").Not.Nullable().Length(50);
			Map(x => x.damage_huge).Column("damage_huge").Not.Nullable().Length(50);
			Map(x => x.damage_gargantuan).Column("damage_gargantuan").Not.Nullable().Length(50);
			Map(x => x.damage_colossal).Column("damage_colossal").Not.Nullable().Length(50);
			Map(x => x.damage_type).Column("damage_type").Not.Nullable().Length(50);
			Map(x => x.attack_type).Column("attack_type").Not.Nullable().Length(50);
			Map(x => x.piercing).Column("piercing").Not.Nullable();
			Map(x => x.slashing).Column("slashing").Not.Nullable();
			Map(x => x.bludgeoning).Column("bludgeoning").Not.Nullable();
        }
    }
}
