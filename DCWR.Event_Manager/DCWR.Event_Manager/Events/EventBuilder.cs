using System;
using DCWR.Event_Manager.Contracts.Events.Commands;

namespace DCWR.Event_Manager.Events
{
    public interface IEventBuilder
    {
        Event BuildWith(CreateEvent command);
    }

    public class EventBuilder : IEventBuilder
    {
        public Event BuildWith(CreateEvent command)
        {
            return new Event(
                id: Guid.NewGuid(),
                name: command.Name,
                description: command.Description,
                location: command.Location,
                startTime: command.StartTime,
                endTime: command.EndTime
            );
        }
    }
}
