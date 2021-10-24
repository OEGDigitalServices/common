using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using Orange.Common.Business;
using Orange.Common.Entities;
using Orange.Common.Utilities;
using Unity;

namespace Orange.Common.WebApiFramework
{
    public class ChannelBodyAttribute : ActionFilterAttribute
    {
        [Dependency]
        public IServicesFailedRequestsManager ServicesFailedRequestsManager { get; set; }
        [Dependency]
        public IChannelsDataManager ChannelsDataManager { get; set; }
        [Dependency]
        public IChannelsPrivilegesManager ChannelsPrivilegesManager { get; set; }
        [Dependency]
        public ILogger Logger { get; set; }
        private string ctrl, action;
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                ctrl = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
                action = actionContext.ActionDescriptor.ActionName;

                var claims = GetRequestBody(actionContext);
                var channelInfo = GetChannel(claims);

                if (ValidateChannelInfo(channelInfo, claims.Dial) && ValidateChannelsPrivilegesManager(channelInfo.Name, claims.Dial))
                    return;
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            }
            catch (Exception exp)
            {
                Logger.LogError(exp.Message, exp, false);
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
        private ChannelInfo GetChannel(ChannelClaims claims)
        {
            if (claims == null || (string.IsNullOrEmpty(claims.ChannelName) && string.IsNullOrEmpty(claims.ChannelPassword)))
                return null;
            return new ChannelInfo { Name = claims.ChannelName, Password = claims.ChannelPassword };
        }
        private bool ValidateChannelInfo(ChannelInfo channelInfo, string dial)
        {
            if (channelInfo == null || (string.IsNullOrEmpty(channelInfo.Name) && string.IsNullOrEmpty(channelInfo.Password)))
            {
                ServicesFailedRequestsManager.Add(new ServicesFailedRequest { Dial = dial, ErrorCode = (int)ServiceFailedRequestsErrorCodes.ChannelNameAndPasswordAreNull, ControllerName = ctrl, ActionName = action });
                return false;
            }
            if (string.IsNullOrEmpty(channelInfo.Name))
            {
                ServicesFailedRequestsManager.Add(new ServicesFailedRequest { Dial = dial, ErrorCode = (int)ServiceFailedRequestsErrorCodes.ChannelNameIsNull, ControllerName = ctrl, ActionName = action });
                return false;
            }
            if (string.IsNullOrEmpty(channelInfo.Password))
            {
                ServicesFailedRequestsManager.Add(new ServicesFailedRequest { Dial = dial, ErrorCode = (int)ServiceFailedRequestsErrorCodes.ChannelPasswordIsNull, ControllerName = ctrl, ActionName = action });
                return false;
            }
            if (!ChannelsDataManager.Validate(channelInfo.Name, channelInfo.Password))
            {
                ServicesFailedRequestsManager.Add(new ServicesFailedRequest { Dial = dial, ErrorCode = (int)ServiceFailedRequestsErrorCodes.ChannekInfoIsInvalid, ControllerName = ctrl, ActionName = action });
                return false;
            }
            return true;
        }
        private bool ValidateChannelsPrivilegesManager(string channelName, string dial)
        {
            if (ChannelsPrivilegesManager.Validate(channelName, action))
                return true;

            ServicesFailedRequestsManager.Add(new ServicesFailedRequest { Dial = dial, ErrorCode = (int)ServiceFailedRequestsErrorCodes.MethodHasInsufficientPrivilege, ControllerName = ctrl, ActionName = action });
            return false;
        }
    }
}
