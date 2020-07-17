using DCWR.Event_Manager.Contracts.Events.Commands;
using DCWR.Event_Manager.Infrastructure;

namespace DCWR.Event_Manager.Events
{
    public interface IEventBuilder
    {
        Event BuildWith(CreateEvent command);
    }

    public class EventBuilder : IEventBuilder
    {
        private readonly IGuidIdGenerator guidIdGenerator;

        public EventBuilder(IGuidIdGenerator guidIdGenerator)
        {
            this.guidIdGenerator = guidIdGenerator;
        }

        public Event BuildWith(CreateEvent command)
        {
            return new Event(
                id: guidIdGenerator.Generate(),
                name: command.Name,
                description: command.Description,
                location: command.Location,
                startTime: command.StartTime,
                endTime: command.EndTime
            );
        }
    }
}
