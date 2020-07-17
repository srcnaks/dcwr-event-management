using DCWR.Event_Manager.Events;
using DCWR.Event_Manager.Registrations;
using Microsoft.EntityFrameworkCore;

namespace DCWR.Event_Manager
{
    public class EventManagerDbContext : DbContext
    {
        public EventManagerDbContext(DbContextOptions<EventManagerDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasMany<Registration>(nameof(Registration.EventId))
                .WithOne(nameof(Event.Id));
            base.OnModelCreating(modelBuilder);
        }
    }
}
