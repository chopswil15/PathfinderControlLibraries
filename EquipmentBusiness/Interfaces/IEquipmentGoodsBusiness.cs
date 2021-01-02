using EquipmentBasic;

namespace EquipmentBusiness
{
    public interface IEquipmentGoodsBusiness
    {
        EquipmentGoods GetGoodsServicesByName(string name);
    }
}