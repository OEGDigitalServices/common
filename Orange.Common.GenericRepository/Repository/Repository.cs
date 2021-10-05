using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Orange.Common.GenericRepository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        protected DbSet<TEntity> DbSet { get; set; }

        public Repository(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public List<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public TEntity FindItem(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).FirstOrDefault();
        }

        public List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }

        public List<TEntity> GetByPage(int pageNumber, int pageSize)
        {
            int skip = (pageNumber * pageSize) - pageSize;
            return Context.Set<TEntity>().Skip(skip).Take(pageSize).ToList();
        }

        public List<TEntity> FindByPage(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize)
        {
            int skip = (pageNumber * pageSize) - pageSize;
            return Context.Set<TEntity>().Where(predicate).ToList().Skip(skip).Take(pageSize).ToList();
        }

        public List<TEntity> FindByPageAndOrder(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate, int pageNumber, int pageSize, bool orderDesc = false)
        {
            int skip = (pageNumber * pageSize) - pageSize;
            if (orderDesc)
                return Context.Set<TEntity>().Where(predicate).OrderByDescending(orderPredicate).Skip(skip).Take(pageSize).ToList();
            return Context.Set<TEntity>().Where(predicate).OrderBy(orderPredicate).Skip(skip).Take(pageSize).ToList();
        }

        public List<TEntity> FindByPageAndOrder(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate, int pageNumber, int pageSize, bool orderDesc = false)
        {
            int skip = (pageNumber * pageSize) - pageSize;
            if (orderDesc)
                return Context.Set<TEntity>().Where(predicate).OrderByDescending(orderPredicate).Skip(skip).Take(pageSize).ToList();
            return Context.Set<TEntity>().Where(predicate).OrderBy(orderPredicate).Skip(skip).Take(pageSize).ToList();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).Count();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).FirstOrDefault();
        }

        public TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate)
        {
            return Context.Set<TEntity>().Where(predicate).OrderByDescending(orderPredicate).FirstOrDefault();
        }
        public TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate)
        {
            return Context.Set<TEntity>().Where(predicate).OrderByDescending(orderPredicate).FirstOrDefault();
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).Any();
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
                return;
            Context.Entry(entity).State = EntityState.Modified;
        }

        public int Count()
        {
            return DbSet.Count();
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public void RemoveAllBy(Expression<Func<TEntity, bool>> predicate)
        {
            List<TEntity> list = Context.Set<TEntity>().Where(predicate).ToList();

            if (list != null && list.Count > 0)
                Context.Set<TEntity>().RemoveRange(list);
        }

        public List<TEntity> FindTopList(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate, int topCount, bool orderDesc = false)
        {
            if (orderDesc)
                return Context.Set<TEntity>().Where(predicate).OrderByDescending(orderPredicate).Take(topCount).ToList();
            return Context.Set<TEntity>().Where(predicate).OrderBy(orderPredicate).Take(topCount).ToList();
        }
        public List<TEntity> FindTopList(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate, int topCount, bool orderDesc = false)
        {
            if (orderDesc)
                return Context.Set<TEntity>().Where(predicate).OrderByDescending(orderPredicate).Take(topCount).ToList();
            return Context.Set<TEntity>().Where(predicate).OrderBy(orderPredicate).Take(topCount).ToList();
        }

        public TEntity FindItemByOrder(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate, bool orderDesc = false)
        {
            if (orderDesc)
                return Context.Set<TEntity>().Where(predicate).OrderByDescending(orderPredicate).FirstOrDefault();
            return Context.Set<TEntity>().Where(predicate).OrderBy(orderPredicate).FirstOrDefault();
        }

        public TEntity FindItemByOrder(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate, bool orderDesc = false)
        {
            if (orderDesc)
                return Context.Set<TEntity>().Where(predicate).OrderByDescending(orderPredicate).FirstOrDefault();
            return Context.Set<TEntity>().Where(predicate).OrderBy(orderPredicate).FirstOrDefault();
        }
        public void Deatach(TEntity entity)
        {
            if (entity == null)
                return;
            Context.Entry(entity).State = EntityState.Detached;
        }
    }
}
