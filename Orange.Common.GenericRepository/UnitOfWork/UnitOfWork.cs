using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Orange.Common.GenericRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;
        public DbContext Context
        {
            get { return _context; }
        }

        public UnitOfWork(DbContext dbContext)
        {
            _context = dbContext;
        }

        public IRepository<T> GetRepository<T>() where T : class, new()
        {
            return new Repository<T>(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public int Submit()
        {
            //try
            //{
            return _context.SaveChanges();
            //}
            //catch (DbEntityValidationException e)
            //{
            //    //TODO Log here
            //}
        }

        public List<T> ExecuteSelectStoredprocedure<T>(string query, params object[] parameters)
        {
            return _context.Database.SqlQuery<T>(query, parameters).ToList();
        }

        public int ExecuteStoredProcedure(string query, params object[] parameters)
        {
            return _context.Database.ExecuteSqlCommand(query, parameters);
        }
    }

}
