using Orange.Common.Entities;
using Orange.Common.Utilities;
using System;

namespace Orange.Common.Business
{
    public class OrangeCashManager : IOrangeCashManager
    {
        #region Props

        private readonly ILogger _logger;
        private readonly IUtilities _utilities;
        private readonly IHttpClientManager _httpClientManager;


        #endregion

        #region Ctor

        public OrangeCashManager(
            ILogger logger,
            IUtilities utilities,
            IHttpClientManager httpClientManager)
        {
            _logger = logger;
            _utilities = utilities;
            _httpClientManager = httpClientManager;
        }

        #endregion

        #region ValidateDialAndPin

        public WalletBalanceInquiryOutput CheckDialAndPin(OrangeCashInput input)
        {
            try
            {
                return _httpClientManager.Post<WalletBalanceInquiryOutput, object>(string.Concat(
                    _utilities.GetAppSetting(Strings.APIs.OrangeCashSecureIntegrationBusURL),
                    Strings.APIs.WalletBalanceInquiry), MapInput(input), null, 120).GetAwaiter().GetResult(); ;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp);
                return new WalletBalanceInquiryOutput
                {
                    ErrorCode = WalletBalanceInquiryOutput.ErrorCodes.ServiceException,
                    ErrorDescription = string.Format(Utilities.Strings.ErrorDescriptions.ServiceException, exp.Message)
                };
            }
        }
        private object MapInput(OrangeCashInput input)
        {
            return new
            {
                MSISDN = input.Dial,
                Pin = input.Pin,
                Channel = Channel.OrangeMoney.ToString(),
                RequestId = Guid.NewGuid(),
                ModuleName = ModulesNames.WalletBalanceInquiry
            };
        }

        #endregion
    }
}
