namespace StatBlockChecker
{
    public interface ISavesChecker
    {
        void CheckFortValue();
        void CheckRefValue();
        void CheckWillValue();
    }
}