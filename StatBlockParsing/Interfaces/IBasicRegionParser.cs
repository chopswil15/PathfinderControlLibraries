namespace StatBlockParsing
{
    public interface IBasicRegionParser
    {
        void ParseBasic(string Basic, ref string ErrorMessage, string CR, string TAB);
    }
}