using Orange.Common.Business.OrangeTriplePlay;
using Orange.Common.Entities.OrangeTriplePlay;
using Orange.Common.Utilities;
using System;
using System.Linq;

namespace Orange.Common.Business.OrangeTriplePlay
{
    public class OrangeTPManager : IOrangeTPManager
    {
        #region Props

        private readonly ILogger _logger;
        private readonly ISecureManager _secureManager;
        private readonly IUtilities _utilities;

        #endregion

        #region Ctor

        public OrangeTPManager(
            ILogger logger,
            ISecureManager secureManager,
            IUtilities utilities)
        {
            _logger = logger;
            _secureManager = secureManager;
            _utilities = utilities;
        }

        #endregion

        #region IsUserIdentified

        public bool IsUserIdentified(TPInput input, string channel, string moduleName)
        {
            try
            {
                var endPoint = _utilities.GetAppSetting(Strings.APIs.CommonSecureIntegrationBusURL) + Strings.APIs.OrangeTriplePlayIdentifyUser;
                var secureOutput = _secureManager.CallSecureConnect<IdentifyUserResponse>(
                    endPoint, MapInput(input, channel, moduleName), Strings.Verbs.Post);
                if (secureOutput == null || secureOutput.ErrorCode != IdentifyUserResponse.ErrorCodes.Success)
                    return false;
                return secureOutput.UserNames.Any(user => user == input.TpUserName);
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp);
                return false;
            }
        }

        private object MapInput(TPInput input, string channel, string moduleName)
        {
            return new
            {
                MSISDN = input.Dial,
                Channel = channel,
                ModulesName = moduleName,
                RequestId = Guid.NewGuid()
            };
        }

        #endregion

    }
}
