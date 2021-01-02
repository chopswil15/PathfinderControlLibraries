namespace StatBlockParsing
{
    public interface IStatisticsRegionParser
    {
        string ParseStatistics(string Statistics, string CR, out string ErrorMessage);
    }
}