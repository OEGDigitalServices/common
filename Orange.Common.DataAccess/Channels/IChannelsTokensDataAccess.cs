using Orange.Common.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.DataAccess
{
    public interface IChannelsTokensDataAccess
    {
        ChannelToken GetByHashed(Guid tokenValue);
        void Update(ChannelToken token);
    }
}
