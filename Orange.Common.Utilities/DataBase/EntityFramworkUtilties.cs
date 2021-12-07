using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Orange.Common.Utilities
{
    public class EntityFramworkUtilties : IEntityFramworkUtilties
    {
        private readonly ILogger _logger;
        public EntityFramworkUtilties(ILogger logger)
        {
            _logger = logger;
        }

        public void HandleDbEntityExceptions(DbEntityValidationException exp)
        {
            foreach (var validationErrors in exp.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    _logger.LogDebug("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                }
            }
        }


        public bool SaveChanges<T>(Action<T> action) where T : DbContext, new()
        {
            try
            {
                using (T dbModel = new T())
                {
                    action(dbModel);
                    dbModel.SaveChanges();
                }
                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                HandleDbEntityExceptions(dbEx);
                return false;
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return false;
            }
        }


        public List<T> GetEntities<T, U>(Func<U, List<T>> list) where U : DbContext, new()
        {
            try
            {
                using (var context = new U())
                {
                    return list(context);
                }
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return null;
            }
        }
        public T GetEntity<T, U>(Func<U, T> item) where U : DbContext, new()
        {
            try
            {
                using (var context = new U())
                {
                    return item(context);
                }
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return default(T);
            }
        }

        public T FindFirstMatch<T, U>(Func<U, T> isExist) where U : DbContext, new()
        {
            try
            {
                using (var context = new U())
                {
                    return isExist(context);
                }
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return default(T);
            }
        }

        public T GetScaler<T, U>(Func<U, T> GetScaler) where U : DbContext, new()
        {
            try
            {
                using (var context = new U())
                {
                    return GetScaler(context);
                }
            }
            catch (Exception exp)
            {
                _logger.LogError(exp.Message, exp, false);
                return default(T);
            }
        }
    }
}
