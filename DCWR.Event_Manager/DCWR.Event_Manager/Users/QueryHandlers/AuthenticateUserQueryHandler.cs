using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Users.Entities;
using DCWR.Event_Manager.Contracts.Users.Queries;
using DCWR.Event_Manager.Infrastructure;

namespace DCWR.Event_Manager.Users.QueryHandlers
{
    public class AuthenticateUserQueryHandler :
        IQueryHandler<AuthenticateUser, UserData>
    {
        private readonly IUserAuthenticator userAuthenticator;

        public AuthenticateUserQueryHandler(IUserAuthenticator userAuthenticator)
        {
            this.userAuthenticator = userAuthenticator;
        }

        public async Task<UserData> HandleAsync(AuthenticateUser query)
        {
            var user = await userAuthenticator.Authenticate(query);
            return user.ToContract();
        }
    }
}
