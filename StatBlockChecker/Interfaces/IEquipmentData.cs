using CommonInterFacesDD;
using MagicItemAbilityWrapper;
using System.Collections.Generic;

namespace StatBlockChecker
{
    public interface IEquipmentData
    {
        Dictionary<IEquipment, int> Armor { get; set; }
        Dictionary<IEquipment, int> EquipementRoster { get; set; }
        List<MagicItemAbilitiesWrapper> MagicItemAbilities { get; set; }
        Dictionary<IEquipment, int> Weapons { get; set; }
        Dictionary<string, int> WeaponsTrainingData { get; set; }
    }
}