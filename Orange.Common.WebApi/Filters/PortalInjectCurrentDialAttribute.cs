using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Orange.Common.Entities;
using Orange.Common.Profile;
using Orange.Common.Utilities;
using Unity;

namespace Orange.Common.WebApi
{ 
    public class PortalInjectCurrentDialAttribute : ActionFilterAttribute
    {
        [Dependency]
        public ILogger Logger { get; set; }
        [Dependency]
        public IProfileUtilities ProfileUtilities { get; set; }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                if (!(actionContext.ActionArguments.ContainsKey("input"))) return;
                var currentDial = ProfileUtilities.GetCurrentDial();
                if (currentDial == null) return;

                var obj = actionContext.ActionArguments["input"];

                if(obj.GetType().GetProperty("Dial") != null && obj.GetType().GetProperty("Dial").PropertyType == typeof(string))
                {
                    obj.GetType().GetProperty("Dial").SetValue(obj, currentDial);
                }
                if (obj.GetType().GetProperty("UserId") != null && obj.GetType().GetProperty("UserId").PropertyType == typeof(Guid))
                {
                    obj.GetType().GetProperty("UserId").SetValue(obj, ProfileUtilities.GetCurrentUserId());
                }
            }
            catch (Exception exp)
            {
                Logger.LogError(exp.Message, exp, false);
            }

        }
    }
}
