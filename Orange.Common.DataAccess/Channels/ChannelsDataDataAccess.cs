using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orange.Common.EntityFramework.Models;
using Orange.Common.Utilities;

namespace Orange.Common.DataAccess
{
   public class ChannelsDataDataAccess : IChannelsDataDataAccess
    {
        private readonly IEntityFramworkUtilties _entityFramworkUtilties;
        public ChannelsDataDataAccess(IEntityFramworkUtilties entityFramworkUtilties)
        {
            _entityFramworkUtilties = entityFramworkUtilties;
        }
        public  bool Validate(string name,string password)
        {
            return _entityFramworkUtilties.GetEntity<ChannelsData,OrangeServicesModel>(dbModel=> Validate(dbModel,name,password)) != null;
        }

        private ChannelsData Validate(OrangeServicesModel dbModel, string name, string password)
        {
            return dbModel.ChannelsDatas.FirstOrDefault(c=>c.ChannelName == name && c.Password == password);
        }
    }
}
