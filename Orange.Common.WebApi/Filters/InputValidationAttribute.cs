using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using Orange.Common.Business;
using Orange.Common.Entities;
using Orange.Common.Utilities;
using Unity;

namespace Orange.Common.WebApiFramework
{
    public class InputValidationAttribute : ActionFilterAttribute
    {
        [Dependency]
        public ILogger Logger { get; set; }
        private string ctrl, action;
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                var input = GetRequestBody(actionContext);
                if (input == null)
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            catch (Exception exp)
            {
                Logger.LogError(exp.Message, exp, false);
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            }

        }
        private Input GetRequestBody(HttpActionContext actionContext)
        {
            var stream = new StreamReader(actionContext.Request.Content.ReadAsStreamAsync().Result);
            stream.BaseStream.Position = 0;
            var rawRequest = stream.ReadToEnd();

            return JsonConvert.DeserializeObject<Input>(rawRequest);
        }
    }
}
