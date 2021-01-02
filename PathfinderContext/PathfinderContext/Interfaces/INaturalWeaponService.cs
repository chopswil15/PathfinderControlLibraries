using PathfinderDomains;

namespace PathfinderContext.Services
{
    public interface INaturalWeaponService
    {
        natural_weapon GetNaturalWeaponByName(string name);
    }
}