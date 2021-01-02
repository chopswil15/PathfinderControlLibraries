using FluentNHibernate.Mapping;
using PathfinderDomains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderNHibernateBase.Mappers
{
    public class FindReplaceTextMap : ClassMap<FindReplaceText>
    {
        public FindReplaceTextMap()
        {
            Table("FindReplaceText");
            LazyLoad();
            Id(x => x.Id).GeneratedBy.Identity().Column("Id");
            Map(x => x.FindText).Column("FindText").Not.Nullable().Length(50);
            Map(x => x.ReplaceText).Column("ReplaceText");
        }
    }
}
