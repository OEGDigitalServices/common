using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orange.Common.Utilities;

namespace Orange.Common.Profile.Membership
{
    internal class UserDataAccess
    {
        /// <summary>
        /// Determines whether [is dial exists] [the specified dial].
        /// </summary>
        /// <param name="dial">The dial.</param>
        /// <returns></returns>
        internal static bool IsDialExists(string dial)
        {
            try
            {
                using (MobinilMembershipModel context = new MobinilMembershipModel())
                {
                    return context.Users.Any(U => U.UserName == dial);
                }
            }
            catch (Exception exp)
            {
                new Logger().LogError(exp.Message, exp, false);
                return true;
            }
        }

        /// <summary>
        /// Determines whether [is email exists] [the specified email].
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        internal static bool IsEmailExists(string email)
        {
            try
            {
                using (MobinilMembershipModel context = new MobinilMembershipModel())
                {
                    return context.Users.Any(U => U.LoweredEmail == email.ToLower());
                }
            }
            catch (Exception exp)
            {
                new Logger().LogError(exp.Message, exp, false);
                return true;
            }
        }

        /// <summary>
        /// Gets the by email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        internal static User GetByEmail(string email)
        {
            try
            {
                using (MobinilMembershipModel context = new MobinilMembershipModel())
                {
                    return context.Users.FirstOrDefault(U => U.LoweredEmail == email.ToLower());
                }
            }
            catch (Exception exp)
            {
                new Logger().LogError(exp.Message, exp, false);
                return null;
            }
        }


        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        internal static User GetUserInfo(string username)
        {
            try
            {
                using (MobinilMembershipModel context = new MobinilMembershipModel())
                {
                    return context.Users.FirstOrDefault(U => U.UserName == username);
                }
            }
            catch (Exception exp)
            {
                new Logger().LogError(exp.Message, exp, false);
                return null;
            }
        }


    }
}
