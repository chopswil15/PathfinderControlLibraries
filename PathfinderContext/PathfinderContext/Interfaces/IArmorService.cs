using PathfinderDomains;

namespace PathfinderContext.Services
{
    public interface IArmorService
    {
        armor FindBy(int id);
        armor GetArmorByName(string name);
    }
}