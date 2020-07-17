using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DCWR.Event_Manager
{
    public class EventManagerDbContext : DbContext
    {
        public EventManagerDbContext(DbContextOptions<EventManagerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyMappings(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        public void ApplyMappings(ModelBuilder modelBuilder)
        {
            var mappingTypes = typeof(EventManagerDbContext).Assembly
                .GetTypes()
                .Where(type =>
                    !type.IsAbstract &&
                    !type.IsInterface &&
                    type.GetInterfaces()
                        .Any(i =>
                            i.IsGenericType &&
                            i.GetGenericTypeDefinition()
                                .IsAssignableFrom(typeof(IEntityTypeConfiguration<>))
                        )
                );

            foreach (var type in mappingTypes)
            {
                dynamic mapping = Activator.CreateInstance(type);
                modelBuilder.ApplyConfiguration(mapping);
            }
        }
    }
}
