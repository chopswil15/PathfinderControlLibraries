using StatBlockCommon.MagicItem_SB;

namespace StatBlockBusiness
{
    public interface IMagicItemStatBlockBusiness
    {
        MagicItemStatBlock GetMagicItemByName(string name);
        MagicItemStatBlock GetMagicItemByNameSource(string name, string source);
    }
}