using Orange.Common.Entities;
using System;

namespace Orange.Common.Business
{
    public interface IDSLBasicAuthenticationTokenManager
    {
        ValidateDSLBasicAuthenticationTokenOutput ValidateToken(string dial, Guid token);
    }}
