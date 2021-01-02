using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonInterFacesDD
{
    public interface IEquipment
    {
        EquipmentType EquipmentType { get; }
        bool Masterwork { get; set; }
        bool Broken { get; set; }
    }

    public enum EquipmentType
    {
        Armor = 1,
        Weapon = 2,
        Goods = 3,
        MagicItem = 4,
        Potion = 5,
        Wand = 6,
        Scroll = 7,
        NaturalWeapon = 8,
        Oil
    }
}
