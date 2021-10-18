using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Orange.Common.Utilities
{
    public interface IEntityFramworkUtilties
    {
        void HandleDbEntityExceptions(DbEntityValidationException exp);
        bool SaveChanges<T>(Action<T> action) where T : DbContext, new();
        List<T> GetEntities<T, U>(Func<U, List<T>> list) where U : DbContext, new();
        T GetEntity<T, U>(Func<U, T> item) where U : DbContext, new();
        T FindFirstMatch<T, U>(Func<U, T> isExist) where U : DbContext, new();
        T GetScaler<T, U>(Func<U, T> GetScaler) where U : DbContext, new();
    }
}
