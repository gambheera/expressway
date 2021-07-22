using Expressway.Contracts.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Expressway.Infrastructure.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext context;

        public GenericRepository(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllOrderByAsync(Expression<Func<TEntity, object>> orderBy)
        {
            return await context.Set<TEntity>().OrderBy(orderBy).ToListAsync();
        }

        public TEntity Get(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public IEnumerable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {

            IQueryable<TEntity> queryable = GetAll().AsQueryable();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<TEntity, object>(includeProperty);
            }

            return queryable;
        }

        public TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return context.Set<TEntity>().SingleOrDefault(match);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await context.Set<TEntity>().SingleOrDefaultAsync(match);
        }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            return context.Set<TEntity>().Where(match).ToList();
        }

        public async Task<IEnumerable<TEntity>> FindAllByPageAsync(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>> orderBy, bool orderByAscending, int page, int pageSize)
        {
            if (orderByAscending)
            {
                return await context.Set<TEntity>().OrderBy(orderBy).Where(match).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {
                return await context.Set<TEntity>().OrderByDescending(orderBy).Where(match).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
        }

        public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await context.Set<TEntity>().Where(match).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAllOrderByAsync(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>> orderBy)
        {
            return await context.Set<TEntity>().OrderBy(orderBy).Where(match).ToListAsync();
        }


        public IEnumerable<TEntity> FindByAllIncluding(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = context.Set<TEntity>().Where(predicate).AsQueryable();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public async Task<IEnumerable<TEntity>> FindByAllIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = context.Set<TEntity>().Where(predicate).AsQueryable();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }

        public TEntity FindIncluding(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = context.Set<TEntity>().Where(match).AsQueryable();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.FirstOrDefault();
        }

        public async Task<TEntity> FindIncludingAsync(Expression<Func<TEntity, bool>> match, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = context.Set<TEntity>().Where(match).AsQueryable();
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync();
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> query = context.Set<TEntity>().Where(predicate);
            return query;
        }

        public async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public TEntity FindByCompositeKey(object key)
        {
            TEntity query = context.Set<TEntity>().Find(key);
            return query;
        }

        public TEntity Add(TEntity t)
        {

            context.Set<TEntity>().Add(t);
            //context.SaveChanges();
            return t;
        }

        public async Task<TEntity> AddAsync(TEntity t)
        {
            await context.Set<TEntity>().AddAsync(t);
            //await context.SaveChangesAsync();
            return t;

        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().AddRange(entities);
            //context.SaveChanges();
            return entities;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await context.Set<TEntity>().AddRangeAsync(entities);
            //await context.SaveChangesAsync();
            return entities;
        }

        public void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
            //context.SaveChanges();
        }

        public TEntity Update(TEntity t, object key)
        {
            if (t == null)
                return null;
            TEntity exist = context.Set<TEntity>().Find(key);
            if (exist != null)
            {
                //if (t is IAuditable)
                //{
                //    var createdBy = exist.GetType().GetProperty("CreatedBy").GetValue(exist);
                //    var createdOn = exist.GetType().GetProperty("CreatedOn").GetValue(exist);
                //    t.GetType().GetProperty("CreatedBy").SetValue(t, createdBy);
                //    t.GetType().GetProperty("CreatedOn").SetValue(t, createdOn);
                //}
                context.Entry(exist).CurrentValues.SetValues(t);
                //context.SaveChanges();
            }
            return exist;
        }

        public async Task<TEntity> UpdateAsync(TEntity t, object key)
        {
            if (t == null)
                return null;
            TEntity exist = await context.Set<TEntity>().FindAsync(key);
            if (exist != null)
            {
                context.Entry(exist).CurrentValues.SetValues(t);
                //await context.SaveChangesAsync();
            }
            return exist;
        }

        public int Count()
        {
            return context.Set<TEntity>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await context.Set<TEntity>().CountAsync();
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().Where(predicate).CountAsync();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            //Dispose(true);
            //GC.SuppressFinalize(this);
        }

        private bool disposed = false;
        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }


    }
}
