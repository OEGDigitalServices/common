using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Orange.Common.Profile.Membership
{
    public static class Provider
    {
        private static MobinilMembershipProvider MobinilMembershipProvider = System.Web.Security.Membership.Providers["MobinilMembershipProvider"] as MobinilMembershipProvider;

        public static string Name
        {
            get
            {
                return MobinilMembershipProvider.Name;
            }
        }

        #region Membership Methods

        public static bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return MobinilMembershipProvider.ChangePassword(username, oldPassword, newPassword);
        }

        public static bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            return MobinilMembershipProvider.ChangePasswordQuestionAndAnswer(username, password, newPasswordQuestion, newPasswordAnswer);
        }

        public static MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            return MobinilMembershipProvider.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
        }

        public static User CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, bool isMobinil, bool isCPUser, string ratePlanID, string firstName, string lastName, DateTime? birthDate, string userIP, string channel, string serverIP, string userAgent, out int errorCode, out MembershipCreateStatus status)
        {
            // return MobinilMembershipProvider.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, isMobinil, isCPUser, ratePlanID, out status);
            return MobinilMembershipProvider.CreateUserWithSubDial(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, isMobinil, isCPUser, ratePlanID, firstName, lastName, birthDate, userIP, channel, serverIP, userAgent, out errorCode, out status);
        }

        //this is fotGuest login
        public static User CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, bool isMobinil, bool isCPUser, string ratePlanID, bool isGuest, string firstName, string lastName, DateTime? birthDate, string userIP, string channel, string serverIP, string userAgent, out int errorCode, out MembershipCreateStatus status)
        {
            return MobinilMembershipProvider.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, isMobinil, isCPUser, ratePlanID, isGuest, firstName, lastName, birthDate, userIP, channel, serverIP, userAgent, out errorCode, out status);
        }

        public static bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            return MobinilMembershipProvider.DeleteUserWithSubDials(username);
        }
        public static bool DeleteUserWithSubDials(string username)
        {
            return MobinilMembershipProvider.DeleteUserWithSubDials(username);
        }
        public static MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return MobinilMembershipProvider.FindUsersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);

        }

        public static MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return MobinilMembershipProvider.FindUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords);
        }

        public static MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            return MobinilMembershipProvider.GetAllUsers(pageIndex, pageSize, out totalRecords);
        }

        public static int GetNumberOfUsersOnline()
        {
            return MobinilMembershipProvider.GetNumberOfUsersOnline();
        }

        public static string GetPassword(string username, string answer)
        {
            return MobinilMembershipProvider.GetPassword(username, answer);
        }

        public static User GetUser(string username, bool userIsOnline)
        {
            if (userIsOnline)
                UnlockUser(username);

            return (User)MobinilMembershipProvider.GetUser(username, userIsOnline);
        }

        public static User GetUser(object providerUserKey, bool userIsOnline)
        {
            return (User)MobinilMembershipProvider.GetUser(providerUserKey, userIsOnline);
        }

        public static string GetUserNameByEmail(string email)
        {
            return MobinilMembershipProvider.GetUserNameByEmail(email);
        }

        public static string ResetPassword(string username, string answer)
        {
            return MobinilMembershipProvider.ResetPassword(username, answer);
        }

        public static string ResetPassword(string username, string generatedPassword, string answer)
        {
            return MobinilMembershipProvider.ResetPassword(username, generatedPassword, answer);
        }

        public static bool LockUser(string userName)
        {
            return MobinilMembershipProvider.LockUser(userName);
        }

        public static bool UnlockUser(string userName)
        {
            return MobinilMembershipProvider.UnlockUser(userName);
        }

        public static bool UnlockUserManually(string userName)
        {
            return MobinilMembershipProvider.UnlockUserManually(userName);
        }

        public static void UpdateUser(MembershipUser user)
        {
            MobinilMembershipProvider.UpdateUser(user);
        }

        public static bool ValidateUser(string username, string password)
        {
            return MobinilMembershipProvider.ValidateUserByUserNameAndPassword(username, password);
        }

        #endregion

        #region Helper Methods
        public static bool ChangeUserName(Guid userID, string oldUserName, string newUserName, out MembershipChangeUserNameStatus status)
        {
            //return MobinilMembershipProvider.ChangeUserName(userID, oldUserName, newUserName, out status);
            return MobinilMembershipProvider.MigrateProfile(userID, oldUserName, newUserName, out status);
        }


        public static bool IsDialExists(string dial)
        {
            return UserDataAccess.IsDialExists(dial);
        }

        public static bool IsEmailExists(string email)
        {
            return UserDataAccess.IsEmailExists(email);
        }

        public static User GetByEmail(string email)
        {
            return UserDataAccess.GetByEmail(email);
        }

        public static User GetUserInfo(string username)
        {
            return UserDataAccess.GetUserInfo(username);
        }

        #endregion
    }
}
