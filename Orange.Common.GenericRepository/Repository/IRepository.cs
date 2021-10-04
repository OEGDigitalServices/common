using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Orange.Common.GenericRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        List<TEntity> GetAll();
        List<TEntity> GetByPage(int pageNumber, int pageSize);
        TEntity FindItem(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> FindList(Expression<Func<TEntity, bool>> predicate);
        List<TEntity> FindByPage(Expression<Func<TEntity, bool>> predicate, int pageNumber, int pageSize);
        List<TEntity> FindByPageAndOrder(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate, int pageNumber, int pageSize, bool orderDesc = false);
        List<TEntity> FindByPageAndOrder(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate, int pageNumber, int pageSize, bool orderDesc = false);
        int Count(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        int Count();
        List<TEntity> FindTopList(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate, int topCount, bool orderDesc = false);
        List<TEntity> FindTopList(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate, int topCount, bool orderDesc = false);
        TEntity FindItemByOrder(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate, bool orderDesc = false);
        TEntity FindItemByOrder(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, int>> orderPredicate, bool orderDesc = false);
        void RemoveAllBy(Expression<Func<TEntity, bool>> predicate);
        void Deatach(TEntity entity);
        TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, DateTime>> orderPredicate);
    }

}
