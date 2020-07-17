using DCWR.Event_Manager.Contracts.Users.Commands;
using DCWR.Event_Manager.Infrastructure;

namespace DCWR.Event_Manager.Users
{
    public interface IUserBuilder
    {
        Users.User BuildWith(CreateUser command);
    }

    public class UserBuilder : IUserBuilder
    {
        private readonly IPasswordManager passwordManager;
        private readonly IGuidIdGenerator guidIdGenerator;

        public UserBuilder(
            IPasswordManager passwordManager,
            IGuidIdGenerator guidIdGenerator)
        {
            this.passwordManager = passwordManager;
            this.guidIdGenerator = guidIdGenerator;
        }

        public Users.User BuildWith(CreateUser command)
        {
            var user = new Users.User(
                id: guidIdGenerator.Generate(),
                userName: command.UserName
            );
            var hash = passwordManager.GenerateHash(user, command.Password);
            user.UpdatePasswordHash(hash);
            return user;
        }
    }
}
