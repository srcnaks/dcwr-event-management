using System;
using System.Collections.Generic;
using System.Text;
using DCWR.Event_Manager.Events;
using DCWR.Event_Manager.Infrastructure;
using DCWR.Event_Manager.Tests.Infrastructure;

namespace DCWR.Event_Manager.Tests.Shared.ObjectBuilders.DomainEntities
{
    public class EventBuilder
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Location { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public EventBuilder()
        {
            Id = GuidIdGenerator.Instance.Generate();
            Name = RandomGenerator.GetWord();
            Description = RandomGenerator.GetWords();
            Location = RandomGenerator.GetWords();
            StartTime = DateTime.Today.AddHours(12);
            EndTime = DateTime.Today.AddHours(14);
        }

        public Event Build()
        {
            return new Event(
                Id,
                Name,
                Description,
                Location,
                StartTime,
                EndTime
            );
        }
    }
}
