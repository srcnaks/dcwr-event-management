using DCWR.Event_Manager.Contracts.Events.Entities;

namespace DCWR.Event_Manager.Events
{
    public static class EventsContractMapperExtensions
    {
        public static EventData ToContract(this Event @event)
        {
            return new EventData(
                id: @event.Id,
                name: @event.Name,
                description: @event.Description,
                location: @event.Location,
                startTime: @event.StartTime,
                endTime: @event.EndTime
            );
        }
    }
}
