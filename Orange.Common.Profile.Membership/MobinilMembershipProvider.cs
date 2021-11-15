using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Orange.Common.Utilities;
using Unity;

namespace Orange.Common.Profile.Membership
{

    internal class MobinilMembershipProvider : MembershipProvider
    {
        private  ILogger _logger;
        private  ISecurityUtilities _securityUtilities;
        private IUtilities _utilities;

        #region Fields

        private static string _ApplicationName;
        private static bool _EnablePasswordReset;
        private static bool _EnablePasswordRetrieval = false;
        private static bool _RequiresQuestionAndAnswer = false;
        private static bool _RequiresUniqueEmail = true;
        private static int _MaxInvalidPasswordAttempts = 5;
        private static int _PasswordLockoutPeriod = 5;
        private static int _PasswordAttemptWindow = 10;
        private static int _MinRequiredPasswordLength = 8;
        private static int _MinRequiredNonalphanumericCharacters;
        private static string _PasswordStrengthRegularExpression;
        private static bool _WriteExceptionsToEventLog = false;
        private static MembershipPasswordFormat _PasswordFormat = MembershipPasswordFormat.Hashed;

        private string eventSource = "MobinilMembershipProvider";
        private string eventLog = "Application";
        private string exceptionMessage = "An exception occurred. Please check the Event Log.";

        #endregion

        #region Properties

        public override bool EnablePasswordReset
        {
            get { return _EnablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return _EnablePasswordRetrieval; }
        }

        public override string ApplicationName
        {
            get
            {
                return _ApplicationName;
            }
            set
            {
                _ApplicationName = value;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return _MaxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _MinRequiredNonalphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return _MinRequiredPasswordLength; }
        }

        public int PasswordLockoutPeriod
        {
            get { return _PasswordLockoutPeriod; }
        }

        public override int PasswordAttemptWindow
        {
            get { return _PasswordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return _PasswordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return _PasswordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return _RequiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return _RequiresUniqueEmail; }
        }

        public bool WriteExceptionsToEventLog
        {
            get { return _WriteExceptionsToEventLog; }
            set { _WriteExceptionsToEventLog = value; }
        }

        #endregion

        #region Initialize

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "MobinilMembershipProvider";

            SetupUnity();

            base.Initialize(name, config);

            _ApplicationName = GetConfigValue(config["applicationName"],
                          System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            _EnablePasswordReset = Convert.ToBoolean(
                          GetConfigValue(config["enablePasswordReset"], "true"));
            _EnablePasswordRetrieval = Convert.ToBoolean(
                          GetConfigValue(config["enablePasswordRetrieval"], "false"));
            _RequiresQuestionAndAnswer = Convert.ToBoolean(
                          GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
            _RequiresUniqueEmail = Convert.ToBoolean(
                          GetConfigValue(config["requiresUniqueEmail"], "true"));
            _MaxInvalidPasswordAttempts = Convert.ToInt32(
                          GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            _PasswordAttemptWindow = Convert.ToInt32(
                          GetConfigValue(config["passwordAttemptWindow"], "10"));
            _PasswordLockoutPeriod = Convert.ToInt32(
                          GetConfigValue(config["passwordLockoutPeriod"], "5"));
            _MinRequiredNonalphanumericCharacters = Convert.ToInt32(
                          GetConfigValue(config["minRequiredNonalphanumericCharacters"], "1"));
            _MinRequiredPasswordLength = Convert.ToInt32(
                          GetConfigValue(config["minRequiredPasswordLength"], "6"));
            _EnablePasswordReset = Convert.ToBoolean(
                          GetConfigValue(config["enablePasswordReset"], "true"));
            _PasswordStrengthRegularExpression = Convert.ToString(
                           GetConfigValue(config["passwordStrengthRegularExpression"], ""));
            _WriteExceptionsToEventLog = Convert.ToBoolean(
                          GetConfigValue(config["writeExceptionsToEventLog"], "false"));
        }

        private void SetupUnity()
        {
            var container = new UnityContainer();
            UnityConfig.RegisterTypes(container);
            _securityUtilities = container.Resolve<ISecurityUtilities>();
            _logger = container.Resolve<ILogger>();
            _utilities = container.Resolve<IUtilities>();

        }

        #endregion



        #region Membership Methods

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (!ValidateUser(username, oldPassword))
                return false;

            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, newPassword, false);

            OnValidatingPassword(args);

            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Change password canceled due to new password validation failure.");

            int rowsAffected = 0;
            MobinilMembershipModel DbContext = null;
            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    var user = DbContext.Users.FirstOrDefault(U => U.UserName == username);
                    if (user != null)
                    {
                        user.LegacyPasswordHash = user.PasswordHash;
                        user.PasswordHash = HashPassword(newPassword, null, HashAlgorithm.Create(HashingAlgorithms[0]));
                        user.LastPasswordChangedDate = DateTime.Now;
                        user.IsPasswordExpired = false;
                        rowsAffected = DbContext.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ChangePassword");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }

        public bool ChangeUserName(Guid userID, string oldUserName, string newUserName, out MembershipChangeUserNameStatus status)
        {
            if (GetUser(newUserName, true) != null)
            {
                status = MembershipChangeUserNameStatus.DuplicateUserName;
                return false;
            }

            int rowsAffected = 0;
            MobinilMembershipModel DbContext = null;
            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    var user = DbContext.Users.FirstOrDefault(U => U.Id == userID);
                    if (user != null)
                    {
                        user.LegacyUserName = oldUserName;
                        user.LastUserNameChangedDate = DateTime.Now;
                        user.UserName = newUserName;
                        rowsAffected = DbContext.SaveChanges();
                    }
                    else
                    {
                        status = MembershipChangeUserNameStatus.InvalidUserID;
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ChangePassword");
                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }

            if (rowsAffected > 0)
            {
                status = MembershipChangeUserNameStatus.Success;
                return true;
            }

            status = MembershipChangeUserNameStatus.ProviderError;
            return false;
        }

        public bool MigrateProfile(Guid userID, string oldUserName, string newUserName, out MembershipChangeUserNameStatus status)
        {
            if (GetUser(newUserName, true) != null)
            {
                status = MembershipChangeUserNameStatus.DuplicateUserName;
                return false;
            }

            int spResult = 0;
            MobinilMembershipModel DbContext = null;
            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    var result = DbContext.MigrateProfile(userID, newUserName, oldUserName, DateTime.Now).ToList();
                   
                    if (result.FirstOrDefault() == null || !result.FirstOrDefault().HasValue)
                    {
                        status = MembershipChangeUserNameStatus.ProviderError;
                        return false;
                    }

                    spResult = result.FirstOrDefault().Value;


                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ChangePassword");
                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }

            if (spResult > 0)
            {
                status = MembershipChangeUserNameStatus.Success;
                return true;
            }

            status = MembershipChangeUserNameStatus.ProviderError;
            return false;
        }


        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            if (!ValidateUser(username, password))
                return false;

            int rowsAffected = 0;
            MobinilMembershipModel DbContext = null;
            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    var user = DbContext.Users.FirstOrDefault(U => U.UserName == username);
                    if (user != null)
                    {
                        user.PasswordQuestion = newPasswordQuestion;
                        user.PasswordAnswer = HashPassword(newPasswordAnswer, null, HashAlgorithm.Create(HashingAlgorithms[0]));
                        rowsAffected = DbContext.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ChangePasswordQuestionAndAnswer");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            return this.CreateUser(username, password, email,
                        passwordQuestion, passwordAnswer,
                        isApproved, providerUserKey, false, false, string.Empty,
                        out status);
        }

