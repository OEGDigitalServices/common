using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Entities
{
    public interface SecureOutput<T> where T : struct, IConvertible
    {
        T ErrorCode { get; set; }
        string ErrorDescription { get; set; }
    }

}
