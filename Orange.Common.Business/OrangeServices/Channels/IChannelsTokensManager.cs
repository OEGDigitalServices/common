using System;

namespace Orange.Common.Business
{
    public interface IChannelsTokensManager
    {
        Entities.ChannelToken GetByHashed(Guid tokenValue);
        void Update(Entities.ChannelToken token);
    }
}
