using Orange.Common.DataAccess;
using System;

namespace Orange.Common.Business
{
    public class ChannelsTokensManager : IChannelsTokensManager
    {
        private readonly IChannelsTokensDataAccess _channelsTokensDataAccess;
        public ChannelsTokensManager(IChannelsTokensDataAccess channelsTokensDataAccess)
        {
            _channelsTokensDataAccess = channelsTokensDataAccess;
        }
        public Entities.ChannelToken GetByHashed(Guid tokenValue)
        {
            var token = _channelsTokensDataAccess.GetByHashed(tokenValue);
            if (token == null)
                return null;
            return new Entities.ChannelToken { Channel = token.Channel, ErrorDescription = token.ErrorDescription, CreatedDate = token.CreatedDate, ErrorCode = token.ErrorCode, ConsumptionStatus = token.ConsumptionStatus, TokenValue = token.TokenValue, ID = token.ID, ModifiedDate = token.ModifiedDate, RequestBody = token.RequestBody, RequestedService = token.RequestedService, ResponseTimeForConsumptionInSeconds = token.ResponseTimeForConsumptionInSeconds, ResponseTimeForCreationInSeconds = token.ResponseTimeForCreationInSeconds, ServerIP = token.ServerIP, TokenHashedValue = token.TokenHashedValue };
        }

        public void Update(Entities.ChannelToken token)
        {
            var dbToken = new EntityFramework.ChannelToken { ErrorDescription = token.ErrorDescription, ErrorCode = token.ErrorCode, Channel = token.Channel, CreatedDate = token.CreatedDate, TokenValue = token.TokenValue, ModifiedDate = token.ModifiedDate, ResponseTimeForConsumptionInSeconds = token.ResponseTimeForConsumptionInSeconds, ConsumptionStatus = token.ConsumptionStatus, RequestedService = token.RequestedService, RequestBody = token.RequestBody, ID = token.ID, TokenHashedValue = token.TokenHashedValue, ServerIP = token.ServerIP, ResponseTimeForCreationInSeconds = token.ResponseTimeForCreationInSeconds };
            _channelsTokensDataAccess.Update(dbToken);
        }
    }
}
