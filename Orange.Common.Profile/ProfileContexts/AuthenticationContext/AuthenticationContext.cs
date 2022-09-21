using Orange.Common.Utilities;
using System;
using System.Web;

namespace Orange.Common.Profile
{
    public class AuthenticationContext : IAuthenticationContext
    {
        private readonly IProfileContext _profileContext;
        private readonly ILogger _logger;

        public AuthenticationContext(IProfileContext profileContext, ILogger logger)
        {
            _profileContext = profileContext;
            _logger = logger;
        }
        public bool IsAnonymousUser()
        {
            try
            {
                //return !HttpContext.Current.Request.IsAuthenticated;
                // IsAnonymousUser
                if (!HttpContext.Current.Request.IsAuthenticated)
                    return true;
                if (_profileContext.CurrectUserTicketData == null)
                    return true;

                if (_profileContext.CurrectUserTicketData.IsCPUser.HasValue && _profileContext.CurrectUserTicketData.IsCPUser.Value)
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex, false);
                return true;
            }
        }
    }
}
