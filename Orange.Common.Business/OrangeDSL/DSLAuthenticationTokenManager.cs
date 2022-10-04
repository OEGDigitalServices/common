using Orange.Common.DataAccess;
using Orange.Common.Entities;
using Orange.Common.Utilities;
using System;

namespace Orange.Common.Business
{
    public class DSLAuthenticationTokenManager : IDSLAuthenticationTokenManager
    {
        #region Props

        private readonly IDSLBasicTokenAuthenticationDataAccess _dataAccess;
        private readonly ILogger _logger;

        #endregion

        #region Ctor

        public DSLAuthenticationTokenManager(IDSLBasicTokenAuthenticationDataAccess dataAccess, ILogger logger)
        {
            _dataAccess = dataAccess;
            _logger = logger;
        }

        #endregion

        #region Methods


        public ValidateDSLBasicAuthenticationTokenOutput ValidateToken(string dial, Guid token)
        {
            try
            {
                var tokenRecord = _dataAccess.ValidateToken(dial, token);
                if (tokenRecord == null)
                    return null;

                return new ValidateDSLBasicAuthenticationTokenOutput
                {
                    Dial = tokenRecord.Dial,
                    Token = tokenRecord.Token,
                    DSLUserStatus = tokenRecord.DSLUserStatus,
                    IsPrePaid = tokenRecord.IsPrePaid,
                    LandLineNumber = tokenRecord.LandLineNumber,
                    IsMigrated = tokenRecord.IsMigrated,
                    UserName = tokenRecord?.UserName,
                    CustomerId = tokenRecord?.CustomerId,
                    UCID = tokenRecord?.UCID,
                    ContractId = tokenRecord?.ContractId,
                    CreatedDate = tokenRecord?.CreatedDate
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return null;
            }
        }

        #endregion

    }
}
