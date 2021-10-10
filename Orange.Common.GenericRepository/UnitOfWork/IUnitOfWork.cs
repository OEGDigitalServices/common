using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Orange.Common.GenericRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class, new();
        DbContext Context { get; }
        int Submit();
        int ExecuteStoredProcedure(string name, params object[] parameters);
        List<T> ExecuteSelectStoredprocedure<T>(string query, params object[] parameters);
    }
}
