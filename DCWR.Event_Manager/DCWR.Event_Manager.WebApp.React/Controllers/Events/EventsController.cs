using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DCWR.Event_Manager.Contracts.Events.Entities;
using DCWR.Event_Manager.Contracts.Events.Queries;
using DCWR.Event_Manager.Contracts.Utilities;
using DCWR.Event_Manager.Infrastructure;
using DCWR.Event_Manager.WebApp.React.Common.Contracts;
using DCWR.Event_Manager.WebApp.React.Controllers.Events.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DCWR.Event_Manager.WebApp.React.Controllers.Events
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
            return new ContentResult
            {
                ContentType = "application/json",
                Content = command.CreatedId.ToString(),
                StatusCode = (int)HttpStatusCode.Created
            };
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