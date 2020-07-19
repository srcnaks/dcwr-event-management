using System;
using DCWR.Event_Manager.Contracts.Events.Commands;
using DCWR.Event_Manager.Contracts.Registrations.Commands;
using DCWR.Event_Manager.WebApp.React.Controllers.Events.Contracts;

namespace DCWR.Event_Manager.WebApp.React.Controllers.Events
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

        public static RegisterToEvent ToCommand(this RegisterToEventRequest request, Guid eventId)
        {
            return new RegisterToEvent(
                eventId: eventId,
                email: request.Email,
                name: request.Name,
                phoneNumber: request.PhoneNumber
            );
        }
    }
}
