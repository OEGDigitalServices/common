using Orange.Common.Entities;
using Orange.Common.Utilities;
using System;
using System.Linq;
using System.Web;

namespace Orange.Common.Profile
{
    public class ProfileUtilities : IProfileUtilities
    {
        private readonly ILogger _logger;
        private readonly IAuthenticationContext _authenticationContext;
        private readonly IProfileContext _profileContext;
        private readonly IUserDialsDataAccess _usersDialsDataAccess;


        public ProfileUtilities(ILogger logger, IAuthenticationContext authenticationContext, IProfileContext profileContext, IUserDialsDataAccess usersDialsDataAccess)
        {
            _logger = logger;
            _authenticationContext = authenticationContext;
            _profileContext = profileContext;
            _usersDialsDataAccess = usersDialsDataAccess;
        }
        public bool IsAuthenticated()
        {
            return !_authenticationContext.IsAnonymousUser() && _profileContext.CurrectUserTicketData != null;
        }
        public Guid GetCurrentUserId()
        {
            if (!_authenticationContext.IsAnonymousUser() && _profileContext.CurrectUserTicketData != null)
                return _profileContext.CurrectUserTicketData.UserID;
            return Guid.Empty;
        }
        public string GetCurrentDial()
        {
            if (_authenticationContext.IsAnonymousUser() || _profileContext.CurrectUserTicketData == null)
                return null;
            if (HttpContext.Current.Session[Strings.Keys.dropdownindex] == null || !int.TryParse(HttpContext.Current.Session[Strings.Keys.dropdownindex].ToString(), out int currentIndex) || currentIndex < 0)
                return _profileContext.CurrectUserTicketData.DialNumber;
            var subDials = _usersDialsDataAccess.GetUserDialsByUserID(_profileContext.CurrectUserTicketData.UserID);
            if (subDials == null || !subDials.Any())
                return null;
            subDials = subDials.OrderBy(d => d.ID).ThenByDescending(d => d.IsPrimary).ToList();
            return subDials[currentIndex].Dial;
        }
        public bool IsRequestFromPortal()
        {
            return HttpContext.Current.Request.Cookies[Strings.Keys.NopCustomer] != null;
        }
    }
}
