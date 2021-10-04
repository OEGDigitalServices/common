namespace Orange.Common.Entities
{
    public class BaseOutput
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string InternalError { get; set; }
        public string InternalErrorDescription { get; set; }
    }
}
