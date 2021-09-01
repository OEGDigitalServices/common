using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orange.Common.EntityFramework.Models;
using Orange.Common.Utilities;

namespace Orange.Common.DataAccess
{
   public class ChannelsPrivilegesDataAccess : IChannelsPrivilegesDataAccess
    {
        private readonly IEntityFramworkUtilties _entityFramworkUtilties;
        public ChannelsPrivilegesDataAccess(IEntityFramworkUtilties entityFramworkUtilties)
        {
            _entityFramworkUtilties = entityFramworkUtilties;
        }
        public  bool Validate(string channel,string method)
        {
            return _entityFramworkUtilties.GetEntity<ChannelsPrivilege,OrangeServicesModel>(dbModel=> Validate(dbModel,channel,method)) != null;
        }

        private ChannelsPrivilege Validate(OrangeServicesModel dbModel, string channel, string method)
        {
            return dbModel.ChannelsPrivileges.FirstOrDefault(c=>c.Channel == channel && c.Method == method);
        }
    }
}
