using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;
using PathfinderDomains; 

namespace PathfinderNHibernateBase
{
    public class GoodsServicesMap : ClassMap<goods_services>
    {        
        public GoodsServicesMap() {
			Table("goods_services");
			LazyLoad();
			Id(x => x.id).GeneratedBy.Identity().Column("id");
			Map(x => x.name).Column("name").Not.Nullable().Length(50);
			Map(x => x.category).Column("category").Not.Nullable().Length(50);
			Map(x => x.cost).Column("cost").Not.Nullable().Length(20);
			Map(x => x.weight).Column("weight").Not.Nullable().Length(20);
			Map(x => x.source).Column("source").Not.Nullable().Length(50);
			Map(x => x.craft_dc).Column("craft_dc").Precision(5);
        }
    }
}
