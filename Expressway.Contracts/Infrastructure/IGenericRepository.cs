using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Expressway.Contracts.Infrastructure
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetAllOrderByAsync(Expression<Func<TEntity, object>> orderBy);

        TEntity Get(int id);

        Task<TEntity> GetAsync(int id);

        IEnumerable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity Find(Expression<Func<TEntity, bool>> match);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> match);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);

        Task<IEnumerable<TEntity>> FindAllOrderByAsync(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>> orderBy);

        IEnumerable<TEntity> FindByAllIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<IEnumerable<TEntity>> FindByAllIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);

        TEntity FindIncluding(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> FindIncludingAsync(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includeProperties);

        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> FindAllByPageAsync(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>> orderBy, bool orderByAscending, int page, int pageSize);

        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity FindByCompositeKey(object key);

        TEntity Add(TEntity t);

        Task<TEntity> AddAsync(TEntity t);

        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);

        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        TEntity Update(TEntity t, object key);

        Task<TEntity> UpdateAsync(TEntity t, object key);

        int Count();

        Task<int> CountAsync();

        Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate);

        void Save();

        Task<int> SaveAsync();

        void Dispose();
    }
}
