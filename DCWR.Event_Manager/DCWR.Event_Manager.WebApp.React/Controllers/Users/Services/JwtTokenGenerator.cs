using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DCWR.Event_Manager.WebApp.React.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DCWR.Event_Manager.WebApp.React.Controllers.Users.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(Guid userId);
    }

    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly AuthenticationSettings authenticationSettings;

        public JwtTokenGenerator(
            IOptions<AuthenticationSettings> authenticationSettings)
        {
            this.authenticationSettings = authenticationSettings.Value;
        }

        public string GenerateJwtToken(Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(authenticationSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(authenticationSettings.Expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
