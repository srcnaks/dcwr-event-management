using DCWR.Event_Manager.Contracts.Users.Commands;
using DCWR.Event_Manager.Contracts.Users.Entities;
using DCWR.Event_Manager.Contracts.Users.Queries;
using DCWR.Event_Manager.Infrastructure;
using DCWR.Event_Manager.Users.CommandHandlers;
using DCWR.Event_Manager.Users.QueryHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace DCWR.Event_Manager.Users
{
    public class UsersDiRegistration : IDiRegistration
    {
        public void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ICommandHandler<CreateUser>, CreateUserCommandHandler>();
            serviceCollection.AddScoped<IUserBuilder, UserBuilder>();
            serviceCollection
                .AddScoped<
                    IQueryHandler<AuthenticateUser, UserData>,
                    AuthenticateUserQueryHandler>();
            serviceCollection.AddScoped<IPasswordManager, PasswordManager>();
            serviceCollection.AddScoped<IUserAuthenticator, UserAuthenticator>();
            serviceCollection.AddScoped<IUserBuilder, UserBuilder>();
        }
    }
}
