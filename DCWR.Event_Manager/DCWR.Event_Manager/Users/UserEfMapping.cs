using DCWR.Event_Manager.Contracts.Users.Commands;
using DCWR.Event_Manager.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCWR.Event_Manager.Users
{
    public class UserEfMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(i => i.UserName);
            builder.Property(i => i.PasswordHash);
            builder.HasIndex(i => i.UserName);

            // todo: might think about a better approach
            CreateFirstUser(builder);
        }

        private void CreateFirstUser(EntityTypeBuilder<User> builder)
        {
            var command = new CreateUser("admin", "admin");
            var user = InitiateUserBuilder().BuildWith(command);
            builder.HasData(user);
        }

        private IUserBuilder InitiateUserBuilder()
        {
            var passwordHasher = new PasswordHasher<User>();
            var passwordManager = new PasswordManager(passwordHasher);
            var userBuilder = new UserBuilder(passwordManager, GuidIdGenerator.Instance);
            return userBuilder;
        }
    }
}
