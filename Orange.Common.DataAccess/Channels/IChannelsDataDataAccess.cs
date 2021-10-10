using Orange.Common.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.DataAccess
{
    public interface IChannelsDataDataAccess
    {
        bool Validate(string name, string password);
    }
}
