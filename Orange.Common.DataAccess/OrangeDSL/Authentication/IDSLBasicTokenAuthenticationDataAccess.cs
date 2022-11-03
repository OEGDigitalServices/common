using System;
using Orange.GSM.Common.EntityFramework.Models;

namespace Orange.Common.DataAccess
{
    public interface IDSLBasicTokenAuthenticationDataAccess
    {
        DSLToken ValidateToken(string dial, Guid token);
    }
}