using Orange.Common.DataAccess.DAL;
using Orange.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.DataAccess.ProfileContexts
{
    public class AuthenticationContext
    {
        public AuthenticationContext(ILogger logger)
        {
            _logger = logger;
        }

        public ILogger _logger { get; }

        public bool ValidateEasyLoginUser(string dial, string password, string channel)
        {
            try
            {
                return new EasyLoginRequestsDataAccess(_logger).IsEasyLoggedIn(dial, password, channel.ToLower());
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return false;
            }
        }
    }
}
