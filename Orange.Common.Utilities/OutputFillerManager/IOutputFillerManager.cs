using Orange.Common.Entities;

namespace Orange.Common.Utilities
{
    public interface IOutputFillerManager
    {
        void FillOutput(BaseOutput output, int errorCode, string errorMessage, string internalErrorMessage = null);
        void FillOutput<TOutput>(Output<TOutput> output, int errorCode, string errorMessage, TOutput data, string internalErrorMessage = null);
    }
}
