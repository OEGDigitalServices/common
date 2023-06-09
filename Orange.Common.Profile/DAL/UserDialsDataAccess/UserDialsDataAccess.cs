﻿using Orange.Common.EntityFramework.Entities;
using Orange.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Orange.Common.Profile
{
    public class UserDialsDataAccess : IUserDialsDataAccess
    {
        private readonly ILogger _logger;

        public UserDialsDataAccess(ILogger logger)
        {
            _logger = logger;
        }

        public List<UserDial> GetUserDialsByUserID(Guid userID)
        {
            try
            {
                using (MobinilProfileModel context = new MobinilProfileModel())
                {
                    return context.UserDials.Where(D => D.UserID == userID && D.IsDeleted.HasValue && !D.IsDeleted.Value)
                        .OrderByDescending(a=>a.IsPrimary).ToList();
                }
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return null;
            }
        }
        public bool CheckIfDialBelongToThisAccount(Guid userGuid, string dial)
        {
            if (!dial.StartsWith("0"))
                dial = "0" + dial;
            List<UserDial> dials = GetUserDialsByUserID(userGuid); 
            var exist = dials.Where(x => x.Dial == dial).FirstOrDefault();
            if (exist != null)
                return true;
            else
                return false;
        }
        public string GetPrimaryUserDial(Guid userID)
        {
            try
            {
                using (MobinilProfileModel context = new MobinilProfileModel())
                {
                    return context.UserDials.FirstOrDefault(D => D.UserID == userID & D.IsPrimary.HasValue && D.IsPrimary.Value)?.Dial;
                }
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return null;
            }
        }
    }
}
