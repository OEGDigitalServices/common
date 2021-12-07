using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orange.Common.DataAccess;
using Orange.Common.EntityFramework.Models;
using Orange.Common.Utilities;

namespace Orange.Common.Business
{
   public class ChannelsDataManager : IChannelsDataManager
    {
        private readonly IChannelsDataDataAccess _channelsDataDataAccess;
        public ChannelsDataManager(IChannelsDataDataAccess channelsDataDataAccess)
        {
            _channelsDataDataAccess = channelsDataDataAccess;
        }
        public bool Validate(string name,string password)
        {
            return _channelsDataDataAccess.Validate(name,password);
        }
    }
}
