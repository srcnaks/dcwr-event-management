using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DCWR.Event_Manager.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DCWR.Event_Manager.Infrastructure
{
    public interface IEntityRepository<T> where T : class, IEntity
    {
        Task AddAsync(T entity);
        Task<bool> ExistsAsync(Guid id);
        Task<int> GetCountAsync(Expression<Func<T, bool>> predicate = null);
        Task<IReadOnlyCollection<T>> GetAsync(int pageSize, int pageNumber, Expression<Func<T, bool>> predicate = null);
        Task<T> FindAsync(Guid id);
        Task<T> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task DeleteAsync(T entity);
    }
    
    internal class EntityRepository<T> : IEntityRepository<T> where T : class, IEntity
    {
        private readonly EventManagerDbContext dbContext;

        public EntityRepository(EventManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(T entity)
        {
            await dbContext.AddAsync(entity);
            await dbContext.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return GetQuery()
                .AnyAsync(x => x.Id == id);
        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>> predicate = null)
        {
            var query = GetQuery();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.CountAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAsync(int pageSize, int pageNumber, Expression<Func<T, bool>> predicate = null)
        {
            if (pageNumber < 1)
                pageNumber = 1;
            return await GetQuery()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<T> FindAsync(Guid id)
        {
            return await GetQuery().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> GetAsync(Guid id)
        {
            var entity = await FindAsync(id);
            if (entity == null)
                throw new EntityNotFound(typeof(T).Name, id);
            return entity;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetAsync(id);
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        protected IQueryable<T> GetQuery()
        {
            return dbContext.Set<T>();
        }
    }
}
