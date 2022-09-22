using Orange.Common.Entities;
using System;

namespace Orange.Common.Business
{
    public interface IDSLAuthenticationTokenManager
    {
        ValidateDSLBasicAuthenticationTokenOutput ValidateToken(string dial, string landLineNumber, Guid token);
    }}
