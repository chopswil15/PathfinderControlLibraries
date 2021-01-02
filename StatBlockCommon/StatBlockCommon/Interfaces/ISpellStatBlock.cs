using System;
namespace StatBlockCommon
{
    public interface ISpellStatBlock
    {
        bool? acid { get; set; }
        int? adept { get; set; }
        bool? air { get; set; }
        int? alchemist { get; set; }
        int? antipaladin { get; set; }
        string area { get; set; }
        string augmented { get; set; }
        int? bard { get; set; }
        int? bloodrager { get; set; }
        string casting_time { get; set; }
        bool? chaotic { get; set; }
        int? cleric { get; set; }
        bool? cold { get; set; }
        string components { get; set; }
        bool costly_components { get; set; }
        bool? curse { get; set; }
        bool? darkness { get; set; }
        bool? death { get; set; }
        string deity { get; set; }
        string description { get; set; }
        string description_formated { get; set; }
        string descriptor { get; set; }
        bool? disease { get; set; }
        bool dismissible { get; set; }
        bool divine_focus { get; set; }
        string domain { get; set; }
        bool? draconic { get; set; }
        int? druid { get; set; }
        string duration { get; set; }
        bool? earth { get; set; }
        string effect { get; set; }
        bool? electricity { get; set; }
        bool? emotion { get; set; }
        bool? evil { get; set; }
        bool? fear { get; set; }
        bool? fire { get; set; }
        bool focus { get; set; }
        bool? force { get; set; }
        string full_text { get; set; }
        bool? good { get; set; }
        string haunt_statistics { get; set; }
        int? hunter { get; set; }
        int id { get; set; }
        int? inquisitor { get; set; }
        int? investigator { get; set; }
        bool? language_dependent { get; set; }
        bool? lawful { get; set; }
        bool? light { get; set; }
        string linktext { get; set; }
        int? magus { get; set; }
        bool material { get; set; }
        int? material_costs { get; set; }
        bool? meditative { get; set; }
        int? medium { get; set; }
        int? mesmerist { get; set; }
        bool? mind_affecting { get; set; }
        bool mythic { get; set; }
        string mythic_text { get; set; }
        string name { get; set; }
        int? occultist { get; set; }
        int? oracle { get; set; }
        bool? pain { get; set; }
        int? paladin { get; set; }
        bool? poison { get; set; }
        int? psychic { get; set; }
        string range { get; set; }
        int? ranger { get; set; }
        bool? ruse { get; set; }
        string saving_throw { get; set; }
        string school { get; set; }
        bool? shadow { get; set; }
        int? shaman { get; set; }
        bool shapeable { get; set; }
        string short_description { get; set; }
        int? skald { get; set; }
        int? SLA_Level { get; set; }
        bool somatic { get; set; }
        bool? sonic { get; set; }
        int? sor { get; set; }
        string source { get; set; }
        string spell_level { get; set; }
        string spell_resistence { get; set; }
        int? spiritualist { get; set; }
        string subschool { get; set; }
        int? summoner { get; set; }
        string targets { get; set; }
        bool verbal { get; set; }
        bool? water { get; set; }
        int? witch { get; set; }
        int? wiz { get; set; }

        int GetSpellLevel();
        int GetSpellLevelByClass(string className, bool HasCurseHaunted, bool HasShadeOfTheUskwood, bool HasRazmiranPriest);
    }
}
