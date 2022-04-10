using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Profile.Membership
{
    public enum MembershipChangeUserNameStatus
    {
        Success = 0,
        InvalidUserName = 1,
        DuplicateUserName = 2,
        InvalidUserID = 3,
        ProviderError = 4
    }
}
