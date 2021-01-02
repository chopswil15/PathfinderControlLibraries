using PathfinderDomains;

namespace PathfinderContext.Services
{
    public interface IWeaponService
    {
        weapon GetWeaponByName(string name);
    }
}