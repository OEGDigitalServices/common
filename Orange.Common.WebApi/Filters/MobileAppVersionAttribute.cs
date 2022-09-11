using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Orange.Common.Entities;
using Orange.Common.Utilities;
using Unity;

namespace Orange.Common.WebApi
{
    public class MobileAppVersionAttribute : ActionFilterAttribute
    {
        [Dependency]
        public ILogger Logger { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                var appVersion = actionContext.Request.Headers.FirstOrDefault(a => a.Key.ToLower() == Strings.Keys.AppVersion);
                var osVersion = actionContext.Request.Headers.FirstOrDefault(a => a.Key.ToLower() == Strings.Keys.OSVersion);
                var isAndroid = actionContext.Request.Headers.FirstOrDefault(a => a.Key.ToLower() == Strings.Keys.IsAndroid);
                bool _isAndroid;
                if (isAndroid.Value == null || isAndroid.Value.FirstOrDefault() == null)
                    _isAndroid = false;
                else
                    _isAndroid = isAndroid.Value.FirstOrDefault().ToLower() != Strings.Keys.False;

                if (!(actionContext.ActionArguments.ContainsKey("input") && actionContext.ActionArguments["input"] is MobileInput input)) return;
                input.AppVersion = !appVersion.Equals(default(KeyValuePair<string, IEnumerable<string>>)) && !string.IsNullOrEmpty(appVersion.Value?.FirstOrDefault()) ? appVersion.Value.FirstOrDefault() : string.Empty;
                input.OsVersion = !osVersion.Equals(default(KeyValuePair<string, IEnumerable<string>>)) && !string.IsNullOrEmpty(osVersion.Value?.FirstOrDefault()) ? osVersion.Value.FirstOrDefault() : string.Empty;
                input.IsAndroid = _isAndroid;
                actionContext.ActionArguments["input"] = input;
            }
            catch (Exception exp)
            {
                Logger.LogError(exp.Message, exp, false);
            }
        }
    }
}
