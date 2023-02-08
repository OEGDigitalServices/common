using Orange.Common.EntityFramework;
using Orange.Common.Utilities;
using System;
using System.Linq;

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

        public DSLToken ValidateToken(string dial, Guid token)
        {
            return _iEntityFrameworkUtilities.GetEntity<DSLToken, OrangeDSLContext>(dbModel => 
                        GetEntity(dbModel, dial, token));
        }

        private DSLToken GetEntity(OrangeDSLContext dbModel, string dial, Guid dslToken)
        {
            return dbModel.DSLTokens.FirstOrDefault(token => token.Dial == dial && token.Token == dslToken);
        }

        #endregion
    }
}