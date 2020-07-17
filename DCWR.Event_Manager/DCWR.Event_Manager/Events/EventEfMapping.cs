using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCWR.Event_Manager.Events
{
    public class EventEfMapping : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Name);
            builder.Property(x => x.Description);
            builder.Property(x => x.Location);
            builder.Property(x => x.StartTime);
            builder.Property(x => x.EndTime);
            builder.HasMany(x => x.Attendees).WithOne();
        }
    }
}
