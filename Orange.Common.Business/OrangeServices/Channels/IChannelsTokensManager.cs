using Orange.Common.Entities;
using Orange.Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Business
{
    public interface IChannelsTokensManager
    {
        Entities.ChannelToken GetByHashed(Guid tokenValue);
        void Update(Entities.ChannelToken token);
    }
}
