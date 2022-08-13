using System;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Orange.Common.Profile;
using Orange.Common.Utilities;
using Unity;

namespace Orange.Common.WebApi
{
    public class PortalInjectLoggedInDialAttribute : ActionFilterAttribute
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

                var loggedInDial = ProfileUtilities.GetLoggedInDial();
                if (loggedInDial == null) return;

                var selectedUserDial = ProfileUtilities.GetCurrentDial();
                if (selectedUserDial == null) return;

                var obj = actionContext.ActionArguments["input"];

                if (obj.GetType().GetProperty("Dial") != null && obj.GetType().GetProperty("Dial").PropertyType == typeof(string))
                {
                    obj.GetType().GetProperty("Dial").SetValue(obj, loggedInDial);
                }
                var selectedUserDialProperty = obj.GetType().GetProperty("SelectedUserDial");
                if (selectedUserDialProperty != null && selectedUserDialProperty.PropertyType == typeof(string))
                {
                    selectedUserDialProperty.SetValue(obj, selectedUserDial);
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