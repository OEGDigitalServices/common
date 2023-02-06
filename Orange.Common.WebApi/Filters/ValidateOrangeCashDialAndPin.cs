using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using Orange.Common.Business;
using Orange.Common.Entities;
using Orange.Common.Utilities;
using Unity;

namespace Orange.Common.WebApi
{
    public class ValidateOrangeCashDialAndPin : ActionFilterAttribute
    {
        [Dependency]
        public IOrangeCashManager _orangeCashManager { get; set; }
        [Dependency]
        public IUtilities _utilities { get; set; }
        [Dependency]
        public IServicesFailedRequestsManager _servicesFailedRequestsManager { get; set; }
        [Dependency]
        public ILogger _logger { get; set; }
        private string ctrl, action;
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                ctrl = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
                action = actionContext.ActionDescriptor.ActionName;
                var claims = GetRequestBody(actionContext);
                if (ValidateDialAndPin(claims))
                    return;
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            }
        }

        private bool ValidateDialAndPin(OrangeCashInput claims)
        {
            if (!ValidateDial(claims))
                return false;

            var walletBalanceOutput = _orangeCashManager.CheckDialAndPin(claims);
            if (walletBalanceOutput.ErrorCode == WalletBalanceInquiryOutput.ErrorCodes.LockedAccount)
            {
                _servicesFailedRequestsManager.Add(new ServicesFailedRequest { Dial = claims.Dial, ErrorCode = (int)ServiceFailedRequestsErrorCodes.AccountIsLocked, ControllerName = ctrl, ActionName = action, ErrorDescription = Common.Utilities.Strings.ErrorDescriptions.AccountIsLocked });
                return false;
            }
            if (walletBalanceOutput.ErrorCode != WalletBalanceInquiryOutput.ErrorCodes.Success)
            {
                _servicesFailedRequestsManager.Add(new ServicesFailedRequest { Dial = claims.Dial, ErrorCode = (int)ServiceFailedRequestsErrorCodes.WalletBalanceUnspecifiedError, ControllerName = ctrl, ActionName = action, ErrorDescription = string.Format(Common.Utilities.Strings.ErrorDescriptions.WalletBalanceUnspecifiedError, walletBalanceOutput.ErrorDescription) });
                return false;
            }
            return true;
        }

        private bool ValidateDial(OrangeCashInput claims)
        {
            if (claims == null || string.IsNullOrEmpty(claims.Dial))
            {
                _servicesFailedRequestsManager.Add(new ServicesFailedRequest { ErrorCode = (int)ServiceFailedRequestsErrorCodes.DialIsNull, ControllerName = ctrl, ActionName = action, ErrorDescription = Common.Utilities.Strings.ErrorDescriptions.DialIsNull });
                return false;
            }
            if (string.IsNullOrEmpty(claims.Pin))
            {
                _servicesFailedRequestsManager.Add(new ServicesFailedRequest { Dial = claims.Dial, ErrorCode = (int)ServiceFailedRequestsErrorCodes.PinIsNullOrEmpty, ControllerName = ctrl, ActionName = action, ErrorDescription = Common.Utilities.Strings.ErrorDescriptions.PinIsNullOrEmpty });
                return false;
            }
            if (!_utilities.IsValidPin(claims.Pin))
            {
                _servicesFailedRequestsManager.Add(new ServicesFailedRequest { ErrorCode = (int)ServiceFailedRequestsErrorCodes.PinNotValid, ControllerName = ctrl, ActionName = action, ErrorDescription = Common.Utilities.Strings.ErrorDescriptions.PinIsNotVaild });
                return false;
            }
            if (!_utilities.IsValidDial(claims.Dial))
            {
                _servicesFailedRequestsManager.Add(new ServicesFailedRequest { ErrorCode = (int)ServiceFailedRequestsErrorCodes.DialIsInvalid, ControllerName = ctrl, ActionName = action });
                return false;
            }
            return true;
        }

        private OrangeCashInput GetRequestBody(HttpActionContext actionContext)
        {
            var stream = new StreamReader(actionContext.Request.Content.ReadAsStreamAsync().Result);
            stream.BaseStream.Position = 0;
            var rawRequest = stream.ReadToEnd();

            return JsonConvert.DeserializeObject<OrangeCashInput>(rawRequest);
        }
    }
}