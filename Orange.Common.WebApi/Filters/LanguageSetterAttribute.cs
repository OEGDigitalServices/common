using Orange.Common.Utilities;
using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Unity;

namespace Orange.Common.WebApi
{
    public class LanguageSetterAttribute : ActionFilterAttribute, IActionFilter
    {
        [Dependency]
        public ILogger _logger { get; set; }

        [Dependency]
        public IServicesUtilties _servicesUtilities { get; set; }

        [Dependency]
        public IUtilities _utilities { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                var header = _servicesUtilities.GetHeaderData(actionContext);
                if (IsValidLanguage(header.Language))
                {
                    System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(header.Language);
                    return;
                }

                //_logger.LogDebug(Strings.ErrorDescriptions.LanguageHeaderMissingOrInvalidErrorLog.Replace("{{url}}", actionContext.Request.RequestUri.AbsoluteUri));
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed)
                {
                    Content = new StringContent(Strings.ErrorDescriptions.LanguageHeaderMissingOrInvalid)
                };
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            }
        }
        private bool IsValidLanguage(string language)
        {
            if (!_utilities.ValidateLanguageInput(language))
                return false;
            return true;
        }
    }
}
