using Orange.Common.Business;
using Orange.Common.Entities;
using Orange.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Unity;
using ChannelToken = Orange.Common.Entities.ChannelToken;
using Strings = Orange.Common.Business.Strings;

namespace Orange.Common.WebApi
{
    public class TokenAuthenticationAttribute : ActionFilterAttribute
    {
        [Dependency]
        public IServicesFailedRequestsManager ServicesFailedRequestsManager { get; set; }
        [Dependency]
        public IChannelsTokensManager ChannelsTokensManager { get; set; }
        [Dependency]
        public ISecurityUtilities SecurityUtilities { get; set; }
        [Dependency]
        public IUtilities Utilities { get; set; }
        [Dependency]

        public ILogger ILogger { get; set; }

        private string ctrl, action;

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                if (!CheckTestingEnvironment())
                    return;
                
                var requestStartDate = DateTime.Now;
                ctrl = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
                action = actionContext.ActionDescriptor.ActionName;
                var htv = actionContext.Request.Headers.FirstOrDefault(a => a.Key == Strings.Keys.Htv);
                var ctv = actionContext.Request.Headers.FirstOrDefault(a => a.Key == Strings.Keys.Ctv);
                if (!CheckPlainAndHashed(htv, ctv))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
                    return;
                }
                if (!Guid.TryParse(ctv.Value.FirstOrDefault(), out var authHeaderClear))
                {
                    ServicesFailedRequestsManager.Add(new ServicesFailedRequest { ErrorCode = (int)ServiceFailedRequestsErrorCodes.InvalidCtvValue, ControllerName = ctrl, ActionName = action });
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
                    return;
                }

                ChannelToken token = ValidateToken(authHeaderClear, requestStartDate);
                if (token == null)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
                    return;
                }
                if (!ValidateTokenValue(ctv.Value.FirstOrDefault(), htv.Value.FirstOrDefault(), token, requestStartDate))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
                    return;
                }

            }
            catch (Exception exp)
            {
                ILogger.LogError(exp.Message, exp, false);
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            }

        }

        private bool CheckTestingEnvironment()
        {
            bool.TryParse(Utilities.GetAppSetting(Strings.AppSettings.IsTokenEnabled), out bool isEnabled);
            return isEnabled;
        }

        private bool CheckPlainAndHashed(KeyValuePair<string, IEnumerable<string>> htv, KeyValuePair<string, IEnumerable<string>> ctv)
        {
            if (htv.Value == null || htv.Value.FirstOrDefault() == null)
            {
                ServicesFailedRequestsManager.Add(new ServicesFailedRequest { ErrorCode = (int)ServiceFailedRequestsErrorCodes.ChannelNameIsNull, ControllerName = ctrl, ActionName = action });
                return false;
            }
            if (ctv.Value == null || ctv.Value.FirstOrDefault() == null)
            {
                ServicesFailedRequestsManager.Add(new ServicesFailedRequest { ErrorCode = (int)ServiceFailedRequestsErrorCodes.ChannelPasswordIsNull, ControllerName = ctrl, ActionName = action });
                return false;
            }
            return true;
        }
        private ChannelToken ValidateToken(Guid authHeaderClear, DateTime requestStartDate)
        {
            var token = ChannelsTokensManager.GetByHashed(authHeaderClear);
            if (token == null)
            {
                ServicesFailedRequestsManager.Add(new ServicesFailedRequest { ErrorCode = (int)ServiceFailedRequestsErrorCodes.TokenIsInvalid, ControllerName = ctrl, ActionName = action });
                return null;
            }

            int.TryParse(Utilities.GetAppSetting(Strings.AppSettings.ServicesAuthorizationTokenLifeTimeInSeconds), out var tokenLifeTimeInSeconds);
            if ((DateTime.Now - token.CreatedDate.Value).TotalSeconds < tokenLifeTimeInSeconds)
                return token;

            token.ConsumptionStatus = TokenConsumptionStatus.Expired.ToString();
            token.RequestedService = string.Format("{0}/{1}", ctrl, action);
            token.ModifiedDate = requestStartDate;
            ChannelsTokensManager.Update(token);
            return null;
        }
        private bool ValidateTokenValue(string clear, string hashed, ChannelToken token, DateTime requestStartDate)
        {
            var salt = GetSaltByChannel(token.Channel.ToLower());
            token.RequestedService = string.Format("{0}/{1}", ctrl, action);
            token.ModifiedDate = requestStartDate;
            if (!SecurityUtilities.VerifyHashedData(hashed, string.Concat(clear, salt)))
            {
                token.ConsumptionStatus = TokenConsumptionStatus.InvalidHashing.ToString();
                ChannelsTokensManager.Update(token);
                return false;
            }

            token.ConsumptionStatus = TokenConsumptionStatus.Consumed.ToString();
            ChannelsTokensManager.Update(token);
            return true;
        }
        private string GetSaltByChannel(string channel)
        {
            return Utilities.GetAppSetting(string.Concat(channel, Strings.AppSettings.TokenHashKey));
        }
    }
}
