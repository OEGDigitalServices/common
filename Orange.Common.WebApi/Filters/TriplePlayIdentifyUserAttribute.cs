using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using Orange.Common.Business;
using Orange.Common.Business.OrangeTriplePlay;
using Orange.Common.Entities;
using Orange.Common.Entities.OrangeTriplePlay;
using Orange.Common.Utilities;
using Unity;

namespace Orange.Common.WebApi
{
    public class TriplePlayIdentifyUserAttribute : ActionFilterAttribute
    {
        [Dependency]
        public IServicesFailedRequestsManager ServicesFailedRequestsManager { get; set; }
        [Dependency]
        public ILogger Logger { get; set; }
        [Dependency]
        public IOrangeTPManager OrangeTPManager;

        public string Channel { get; set; }
        public string ModuleName { get; set; }

        private string ctrl, action;

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                ctrl = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
                action = actionContext.ActionDescriptor.ActionName;

                var input = GetRequestBody<TPInput>(actionContext);
                if(input == null)
                {
                    ServicesFailedRequestsManager.Add(new ServicesFailedRequest { ErrorCode = (int)ServiceFailedRequestsErrorCodes.InputIsNull, ControllerName = ctrl, ActionName = action });
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
                }
                if(!OrangeTPManager.IsUserIdentified(input,Channel, ModuleName))
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            }
            catch (Exception exp)
            {
                Logger.LogError(exp.Message, exp, false);
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            }
        }
        private T GetRequestBody<T>(HttpActionContext actionContext)
        {
            var stream = new StreamReader(actionContext.Request.Content.ReadAsStreamAsync().Result);

            stream.BaseStream.Position = 0;
            var rawRequest = stream.ReadToEnd();

            return JsonConvert.DeserializeObject<T>(rawRequest);
        }


    }
}
