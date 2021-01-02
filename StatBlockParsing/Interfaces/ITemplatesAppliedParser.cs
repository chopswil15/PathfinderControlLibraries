namespace StatBlockParsing
{
    public interface ITemplatesAppliedParser
    {
        void ParseTemplatesApplied(ref string temp, ref string raceLine, string raceLineHold);
    }
}