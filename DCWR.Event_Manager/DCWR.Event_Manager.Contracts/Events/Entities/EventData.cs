using System;

namespace DCWR.Event_Manager.Contracts.Events.Entities
{
    public class EventData
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string Location { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }

        public EventData(
            Guid id,
            string name,
            string description,
            string location,
            DateTime startTime,
            DateTime endTime)
        {
            Id = id;
            Name = name;
            Description = description;
            Location = location;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
