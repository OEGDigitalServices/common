using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orange.Common.DataAccess;

namespace Orange.Common.Business
{
   public class ChannelsPrivilegesManager : IChannelsPrivilegesManager
    {
        private readonly IChannelsPrivilegesDataAccess _channelsPrivilegesDataAccess;
        public ChannelsPrivilegesManager(IChannelsPrivilegesDataAccess channelsPrivilegesDataAccess)
        {
            _channelsPrivilegesDataAccess = channelsPrivilegesDataAccess;
        }
        public bool Validate(string channel,string method)
        {
            return _channelsPrivilegesDataAccess.Validate(channel,method);
        }
    }
}
