using Orange.Common.Utilities;
using System;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Orange.Common.Profile
{
    public class ProfileContext : IProfileContext
    {
        private readonly ILogger _logger;

        public ProfileContext(ILogger logger)
        {
            _logger = logger;
        }

        public UserTicketData CurrectUserTicketData
        {
            get
            {
                try
                {
                    if (HttpContext.Current.Request.IsAuthenticated && (HttpContext.Current.User.Identity.GetType() == typeof(Microsoft.IdentityModel.Claims.ClaimsIdentity) || HttpContext.Current.User.Identity.AuthenticationType.ToLower() == "forms"))
                    {
                        var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                        if (authCookie == null)
                            return null;
                        var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                        if (ticket.Expired)
                            return null;
                        if (!string.IsNullOrEmpty(ticket.UserData))
                        {
                            string userData = ticket.UserData;
                            UserTicketData userTicketData = GetUserDataFromUserTicket(userData);
                            /*  return null if user u control   */
                            if (!HttpContext.Current.Request.RawUrl.ToLower().Contains("/business/"))
                            {
                                if (userTicketData.IsCPUser.HasValue && userTicketData.IsCPUser.Value)
                                    return null;
                            }
                            return userTicketData;
                        }
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex, false);
                    return null;
                }
            }
        }
        public UserTicketData GetUserDataFromUserTicket(string userData)
        {
            try
            {
                UserTicketData userTicketData = new UserTicketData();

                string userTicketSplitter = Resources.Strings.userTicketSplitter;
                var keyValuePairs = userData.Split(userTicketSplitter.ToCharArray())
                                            .Select(x => x.Split('='))
                                            .Where(x => x.Length == 2)
                                            .ToDictionary(x => x.First(), x => x.Last());

                userTicketData.UserID = keyValuePairs.ContainsKey("UserID") ? new Guid(keyValuePairs["UserID"]) : Guid.Empty;
                userTicketData.DialNumber = keyValuePairs.ContainsKey("Dial") ? keyValuePairs["Dial"] : null;
                userTicketData.Email = keyValuePairs.ContainsKey("Email") ? keyValuePairs["Email"] : null;
                userTicketData.FirstName = keyValuePairs.ContainsKey("FirstName") ? keyValuePairs["FirstName"] : null;
                userTicketData.LastName = keyValuePairs.ContainsKey("LastName") ? keyValuePairs["LastName"] : null;
                userTicketData.IsMobinil = keyValuePairs.ContainsKey("IsMobinil") ? Convert.ToBoolean(keyValuePairs["IsMobinil"]) : false;
                userTicketData.Address = keyValuePairs.ContainsKey("Address") ? keyValuePairs["Address"] : null;
                userTicketData.NickName = keyValuePairs.ContainsKey("NickName") ? keyValuePairs["NickName"] : null;
                userTicketData.IsCPUser = keyValuePairs.ContainsKey("IsCPUser") ? Convert.ToBoolean(keyValuePairs["IsCPUser"]) : false;
                userTicketData.CookieCreatedDate = keyValuePairs.ContainsKey("CreatedDate") ? keyValuePairs["CreatedDate"] : null;
                if (keyValuePairs.ContainsKey("IsGuest"))
                {
                    bool isGuest = false;
                    bool.TryParse(keyValuePairs["IsGuest"], out isGuest);
                    userTicketData.IsGuest = isGuest;
                }
                // userTicketData.Key = keyValuePairs.ContainsKey("key") ? keyValuePairs["key"] : null;

                return userTicketData;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return null;
            }
        }
    }
}
