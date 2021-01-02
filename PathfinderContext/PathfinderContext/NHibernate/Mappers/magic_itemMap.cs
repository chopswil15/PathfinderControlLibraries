using System; 
using System.Collections.Generic; 
using System.Text; 
using FluentNHibernate.Mapping;
using PathfinderDomains;

namespace PathfinderContext
{   
    public class magic_itemMap : ClassMap<magic_item>
    {        
        public magic_itemMap() {
            Table("magic_item");
            Id(x => x.id).GeneratedBy.Identity().Column("id");
            Map(x => x.name).Column("Name").Not.Nullable().Length(50);
            Map(x => x.Aura).Column("Aura").Not.Nullable().Length(70);
            Map(x => x.CL).Column("CL").Not.Nullable().Length(100);
            Map(x => x.Slot).Column("Slot").Not.Nullable().Length(100);
            Map(x => x.Price).Column("Price").Not.Nullable().Length(240);
            Map(x => x.Weight).Column("Weight").Not.Nullable().Length(50);
            Map(x => x.Description).Column("Description").Not.Nullable().Length(7999);
            Map(x => x.Requirements).Column("Requirements").Not.Nullable().Length(310);
            Map(x => x.Cost).Column("Cost").Not.Nullable().Length(280);
            Map(x => x.Group).Column("[Group]").Not.Nullable().Length(50);
            Map(x => x.Source).Column("Source").Not.Nullable().Length(50);
            Map(x => x.AL).Column("AL").Length(30);
            Map(x => x.Int).Column("Int").Length(20);
            Map(x => x.Wis).Column("Wis").Length(20);
            Map(x => x.Cha).Column("Cha").Length(20);
            Map(x => x.Ego).Column("Ego").Length(50);
            Map(x => x.Communication).Column("Communication").Length(50);
            Map(x => x.Senses).Column("Senses").Length(60);
            Map(x => x.Powers).Column("Powers").Length(250);
            Map(x => x.SpecialPurpose).Column("SpecialPurpose").Length(500);
            Map(x => x.DedicatedPowers).Column("DedicatedPowers").Length(230);
            Map(x => x.MagicItems).Column("MagicItems").Length(180);
            Map(x => x.full_text).Column("FullText").CustomSqlType("varchar(max)").Length(int.MaxValue);
            Map(x => x.Destruction).Column("Destruction").Length(1300);
            Map(x => x.MinorArtifactFlag).Column("MinorArtifactFlag").Not.Nullable();
            Map(x => x.MajorArtifactFlag).Column("MajorArtifactFlag").Not.Nullable();
            Map(x => x.Abjuration).Column("Abjuration").Not.Nullable();
            Map(x => x.Conjuration).Column("Conjuration").Not.Nullable();
            Map(x => x.Divination).Column("Divination").Not.Nullable();
            Map(x => x.Enchantment).Column("Enchantment").Not.Nullable();
            Map(x => x.Evocation).Column("Evocation").Not.Nullable();
            Map(x => x.Necromancy).Column("Necromancy").Not.Nullable();
            Map(x => x.Transmutation).Column("Transmutation").Not.Nullable();
            Map(x => x.AuraStrength).Column("AuraStrength").Length(15);
            Map(x => x.WeightValue).Column("WeightValue").Precision(6).Scale(1);
            Map(x => x.PriceValue).Column("PriceValue").Not.Nullable().Precision(10);
            Map(x => x.CostValue).Column("CostValue").Not.Nullable().Precision(10);
            Map(x => x.Languages).Column("Languages").Length(90);
            Map(x => x.LinkText).Column("LinkText").Length(200);
            Map(x => x.BaseItem).Column("BaseItem").Length(110);
            Map(x => x.Mythic).Column("Mythic").Not.Nullable();
            Map(x => x.LegendaryWeapon).Column("LegendaryWeapon").Not.Nullable();
            Map(x => x.Illusion).Column("Illusion").Not.Nullable();
            Map(x => x.Universal).Column("Universal").Not.Nullable();
            Map(x => x.Scaling).Column("Scaling").Not.Nullable();
            Map(x => x.IsIntelligentItem).Column("IsIntelligentItem").Not.Nullable();
        }
    }
}
