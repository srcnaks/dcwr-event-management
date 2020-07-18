using System;
using System.Threading.Tasks;
using DCWR.Event_Manager.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace DCWR.Event_Manager.Tests.Infrastructure
{
    public static class FixtureExtensions
    {
        public static IServiceScope CreateScope(this Fixture fixture)
        {
            return fixture.ServiceProvider.GetService<IServiceScopeFactory>().CreateScope();
        }

        public static T GetService<T>(this Fixture fixture, IServiceScope scope = null)
        {
            return (
                    scope != null
                        ? scope.ServiceProvider
                        : fixture.CreateScope().ServiceProvider
                )
                .GetService<T>();
        }

        public static async Task<T> AddEntityAsync<T>(this Fixture fixture, T entity) 
            where T : class, IEntity
        {
            using var scope = fixture.CreateScope();
            var repository = fixture.GetService<IEntityRepository<T>>(scope);
            await repository.AddAsync(entity);
            return entity;
        }

        public static async Task DeleteEntityAsync<T>(this Fixture fixture, T entity)
            where T : class, IEntity
        {
            using var scope = fixture.CreateScope();
            var repository = fixture.GetService<IEntityRepository<T>>(scope);
            await repository.DeleteAsync(entity);
        }

        public static async Task DeleteEntityAsync<T>(this Fixture fixture, Guid entityId)
            where T : class, IEntity
        {
            using var scope = fixture.CreateScope();
            var repository = fixture.GetService<IEntityRepository<T>>(scope);
            await repository.DeleteAsync(entityId);
        }
    }
}
