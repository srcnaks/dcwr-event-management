using System;
using DCWR.Event_Manager.Contracts.Events.Commands;

namespace DCWR.Event_Manager.Tests.Shared.ObjectBuilders.Contracts
{
    public class CreateEventBuilder
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Location { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public CreateEventBuilder()
        {
            Name = "test event";
            Description = "test description";
            Location = "random location";
            StartTime = DateTime.Today.AddHours(12);
            EndTime = DateTime.Today.AddHours(14);
        }

        public CreateEvent Build()
        {
            return new CreateEvent(
                name: Name,
                description: Description,
                location: Location,
                startTime: StartTime,
                endTime: EndTime
            );
        }
    }
}
