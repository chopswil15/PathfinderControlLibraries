using System;
namespace StatBlockCommon
{
    public interface IFeatStatBlock
    {
        bool achievement { get; set; }
        bool armor_mastery { get; set; }
        string benefit { get; set; }
        bool betrayal { get; set; }
        bool blood_hex { get; set; }
        bool companionfamiliar { get; set; }
        string completion_benefit { get; set; }
        bool critical { get; set; }
        string description { get; set; }
        bool esoteric { get; set; }
        string full_text { get; set; }
        string goal { get; set; }
        bool grit { get; set; }
        int id { get; set; }
        bool item_mastery { get; set; }
        bool multiples { get; set; }
        bool mythic { get; set; }
        string name { get; set; }
        string normal { get; set; }
        string note { get; set; }
        bool panache { get; set; }
        bool peformance { get; set; }
        string prerequisites { get; set; }
        string prerequisite_feats { get; set; }
        string prerequisite_skills { get; set; }
        string race_name { get; set; }
        bool racial { get; set; }
        bool shield_mastery { get; set; }
        string source { get; set; }
        string special { get; set; }
        bool stare { get; set; }
        bool style { get; set; }
        string suggested_traits { get; set; }
        bool targeting { get; set; }
        bool teamwork { get; set; }
        bool trick { get; set; }
        string type { get; set; }
        bool weapon_mastery { get; set; }
    }
}
