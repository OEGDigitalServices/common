using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Business
{
    public interface IChannelsDataManager
    {
        bool Validate(string name, string password);
    }
}
