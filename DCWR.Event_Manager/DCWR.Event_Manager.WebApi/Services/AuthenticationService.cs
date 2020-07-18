using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Users.Entities;
using DCWR.Event_Manager.Contracts.Users.Queries;
using DCWR.Event_Manager.Infrastructure;
using DCWR.Event_Manager.WebApi.Models;

namespace DCWR.Event_Manager.WebApi.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly ICommandQueryDispatcher dispatcher;
        private readonly IJwtTokenGenerator tokenGenerator;

        public AuthenticationService(
            ICommandQueryDispatcher dispatcher, 
            IJwtTokenGenerator tokenGenerator)
        {
            this.dispatcher = dispatcher;
            this.tokenGenerator = tokenGenerator;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            var user = await dispatcher
                .DispatchAsync<UserData, AuthenticateUser>(new AuthenticateUser(request.Username, request.Password));
            var token = tokenGenerator.GenerateJwtToken(user.Id);
            return new AuthenticateResponse(
                id: user.Id,
                username: user.UserName,
                token: token
            );
        }
    }
}