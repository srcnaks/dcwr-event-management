using System;
using DCWR.Event_Manager.Contracts.Utilities;

namespace DCWR.Event_Manager.Contracts.Events.Commands
{
    public class CreateEvent : ICommand
    {
        public Guid CreatedId { get; set; }
        public string Name { get; }
        public string Description { get; }
        public string Location { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }

        public CreateEvent(
            string name, 
            string description,
            string location, 
            DateTime startTime,
            DateTime endTime)
        {
            Name = name;
            Description = description;
            Location = location;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