        public User CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, bool isMobinil, bool isCPUser, string ratePlanID, out MembershipCreateStatus status)
        {
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (RequiresUniqueEmail && GetUserNameByEmail(email) != string.Empty)
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            MobinilMembershipModel DbContext = null;

            try
            {
                MembershipUser user = GetUser(username, true);

                if (user == null)
                {
                    if (providerUserKey == null)
                    {
                        providerUserKey = Guid.NewGuid();
                    }
                    else
                    {
                        if (!(providerUserKey is Guid))
                        {
                            status = MembershipCreateStatus.InvalidProviderUserKey;
                            return null;
                        }
                    }

                    DateTime createDate = DateTime.Now;

                    User userObj = new User()
                    {
                        Id = (Guid)providerUserKey,
                        UserName = username,
                        PasswordHash = HashPassword(password, null, HashAlgorithm.Create(HashingAlgorithms[0])),
                        EmailConfirmed = false,
                        PhoneNumber = string.Empty,
                        PhoneNumberConfirmed = false,
                        LockoutEndDateUtc = null,
                        LockoutEnabled = true,
                        AccessFailedCount = 0,
                        LegacyPasswordHash = null,
                        LegacyUserName = null,
                        LoweredUserName = username.ToLower(),
                        IsMobinil = isMobinil,
                        IsCPUser = isCPUser,
                        RatePlanID = ratePlanID,
                        LastActivityDate = createDate,
                        Email = !isCPUser ? email : null,
                        LoweredEmail = !isCPUser && !String.IsNullOrEmpty(email) ? email.ToLower() : null,
                        CPEmail = isCPUser ? email : null,
                        LoweredCPEmail = isCPUser && !String.IsNullOrEmpty(email) ? email.ToLower() : null,
                        PasswordQuestion = passwordQuestion,
                        PasswordAnswer = HashPassword(passwordAnswer, null, HashAlgorithm.Create(HashingAlgorithms[0])),
                        IsApproved = isApproved,
                        IsLockedOut = false,
                        CreateDate = createDate,
                        LastLoginDate = createDate,
                        LastPasswordChangedDate = createDate,
                        LastUserNameChangedDate = createDate,
                        LastLockoutDate = DateTime.Parse("1/1/1754"),
                        FailedPasswordAttemptCount = 0,
                        FailedPasswordAttemptWindowStart = DateTime.Parse("1/1/1754"),
                        FailedPasswordAnswerAttemptCount = 0,
                        FailedPasswordAnswerAttemptWindowStart = DateTime.Parse("1/1/1754"),
                    };

                    int recAdded = 0;

                    using (DbContext = new MobinilMembershipModel())
                    {
                        DbContext.Users.Add(userObj);
                        recAdded = DbContext.SaveChanges();
                    }

                    if (recAdded > 0)
                    {
                        status = MembershipCreateStatus.Success;
                    }
                    else
                    {
                        status = MembershipCreateStatus.UserRejected;
                    }


                    return (User)GetUser(username, true);

                }
                else
                {
                    status = MembershipCreateStatus.DuplicateUserName;
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "CreateUser");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }

            return null;
        }

