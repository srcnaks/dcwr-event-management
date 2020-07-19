using System;
using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Registrations.Entities;
using DCWR.Event_Manager.Contracts.Registrations.Queries;
using DCWR.Event_Manager.Contracts.Utilities;
using DCWR.Event_Manager.Infrastructure;
using DCWR.Event_Manager.WebApp.React.Common.Contracts;
using DCWR.Event_Manager.WebApp.React.Controllers.Events.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DCWR.Event_Manager.WebApp.React.Controllers.Events
{
    [Route("api/events/{eventId}/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly ICommandQueryDispatcher dispatcher;

        public RegistrationsController(ICommandQueryDispatcher dispatcher)
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
        public async Task<PagedResponse<AttendeeData>> GetRegistrationsOfEvent(
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