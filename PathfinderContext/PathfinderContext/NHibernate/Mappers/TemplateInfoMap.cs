using FluentNHibernate.Mapping;
using PathfinderDomains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathfinderContext
{
    public class TemplateInfoMap : ClassMap<TemplateInfo>
    {
        public TemplateInfoMap()
        {
            Table("TemplateInfo");
            LazyLoad();
            Id(x => x.id).GeneratedBy.Identity().Column("id");
            Map(x => x.TemplateName).Column("TemplateName").Not.Nullable().Length(50);
            Map(x => x.TemplateOrderTypeId).Column("TemplateOrderTypeId");
        }
     }
}
