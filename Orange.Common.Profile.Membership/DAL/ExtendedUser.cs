using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Orange.Common.Profile.Membership
{
    partial class User : MembershipUser
    {
        public User()
            : base()
        {

        }

        public User(string providername,
                                 string username,
                                 object providerUserKey,
                                 string email,
                                 string passwordQuestion,
                                 string comment,
                                 bool isApproved,
                                 bool isLockedOut,
                                 DateTime creationDate,
                                 DateTime lastLoginDate,
                                 DateTime lastActivityDate,
                                 DateTime lastPasswordChangedDate,
                                 DateTime lastLockedOutDate,
                                 bool isMobinil,
                                 bool isCPUser) :
            base(providername,
                                      username,
                                      providerUserKey,
                                      email,
                                      passwordQuestion,
                                      comment,
                                      isApproved,
                                      isLockedOut,
                                      creationDate,
                                      lastLoginDate,
                                      lastActivityDate,
                                      lastPasswordChangedDate,
                                      lastLockedOutDate)
        {
            this.IsMobinil = isMobinil;
            this.IsCPUser = isCPUser;
        }

        public User(string providername, User obj) :
            base(providername,
                              obj.UserName,
                              obj.Id,
                              obj.Email,
                              obj.PasswordQuestion,
                              obj.Comment,
                              obj.IsApproved,
                              obj.IsLockedOut,
                              obj.CreationDate,
                              obj.LastLoginDate,
                              obj.LastActivityDate,
                              obj.LastPasswordChangedDate,
                              obj.LastLockoutDate)
        {
            this.Id = obj.Id;
            this.UserName = obj.UserName;
            this.PasswordHash = obj.PasswordHash;
            this.EmailConfirmed = obj.EmailConfirmed;
            this.PhoneNumber = obj.PhoneNumber;
            this.PhoneNumberConfirmed = obj.PhoneNumberConfirmed;
            this.LockoutEndDateUtc = obj.LockoutEndDateUtc;
            this.LockoutEnabled = obj.LockoutEnabled;
            this.AccessFailedCount = obj.AccessFailedCount;
            this.LegacyPasswordHash = obj.LegacyPasswordHash;
            this.LoweredUserName = obj.LoweredUserName;
            this.IsMobinil = obj.IsMobinil;
            this.IsCPUser = obj.IsCPUser;
            this.LastActivityDate = obj.LastActivityDate;
            this.Email = obj.Email;
            this.LoweredEmail = obj.LoweredEmail;
            this.CPEmail = obj.CPEmail;
            this.LoweredCPEmail = obj.LoweredCPEmail;
            this.PasswordQuestion = obj.PasswordQuestion;
            this.PasswordAnswer = obj.PasswordAnswer;
            this.IsApproved = obj.IsApproved;
            this.IsLockedOut = obj.IsLockedOut;
            this.CreateDate = obj.CreateDate;
            this.LastLoginDate = obj.LastLoginDate;
            this.LastPasswordChangedDate = obj.LastPasswordChangedDate;
            this.LastLockoutDate = obj.LastLockoutDate;
            this.FailedPasswordAttemptCount = obj.FailedPasswordAttemptCount;
            this.FailedPasswordAttemptWindowStart = obj.FailedPasswordAttemptWindowStart;
            this.FailedPasswordAnswerAttemptCount = obj.FailedPasswordAnswerAttemptCount;
            this.FailedPasswordAnswerAttemptWindowStart = obj.FailedPasswordAnswerAttemptWindowStart;
            this.FirstName = obj.FirstName;
            this.LastName = obj.LastName;
            this.NickName = obj.NickName;
            this.BirthDate = obj.BirthDate;
            this.Profession = obj.Profession;
            this.Education = obj.Education;
            this.PreferredAddress = obj.PreferredAddress;
            this.PreferredLanguage = obj.PreferredLanguage;
            this.FacebookName = obj.FacebookName;
            this.RatePlanID = obj.RatePlanID;
            this.IDType = obj.IDType;
            this.IDNumber = obj.IDNumber;
            this.City = obj.City;
            this.StreetName = obj.StreetName;
            this.BuildingNumber = obj.BuildingNumber;
            this.PostalCode = obj.PostalCode;
            this.IsPasswordExpired = obj.IsPasswordExpired;
            this.Comment = obj.Comment;
            this.IsGuest = obj.IsGuest;
        }
    }
}
