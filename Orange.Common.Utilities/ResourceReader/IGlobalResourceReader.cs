namespace Orange.TriplePlay.Common.Utilities
{
    public interface IGlobalResourceReader
    {
        string GetValueByKey(string resourceFileName, string resourceKey);
        string GetValueByKey(string resourceFileName, string resourceKey, string cultureName);
    }
}
