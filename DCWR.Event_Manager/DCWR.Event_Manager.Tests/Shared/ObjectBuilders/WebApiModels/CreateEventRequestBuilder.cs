using System;
using DCWR.Event_Manager.Tests.Infrastructure;
using DCWR.Event_Manager.WebApi.Models;

namespace DCWR.Event_Manager.Tests.Shared.ObjectBuilders.WebApiModels
{
    public class CreateEventRequestBuilder
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Location { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public CreateEventRequestBuilder()
        {
            Name = RandomGenerator.GetWord();
            Description = RandomGenerator.GetWords();
            Location = RandomGenerator.GetWords();
            StartTime = DateTime.Today.AddHours(12);
            EndTime = DateTime.Today.AddHours(14);
        }

        public CreateEventRequest Build()
        {
            return new CreateEventRequest()
            {
                Name= Name,
                Description = Description,
                Location = Location,
                StartTime = StartTime,
                EndTime = EndTime
            };
        }
    }
}
