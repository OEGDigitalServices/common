using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Orange.Common.Business;
using Orange.Common.Utilities;
using Unity;

namespace Orange.Common.WebApi
{
    public class PortalSourceAuthenticationAttribute : ActionFilterAttribute
    {
        [Dependency]
        public ILogger Logger { get; set; }

        [Dependency]
        public IProfileManager ProfileManager { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                if (!ProfileManager.IsRequestFromPortal())
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            catch (Exception exp)
            {
                Logger.LogError(exp.Message, exp, false);
            }
        }
    }
}
