using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Orange.Common.Business;
using Orange.Common.Entities;
using Orange.Common.Utilities;
using Unity;

namespace Orange.Common.WebApi
{
    public class DSLBasicAuthenticationTokenAttribute : ActionFilterAttribute
    {
        [Dependency]
        public IDSLAuthenticationTokenManager DSLAuthenticationTokenManager { get; set; }
        [Dependency]
        public IServicesFailedRequestsManager ServicesFailedRequestsManager { get; set; }
        [Dependency]
        public ILogger ILogger { get; set; }
        [Dependency]
        public IUtilities Utilities { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                var input = GetRequestBody<DSLInput>(actionContext);
                var dslClaims = DSLAuthenticationTokenManager.ValidateToken(input.Dial, input.DSLToken);
                if (dslClaims == null || IsTokenExpired(dslClaims))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    LogToFailedRequests(ServiceFailedRequestsErrorCodes.DSLTokenIsInvalid, actionContext);
                    return;
                }

                var dslInput = (dynamic)actionContext.ActionArguments["input"];
                //if (!(actionContext.ActionArguments.ContainsKey("input") && 
                //actionContext.ActionArguments["input"] is DSLInput dslInput)) return;
                if (dslInput == null)
                    return;
                dslInput.DSLNumber = dslClaims.LandLineNumber;
                dslInput.DSLUserStatus = dslClaims.DSLUserStatus;
                dslInput.IsMigrated = dslClaims.IsMigrated;
                dslInput.DSLToken = dslClaims.Token.Value;
                dslInput.UserName = dslClaims.UserName;
                dslInput.UCID = dslClaims.UCID;
                dslInput.CustomerId = dslClaims.CustomerId;
                dslInput.ContractId = dslClaims.ContractId;

                actionContext.ActionArguments["input"] = dslInput;

                base.OnActionExecuting(actionContext);
            }
            catch (Exception exp)
            {
                ILogger.LogError(exp.Message, exp, false);
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            }
        }

        private bool IsTokenExpired(ValidateDSLBasicAuthenticationTokenOutput dslClaims)
        {
            string expiryHours = Utilities.GetAppSetting(Business.Strings.AppSettings.TokenExpiryHours);
            double.TryParse(expiryHours, out double expHours);
            return DateTime.Now.Subtract(dslClaims.CreatedDate.Value).TotalHours >= expHours;
        }

        #region Helpers

        private void LogToFailedRequests(ServiceFailedRequestsErrorCodes errorCode, HttpActionContext actionContext)
        {
            var controllerName = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            var actionName = actionContext.ActionDescriptor.ActionName;
            var input = GetRequestBody<DSLInput>(actionContext);
            ServicesFailedRequestsManager.Add(new ServicesFailedRequest
            {
                ActionName = actionName,
                ControllerName = controllerName,
                Dial = input.Dial,
                ErrorCode = (int)errorCode,
            });
        }
        private T GetRequestBody<T>(HttpActionContext actionContext)
        {
            var stream = new StreamReader(actionContext.Request.Content.ReadAsStreamAsync().Result);

            stream.BaseStream.Position = 0;
            var rawRequest = stream.ReadToEnd();

            return JsonConvert.DeserializeObject<T>(rawRequest);
        }


        #endregion
    }
}