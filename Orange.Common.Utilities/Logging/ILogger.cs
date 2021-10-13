using System;

namespace Orange.Common.Utilities

{
    public interface ILogger
    {
        void LogError(string message, Exception exception, bool rethrowException = false);
        void LogDebug(string message);
    }
}
