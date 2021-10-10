namespace Orange.Common.Entities
{
    public class Output<TModel> : BaseOutput
    {
        public TModel Data { get; set; }
        public Output()
        {

        }
        public Output(int errorCode, string errorDescription, TModel data)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorDescription;
            Data = data;
        }
    }
}
