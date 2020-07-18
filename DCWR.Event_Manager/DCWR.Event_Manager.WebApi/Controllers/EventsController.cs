using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Events.Entities;
using DCWR.Event_Manager.Contracts.Events.Queries;
using DCWR.Event_Manager.Contracts.Utilities;
using DCWR.Event_Manager.Infrastructure;
using DCWR.Event_Manager.WebApi.Models;
using DCWR.Event_Manager.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DCWR.Event_Manager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ICommandQueryDispatcher dispatcher;

        public EventsController(ICommandQueryDispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventRequest request)
        {
            var command = request.ToCommand();
            await dispatcher.DispatchAsync(command);
            return Created("", command.CreatedId);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<PagedResponse<EventData>> GetEvents([FromQuery] PagedRequest request)
        {
            var query = new GetEvents(request.PageSize, request.PageNumber);
            var response = await dispatcher
                .DispatchAsync<PagedResponse<EventData>, GetEvents>(query);
            return response;
        }
    }
}