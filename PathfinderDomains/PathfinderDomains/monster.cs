using System.Collections.Generic; 
using System.Text; 
using System;
using System.Xml.Serialization; 


namespace PathfinderDomains {

    [Serializable]
    [XmlRootAttribute(ElementName = "Monster", IsNullable = false)]
    public class monster {
        public monster() { }
        public virtual int id { get; set; }
        public virtual string name { get; set; }
        public virtual string CR { get; set; }
        public virtual string XP { get; set; }
        public virtual string Race { get; set; }
        public virtual string Class { get; set; }
        public virtual string MonsterSource { get; set; }
        public virtual string Alignment { get; set; }
        public virtual string Size { get; set; }
        public virtual string Type { get; set; }
        public virtual string SubType { get; set; }
        public virtual string Init { get; set; }
        public virtual string Senses { get; set; }
        public virtual string Aura { get; set; }
        public virtual string AC { get; set; }
        public virtual string AC_Mods { get; set; }
        public virtual string HP { get; set; }
        public virtual string HD { get; set; }
        public virtual string HP_Mods { get; set; }
        public virtual string Saves { get; set; }
        public virtual string Fort { get; set; }
        public virtual string Ref { get; set; }
        public virtual string Will { get; set; }
        public virtual string Save_Mods { get; set; }
        public virtual string DefensiveAbilities { get; set; }
        public virtual string DR { get; set; }
        public virtual string Immune { get; set; }
        public virtual string Resist { get; set; }
        public virtual string SR { get; set; }
        public virtual string Weaknesses { get; set; }
        public virtual string Speed { get; set; }
        public virtual string Speed_Mod { get; set; }
        public virtual string Melee { get; set; }
        public virtual string Ranged { get; set; }
        public virtual string Space { get; set; }
        public virtual string Reach { get; set; }
        public virtual string SpecialAttacks { get; set; }
        public virtual string SpellLikeAbilities { get; set; }
        public virtual string SpellsKnown { get; set; }
        public virtual string SpellsPrepared { get; set; }
        public virtual string SpellDomains { get; set; }
        public virtual string AbilityScores { get; set; }
        public virtual string AbilityScore_Mods { get; set; }
        public virtual string BaseAtk { get; set; }
        public virtual string CMB { get; set; }
        public virtual string CMD { get; set; }
        public virtual string Feats { get; set; }
        public virtual string Skills { get; set; }
        public virtual string RacialMods { get; set; }
        public virtual string Languages { get; set; }
        public virtual string SQ { get; set; }
        public virtual string Environment { get; set; }
        public virtual string Organization { get; set; }
        public virtual string Treasure { get; set; }
        public virtual string Description_Visual { get; set; }
        public virtual string Group { get; set; }
        public virtual string Source { get; set; }
        public virtual System.Nullable<bool> IsTemplate { get; set; }
        public virtual string SpecialAbilities { get; set; }
        public virtual string Description { get; set; }
        public virtual string FullText { get; set; }
        public virtual string Gender { get; set; }
        public virtual string Bloodline { get; set; }
        public virtual string ProhibitedSchools { get; set; }
        public virtual string BeforeCombat { get; set; }
        public virtual string DuringCombat { get; set; }
        public virtual string Morale { get; set; }
        public virtual string Gear { get; set; }
        public virtual string OtherGear { get; set; }
        public virtual string Vulnerability { get; set; }
        public virtual string Note { get; set; }
        public virtual bool CharacterFlag { get; set; }
        public virtual bool CompanionFlag { get; set; }
        public virtual bool Fly { get; set; }
        public virtual bool Climb { get; set; }
        public virtual bool Burrow { get; set; }
        public virtual bool Swim { get; set; }
        public virtual bool Land { get; set; }
        public virtual string LinkText { get; set; }
        public virtual string TemplatesApplied { get; set; }
        public virtual string OffenseNote { get; set; }
        public virtual string BaseStatistics { get; set; }
        public virtual string ExtractsPrepared { get; set; }
        public virtual string AgeCategory { get; set; }
        public virtual bool DontUseRacialHD { get; set; }
        public virtual string VariantParent { get; set; }
        public virtual string Mystery { get; set; }
        public virtual string ClassArchetypes { get; set; }
        public virtual string Patron { get; set; }
        public virtual System.Nullable<int> CompanionFamiliarLink { get; set; }
        public virtual string FocusedSchool { get; set; }
        public virtual string Traits { get; set; }
        public virtual string AlternateNameForm { get; set; }
        public virtual bool UniqueMonster { get; set; }
        public virtual string ThassilonianSpecialization { get; set; }
        public virtual bool Variant { get; set; }
        public virtual string AdditionalExtractsKnown { get; set; }
        public virtual int MR { get; set; }
        public virtual bool Mythic { get; set; }
        public virtual int MT { get; set; }
        public virtual string Spirit { get; set; }
        public virtual string PsychicMagic { get; set; }
        public virtual string KineticistWildTalents { get; set; }
        public virtual string Implements { get; set; }
        public virtual string PsychicDiscipline { get; set; }
        public virtual bool IsBestiary { get; set; }
    }
}
