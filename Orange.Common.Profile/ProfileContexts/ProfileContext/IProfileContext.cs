using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Profile
{
    public interface IProfileContext
    {
        UserTicketData CurrectUserTicketData { get; }
        UserTicketData GetUserDataFromUserTicket(string userData);
    }
}
