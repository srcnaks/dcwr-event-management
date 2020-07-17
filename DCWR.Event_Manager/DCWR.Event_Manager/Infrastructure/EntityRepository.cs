using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DCWR.Event_Manager.Infrastructure
{
    public interface IEntityRepository<T> where T : class, IEntity
    {
        Task AddAsync(T entity);
        Task<bool> ExistsAsync(Guid id);
        Task<int> GetCount(Predicate<T> predicate = null);
        Task<IReadOnlyCollection<T>> GetAsync(int pageSize, int pageNumber, Predicate<T> predicate = null);
    }
    
    internal class EntityRepository<T> : IEntityRepository<T> where T : class, IEntity
    {
        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            return GetQuery()
                .AnyAsync(x => x.Id == id);
        }

        public async Task<int> GetCount(Predicate<T> predicate = null)
        {
            var query = GetQuery();
            if (predicate != null)
            {
                query = query.Where(x => predicate(x));
            }

            return await query.CountAsync();
        }

        public async Task<IReadOnlyCollection<T>> GetAsync(int pageSize, int pageNumber, Predicate<T> predicate = null)
        {
            if (pageNumber < 1)
                pageNumber = 1;
            return await GetQuery()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        private IQueryable<T> GetQuery()
        {
            throw new NotImplementedException();
        }
    }
}
