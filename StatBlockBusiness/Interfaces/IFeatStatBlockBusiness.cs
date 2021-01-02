

using StatBlockCommon;

namespace StatBlockBusiness
{
    public interface IFeatStatBlockBusiness
    {
        IFeatStatBlock GetFeatByName(string name);
        IFeatStatBlock GetFeatByNameSource(string name, string source);
        IFeatStatBlock GetMythicFeatByName(string name);
    }
}