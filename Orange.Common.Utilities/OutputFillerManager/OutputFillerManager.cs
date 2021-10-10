

using Orange.Common.Entities;

namespace Orange.TriplePay.Common.Utilities
{
    public class OutputFillerManager : IOutputFillerManager
    {
        public void FillOutput(BaseOutput output, int errorCode, string errorMessage, string internalErrorMessage = null)
        {
            output.ErrorCode = errorCode;
            output.ErrorMessage = errorMessage;
            output.InternalErrorMessage = internalErrorMessage;
        }

        public void FillOutput<TOutput>(Output<TOutput> output, int errorCode, string errorMessage, TOutput data, string internalErrorMessage = null)
        {
            FillOutput(output, errorCode, errorMessage, internalErrorMessage);
            output.Data = data;
        }

    }
}
