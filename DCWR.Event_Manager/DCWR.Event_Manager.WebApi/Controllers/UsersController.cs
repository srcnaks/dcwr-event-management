using DCWR.Event_Manager.WebApi.Models;
using DCWR.Event_Manager.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DCWR.Event_Manager.WebApi.Controllers
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
        public IActionResult Authenticate([FromBody]AuthenticateRequest model)
        {
            var response = authenticationService.Authenticate(model);
            return Ok(response);
        }
    }
}