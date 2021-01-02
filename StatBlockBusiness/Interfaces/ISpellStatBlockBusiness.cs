

using StatBlockCommon;

namespace StatBlockBusiness
{
    public interface ISpellStatBlockBusiness
    {
        ISpellStatBlock FindById(int Id);
        ISpellStatBlock GetSpellByName(string name);
    }
}