using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Business
{
    public interface IChannelsPrivilegesManager
    {
        bool Validate(string channel, string method);
    }
}
