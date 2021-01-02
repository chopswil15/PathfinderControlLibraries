using StatBlockCommon.Affliction_SB;

namespace StatBlockBusiness
{
    public interface IAfflictionStatBlockBusiness
    {
        AfflictionStatBlock GetAfflictionByName(string name);
    }
}