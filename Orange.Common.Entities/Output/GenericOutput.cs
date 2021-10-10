using System;
using System.Collections.Generic;

namespace Orange.Common.Entities
{
    public class GenericOutput<T, U> where T : struct, IConvertible where U : class
    {
        public T ErrorCode { get; set; }
        public IList<U> ValidationErrors { get; set; }
    }
}
