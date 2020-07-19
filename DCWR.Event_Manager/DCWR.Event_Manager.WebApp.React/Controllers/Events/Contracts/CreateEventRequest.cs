using System;

namespace DCWR.Event_Manager.WebApp.React.Controllers.Events.Contracts
{
    public class CreateEventRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