        public User CreateUserWithSubDial(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, bool isMobinil, bool isCPUser, string ratePlanID, string firstName, string lastName, DateTime? birthDate, string userIP, string channel, string serverIP, string userAgent, out int errorCode, out MembershipCreateStatus status)
        {
            errorCode = 0;
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (RequiresUniqueEmail && GetUserNameByEmail(email) != string.Empty)
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            MobinilMembershipModel DbContext = null;

            try
            {
                MembershipUser user = GetUser(username, true);

                if (user == null)
                {
                    if (providerUserKey == null)
                    {
                        providerUserKey = Guid.NewGuid();
                    }
                    else
                    {
                        if (!(providerUserKey is Guid))
                        {
                            status = MembershipCreateStatus.InvalidProviderUserKey;
                            return null;
                        }
                    }

                    DateTime createDate = DateTime.Now;

                    User userObj = new User()
                    {
                        Id = (Guid)providerUserKey,
                        UserName = username,
                        PasswordHash = HashPassword(password, null, HashAlgorithm.Create(HashingAlgorithms[0])),
                        EmailConfirmed = false,
                        PhoneNumber = string.Empty,
                        PhoneNumberConfirmed = false,
                        LockoutEndDateUtc = null,
                        LockoutEnabled = true,
                        AccessFailedCount = 0,
                        LegacyPasswordHash = null,
                        LegacyUserName = null,
                        LoweredUserName = username.ToLower(),
                        IsMobinil = isMobinil,
                        IsCPUser = isCPUser,
                        RatePlanID = ratePlanID,
                        LastActivityDate = createDate,
                        Email = !isCPUser ? email : null,
                        LoweredEmail = !isCPUser && !String.IsNullOrEmpty(email) ? email.ToLower() : null,
                        CPEmail = isCPUser ? email : null,
                        LoweredCPEmail = isCPUser && !String.IsNullOrEmpty(email) ? email.ToLower() : null,
                        PasswordQuestion = passwordQuestion,
                        PasswordAnswer = HashPassword(passwordAnswer, null, HashAlgorithm.Create(HashingAlgorithms[0])),
                        IsApproved = isApproved,
                        IsLockedOut = false,
                        CreateDate = createDate,
                        LastLoginDate = createDate,
                        LastPasswordChangedDate = createDate,
                        LastUserNameChangedDate = createDate,
                        LastLockoutDate = DateTime.Parse("1/1/1754"),
                        FailedPasswordAttemptCount = 0,
                        FailedPasswordAttemptWindowStart = DateTime.Parse("1/1/1754"),
                        FailedPasswordAnswerAttemptCount = 0,
                        FailedPasswordAnswerAttemptWindowStart = DateTime.Parse("1/1/1754"),
                        FirstName = firstName,
                        LastName = lastName,
                        BirthDate = birthDate,
                        IsGuest = false
                    };

                    int spResult = 0;

                    using (DbContext = new MobinilMembershipModel())
                    {

                        var result = DbContext.CreateUserWithSubDial(userObj, serverIP, userAgent, channel, userIP).ToList();
                        
                        if (result.FirstOrDefault() == null || !result.FirstOrDefault().HasValue)
                        {
                            status = MembershipCreateStatus.UserRejected;
                            return null;
                        }

                        spResult = result.FirstOrDefault().Value;

                    }

                    errorCode = spResult;

                    status = spResult != 0 ? MembershipCreateStatus.Success : MembershipCreateStatus.UserRejected;


                    return (User)GetUser(username, true);

                }
                else
                {
                    status = MembershipCreateStatus.DuplicateUserName;
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "CreateUser");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }

            return null;
        }

