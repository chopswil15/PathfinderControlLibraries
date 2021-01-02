using CommonInterFacesDD;
using MagicItemAbilityWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatBlockChecker
{
    public class EquipmentData : IEquipmentData
    {
        public List<MagicItemAbilitiesWrapper> MagicItemAbilities { get; set; }
        public Dictionary<IEquipment, int> EquipementRoster { get; set; }
        public Dictionary<IEquipment, int> Armor { get; set; }
        public Dictionary<IEquipment, int> Weapons { get; set; }
        public Dictionary<string, int> WeaponsTrainingData { get; set; }


        public EquipmentData()
        {
            WeaponsTrainingData = new Dictionary<string, int>();
        }
    }
}
