using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;
using PathfinderDomains;

namespace PathfinderContext
{   
    public class afflictionMap : ClassMap<affliction>
	{        
        public afflictionMap() {
			Table("affliction");
			LazyLoad();
			Id(x => x.id).GeneratedBy.Identity().Column("id");
			Map(x => x.name).Column("name").Not.Nullable().Length(50);
			Map(x => x.type).Column("type").Not.Nullable().Length(200);
			Map(x => x.onset).Column("onset").Not.Nullable().Length(50);
			Map(x => x.frequency).Column("frequency").Not.Nullable().Length(70);
			Map(x => x.effect).Column("effect").Not.Nullable().Length(1700);
			Map(x => x.cure).Column("cure").Not.Nullable().Length(380);
			Map(x => x.source).Column("source").Not.Nullable().Length(50);
			Map(x => x.fulltext).Column("fulltext").Not.Nullable().Length(2500);
			Map(x => x.cost).Column("cost").Length(50);
			Map(x => x.save).Column("[save]").Length(235);
			Map(x => x.poison).Column("poison").Not.Nullable();
			Map(x => x.disease).Column("disease").Not.Nullable();
			Map(x => x.curse).Column("curse").Not.Nullable();
			Map(x => x.save_value).Column("save_value");
			Map(x => x.initial_effect).Column("initial_effect").Length(150);
			Map(x => x.secondary_effect).Column("secondary_effect").Length(700);
			Map(x => x.description).Column("description").Length(2500);
            Map(x => x.addiction).Column("addiction").Length(50);
            Map(x => x.drug).Column("drug").Not.Nullable();
            Map(x => x.damage).Column("damage").Length(50);
            Map(x => x.spell_effect).Column("spell_effect").Length(50);
            Map(x => x.special).Column("special").Length(400);
        }
    }
}
