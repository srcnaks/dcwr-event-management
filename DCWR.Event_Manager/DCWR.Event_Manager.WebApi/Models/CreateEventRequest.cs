using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DCWR.Event_Manager.WebApi.Models
{
    public class CreateEventRequest
    {
        public string Name { get; }
        public string Description { get; }
        public string Location { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }

        public CreateEventRequest(
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
