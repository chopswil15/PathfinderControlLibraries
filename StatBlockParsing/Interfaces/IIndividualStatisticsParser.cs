namespace StatBlockParsing
{
    public interface IIndividualStatisticsParser
    {
        string ParseIndividualStatistics(ref string Statistics, string CR);
    }
}