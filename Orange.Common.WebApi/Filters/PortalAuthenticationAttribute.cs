using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Orange.Common.Profile;
using Orange.Common.Utilities;
using Unity;

namespace Orange.Common.WebApi
{
    public class PortalAuthenticationAttribute : ActionFilterAttribute
    {
        [Dependency]
        public ILogger Logger { get; set; }
        [Dependency]
        public IProfileUtilities ProfileUtilities { get; set; }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                if (!ProfileUtilities.IsAuthenticated())
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            catch (Exception exp)
            {
                Logger.LogError(exp.Message, exp, false);
            }
        }
    }
}
