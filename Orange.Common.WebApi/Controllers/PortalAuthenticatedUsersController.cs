using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Orange.Common.WebApi;

namespace Orange.Common.WebApi
{
    [PortalAuthentication]
    [PortalInjectCurrentDial]
    public class PortalAuthenticatedUsersController : PortalBasicController
    {
    }
}
