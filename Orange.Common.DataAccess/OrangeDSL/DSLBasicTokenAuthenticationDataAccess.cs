using Orange.Common.Utilities;
using Orange.GSM.Common.EntityFramework.Models;
using System;
using System.Linq;
using DSLToken = Orange.GSM.Common.EntityFramework.Models.DSLToken;

namespace Orange.Common.DataAccess
{
    public class DSLBasicTokenAuthenticationDataAccess : IDSLBasicTokenAuthenticationDataAccess
    {
        #region Props

        private readonly IEntityFramworkUtilties _iEntityFrameworkUtilities;


        #endregion

        #region ctor

        public DSLBasicTokenAuthenticationDataAccess(IEntityFramworkUtilties iEntityFrameworkUtilities)
        {
            _iEntityFrameworkUtilities = iEntityFrameworkUtilities;
        }

        #endregion

        #region Validate Token

        public DSLToken ValidateToken(string dial, Guid token, string landLineNumber)
        {
            return _iEntityFrameworkUtilities.GetEntity<DSLToken, OrangeDSLContext>(dbModel => 
                        GetEntity(dbModel, dial, token, landLineNumber));
        }

        private DSLToken GetEntity(OrangeDSLContext dbModel, string dial, Guid dslToken, string landLineNumber)
        {
            return dbModel.DSLTokens.FirstOrDefault(token => token.Dial == dial && token.LandLineNumber == landLineNumber && token.Token == dslToken);
        }

        #endregion
    }
}