        public bool DeleteUserWithSubDials(string username)
        {
            int spResult = 0;
            MobinilMembershipModel DbContext = null;
            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    if (DbContext.Database.Exists())
                    {

                        var result = DbContext.DeleteUserWithSubDials(username).ToList();
                      
                        if (result.FirstOrDefault() == null || !result.FirstOrDefault().HasValue)
                        {
                            return false;
                        }

                        spResult = result.FirstOrDefault().Value;
                    }
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "DeleteUser");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }

            if (spResult != 0)
                return true;

            return false;
        }
        //this is for guest login
        public User CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, bool isMobinil, bool isCPUser, string ratePlanID, bool isGuest, string firstName, string lastName, DateTime? birthDate, string userIP, string channel, string serverIP, string userAgent, out int errorCode, out MembershipCreateStatus status)
        {
            errorCode = 0;
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (RequiresUniqueEmail && GetUserNameByEmail(email) != string.Empty)
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            MobinilMembershipModel DbContext = null;

            try
            {
                MembershipUser user = GetUser(username, true);

                if (user == null)
                {
                    if (providerUserKey == null)
                    {
                        providerUserKey = Guid.NewGuid();
                    }
                    else
                    {
                        if (!(providerUserKey is Guid))
                        {
                            status = MembershipCreateStatus.InvalidProviderUserKey;
                            return null;
                        }
                    }

                    DateTime createDate = DateTime.Now;

                    User userObj = new User()
                    {
                        Id = (Guid)providerUserKey,
                        UserName = username,
                        PasswordHash = HashPassword(password, null, HashAlgorithm.Create(HashingAlgorithms[0])),
                        EmailConfirmed = false,
                        PhoneNumber = string.Empty,
                        PhoneNumberConfirmed = false,
                        LockoutEndDateUtc = null,
                        LockoutEnabled = true,
                        AccessFailedCount = 0,
                        LegacyPasswordHash = null,
                        LegacyUserName = null,
                        LoweredUserName = username.ToLower(),
                        IsMobinil = isMobinil,
                        IsCPUser = isCPUser,
                        RatePlanID = ratePlanID,
                        LastActivityDate = createDate,
                        Email = !isCPUser ? email : null,
                        LoweredEmail = !isCPUser && !String.IsNullOrEmpty(email) ? email.ToLower() : null,
                        CPEmail = isCPUser ? email : null,
                        LoweredCPEmail = isCPUser && !String.IsNullOrEmpty(email) ? email.ToLower() : null,
                        PasswordQuestion = passwordQuestion,
                        PasswordAnswer = HashPassword(passwordAnswer, null, HashAlgorithm.Create(HashingAlgorithms[0])),
                        IsApproved = isApproved,
                        IsLockedOut = false,
                        CreateDate = createDate,
                        LastLoginDate = createDate,
                        LastPasswordChangedDate = createDate,
                        LastUserNameChangedDate = createDate,
                        LastLockoutDate = DateTime.Parse("1/1/1754"),
                        FailedPasswordAttemptCount = 0,
                        FailedPasswordAttemptWindowStart = DateTime.Parse("1/1/1754"),
                        FailedPasswordAnswerAttemptCount = 0,
                        FailedPasswordAnswerAttemptWindowStart = DateTime.Parse("1/1/1754"),
                        IsGuest = isGuest,
                    };

                    int spResult = 0;

                    using (DbContext = new MobinilMembershipModel())
                    {

                        var result = DbContext.CreateUserWithSubDial(userObj, serverIP, userAgent, channel, userIP).ToList();
                        
                        if (result.FirstOrDefault() == null || !result.FirstOrDefault().HasValue)
                        {
                            status = MembershipCreateStatus.UserRejected;
                            return null;
                        }

                        spResult = result.FirstOrDefault().Value;

                    }

                    errorCode = spResult;

                    status = spResult != 0 ? MembershipCreateStatus.Success : MembershipCreateStatus.UserRejected;


                    return (User)GetUser(username, true);

                }
                else
                {
                    status = MembershipCreateStatus.DuplicateUserName;
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "CreateUser");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }

            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            int rowsAffected = 0;
            MobinilMembershipModel DbContext = null;
            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    if (DbContext.Database.Exists())
                    {
                        var user = DbContext.Users.FirstOrDefault(U => U.UserName == username);
                        if (user != null)
                        {
                            DbContext.Users.Remove(user);
                            rowsAffected = DbContext.SaveChanges();

                            if (deleteAllRelatedData)
                            {
                                // Process commands to delete all data for the user in the database.
                                // ----------------------------------------------------------------

                                // - Delete Facebook or any external account & account data

                                // - Delete User primary dial & sub dials form UserDials  

                                // - Delete user language prefrences

                                // - Delete user addresses
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "DeleteUser");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }

            if (rowsAffected > 0)
                return true;

            return false;
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            MobinilMembershipModel DbContext = null;

            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    totalRecords = 0;
                    if (DbContext.Database.Exists())
                    {
                        totalRecords = DbContext.Users.Where(M => M.Email == M.Email).Count();

                        if (totalRecords > 0)
                        {
                            MembershipUserCollection membershipUserCollection = new MembershipUserCollection();

                            var membershipList = DbContext.Users.Where(M => M.Email == M.Email)
                                                                   .Skip((pageIndex - 1) * pageSize)
                                                                   .Take(pageSize);

                            foreach (var item in membershipList)
                            {
                                membershipUserCollection.Add(item);
                            }

                            return membershipUserCollection;
                        }

                    }
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "FindUsersByEmail");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }

            return null;

        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            MobinilMembershipModel DbContext = null;
            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    totalRecords = 0;
                    if (DbContext.Database.Exists())
                    {
                        totalRecords = DbContext.Users.Count();
                        if (totalRecords > 0)
                        {
                            MembershipUserCollection membershipUserCollection = new MembershipUserCollection();
                            var membershipList = DbContext.Users.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                            foreach (var item in membershipList)
                            {
                                membershipUserCollection.Add(item);
                            }
                            return membershipUserCollection;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "DeleteUser");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }

            return null;
        }

        public override int GetNumberOfUsersOnline()
        {
            TimeSpan onlineSpan = new TimeSpan(0, System.Web.Security.Membership.UserIsOnlineTimeWindow, 0);
            DateTime compareTime = DateTime.Now.Subtract(onlineSpan);
            MobinilMembershipModel DbContext = null;
            int numOnline = 0;
            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    if (DbContext.Database.Exists())
                    {
                        numOnline = DbContext.Users.Count(U => U.LastActivityDate > compareTime);
                    }
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetNumberOfUsersOnline");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }

            return numOnline;
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            MobinilMembershipModel DbContext = null;
            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    if (DbContext.Database.Exists())
                    {
                        User user = DbContext.Users.FirstOrDefault(u => u.UserName == username);
                        if (user != null)
                        {
                            if (userIsOnline)
                            {
                                user.LastActivityDate = DateTime.Now;
                                DbContext.SaveChanges();
                            }
                            return new User(this.Name, user);
                        }
                    }
                    return null;
                }
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetUser(String, Boolean)");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            MobinilMembershipModel DbContext = null;
            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    if (DbContext.Database.Exists())
                    {
                        Guid userID = new Guid(providerUserKey.ToString());
                        User user = DbContext.Users.FirstOrDefault(u => u.Id == userID);
                        if (user != null)
                        {
                            if (userIsOnline)
                            {
                                user.LastActivityDate = DateTime.Now;
                                DbContext.SaveChanges();
                            }
                            return user;
                        }
                    }
                    return null;
                }
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetUser(String, Boolean)");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }
        }

        public override string GetUserNameByEmail(string email)
        {
            MobinilMembershipModel DbContext = null;
            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    if (DbContext.Database.Exists())
                    {
                        User user = DbContext.Users.FirstOrDefault(u => u.Email == email);
                        if (user != null)
                        {
                            if (user != null)
                                return user.UserName;
                        }
                    }
                    return String.Empty;
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetUserNameByEmail(String)");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }
        }

        public override string ResetPassword(string username, string answer)
        {
            if (!EnablePasswordReset)
            {
                throw new NotSupportedException("Password reset is not enabled.");
            }

            if (answer == null && RequiresQuestionAndAnswer)
            {
                UpdateFailureCount(username, FailureType.PasswordAnswer);

                throw new ProviderException("Password answer required for password reset.");
            }

            string newPassword =
              System.Web.Security.Membership.GeneratePassword(MinRequiredPasswordLength, MinRequiredNonAlphanumericCharacters);

            ValidatePasswordEventArgs args =
              new ValidatePasswordEventArgs(username, newPassword, true);

            OnValidatingPassword(args);

            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Reset password canceled due to password validation failure.");

            int rowsAffected = 0;
            string passwordAnswer = "";
            MobinilMembershipModel DbContext = null;

            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    var user = DbContext.Users.FirstOrDefault(U => U.UserName == username);
                    if (user != null)
                    {
                        if (user.IsLockedOut)
                            throw new MembershipPasswordException("The supplied user is locked out.");

                        passwordAnswer = user.PasswordAnswer;
                    }
                    else
                    {
                        throw new MembershipPasswordException("The supplied user name is not found.");
                    }

                    if (RequiresQuestionAndAnswer && !VerifyHashedPassword(answer, passwordAnswer))
                    {
                        UpdateFailureCount(username, FailureType.PasswordAnswer);

                        throw new MembershipPasswordException("Incorrect password answer.");
                    }
                    user.LegacyPasswordHash = user.PasswordHash;
                    user.PasswordHash = HashPassword(newPassword, null, HashAlgorithm.Create(HashingAlgorithms[0]));
                    user.LastPasswordChangedDate = DateTime.Now;
                    user.IsPasswordExpired = false;
                    rowsAffected = DbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ResetPassword");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }

            if (rowsAffected > 0)
            {
                return newPassword;
            }
            else
            {
                throw new MembershipPasswordException("User not found, or user is locked out. Password not Reset.");
            }
        }

        public string ResetPassword(string username, string generatedPassword, string answer)
        {
            if (!EnablePasswordReset)
            {
                throw new NotSupportedException("Password reset is not enabled.");
            }

            if (answer == null && RequiresQuestionAndAnswer)
            {
                UpdateFailureCount(username, FailureType.PasswordAnswer);

                throw new ProviderException("Password answer required for password reset.");
            }

            string newPassword = generatedPassword;
            //System.Web.Security.Membership.GeneratePassword(MinRequiredPasswordLength, MinRequiredNonAlphanumericCharacters);

            ValidatePasswordEventArgs args =
              new ValidatePasswordEventArgs(username, newPassword, true);

            OnValidatingPassword(args);

            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Reset password canceled due to password validation failure.");

            int rowsAffected = 0;
            string passwordAnswer = "";
            MobinilMembershipModel DbContext = null;

            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    var user = DbContext.Users.FirstOrDefault(U => U.UserName == username);
                    if (user != null)
                    {
                        if (user.IsLockedOut)
                            throw new MembershipPasswordException("The supplied user is locked out.");

                        passwordAnswer = user.PasswordAnswer;
                    }
                    else
                    {
                        throw new MembershipPasswordException("The supplied user name is not found.");
                    }

                    if (RequiresQuestionAndAnswer && !VerifyHashedPassword(answer, passwordAnswer))
                    {
                        UpdateFailureCount(username, FailureType.PasswordAnswer);

                        throw new MembershipPasswordException("Incorrect password answer.");
                    }

                    user.LegacyPasswordHash = user.PasswordHash;
                    user.PasswordHash = HashPassword(newPassword, null, HashAlgorithm.Create(HashingAlgorithms[0]));
                    user.LastPasswordChangedDate = DateTime.Now;
                    user.IsPasswordExpired = false;

                    rowsAffected = DbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ResetPassword");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }

            if (rowsAffected > 0)
            {
                return newPassword;
            }
            else
            {
                throw new MembershipPasswordException("User not found, or user is locked out. Password not Reset.");
            }
        }

        public bool LockUser(string userName)
        {
            MobinilMembershipModel DbContext = null;
            int rowsAffected = 0;

            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    var user = DbContext.Users.FirstOrDefault(U => U.UserName == userName);
                    if (user != null)
                    {
                        user.IsLockedOut = true;
                        user.LastLockoutDate = DateTime.Now;
                        rowsAffected = DbContext.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "UnlockUser");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }

            if (rowsAffected > 0)
                return true;

            return false;
        }

        public override bool UnlockUser(string userName)
        {
            MobinilMembershipModel DbContext = null;
            int rowsAffected = 0;

            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    var user = DbContext.Users.FirstOrDefault(U => U.UserName == userName);
                    if (user != null)
                    {
                        if (user.IsLockedOut)
                        {
                            DateTime lastLockOut = user.LastLockoutDate;
                            if (DateTime.Now > lastLockOut.AddMinutes(PasswordLockoutPeriod))
                            {
                                user.IsLockedOut = false;
                                user.LastLockoutDate = DateTime.Now;
                                user.FailedPasswordAttemptCount = 0;
                                rowsAffected = DbContext.SaveChanges();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "UnlockUser");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }

            if (rowsAffected > 0)
                return true;

            return false;
        }

        public bool UnlockUserManually(string userName)
        {
            MobinilMembershipModel DbContext = null;
            int rowsAffected = 0;

            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    var user = DbContext.Users.FirstOrDefault(U => U.UserName == userName);
                    if (user != null)
                    {
                        if (user.IsLockedOut)
                        {
                            user.IsLockedOut = false;
                            user.LastLockoutDate = DateTime.Now;
                            user.FailedPasswordAttemptCount = 0;
                            rowsAffected = DbContext.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "UnlockUser");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }

            if (rowsAffected > 0)
                return true;

            return false;
        }


        public override void UpdateUser(MembershipUser user)
        {
            MobinilMembershipModel DbContext = null;
            User updatedUser = (User)user;

            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    if (updatedUser != null)
                    {
                        updatedUser.LastActivityDate = DateTime.Now;

                        DbContext.Users.Attach(updatedUser);
                        var entry = DbContext.Entry(updatedUser);
                        entry.Property(e => e.FirstName).IsModified = true;
                        entry.Property(e => e.LastName).IsModified = true;
                        entry.Property(e => e.NickName).IsModified = true;
                        entry.Property(e => e.Email).IsModified = true;
                        entry.Property(e => e.LoweredEmail).IsModified = true;
                        entry.Property(e => e.CPEmail).IsModified = true;
                        entry.Property(e => e.LoweredCPEmail).IsModified = true;
                        entry.Property(e => e.BirthDate).IsModified = true;
                        entry.Property(e => e.Profession).IsModified = true;
                        entry.Property(e => e.Education).IsModified = true;
                        entry.Property(e => e.PreferredAddress).IsModified = true;
                        entry.Property(e => e.PreferredLanguage).IsModified = true;
                        entry.Property(e => e.PhoneNumber).IsModified = true;
                        entry.Property(e => e.IDType).IsModified = true;
                        entry.Property(e => e.IDNumber).IsModified = true;
                        entry.Property(e => e.City).IsModified = true;
                        entry.Property(e => e.StreetName).IsModified = true;
                        entry.Property(e => e.BuildingNumber).IsModified = true;
                        entry.Property(e => e.PostalCode).IsModified = true;
                        entry.Property(e => e.IsPasswordExpired).IsModified = true;
                        entry.Property(e => e.LastActivityDate).IsModified = true;

                        DbContext.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "UpdateUser");
                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }
        }

        /// <summary>
        /// Validates the user by user name and password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        /// <exception cref="System.Configuration.Provider.ProviderException"></exception>
        public bool ValidateUserByUserNameAndPassword(string username, string password)
        {
            bool isValid = false;
            //OrangeDSL Case in case DSL Line is physically connected 
            if (_securityUtilities.VerifyHashedData(password, username))
            {
                return true;
            }

            MobinilMembershipModel DbContext = null;
            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    if (DbContext.Database.Exists())
                    {
                        User user = DbContext.Users.FirstOrDefault(u => u.UserName == username);
                        if (user != null)
                        {
                            if (VerifyHashedPassword(password, user.PasswordHash))
                            {
                                if (user.IsApproved)
                                {
                                    isValid = true;
                                    user.LastLoginDate = DateTime.Now;

                                    if (user.IsLockedOut)
                                    {
                                        DateTime lastLockOut = user.LastLockoutDate;
                                        if (DateTime.Now > lastLockOut.AddMinutes(PasswordLockoutPeriod))
                                        {
                                            user.IsLockedOut = false;
                                            user.LastLockoutDate = DateTime.Now;
                                            user.FailedPasswordAttemptCount = 0;
                                        }
                                    }

                                    DbContext.SaveChanges();
                                }
                            }
                            else
                            {
                                UpdateFailureCount(username, FailureType.Password);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ValidateUser");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }

            return isValid;
        }

        public override bool ValidateUser(string username, string password)
        {
            bool isValid = false;
            //OrangeDSL Case in case DSL Line is physically connected 
            if (_securityUtilities.VerifyHashedData(password, username))
            {
                return true;
            }
            MobinilMembershipModel DbContext = null;
            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    if (DbContext.Database.Exists())
                    {
                        User user = DbContext.Users.FirstOrDefault(u => u.UserName == username);
                        if (user != null)
                        {

                            if (VerifyHashedPassword(password, user.PasswordHash))
                            {
                                if (user.IsApproved)
                                {
                                    isValid = true;
                                    user.LastLoginDate = DateTime.Now;
                                    DbContext.SaveChanges();
                                }
                            }
                            // To handle Facebook Login
                            else if (_securityUtilities.VerifyHashedData(password, user.UserName + user.Id.ToString() + _utilities.GetAppSetting("ProfileHashKey")))
                            {
                                if (user.IsApproved)
                                {
                                    isValid = true;
                                    user.LastLoginDate = DateTime.Now;
                                    DbContext.SaveChanges();
                                }
                            }
                            else
                            {
                                UpdateFailureCount(username, FailureType.Password);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ValidateUser");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }

            return isValid;
        }


        #endregion

        #region Helper Methods

        /// <summary>
        /// Updates the failure count.
        ///  A helper method that performs the checks and updates associated with password failure tracking.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="failureType">Type of the failure.</param>
        /// <exception cref="System.Configuration.Provider.ProviderException">
        /// Unable to update failure count and window start.
        /// or
        /// Unable to lock out user.
        /// or
        /// Unable to update failure count.
        /// or
        /// </exception>
        private void UpdateFailureCount(string username, FailureType failureType)
        {
            DateTime windowStart = new DateTime();
            int failureCount = 0;

            MobinilMembershipModel DbContext = null;
            try
            {
                using (DbContext = new MobinilMembershipModel())
                {
                    var user = DbContext.Users.FirstOrDefault(U => U.UserName == username);
                    if (user != null)
                    {
                        if (failureType == FailureType.Password)
                        {
                            failureCount = user.FailedPasswordAttemptCount;
                            windowStart = user.FailedPasswordAttemptWindowStart;
                        }

                        if (failureType == FailureType.PasswordAnswer)
                        {
                            failureCount = user.FailedPasswordAnswerAttemptCount;
                            windowStart = user.FailedPasswordAnswerAttemptWindowStart;
                        }

                        DateTime windowEnd = windowStart.AddMinutes(PasswordAttemptWindow);

                        if (failureCount == 0 || DateTime.Now > windowEnd)
                        {
                            // First password failure or outside of PasswordAttemptWindow.  
                            // Start a new password failure count from 1 and a new window starting now. 

                            if (failureType == FailureType.Password)
                            {
                                user.FailedPasswordAttemptCount = 1;
                                user.FailedPasswordAttemptWindowStart = DateTime.Now;
                            }

                            if (failureType == FailureType.PasswordAnswer)
                            {
                                user.FailedPasswordAnswerAttemptCount = 1;
                                user.FailedPasswordAnswerAttemptWindowStart = DateTime.Now;
                            }

                            if (DbContext.SaveChanges() < 0)
                                throw new ProviderException("Unable to update failure count and window start.");
                        }
                        else
                        {
                            if (failureCount++ >= MaxInvalidPasswordAttempts)
                            {
                                // Password attempts have exceeded the failure threshold. Lock out the user.

                                user.IsLockedOut = true;
                                user.LastLockoutDate = DateTime.Now;

                                if (DbContext.SaveChanges() < 0)
                                    throw new ProviderException("Unable to lock out user.");
                            }
                            else
                            {
                                // Password attempts have not exceeded the failure threshold. Update 
                                // the failure counts. Leave the window the same. 

                                if (failureType == FailureType.Password)
                                {
                                    user.FailedPasswordAttemptCount = failureCount;
                                }

                                if (failureType == FailureType.PasswordAnswer)
                                {
                                    user.FailedPasswordAnswerAttemptCount = failureCount;
                                }

                                if (DbContext.SaveChanges() < 0)
                                    throw new ProviderException("Unable to update failure count.");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "UpdateFailureCount");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (DbContext != null)
                    DbContext.Dispose();
            }
        }

        public static bool VerifyHashedPassword(string password, string profilePassword)
        {
            int saltLength = SaltValueSize * UnicodeEncoding.CharSize;

            if (string.IsNullOrEmpty(profilePassword) ||
                string.IsNullOrEmpty(password) ||
                profilePassword.Length < saltLength)
            {
                return false;
            }

            // Strip the salt value off the front of the stored password.
            string saltValue = profilePassword.Substring(0, saltLength);

            foreach (string hashingAlgorithmName in HashingAlgorithms)
            {
                HashAlgorithm hash = HashAlgorithm.Create(hashingAlgorithmName);
                string hashedPassword = HashPassword(password, saltValue, hash);
                if (profilePassword.Equals(hashedPassword, StringComparison.Ordinal))
                    return true;
            }

            // None of the hashing algorithms could verify the password.
            return false;
        }

        private static string GenerateSaltValue()
        {
            UnicodeEncoding utf16 = new UnicodeEncoding();

            if (utf16 != null)
            {
                // Create a random number object seeded from the value
                // of the last random seed value. This is done
                // interlocked because it is a static value and we want
                // it to roll forward safely.

                Random random = new Random(unchecked((int)DateTime.Now.Ticks));

                if (random != null)
                {
                    // Create an array of random values.

                    byte[] saltValue = new byte[SaltValueSize];

                    random.NextBytes(saltValue);

                    // Convert the salt value to a string. Note that the resulting string
                    // will still be an array of binary values and not a printable string. 
                    // Also it does not convert each byte to a double byte.

                    string saltValueString = utf16.GetString(saltValue);

                    // Return the salt value as a string.

                    return saltValueString;
                }
            }

            return null;
        }

        private static string ByteArrayToString(byte[] byteArray)
        {
            StringBuilder hex = new StringBuilder(byteArray.Length * 2);

            foreach (byte b in byteArray)
                hex.AppendFormat("{0:x2}", b);

            return hex.ToString();
        }

        private static string GenerateHexSaltString()
        {
            Random random = new Random();

            // Create an array of random values.
            byte[] saltValue = new byte[SaltValueSize];

            random.NextBytes(saltValue);

            string saltValueString = ByteArrayToString(saltValue);

            // Return the salt value as a string.
            return saltValueString;
        }

        private static string HashPassword(string clearData, string saltValue, HashAlgorithm hash)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();

            if (clearData != null && hash != null && encoding != null)
            {
                // If the salt string is null or the length is invalid then
                // create a new valid salt value.

                if (saltValue == null)
                {
                    // Generate a salt string.
                    //saltValue = GenerateSaltValue();
                    saltValue = GenerateHexSaltString();
                }

                // Convert the salt string and the password string to a single
                // array of bytes. Note that the password string is Unicode and
                // therefore may or may not have a zero in every other byte.

                byte[] binarySaltValue = new byte[SaltValueSize];

                binarySaltValue[0] = byte.Parse(saltValue.Substring(0, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                binarySaltValue[1] = byte.Parse(saltValue.Substring(2, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                binarySaltValue[2] = byte.Parse(saltValue.Substring(4, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                binarySaltValue[3] = byte.Parse(saltValue.Substring(6, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);

                byte[] valueToHash = new byte[SaltValueSize + encoding.GetByteCount(clearData)];
                byte[] binaryPassword = encoding.GetBytes(clearData);

                // Copy the salt value and the password to the hash buffer.

                binarySaltValue.CopyTo(valueToHash, 0);
                binaryPassword.CopyTo(valueToHash, SaltValueSize);

                byte[] hashValue = hash.ComputeHash(valueToHash);

                // The hashed password is the salt plus the hash value (as a string).

                string hashedPassword = saltValue;

                foreach (byte hexdigit in hashValue)
                {
                    hashedPassword += hexdigit.ToString("X2", CultureInfo.InvariantCulture.NumberFormat);
                }

                // Return the hashed password as a string.

                return hashedPassword;
            }

            return null;
        }

        private const int SaltValueSize = 4;

        /// <summary>
        /// Hashing algorithms used to verify one-way-hashed passwords:
        /// MD5 is used for backward compatibility with Commerce Server 2002. If you have no legacy data, MD5 can be removed.
        /// SHA256 is used on Windows Server 2003.
        /// SHA1 should be used on Windows XP (SHA256 is not supported).
        /// </summary>
        private static string[] HashingAlgorithms = new string[] { "SHA256", "MD5" };

        /// <summary>
        /// A helper function to retrieve config values from the configuration file.
        /// </summary>
        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (string.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }

        /// <summary>
        /// WriteToEventLog 
        /// A helper function that writes exception detail to the event log. Exceptions 
        /// are written to the event log as a security measure to avoid private database 
        /// details from being returned to the browser. If a method does not return a status 
        /// or boolean indicating the action succeeded or failed, a generic exception is also  
        /// thrown by the caller. 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="action"></param>
        private void WriteToEventLog(Exception e, string action)
        {
            EventLog log = new EventLog();
            log.Source = eventSource;
            log.Log = eventLog;

            string message = "An exception occurred communicating with the data source.\n\n";
            message += "Action: " + action + "\n\n";
            message += "Exception: " + e.ToString();

            log.WriteEntry(message, EventLogEntryType.Error);
        }

        #endregion
    }

    internal enum FailureType
    {
        Password,
        PasswordAnswer,
    }

}