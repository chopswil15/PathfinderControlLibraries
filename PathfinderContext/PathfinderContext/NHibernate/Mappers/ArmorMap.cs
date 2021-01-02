using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;
using PathfinderDomains; 

namespace PathfinderContext 
{   
    public class ArmorMap : ClassMap<armor>
	{        
        public ArmorMap() {
			Table("armor");
			LazyLoad();
			Id(x => x.id).GeneratedBy.Identity().Column("id");
			Map(x => x.name).Column("name").Not.Nullable().Length(50);
			Map(x => x.category).Column("category").Not.Nullable().Length(50);
			Map(x => x.cost).Column("cost").Not.Nullable().Length(20);
			Map(x => x.bonus).Column("bonus").Precision(5);
			Map(x => x.max_dex_bonus).Column("max_dex_bonus").Precision(5);
			Map(x => x.armor_check_penalty).Column("armor_check_penalty").Precision(5);
			Map(x => x.spell_failure_percent).Column("spell_failure_percent").Precision(5);
			Map(x => x.speed_30).Column("speed_30").Precision(5);
			Map(x => x.speed_20).Column("speed_20").Precision(5);
			Map(x => x.weight).Column("weight").Precision(5);
			Map(x => x.source).Column("source").Length(50);
			Map(x => x.eastern).Column("eastern").Not.Nullable();
        }
    }
}
