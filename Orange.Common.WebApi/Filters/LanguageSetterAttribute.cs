using Newtonsoft.Json;
using Orange.Common.Entities;
using Orange.Common.Utilities;
using System;
using System.Globalization;
using System.IO;
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
                var info = GetLanguage(actionContext);
                var header = _servicesUtilities.GetHeaderData(actionContext);
                if (IsValidLanguage(header.Language))
                {
                    SetLanguageFromHeader(actionContext, header);
                    return;
                }
                else if (IsValidLanguage(info.Language))
                {
                    SetLanguageFromBody(actionContext, info);
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
        private LanguageInfo GetLanguage(HttpActionContext actionContext)
        {
            var stream = new StreamReader(actionContext.Request.Content.ReadAsStreamAsync().Result);

            stream.BaseStream.Position = 0;
            var rawRequest = stream.ReadToEnd();

            return JsonConvert.DeserializeObject<LanguageInfo>(rawRequest);
        }

        private bool IsValidLanguage(string language)
        {
            if (!_utilities.ValidateLanguageInput(language))
                return false;
            return true;
        }

        private static void SetLanguageFromHeader(HttpActionContext actionContext, HeaderData header)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(header.Language);
            var obj = actionContext.ActionArguments["input"];
            if (obj is Input)
            {
                var input = obj as Input;
                input.Language = header.Language;
                actionContext.ActionArguments["input"] = input;
            }
            return;
        }
        private static void SetLanguageFromBody(HttpActionContext actionContext, LanguageInfo info)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(info.Language);
            var obj = actionContext.ActionArguments["input"];
            if (obj is Input)
            {
                var input = obj as Input;
                input.Language = info.Language;
                actionContext.ActionArguments["input"] = input;
            }
        }
        
    }
}
