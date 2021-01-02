using FluentNHibernate.Mapping;
using PathfinderDomains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderNHibernateBase.Mappers
{
    public class TemplateRaceOverrideMap : ClassMap<TemplateRaceOverride>
    {
        public TemplateRaceOverrideMap()
        {
            Table("TemplateRaceOverride");
            LazyLoad();
            Id(x => x.Id).GeneratedBy.Identity().Column("Id");
            Map(x => x.RaceName).Column("RaceName").Not.Nullable().Length(50);
            Map(x => x.TemplateName).Column("TemplateName").Not.Nullable().Length(50);
            Map(x => x.OverrideActionTypeId).Column("OverrideActionTypeId");
        }
    }
}
