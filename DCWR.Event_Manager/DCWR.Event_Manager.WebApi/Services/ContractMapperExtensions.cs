using DCWR.Event_Manager.Contracts.Events.Commands;
using DCWR.Event_Manager.WebApi.Models;

namespace DCWR.Event_Manager.WebApi.Services
{
    public static class ContractMapperExtensions
    {
        public static CreateEvent ToCommand(this CreateEventRequest request)
        {
            return new CreateEvent(
                name: request.Name,
                description: request.Description,
                location: request.Location,
                startTime: request.StartTime,
                endTime: request.EndTime
            );
        }
    }
}
