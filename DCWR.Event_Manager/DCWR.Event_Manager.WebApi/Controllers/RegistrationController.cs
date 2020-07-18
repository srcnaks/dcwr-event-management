using System;
using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Registrations.Entities;
using DCWR.Event_Manager.Contracts.Registrations.Queries;
using DCWR.Event_Manager.Contracts.Utilities;
using DCWR.Event_Manager.Infrastructure;
using DCWR.Event_Manager.WebApi.Models;
using DCWR.Event_Manager.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DCWR.Event_Manager.WebApi.Controllers
{
    [Route("api/events/{eventId}/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ICommandQueryDispatcher dispatcher;

        public RegistrationController(ICommandQueryDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterToEvent(
            [FromRoute(Name = "eventId")] Guid eventId,
            [FromBody] RegisterToEventRequest request)
        {
            var command = request.ToCommand(eventId);
            await dispatcher.DispatchAsync(command);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<PagedResponse<AttendeeData>> GetEvents(
            [FromRoute(Name = "eventId")] Guid eventId,
            [FromQuery] PagedRequest request)
        {
            var query = new GetRegistrations(eventId, request.PageSize, request.PageNumber);
            var response = await dispatcher
                .DispatchAsync<PagedResponse<AttendeeData>, GetRegistrations>(query);
            return response;
        }
    }
}