using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Security;
using Orange.Common.Business;
using Orange.Common.Entities;
using Orange.Common.Utilities;
using Strings = Orange.Common.Business.Strings;
using Newtonsoft.Json;
using Unity;
using Orange.Common.DataAccess.ProfileContexts;

namespace Orange.GSM.Common.WebApiFramework
{
    public class AuthenticationBodyAttribute : ActionFilterAttribute
    {
        [Dependency]
        public IServicesFailedRequestsManager ServicesFailedRequestsManager { get; set; }
        [Dependency]
        public ILogger ILogger { get; set; }
        [Dependency]
        public IUtilities IUtilities { get; set; }
        private string ctrl, action;
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                ctrl = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
                action = actionContext.ActionDescriptor.ActionName;
                var claims = GetRequestBody(actionContext);
                var isEasyLogin = actionContext.Request.Headers.FirstOrDefault(a => a.Key.ToLower() == Strings.Keys.IsEasyLogin);
                var channelInfo = GetChannelFromBodyOrHeader(claims, actionContext);

                bool _isEasyLogin;
                if (isEasyLogin.Value == null || isEasyLogin.Value.FirstOrDefault() == null)
                    _isEasyLogin = false;
                else
                    _isEasyLogin = isEasyLogin.Value.FirstOrDefault().ToLower() != Strings.Keys.False;

                if (ValidateDialAndPassword(claims) && AuthenticateUserUsingDialAndPassword(claims, _isEasyLogin, channelInfo?.Name))
                    return;

                actionContext.Response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            }
            catch (Exception exp)
            {
                ILogger.LogError(exp.Message, exp, false);
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            }

        }

        private ChannelClaims GetRequestBody(HttpActionContext actionContext)
        {
            var stream = new StreamReader(actionContext.Request.Content.ReadAsStreamAsync().Result);

            stream.BaseStream.Position = 0;
            var rawRequest = stream.ReadToEnd();

            return JsonConvert.DeserializeObject<ChannelClaims>(rawRequest);
        }
        private ChannelInfo GetChannelFromBodyOrHeader(ChannelClaims claims, HttpActionContext actionContext)
        {
            if (claims == null)
                return null;
            if (string.IsNullOrEmpty(claims.ChannelName))
            {
                var channelName = actionContext.Request.Headers.FirstOrDefault(a => a.Key.ToLower() == Strings.Keys.ChannelName);
                return new ChannelInfo { Name = channelName.Value.FirstOrDefault() };
            }
            return new ChannelInfo { Name = claims.ChannelName, Password = claims.ChannelPassword };
        }
        private bool ValidateDialAndPassword(OrangeClaims claims)
        {
            if (claims == null)
            {
                ServicesFailedRequestsManager.Add(new ServicesFailedRequest { ErrorCode = (int)ServiceFailedRequestsErrorCodes.DialAndPasswordAreNull, ErrorDescription = "", ControllerName = ctrl, ActionName = action });
                return false;
            }
            if (string.IsNullOrWhiteSpace(claims.Dial))
            {
                ServicesFailedRequestsManager.Add(new ServicesFailedRequest { ErrorCode = (int)ServiceFailedRequestsErrorCodes.DialIsNull, ErrorDescription = "", ControllerName = ctrl, ActionName = action });
                return false;
            }
            if (!IUtilities.IsValidDial(claims.Dial))
            {
                ServicesFailedRequestsManager.Add(new ServicesFailedRequest { ErrorCode = (int)ServiceFailedRequestsErrorCodes.DialIsInvalid, ControllerName = ctrl, ActionName = action });
                return false;
            }
            if (string.IsNullOrWhiteSpace(claims.Password))
            {
                ServicesFailedRequestsManager.Add(new ServicesFailedRequest { Dial = claims.Dial, ErrorCode = (int)ServiceFailedRequestsErrorCodes.PasswordIsNull, ControllerName = ctrl, ActionName = action });
                return false;
            }
            return true;
        }
        private bool AuthenticateUserUsingDialAndPassword(OrangeClaims claims, bool isEasyLogin, string channelName)
        {
            if (!isEasyLogin && !Membership.ValidateUser(claims.Dial, claims.Password))
            {
                ServicesFailedRequestsManager.Add(new ServicesFailedRequest { Dial = claims.Dial, ErrorCode = (int)ServiceFailedRequestsErrorCodes.AuthenticationFailed, ControllerName = ctrl, ActionName = action });
                return false;
            }
            if (isEasyLogin && !new AuthenticationContext(ILogger).ValidateEasyLoginUser(claims.Dial, claims.Password, channelName))
            {
                ServicesFailedRequestsManager.Add(new ServicesFailedRequest { Dial = claims.Dial, ErrorCode = (int)ServiceFailedRequestsErrorCodes.AuthenticationFailed, ControllerName = ctrl, ActionName = action });
                return false;
            }
            return true;
        }

    }


}
