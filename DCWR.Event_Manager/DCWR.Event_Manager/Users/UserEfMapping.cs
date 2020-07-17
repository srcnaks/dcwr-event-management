using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCWR.Event_Manager.Users
{
    public class UserEfMapping : IEntityTypeConfiguration<Users.User>
    {
        public void Configure(EntityTypeBuilder<Users.User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(i => i.UserName);
            builder.Property(i => i.PasswordHash);
            builder.HasIndex(i => i.UserName);
        }
    }
}
