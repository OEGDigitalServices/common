using System;

namespace Orange.Common.Entities
{
    public interface OutputFiller<T> where T : struct, IConvertible
    {
        T ErrorCode { get; set; }
        string ErrorDescription { get; set; }
    }
}
