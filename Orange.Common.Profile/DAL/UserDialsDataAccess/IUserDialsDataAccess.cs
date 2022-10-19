using Orange.Common.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Profile
{
    public interface IUserDialsDataAccess
    {
        List<UserDial> GetUserDialsByUserID(Guid userID);
        bool CheckIfDialBelongToThisAccount(Guid userGuid, string dial);
    }
}
