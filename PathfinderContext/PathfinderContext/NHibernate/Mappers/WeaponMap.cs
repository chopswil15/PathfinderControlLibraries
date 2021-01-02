using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;
using PathfinderDomains; 

namespace PathfinderContext
{   
    public class WeaponMap : ClassMap<weapon> {
        
        public WeaponMap() {
			Table("weapon");
			LazyLoad();
			Id(x => x.id).GeneratedBy.Identity().Column("id");
			Map(x => x.group).Column("[group]").Not.Nullable().Length(50);
			Map(x => x.category).Column("category").Not.Nullable().Length(50);
			Map(x => x.name).Column("name").Not.Nullable().Length(40);
			Map(x => x.search_name).Column("search_name").Length(40);
			Map(x => x.cost).Column("cost").Not.Nullable().Length(15);
			Map(x => x.damage_small).Column("damage_small").Not.Nullable().Length(15);
			Map(x => x.damage_medium).Column("damage_medium").Not.Nullable().Length(15);
			Map(x => x.critical).Column("critical").Not.Nullable().Length(15);
			Map(x => x.range).Column("range").Not.Nullable().Length(15);
			Map(x => x.weight).Column("weight").Not.Nullable().Length(15);
			Map(x => x.type).Column("type").Not.Nullable().Length(20);
			Map(x => x.special).Column("special").Not.Nullable().Length(50);
			Map(x => x.bludgeoning).Column("bludgeoning").Not.Nullable();
			Map(x => x.piercing).Column("piercing").Not.Nullable();
			Map(x => x.slashing).Column("slashing").Not.Nullable();
			Map(x => x.brace).Column("brace").Not.Nullable();
			Map(x => x.disarm).Column("disarm").Not.Nullable();
			Map(x => x.@double).Column("[double]").Not.Nullable();
			Map(x => x.monk).Column("monk").Not.Nullable();
			Map(x => x.nonlethal).Column("nonlethal").Not.Nullable();
			Map(x => x.reach).Column("reach").Not.Nullable();
			Map(x => x.sunder).Column("sunder").Not.Nullable();
			Map(x => x.trip).Column("trip").Not.Nullable();
			Map(x => x.performance).Column("performance").Not.Nullable();
			Map(x => x.deadly).Column("deadly").Not.Nullable();
			Map(x => x.distracting).Column("distracting").Not.Nullable();
			Map(x => x.uses_ammunition).Column("uses_ammunition").Not.Nullable();
			Map(x => x.source).Column("source").Not.Nullable().Length(50);
			Map(x => x.eastern).Column("eastern").Not.Nullable();
            Map(x => x.grapple).Column("grapple").Not.Nullable();
            Map(x => x.misfire).Column("misfire").Not.Nullable().Length(50);
            Map(x => x.capacity).Column("capacity").Not.Nullable().Precision(10);
            Map(x => x.firearm).Column("firearm").Not.Nullable();
            Map(x => x.technological).Column("technological").Not.Nullable();
            Map(x => x.capacity_technological).Column("capacity_technological").Not.Nullable().Length(20);
            Map(x => x.charge_usage).Column("charge_usage").Not.Nullable().Length(20);
        }
    }
}
