using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Utilities

{
    public interface ILogger
    {
        void LogError(string message, Exception exception, bool rethrowException = false);
        void LogDebug(string message);
    }
}
