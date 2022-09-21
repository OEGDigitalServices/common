using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Profile
{
  public interface IProfileUtilities
    {
        bool IsAuthenticated();
        Guid GetCurrentUserId();
        string GetCurrentDial();
        bool IsRequestFromPortal();
        string GetLoggedInDial();
    }
}
