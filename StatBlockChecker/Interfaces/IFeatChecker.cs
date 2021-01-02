namespace StatBlockChecker
{
    public interface IFeatChecker
    {
        void CheckFeatCount(string Feats);
        void CheckFeatPreReqs();
    }
}