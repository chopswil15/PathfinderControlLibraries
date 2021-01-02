using EquipmentBasic;

namespace EquipmentBusiness
{
    public interface IWeaponBusiness
    {
        Weapon GetWeaponByName(string name);
    }
}