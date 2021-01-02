using EquipmentBasic;
using StatBlockCommon.Individual_SB;
using System.Collections.Generic;

namespace StatBlockChecker
{
    public interface IWeaponChecker
    {
        void CheckMeleeWeaponDamage(Weapon weapon, string weaponsDamage, bool TwoWeaponFighting, bool BiteAttack, int weaponCount, int weaponIndex, string Size, AbilityScores.AbilityScores _abilityScores, MonSBSearch _monSBSearch, StatBlockMessageWrapper _messageXML, int FighterLevel, int ACDefendingMod, bool MagicWeapon, bool GreaterMagicWeapon, IndividualStatBlock_Combat _indvSB);
        void CheckRangedWeaponDamage(Weapon weapon, string weaponsDamage, string size, AbilityScores.AbilityScores _abilityScores, MonSBSearch _monSBSearch, StatBlockMessageWrapper _messageXML, bool MagicWeapon, bool GreaterMagicWeapon, IndividualStatBlock_Combat _indvSB);
        void ParseSingleNaturalWeapon(NaturalWeapon natural_weapon, ref string weaponBonus, ref string weaponsDamage, ref string holdWeapon, ref List<string> Bonuses);
        void ParseSingleWeapon(Weapon weapon, ref string weaponBonus, ref string weaponsDamage, ref string holdWeapon, ref List<string> Bonuses);
        int PoleArmTraingMods(Weapon weapon, MonSBSearch _monSBSearch, int FighterLevel, ref string formula);
    }
}