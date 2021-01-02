using CommonInterFacesDD;
using OnGoing;
using StatBlockCommon.Monster_SB;
using System.Collections.Generic;

namespace StatBlockChecker
{
    public interface IMonSBSearch
    {
        Dictionary<IEquipment, int> Armor { set; }
        string Bloodline { get; }
        bool IsBestirarySB { get; }
        bool IsMythic { get; }
        MonsterStatBlock MonSB { get; }
        string Mystery { get; }
        int MythicRank { get; }
        int MythicTier { get; }
        int MythicValue { get; }
        string Name { get; }

        int FeatItemCount(string FeatName);
        string FindFamiliarString(bool HasWitchClass);
        int GetAbilityMod(AbilityScores.AbilityScores.AbilityName abilityName);
        int GetAbilityScoreValue(AbilityScores.AbilityScores.AbilityName abilityName);
        List<string> GetArchetypes();
        int GetBaseAbilityMod(AbilityScores.AbilityScores.AbilityName abilityName);
        List<string> GetDeeds();
        string GetGearString(string GearName);
        int GetOnGoingStatBlockModValue(OnGoingStatBlockModifier.StatBlockModifierTypes Type, OnGoingStatBlockModifier.StatBlockModifierSubTypes SubType, ref string formula);
        string GetRagePower(string ragePower);
        string GetSpecialAttack(string specialAttackName);
        string GetSQ(string sqName);
        int GetWeaponsTrainingModifier(string weaponName, ref string formula);
        bool HasAnyClassArchetypes();
        bool HasArchetype(string Archetype);
        bool HasArmor();
        bool HasBloodline();
        bool HasBloodline(string bloodlineName);
        bool HasCavalierOrder(string Order);
        bool HasClassArchetype(string ArchetypeName);
        bool HasCompanion();
        bool HasCurse(string curseName);
        bool HasDeed(string deedName);
        bool HasDefensiveAbility(string defensiveAbility);
        bool HasDomain();
        bool HasDomain(string domainName);
        bool HasElementalSkillFocusFeat(out List<string> School);
        bool HasFamiliar();
        bool HasFeat(string featName);
        bool HasGear(string GearName);
        bool HasGreaterElementalSkillFocusFeat(out List<string> School);
        bool HasGreaterSpellFocusFeat(out List<string> School);
        bool HasHex(string hexString);
        bool HasLightArmor();
        bool HasMystery();
        bool HasMystery(string mysteryName);
        bool HasMythicFeat(string FeatName);
        bool HasOnlyClassHitdice();
        bool HasPatron();
        bool HasRagePower(string ragePower);
        bool HasShield();
        bool HasSkill(string skillName);
        bool HasSpecialAttack(string specialAttackName);
        bool HasSpecialAttackGeneral(string specialAttackName);
        bool HasSpellDomain(string domain);
        bool HasSpellDomians();
        bool HasSpellFocusFeat(out List<string> School);
        bool HasSQ(string sq);
        bool HasSubType(string subType);
        bool HasTemplate(string TemplateName);
        bool HasTrait(string TraitName);
        bool HasType(string type);
        string Race();
        int SkillValue(string skillName);
        int TemplateCount();
        void UpdateAbilityScore(AbilityScores.AbilityScores AbilityValues);
    }
}