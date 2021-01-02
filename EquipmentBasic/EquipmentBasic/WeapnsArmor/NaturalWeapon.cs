using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonInterFacesDD;

namespace EquipmentBasic
{
    public class NaturalWeapon : IEquipment
    {
        public bool Masterwork { get; set; }
        public bool Broken { get; set; }

        public EquipmentType EquipmentType
        {
            get { return EquipmentType.NaturalWeapon; }
        }

        #region Properties
        public int id { get; set; }
        public string name { get; set; }
        public string damage_fine { get; set; }
        public string damage_diminutive { get; set; }
        public string damage_tiny { get; set; }
        public string damage_small { get; set; }
        public string damage_medium { get; set; }
        public string damage_large { get; set; }
        public string damage_huge { get; set; }
        public string damage_gargantuan { get; set; }
        public string damage_colossal { get; set; }
        public string damage_type { get; set; }
        public string attack_type { get; set; }
        public bool piercing { get; set; }
        public bool slashing { get; set; }
        public bool bludgeoning { get; set; }
        #endregion Properties

        public NaturalWeapon()
        {
            name = string.Empty;
            damage_fine = string.Empty;
            damage_diminutive = string.Empty;
            damage_small = string.Empty;
            damage_medium = string.Empty;
            damage_large = string.Empty;
            damage_huge = string.Empty;
            damage_gargantuan = string.Empty;
            damage_colossal = string.Empty;
            damage_type = string.Empty;
            attack_type = string.Empty;
            piercing = false;
            slashing = false;
            bludgeoning = false;
        }
    }
}
