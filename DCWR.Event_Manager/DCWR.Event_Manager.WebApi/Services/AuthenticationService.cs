using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Users.Entities;
using DCWR.Event_Manager.Contracts.Users.Queries;
using DCWR.Event_Manager.Infrastructure;
using DCWR.Event_Manager.WebApi.Models;
using DCWR.Event_Manager.WebApi.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DCWR.Event_Manager.WebApi.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest request);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly IQueryHandler<AuthenticateUser, UserData> queryHandler;
        private readonly AuthenticationSettings authenticationSettings;

        public AuthenticationService(
            IQueryHandler<AuthenticateUser, UserData> queryHandler,
            IOptions<AuthenticationSettings> authenticationSettings)
        {
            this.queryHandler = queryHandler;
            this.authenticationSettings = authenticationSettings.Value;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
        {
            var user = await queryHandler
                .HandleAsync(
                    new AuthenticateUser(request.Username, request.Password)
                );
            var token = GenerateJwtToken(user);
            return new AuthenticateResponse(
                id: user.Id,
                username: user.UserName,
                token: token
            );
        }

        private string GenerateJwtToken(UserData user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(authenticationSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(authenticationSettings.Expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}