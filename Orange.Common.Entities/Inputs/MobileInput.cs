namespace Orange.Common.Entities
{
    public class MobileInput : Input
    {
        public string AppVersion { get; set; }
        public string OsVersion { get; set; }
        public bool IsAndroid { get; set; }
    }
}
