using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orange.Common.EntityFramework;
using Orange.Common.EntityFramework.Models;
using Orange.Common.Utilities;

namespace Orange.Common.DataAccess
{
    public class ChannelsTokensDataAccess : IChannelsTokensDataAccess
    {
        private readonly IEntityFramworkUtilties _entityFramworkUtilties;
        public ChannelsTokensDataAccess(IEntityFramworkUtilties entityFramworkUtilties)
        {
            _entityFramworkUtilties = entityFramworkUtilties;
        }
        public  ChannelToken GetByHashed(Guid tokenValue)
        {
            return _entityFramworkUtilties.GetEntity<ChannelToken,OrangeServicesModel>(dbModel=> GetByHashed(dbModel,tokenValue));
        }

        private ChannelToken GetByHashed(OrangeServicesModel dbModel, Guid tokenValue)
        {
            return dbModel.ChannelTokens.FirstOrDefault(t=>t.TokenValue == tokenValue);
        }

        public void Update(ChannelToken token)
        {
            _entityFramworkUtilties.SaveChanges<OrangeServicesModel>(dbModel=>Update(dbModel,token));
        }

        private void Update(OrangeServicesModel dbModel, ChannelToken token)
        {
            token.ResponseTimeForConsumptionInSeconds=(DateTime.Now-token.ModifiedDate).Value.Seconds;
            token.ModifiedDate = DateTime.Now;
            dbModel.Entry(token).State = EntityState.Modified;
        }
    }
}
