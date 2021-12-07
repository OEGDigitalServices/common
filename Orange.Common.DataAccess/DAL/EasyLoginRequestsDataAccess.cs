using Orange.Common.EntityFramework;
using Orange.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.DataAccess.DAL
{
    public class EasyLoginRequestsDataAccess
    {

        public ILogger _logger { get; }

        public EasyLoginRequestsDataAccess(ILogger logger)
        {
            _logger = logger;
        }
        public bool IsEasyLoggedIn(string dial, string password, string channel)
        {
            try
            {
                using (var dbModel = new MobinilProfileModel())
                {
                    return dbModel.EasyLoginRequests.FirstOrDefault(e => e.Dial == dial && e.TempPassword == password && e.Channel.ToLower() == channel) != null;
                }
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return false;
            }
        }
    }
}
