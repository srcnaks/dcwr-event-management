using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DCWR.Event_Manager.Infrastructure;

namespace DCWR.Event_Manager.Events
{
    public class Event : IEntity
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Location { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        [ForeignKey("EventId")]
        public ICollection<Registrations.Registration> Attendees { get; set; }

        public Event(
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
