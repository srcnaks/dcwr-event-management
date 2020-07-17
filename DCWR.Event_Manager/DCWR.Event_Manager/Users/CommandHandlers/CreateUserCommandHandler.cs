using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Users.Commands;
using DCWR.Event_Manager.Infrastructure;

namespace DCWR.Event_Manager.Users.CommandHandlers
{
    public class CreateUserCommandHandler :
        ICommandHandler<CreateUser>
    {
        private readonly IUserBuilder userBuilder;
        private readonly IEntityRepository<User> userRepository;

        public CreateUserCommandHandler(
            IUserBuilder userBuilder,
            IEntityRepository<User> userRepository)
        {
            this.userBuilder = userBuilder;
            this.userRepository = userRepository;
        }

        public async Task HandleAsync(CreateUser command)
        {
            var user = userBuilder.BuildWith(command);
            await userRepository.AddAsync(user);
        }
    }
}
