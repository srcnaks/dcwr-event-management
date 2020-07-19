using System.Threading.Tasks;
using DCWR.Event_Manager.Infrastructure.Exceptions;
using DCWR.Event_Manager.WebApp.React.Controllers.Users.Contracts;
using DCWR.Event_Manager.WebApp.React.Controllers.Users.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DCWR.Event_Manager.WebApp.React.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public UsersController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateRequest model)
        {
            try
            {
                var response = await authenticationService.Authenticate(model);
                return Ok(response);
            }
            catch (AuthenticationFailed ex)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
        }
    }
}