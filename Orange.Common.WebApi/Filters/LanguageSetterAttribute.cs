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
                var lang = GetLanguageFromHeaderOrBody(actionContext);
                if (IsValidLanguage(lang))
                {
                    SetLanguageToInput(actionContext, lang);
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
        private string GetLanguageFromHeaderOrBody(HttpActionContext actionContext)
        {
            var header = _servicesUtilities.GetHeaderData(actionContext);
            if (!string.IsNullOrEmpty(header?.Language))
                return header?.Language;

            var stream = new StreamReader(actionContext.Request.Content.ReadAsStreamAsync().Result);
            stream.BaseStream.Position = 0;
            var rawRequest = stream.ReadToEnd();
            var langInfo = JsonConvert.DeserializeObject<LanguageInfo>(rawRequest);
            
            return langInfo?.Language;
        }

        private bool IsValidLanguage(string language)
        {
            if (!_utilities.ValidateLanguageInput(language))
                return false;
            return true;
        }

        private static void SetLanguageToInput(HttpActionContext actionContext, string lang)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            var obj = actionContext.ActionArguments["input"];
            if (obj is Input)
            {
                var input = obj as Input;
                input.Language = lang;
                actionContext.ActionArguments["input"] = input;
            }
            return;
        }
    }
}
