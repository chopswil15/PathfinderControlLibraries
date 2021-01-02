using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using PathfinderDomains;

namespace PathfinderContext.Mappers
{
    public class featsMap : ClassMap<feats>
    {
        public featsMap()
        {
            Table("feats");
            Id(x => x.id).GeneratedBy.Identity().Column("id");
            Map(x => x.name).Column("name").Not.Nullable().Length(50);
            Map(x => x.type).Column("type").Not.Nullable().Length(50);
            Map(x => x.description).Column("description").Not.Nullable().Length(800);
            Map(x => x.prerequisites).Column("prerequisites").Not.Nullable().Length(610);
            Map(x => x.prerequisite_feats).Column("prerequisite_feats").Not.Nullable().Length(200);
            Map(x => x.benefit).Column("benefit").Not.Nullable().Length(5500);
            Map(x => x.normal).Column("normal").Not.Nullable().Length(400);
            Map(x => x.special).Column("special").Not.Nullable().Length(700);
            Map(x => x.source).Column("source").Not.Nullable().Length(50);
            Map(x => x.full_text).Column("fulltext").CustomSqlType("varchar(max)").Length(int.MaxValue);
            Map(x => x.teamwork).Column("teamwork").Not.Nullable();
            Map(x => x.critical).Column("critical").Not.Nullable();
            Map(x => x.grit).Column("grit").Not.Nullable();
            Map(x => x.style).Column("style").Not.Nullable();
            Map(x => x.performance).Column("performance").Not.Nullable();
            Map(x => x.mythic).Column("mythic").Not.Nullable();
            Map(x => x.racial).Column("racial").Not.Nullable();
            Map(x => x.companionfamiliar).Column("companion_familiar").Not.Nullable();
            Map(x => x.achievement).Column("achievement").Not.Nullable();
            Map(x => x.race_name).Column("race_name").Not.Nullable().Length(50);
            Map(x => x.note).Column("note").Not.Nullable().Length(500);
            Map(x => x.goal).Column("goal").Not.Nullable().Length(800);
            Map(x => x.completion_benefit).Column("completion_benefit").Not.Nullable().Length(1000);
            Map(x => x.multiples).Column("multiples").Not.Nullable();
            Map(x => x.suggested_traits).Column("suggested_traits").Not.Nullable().Length(75);
            Map(x => x.prerequisite_skills).Column("prerequisite_skills").Not.Nullable().Length(200);
            Map(x => x.panache).Column("panache").Not.Nullable();
            Map(x => x.betrayal).Column("betrayal").Not.Nullable();
            Map(x => x.targeting).Column("targeting").Not.Nullable();
            Map(x => x.esoteric).Column("esoteric").Not.Nullable();
            Map(x => x.stare).Column("stare").Not.Nullable();
            Map(x => x.weapon_mastery).Column("weapon_mastery").Not.Nullable();
            Map(x => x.item_mastery).Column("item_mastery").Not.Nullable();
            Map(x => x.armor_mastery).Column("armor_mastery").Not.Nullable();
            Map(x => x.shield_mastery).Column("shield_mastery").Not.Nullable();
            Map(x => x.blood_hex).Column("blood_hex").Not.Nullable();
            Map(x => x.trick).Column("trick").Not.Nullable();
            Map(x => x.money).Column("money").Not.Nullable();
        }
    }
}
