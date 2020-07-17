using DCWR.Event_Manager.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCWR.Event_Manager.Registrations
{
    public class RegistrationEfMapping : IEntityTypeConfiguration<Registration>
    {
        public void Configure(EntityTypeBuilder<Registration> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(i => i.EventId);
            builder.Property(i => i.Email);
            builder.Property(i => i.Name);
            builder.Property(i => i.PhoneNumber);
            builder.HasAlternateKey(i => new {i.EventId, i.Email});
            builder.HasIndex(i => i.EventId);
        }
    }
}
