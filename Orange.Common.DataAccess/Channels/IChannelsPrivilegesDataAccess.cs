using Orange.Common.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.DataAccess
{
    public interface IChannelsPrivilegesDataAccess
    {
        bool Validate(string channel, string method);
    }
}
