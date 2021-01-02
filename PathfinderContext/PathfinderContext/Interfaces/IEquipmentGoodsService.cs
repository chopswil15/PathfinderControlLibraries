using PathfinderDomains;

namespace PathfinderContext.Services
{
    public interface IEquipmentGoodsService
    {
        goods_services GetGoodsServicesByName(string name);
    }
}