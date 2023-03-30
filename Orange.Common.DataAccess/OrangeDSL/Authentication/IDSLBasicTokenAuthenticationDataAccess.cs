using Orange.Common.EntityFramework;
using System;

namespace Orange.Common.DataAccess
{
    public interface IDSLBasicTokenAuthenticationDataAccess
    {
        DSLToken ValidateToken(string dial, Guid token);
    }
}