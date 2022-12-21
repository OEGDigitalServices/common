

using Orange.Common.Entities;
using System;

namespace Orange.Common.Utilities
{
    public class OutputFillerManager : IOutputFillerManager
    {
        public void FillOutput(BaseOutput output, int errorCode, string errorMessage, string internalErrorMessage = null)
        {
            output.ErrorCode = errorCode;
            output.ErrorMessage = errorMessage;
            output.InternalErrorDescription = internalErrorMessage;
        }

        public void FillOutput<TOutput>(Output<TOutput> output, int errorCode, string errorMessage, TOutput data, string internalErrorMessage = null)
        {
            FillOutput(output, errorCode, errorMessage, internalErrorMessage);
            output.Data = data;
        }
        public T FillOutput<T, ErrorCodeEnum>(T output, ErrorCodeEnum errorCode, string errorDesc) where T : SecureOutput<ErrorCodeEnum> where ErrorCodeEnum : struct, IConvertible
        {
            output.ErrorCode = errorCode;
            output.ErrorDescription = errorDesc;
            return output;
        }
        public T OutputFiller<T, ErrorCodeEnum>(T output, ErrorCodeEnum errorCode, string errorDesc) where T : OutputFiller<ErrorCodeEnum> where ErrorCodeEnum : struct, IConvertible
        {
            output.ErrorCode = errorCode;
            output.ErrorDescription = errorDesc;
            return output;
        }
    }
}
