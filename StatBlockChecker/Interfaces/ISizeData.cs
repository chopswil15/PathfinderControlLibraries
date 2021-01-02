using CommonStatBlockInfo;

namespace StatBlockChecker
{
    public interface ISizeData
    {
        StatBlockInfo.SizeCategories SizeCat { get; set; }
        int SizeMod { get; set; }
    }
}