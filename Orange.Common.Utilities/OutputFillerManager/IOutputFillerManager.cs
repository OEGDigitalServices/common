using Orange.Common.Entities;
using System;

namespace Orange.Common.Utilities
{
    public interface IOutputFillerManager
    {
        void FillOutput(BaseOutput output, int errorCode, string errorMessage, string internalErrorMessage = null);
        void FillOutput<TOutput>(Output<TOutput> output, int errorCode, string errorMessage, TOutput data, string internalErrorMessage = null);
        T FillOutput<T, ErrorCodeEnum>(T output, ErrorCodeEnum errorCode, string errorDesc)
            where T : SecureOutput<ErrorCodeEnum>
            where ErrorCodeEnum : struct, System.IConvertible;
        T OutputFiller<T, ErrorCodeEnum>(T output, ErrorCodeEnum errorCode, string errorDesc) where T :
            OutputFiller<ErrorCodeEnum> where ErrorCodeEnum : struct, IConvertible;

    }
}
